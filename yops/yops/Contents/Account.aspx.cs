using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using yops_lib.Constants;
using yops_lib.Utils;

namespace yops.Contents
{
    public partial class Account : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /*
         * Submit button click handler.
         */
        protected void Submit_Click(object sender, EventArgs e)
        {
            // get form values
            string oldPassword = OldPassword.Value;
            string newPassword = NewPassword.Value;
            string confPassword = ConfPassword.Value;
            string firstName = FirstName.Value;
            string lastName = LastName.Value;
            string email = Email.Value;
            string token = Request.Cookies[UserConstants.TOKEN].Value;
            
            // create post json
            JObject postData = new JObject();

            // only executes if the user change password
            if (oldPassword.Length > 0 && newPassword.Length > 0)
            {
                // confirm password does not match password
                if (!confPassword.Equals(newPassword))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), UserConstants.CONFIRM_PASSWORD, string.Format(SharedConstants.ALERT_PATTERN, Messages.CONFIRM_PASSWORD_MESSAGE), true);
                    return;
                }
                // add old and new passwords to post data
                postData.Add(UserConstants.OLD_PASSWORD, oldPassword);
                postData.Add(UserConstants.PASSWORD, newPassword);
            }

            // add remaining info to post data
            postData.Add(UserConstants.FIRST_NAME, firstName);
            postData.Add(UserConstants.LAST_NAME, lastName);
            postData.Add(UserConstants.EMAIL, email);
            postData.Add(UserConstants.TOKEN, token);

            SendUpdateRequest(postData);
        }

        private void SendUpdateRequest(JObject postData)
        {
            RestUtil restReq = new RestUtil(WebServices.UPDATE_ACCOUNT_URL);
            JObject response = restReq.sendPostRequest(postData);

            // redirect to account page
            if (response[SharedConstants.ERROR] != null)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), UserConstants.ACCOUNT_ERROR, string.Format(SharedConstants.ALERT_PATTERN, response[SharedConstants.ERROR]), true);
                return;
            }
            else
            {
                Response.Redirect("/yops/Contents/Account.aspx");
            }
        }
    }
}