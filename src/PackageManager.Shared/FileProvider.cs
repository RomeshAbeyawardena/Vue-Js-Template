﻿using PackageManager.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageManager.Shared
{
    public class FileProvider : IFileProvider
    {
        public IEnumerable<FileInfo> GetFiles(DirectoryInfo directory)
        {
            var directories = directory.GetDirectories("*", SearchOption.AllDirectories);
            return directories.SelectMany(a => a.GetFiles());
        }

        public IEnumerable<FileInfo> GetFiles(string path)
        {
            var directory = new DirectoryInfo(path);
            return directory.GetFiles("*.*", new EnumerationOptions { 
                RecurseSubdirectories = true, 
                IgnoreInaccessible = true });
        }
    }
}