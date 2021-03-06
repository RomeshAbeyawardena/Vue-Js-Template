using System;

namespace PackageManager.Shared.Abstractions
{
    public interface IConsole<TConsoleReadInfo>
    {
        string ReadLine();
        TConsoleReadInfo Read(bool bypass = false);

        void ClearParametersDictionary();
        void Write(string value);
        void WriteLine(string value);

        void Write(string value,
            Action<IDictionaryBuilder<string, string>> builder);
        void WriteLine(string value,
            Action<IDictionaryBuilder<string, string>> builder);
    }
}
