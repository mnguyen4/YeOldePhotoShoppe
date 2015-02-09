using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yops_lib.Constants;

namespace yops_lib.Utils
{
    /**
     * This is a utility class to easily create error json
     */
    public class ErrorUtils
    {
        private string action;
        private string message;

        // init action and error message
        public ErrorUtils(string action, string message)
        {
            this.action = action;
            this.message = message;
        }

        public ErrorUtils(string message)
        {
            this.message = message;
        }

        // build and return error json
        public JObject GetErrorJson()
        {
            JObject error = new JObject();
            error[action] = SharedConstants.FAILED;
            error[SharedConstants.ERROR] = message;

            return error;
        }

        public JObject GetErrorJsonNoAction()
        {
            JObject error = new JObject();
            error[SharedConstants.ERROR] = message;

            return error;
        }
    }
}
