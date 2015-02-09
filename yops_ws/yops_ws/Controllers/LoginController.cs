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
    public class LoginController : ApiController
    {
        // POST api/login
        [PostFilter]
        public JObject Post(HttpRequestMessage message)
        {
            JObject postData = (JObject)System.Web.HttpContext.Current.Items[SharedConstants.POST_DATA];
            return authenticate(postData);
        }

        /**
         * Authenticate the user and send the response.
         */
        private JObject authenticate(JObject postData)
        {
            // get the values from the POST JSON
            string username = StringUtils.NullToString(postData.SelectToken(UserConstants.USERNAME));
            string password = StringUtils.NullToString(postData.SelectToken(UserConstants.PASSWORD));

            JObject response = new JObject();

            // create transaction to save auth token
            using (yeoldephotoshoppeEntities yops = new yeoldephotoshoppeEntities())
            {
                using (DbContextTransaction transaction = yops.Database.BeginTransaction())
                {
                    try
                    {
                        // search for user account
                        UserAccount account = yops.UserAccounts.Where(ua => ua.username.Equals(username)).FirstOrDefault();
                        // cannot find username
                        if (account == null)
                        {
                            ErrorUtils error = new ErrorUtils(Actions.LOGIN, Messages.INVALID_USERNAME_OR_PASSWORD);
                            return error.GetErrorJson();
                        }

                        // validate user password
                        string hash = account.password;
                        if (!SaltHash.validateHash(password, hash))
                        {
                            ErrorUtils error = new ErrorUtils(Actions.LOGIN, Messages.INVALID_USERNAME_OR_PASSWORD);
                            return error.GetErrorJson();
                        }

                        // generate auth token
                        string token = DateTime.Now.Millisecond.ToString();
                        token = SaltHash.getHash(token);

                        // create new user token
                        Token authToken = new Token();
                        authToken.hash = token;
                        authToken.UserAccount = account;
                        authToken.accessTime = DateTime.Now;

                        // save token and commit transaction
                        yops.Tokens.Add(authToken);
                        yops.SaveChanges();
                        transaction.Commit();

                        response[Actions.LOGIN] = SharedConstants.SUCCESS;
                        response[UserConstants.USERNAME] = account.username;
                        response[UserConstants.FIRST_NAME] = account.firstName;
                        response[UserConstants.LAST_NAME] = account.lastName;
                        response[UserConstants.TOKEN] = token;
                    }
                    catch (Exception e)
                    {
                        // rollback DB changes
                        transaction.Rollback();
                        // send error response
                        ErrorUtils error = new ErrorUtils(Actions.LOGIN, Messages.USER_QUERY_FAILED + ", " + e.Message);
                        return error.GetErrorJson();
                    }
                }
            }

            // return a response
            return response;
        }
    }
}
