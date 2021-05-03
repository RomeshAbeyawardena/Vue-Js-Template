using PackageManager.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageManager.Shared
{
    public class SystemConsole : IConsole<ConsoleKeyInfo>
    {
        public SystemConsole()
        {

        }

        public ConsoleKeyInfo Read(bool bypass)
        {
            return Console.ReadKey();
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void Write(string value)
        {
            Console.Write(value);
        }

        public void Write(string value, Action<IDictionaryBuilder<string, string>> builder)
        {
            throw new NotImplementedException();
        }

        public void WriteLine(string value)
        {
            Console.WriteLine(value);
        }

        public void WriteLine(string value, Action<IDictionaryBuilder<string, string>> builder)
        {
            throw new NotImplementedException();
        }
    }
}
