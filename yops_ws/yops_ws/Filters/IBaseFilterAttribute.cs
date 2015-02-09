using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace yops_ws.Filters
{
    /*
     * This interface will add the "order" attribute to filters.
     */
    public interface IBaseFilterAttribute : IFilter
    {
        int Order { get; set; }
    }
}