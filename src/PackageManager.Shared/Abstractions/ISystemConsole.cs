using System;

namespace PackageManager.Shared.Abstractions
{
    public interface ISystemConsole : IConsole<ConsoleKeyInfo>
    {
        void WriteError(string error);
        void WriteErrorLine(string error);

        void WriteError(string value,
            Action<IDictionaryBuilder<string, string>> builder);

        void WriteErrorLine(string value,
            Action<IDictionaryBuilder<string, string>> builder);
    }
}
