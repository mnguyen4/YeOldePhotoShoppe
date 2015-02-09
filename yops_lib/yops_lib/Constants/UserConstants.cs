using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yops_lib.Constants
{
    public static class UserConstants
    {
        // user account constants
        public const string USERNAME = "username";
        public const string PASSWORD = "password";
        public const string OLD_PASSWORD = "oldPassword";

        public const string FIRST_NAME = "firstName";
        public const string LAST_NAME = "lastName";
        public const string EMAIL = "email";
        public const string TOKEN = "token";

        // misc user constants
        public const string CONFIRM_PASSWORD = "ConfirmPassword";
        public const string ACCOUNT_ERROR = "AccountError";
        public const string USER_COOKIE = "UserCookie";
        public const double SESSION_TIME_LIMIT = 60;
    }
}
