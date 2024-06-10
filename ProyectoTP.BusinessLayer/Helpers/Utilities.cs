using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTP.BusinessLayer.Helpers
{
    public static class Utilities
    {
        public static T Map<T, TU>(this T target, TU source)
        {
            // get property list of the target object.
            // this is a reflection extension which simply gets properties (CanWrite = true).
            var tprops = target?.GetType().GetProperties();

            tprops?.Where(x => x.CanWrite == true).ToList().ForEach(prop =>
            {
                // check whether source object has the the property
                var sp = source?.GetType().GetProperty(prop.Name);
                if (sp != null)
                {
                    // if yes, copy the value to the matching property
                    var value = sp.GetValue(source, null);
                    target?.GetType()?.GetProperty(prop.Name)?.SetValue(target, value, null);
                }
            });

            return target;
        }
    }
}
