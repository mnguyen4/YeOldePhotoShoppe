using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yops_lib.Constants
{
    public class Messages
    {
        // error messages
        public const string USER_SAVE_FAILED = "Failed to save user account.";
        public const string USER_QUERY_FAILED = "Failed to find user account.";
        public const string TOKEN_QUERY_FAILED = "Failed to find user session.";
        public const string USERNAME_EXIST = "The username is already in use.";
        public const string CONFIRM_PASSWORD_MESSAGE = "The passwords do not match.";
        public const string INVALID_USERNAME_OR_PASSWORD = "Username or password is incorrect.";
        public const string INVALID_SESSION = "Your session is no longer valid.";
        public const string INVALID_OLD_PASSWORD = "The password you entered does not match our records.";
        public const string INVALID_PASSWORD_FORMAT = "The password must be at least 8 characters long and contain at least 1 number, 1 upper-case letter, and 1 lower-case letter.";
    }
}
