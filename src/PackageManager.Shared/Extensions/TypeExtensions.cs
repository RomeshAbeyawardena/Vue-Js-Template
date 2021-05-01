using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageManager.Shared.Extensions
{
    public static class TypeExtensions
    {
        public static T Resolve<T>(this Type type, IServiceProvider serviceProvider)
        {
            var defaultConstructor = type.GetConstructors().FirstOrDefault(a => a.IsPublic);

            var parameterTypes = defaultConstructor.GetParameters()
                .Select(a => serviceProvider.GetService(a.ParameterType));

            return (T)Activator.CreateInstance(type, parameterTypes.ToArray());
        }
    }
}
