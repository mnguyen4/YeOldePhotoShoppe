using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace yops_ws.Filters
{
    /*
     * Custom class to replace FilterInfo.
     * Can only be used with filters derived from IBaseFilterAttribute.
     */
    public class CustomFilterInfo : IComparable
    {
        public IFilter Instance { set; get; }
        public FilterScope Scope { set; get; }

        public CustomFilterInfo(IFilter instance, FilterScope scope)
        {
            this.Instance = instance;
            this.Scope = scope;
        }

        public int CompareTo(Object obj)
        {
            if (obj is CustomFilterInfo)
            {
                var item = obj as CustomFilterInfo;

                // check to see if the filter in use is the custom filters
                if (item.Instance is IBaseFilterAttribute)
                {
                    var attr = item.Instance as IBaseFilterAttribute;
                    return (this.Instance as IBaseFilterAttribute).Order.CompareTo(attr.Order);
                }
                else
                {
                    throw new ArgumentException("Object is of wrong type");
                }
            }
            else
            {
                throw new ArgumentException("Object is of wrong type");
            }
        }

        public FilterInfo ConvertToFilterInfo()
        {
            return new FilterInfo(this.Instance, this.Scope);
        }
    }
}