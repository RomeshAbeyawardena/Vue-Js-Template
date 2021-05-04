using System;
using System.IO;
using System.Linq;

namespace PackageManager.Shared.Extensions
{
    public static class TypeExtensions
    {
        public static T Resolve<T>(this Type type, 
            IServiceProvider serviceProvider, 
            params object[] args)
        {
            if (args.Length == 0)
            {
                var defaultConstructor = type.GetConstructors().FirstOrDefault(a => a.IsPublic);

                args = defaultConstructor.GetParameters()
                    .Select(a => serviceProvider.GetService(a.ParameterType)).ToArray();
            }

            return (T)Activator.CreateInstance(type, args);
        }

        public static string GetLocation(this Type type)
        {
            var fileInfo = new FileInfo(type.Assembly.Location);

            return fileInfo.DirectoryName;
        }
    }
}
