using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using yops_lib.Classes;
using yops_lib.Constants;
using yops_lib.Utils;
using yops_ws.Models;

namespace yops_ws.Filters
{
    /*
     * Action filter that will verify the client's authentication token.
     */
    public class TokenFilter : BaseActionFilterAttribute
    {
        public TokenFilter()
            : base()
        {
            // call base constructor
        }

        public TokenFilter(int order)
            : base(order)
        {
            // call base constructor with order parameter
        }

        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            // get the http context
            HttpRequestMessage requestMsg = filterContext.Request;

            JObject postJson = (JObject)HttpContext.Current.Items[SharedConstants.POST_DATA];
            string token = StringUtils.NullToString(postJson.SelectToken(UserConstants.TOKEN));

            // check the token validity
            using (yeoldephotoshoppeEntities yops = new yeoldephotoshoppeEntities())
            {
                try
                {
                    ErrorUtils error = new ErrorUtils(Messages.INVALID_SESSION);

                    // check auth token
                    Token authToken = yops.Tokens.Where(t => t.hash.Equals(token)).FirstOrDefault();
                    if (authToken == null)
                    {
                        filterContext.Response = requestMsg.CreateResponse(HttpStatusCode.OK, error.GetErrorJsonNoAction(), new ApplicationJsonFormatter());
                    }
                    else
                    {
                        DateTime lastAccess = authToken.accessTime;
                        DateTime current = DateTime.Now;
                        double minutes = current.Subtract(lastAccess).TotalMinutes;
                        if (minutes > UserConstants.SESSION_TIME_LIMIT)
                        {
                            filterContext.Response = requestMsg.CreateResponse(HttpStatusCode.OK, error.GetErrorJsonNoAction(), filterContext.ControllerContext.Configuration.Formatters.JsonFormatter);
                        }
                    }
                }
                catch (Exception e)
                {
                    ErrorUtils error = new ErrorUtils(Messages.TOKEN_QUERY_FAILED + ", " + e.Message);
                    filterContext.Response = requestMsg.CreateResponse(HttpStatusCode.OK, error.GetErrorJsonNoAction(), filterContext.ControllerContext.Configuration.Formatters.JsonFormatter);
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}