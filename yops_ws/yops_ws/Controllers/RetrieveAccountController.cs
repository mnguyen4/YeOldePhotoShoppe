using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using yops_lib.Actions;
using yops_lib.Constants;
using yops_lib.Utils;
using yops_ws.Filters;
using yops_ws.Models;

namespace yops_ws.Controllers
{
    public class RetrieveAccountController : ApiController
    {
        // POST api/retrieveaccount
        [PostFilter(0)]
        [TokenFilter(1)]
        public JObject Post(HttpRequestMessage message)
        {
            JObject postData = (JObject)HttpContext.Current.Items[SharedConstants.POST_DATA];
            return RetrieveUserAccount(postData);
        }

        private JObject RetrieveUserAccount(JObject postData)
        {
            // get token from post json
            string token = StringUtils.NullToString(postData.SelectToken(UserConstants.TOKEN));

            JObject response = new JObject();

            // get context to retrieve user account info
            using (yeoldephotoshoppeEntities yops = new yeoldephotoshoppeEntities())
            {
                try
                {
                    // search for token
                    Token userToken = yops.Tokens.Where(t => t.hash.Equals(token)).FirstOrDefault();

                    // get the account
                    UserAccount account = userToken.UserAccount;

                    // build response JSON
                    response[Actions.RETRIEVE_ACCOUNT] = SharedConstants.SUCCESS;
                    response[UserConstants.USERNAME] = account.username;
                    response[UserConstants.FIRST_NAME] = account.firstName;
                    response[UserConstants.LAST_NAME] = account.lastName;
                    response[UserConstants.EMAIL] = account.email;
                }
                catch (Exception e)
                {
                    // database exception
                    ErrorUtils error = new ErrorUtils(Actions.RETRIEVE_ACCOUNT, Messages.USER_QUERY_FAILED + ". " + e.Message);
                    return error.GetErrorJson();
                }
            }

            // return a response
            return response;
        }
    }
}
