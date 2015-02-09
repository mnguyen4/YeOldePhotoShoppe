using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using yops_lib.Constants;
using yops_lib.Utils;

namespace yops
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /*
         * Register click handler.
         */
        protected void Register_Click(object sender, EventArgs e)
        {
            Response.Redirect("/yops/Contents/Register.aspx");
        }

        /*
         * Login click handler.
         */
        protected void Login_Click(object sender, EventArgs e)
        {
            // get form values
            string username = Username.Value;
            string password = Password.Value;

            // build json
            JObject postData = new JObject();
            postData.Add(UserConstants.USERNAME, username);
            postData.Add(UserConstants.PASSWORD, password);

            SendLoginRequest(postData);
        }

        /*
         * Send Login request to Web API.
         */
        private void SendLoginRequest(JObject postData)
        {
            RestUtil restReq = new RestUtil(WebServices.LOGIN_URL);
            JObject response = restReq.sendPostRequest(postData);

            // redirect to app page
            if (response[SharedConstants.ERROR] != null)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), UserConstants.ACCOUNT_ERROR, string.Format(SharedConstants.ALERT_PATTERN, response[SharedConstants.ERROR]), true);
                return;
            }
            else
            {
                // set user info cookie
                Response.Cookies[UserConstants.USERNAME].Value = (string)response[UserConstants.USERNAME];
                Response.Cookies[UserConstants.FIRST_NAME].Value = (string)response[UserConstants.FIRST_NAME];
                Response.Cookies[UserConstants.LAST_NAME].Value = (string)response[UserConstants.LAST_NAME];
                Response.Cookies[UserConstants.TOKEN].Value = (string)response[UserConstants.TOKEN];

                Response.Redirect("/yops/Contents/AppHome.aspx");
            }
        }
    }
}