using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using yops_ws.Models;
using yops_lib.Constants;
using yops_lib.Actions;
using System.Web;
using yops_ws.Filters;
using System.Data.Entity;
using yops_lib.Utils;
using yops_lib.Crypto;

namespace yops_ws.Controllers
{
    public class RegisterController : ApiController
    {
        // POST api/register
        [PostFilter]
        public JObject Post(HttpRequestMessage message)
        {
            // get the POST data saved by the post filter
            JObject postData = (JObject)HttpContext.Current.Items[SharedConstants.POST_DATA];
            return registerUser(postData);
        }

        /**
         * Save user account using transactions for data integrity.
         */
        private JObject registerUser(JObject postData)
        {
            // get the values from the POST JSON
            string username = StringUtils.NullToString(postData.SelectToken(UserConstants.USERNAME));
            string password = StringUtils.NullToString(postData.SelectToken(UserConstants.PASSWORD));
            string firstName = StringUtils.NullToString(postData.SelectToken(UserConstants.FIRST_NAME));
            string lastName = StringUtils.NullToString( postData.SelectToken(UserConstants.LAST_NAME));
            string email = StringUtils.NullToString(postData.SelectToken(UserConstants.EMAIL));

            if (!StringUtils.IsValidPassword(password)) {
                ErrorUtils error = new ErrorUtils(Actions.REGISTER, Messages.INVALID_PASSWORD_FORMAT);
                return error.GetErrorJson();
            }

            // create a new user account
            UserAccount account = new UserAccount();
            account.username = username;
            account.password = SaltHash.getHash(password);
            account.firstName = firstName;
            account.lastName = lastName;
            account.email = email;

            // starts the transaction
            using (yeoldephotoshoppeEntities yops = new yeoldephotoshoppeEntities())
            {
                using (DbContextTransaction transaction = yops.Database.BeginTransaction())
                {
                    try
                    {
                        // check user account
                        var result = yops.UserAccounts.Where(ua => ua.username.Equals(username)).Count();
                        if (result > 0)
                        {
                            ErrorUtils error = new ErrorUtils(Actions.REGISTER, Messages.USERNAME_EXIST);
                            return error.GetErrorJson();
                        }

                        // save to database
                        yops.UserAccounts.Add(account);
                        yops.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        // rollback DB changes
                        transaction.Rollback();
                        // send response
                        ErrorUtils error = new ErrorUtils(Actions.REGISTER, Messages.USER_SAVE_FAILED + ", " + e.Message);
                        return error.GetErrorJson();
                    }
                }
            }

            // send response
            JObject response = new JObject();
            response[Actions.REGISTER] = SharedConstants.SUCCESS;
            return response;
        }
    }
}
