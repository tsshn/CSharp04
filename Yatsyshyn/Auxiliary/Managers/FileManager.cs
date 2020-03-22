using System;
using System.IO;

namespace Yatsyshyn.Auxiliary.Managers
{
    internal static class FileManager
    {
        private static readonly string AppDataPath =
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        internal static readonly string AppFolderPath =
            Path.Combine(AppDataPath, "CSharp06");

        internal static readonly string StorageFilePath =
            Path.Combine(AppFolderPath, "Storage.cs06");

        internal static bool CreateFolderAndCheckFileExistence(string filePath)
        {
            var file = new FileInfo(filePath);
            return file.CreateFolderAndCheckFileExistence();
        }

        internal static bool CreateFolderAndCheckFileExistence(this FileInfo file)
        {
            if (file.Directory != null && !file.Directory.Exists)
            {
                file.Directory.Create();
            }

            return file.Exists;
        }
    }
}