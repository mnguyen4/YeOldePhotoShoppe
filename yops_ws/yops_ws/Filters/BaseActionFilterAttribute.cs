using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace yops_ws.Filters
{
    /*
     * This class adds the order attribute to the ActionFilterAttribute class.
     */
    public class BaseActionFilterAttribute : ActionFilterAttribute, IBaseFilterAttribute
    {
        public int Order { set; get; }

        public BaseActionFilterAttribute()
        {
            this.Order = 0;
        }

        public BaseActionFilterAttribute(int order)
        {
            this.Order = order;
        }
    }
}