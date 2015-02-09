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
using yops_lib.Utils;
using yops_ws.Filters;
using yops_ws.Models;

namespace yops_ws.Controllers
{
    public class LogoutController : ApiController
    {
        // POST api/logout
        [PostFilter(0)]
        [TokenFilter(1)]
        public JObject Post(HttpRequestMessage message)
        {
            JObject postData = (JObject)System.Web.HttpContext.Current.Items[SharedConstants.POST_DATA];
            return EndSession(postData);
        }

        /*
         * Invalidates the user token and session.
         */
        private JObject EndSession(JObject postData)
        {
            // get token from post json
            string token = StringUtils.NullToString(postData.SelectToken(UserConstants.TOKEN));

            // create transaction to delete auth token
            using (yeoldephotoshoppeEntities yops = new yeoldephotoshoppeEntities())
            {
                using (DbContextTransaction transaction = yops.Database.BeginTransaction())
                {
                    try
                    {
                        // search for token
                        Token userToken = yops.Tokens.Where(t => t.hash.Equals(token)).FirstOrDefault();

                        // remove token and commit changes
                        yops.Tokens.Remove(userToken);
                        yops.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        // rollback DB changes
                        transaction.Rollback();
                        // send error response
                        ErrorUtils error = new ErrorUtils(Actions.LOGOUT, Messages.TOKEN_QUERY_FAILED + ", " + e.Message);
                        return error.GetErrorJson();
                    }
                }
            }

            // return a response
            JObject response = new JObject();
            response[Actions.LOGOUT] = SharedConstants.SUCCESS;
            return response;
        }
    }
}
