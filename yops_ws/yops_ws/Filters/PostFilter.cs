using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using yops_lib.Constants;

namespace yops_ws.Filters
{
    /**
     * Action filter that will extract the post body.
     */
    public class PostFilter : BaseActionFilterAttribute
    {
        public PostFilter()
            : base()
        {
            // call base constructor
        }
        public PostFilter(int order)
            : base(order)
        {
            // call base constructor with order parameter
        }
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            // get the http context
            HttpRequestMessage requestMsg = filterContext.Request;
            string postStr = requestMsg.Content.ReadAsStringAsync().Result;
            
            JObject postJson = JObject.Parse(postStr);
            HttpContext.Current.Items[SharedConstants.POST_DATA] = postJson;
            
            base.OnActionExecuting(filterContext);
        }
    }
}