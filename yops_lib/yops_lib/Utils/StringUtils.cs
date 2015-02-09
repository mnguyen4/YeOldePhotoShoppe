using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yops_lib.Utils
{
    public class StringUtils
    {
        /*
         * Return an empty string if the object is null.
         */
        public static string NullToString(object chkObject)
        {
            if (chkObject == null)
            {
                return "";
            }
            else
            {
                return chkObject.ToString();
            }
        }

        /*
         * Check if a password is in the valid format.
         */
        public static bool IsValidPassword(string password)
        {
            // passwords must be at least 8 characters
            bool isValid = password.Length >= 8;
            // passwords must contain at least 1 number
            isValid = isValid && password.Any(c => char.IsDigit(c));
            // passwords must contain at least 1 lowercase letter
            isValid = isValid && password.Any(c => char.IsLower(c));
            // passwords must contain at least 1 uppercase letter
            isValid = isValid && password.Any(c => char.IsUpper(c));

            return isValid;
        }
    }
}
