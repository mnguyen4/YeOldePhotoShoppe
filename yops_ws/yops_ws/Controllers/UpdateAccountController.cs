using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using yops_lib.Actions;
using yops_lib.Constants;
using yops_lib.Crypto;
using yops_lib.Utils;
using yops_ws.Filters;
using yops_ws.Models;

namespace yops_ws.Controllers
{
    public class UpdateAccountController : ApiController
    {
        // POST api/updateaccount
        [PostFilter(0)]
        [TokenFilter(1)]
        public JObject Post(HttpRequestMessage message)
        {
            JObject postData = (JObject)System.Web.HttpContext.Current.Items[SharedConstants.POST_DATA];
            return UpdateUserAccount(postData);
        }

        private JObject UpdateUserAccount(JObject postData)
        {
            // get token from post json
            string token = (string)postData.SelectToken(UserConstants.TOKEN);

            // get values from post json
            string oldPassword = StringUtils.NullToString(postData.SelectToken(UserConstants.OLD_PASSWORD));
            string newPassword = StringUtils.NullToString(postData.SelectToken(UserConstants.PASSWORD));
            string firstName = StringUtils.NullToString(postData.SelectToken(UserConstants.FIRST_NAME));
            string lastName = StringUtils.NullToString(postData.SelectToken(UserConstants.LAST_NAME));
            string email = StringUtils.NullToString(postData.SelectToken(UserConstants.EMAIL));

            // get context to retrieve account
            using (yeoldephotoshoppeEntities yops = new yeoldephotoshoppeEntities())
            {
                using (DbContextTransaction transaction = yops.Database.BeginTransaction())
                {
                    try
                    {
                        // search for token
                        Token userToken = yops.Tokens.Where(t => t.hash.Equals(token)).FirstOrDefault();

                        // get the account
                        UserAccount account = userToken.UserAccount;

                        // only executes if the user wants to change password
                        if (oldPassword.Length > 0 && newPassword.Length > 0)
                        {
                            // check old password
                            string hash = account.password;
                            if (!SaltHash.validateHash(oldPassword, hash))
                            {
                                ErrorUtils error = new ErrorUtils(Actions.UPDATE_ACCOUNT, Messages.INVALID_OLD_PASSWORD);
                                return error.GetErrorJson();
                            }
                            if (!StringUtils.IsValidPassword(newPassword))
                            {
                                ErrorUtils error = new ErrorUtils(Actions.REGISTER, Messages.INVALID_PASSWORD_FORMAT);
                                return error.GetErrorJson();
                            }
                            // set new password
                            account.password = SaltHash.getHash(newPassword);
                        }

                        // set other account info
                        if (firstName.Length > 0)
                        {
                            account.firstName = firstName;
                        }
                        if (lastName.Length > 0)
                        {
                            account.lastName = lastName;
                        }
                        if (email.Length > 0)
                        {
                            account.email = email;
                        }

                        yops.Entry(account).State = EntityState.Modified;
                        yops.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        // send response
                        ErrorUtils error = new ErrorUtils(Actions.REGISTER, Messages.USER_SAVE_FAILED + ", " + e.Message);
                        return error.GetErrorJson();
                    }
                }
            }

            // return a response
            JObject response = new JObject();
            response[Actions.UPDATE_ACCOUNT] = SharedConstants.SUCCESS;
            return response;
        }
    }
}
