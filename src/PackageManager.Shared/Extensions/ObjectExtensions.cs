using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageManager.Shared.Extensions
{
    public static class ObjectExtensions
    {
        public static object Update(this object instance,
            IDictionary<string, object> dictionary,
            Type valueType = default)
        {
            if(valueType == null)
            {
                valueType = instance.GetType();
            }

            foreach (var property in valueType.GetProperties())
            {
                if(dictionary.TryGetValue(property.Name, out var value))
                {
                    var propertyType = property.PropertyType;
                    if (value != null)
                    {
                        if (value.GetType() != propertyType)
                        {
                            value = Convert.ChangeType(value, propertyType);
                        }

                        property.SetValue(instance, value);
                    }
                }
            }

            return instance;
        }

    }
}
