using PackageManager.Shared.Abstractions;
using PackageManager.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageManager.Shared
{
    public class SystemConsole : ISystemConsole, IConsole<ConsoleKeyInfo>
    {
        private readonly IDictionaryBuilder<string, string> dictionaryBuilder;
        private readonly IDictionary<string, string> parameterDictionary;
        private string ReplaceValues(string value, 
            Action<IDictionaryBuilder<string, string>> builder)
        {
            builder?.Invoke(dictionaryBuilder);

            return value.Format(dictionaryBuilder);
        }

        public SystemConsole()
        {
            parameterDictionary = new Dictionary<string, string>();
            dictionaryBuilder = DictionaryBuilder
                .Create(parameterDictionary);
        }

        public ConsoleKeyInfo Read(bool bypass)
        {
            return Console.ReadKey(bypass);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void Write(string value)
        {
            Console.Write(value);
        }

        public void Write(string value,
            Action<IDictionaryBuilder<string, string>> builder)
        {
            Write(ReplaceValues(value, builder));
        }

        public void WriteLine(string value)
        {
            Console.WriteLine(value);
        }

        public void WriteLine(string value, 
            Action<IDictionaryBuilder<string, string>> builder)
        {
            WriteLine(ReplaceValues(value, builder));
        }

        public void ClearParametersDictionary()
        {
            parameterDictionary.Clear();
        }
    }
}
