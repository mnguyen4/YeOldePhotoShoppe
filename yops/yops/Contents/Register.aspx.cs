using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using yops_lib.Constants;
using yops_lib.Crypto;
using yops_lib.Utils;

namespace yops.Contents
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /*
         * Cancel button click handler.
         */
        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("/yops/Default.aspx");
        }

        /*
         * Submit button click handler.
         */
        protected void Submit_Click(object sender, EventArgs e)
        {
            // get form values
            string username = Username.Value;
            string password = Password.Value;
            string confPassword = ConfPassword.Value;
            string firstName = FirstName.Value;
            string lastName = LastName.Value;
            string email = Email.Value;

            // confirm password does not match password
            if (!confPassword.Equals(password))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), UserConstants.CONFIRM_PASSWORD, string.Format(SharedConstants.ALERT_PATTERN, Messages.CONFIRM_PASSWORD_MESSAGE), true);
                return;
            }

            // build json
            JObject postData = new JObject();
            postData.Add(UserConstants.USERNAME, username);
            postData.Add(UserConstants.PASSWORD, password);
            postData.Add(UserConstants.FIRST_NAME, firstName);
            postData.Add(UserConstants.LAST_NAME, lastName);
            postData.Add(UserConstants.EMAIL, email);

            SendRegisterRequest(postData);
        }

        /*
         * Sends the registration request to yops_ws
         */
        private void SendRegisterRequest(JObject postData)
        {
            RestUtil restReq = new RestUtil(WebServices.REGISTER_URL);
            JObject response = restReq.sendPostRequest(postData);

            // redirect to result page
            if (response[SharedConstants.ERROR] != null)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), UserConstants.ACCOUNT_ERROR, string.Format(SharedConstants.ALERT_PATTERN, response[SharedConstants.ERROR]), true);
                return;
            }
            else
            {
                Response.Redirect("/yops/Contents/Registered.aspx");
            }
        }
    }
}