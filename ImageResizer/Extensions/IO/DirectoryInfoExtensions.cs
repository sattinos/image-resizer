using System;
using System.IO;
using System.Collections.Generic;

namespace ImageResizer.Extensions.IO
{
    public static class DirectoryInfoExtensions
    {
        public static List<FileInfo> EnumerateFiles(this DirectoryInfo dInfo, string[] imageExtensions, string execludedPath)
        {
            var files = new List<FileInfo>();
            foreach (var file in dInfo.EnumerateFiles("*.*", SearchOption.AllDirectories))
            {
                if ( !string.IsNullOrWhiteSpace(execludedPath) && file.FullName.Contains(execludedPath))
                {
                    continue;
                }

                if (Array.IndexOf(imageExtensions, file.Extension) > -1)
                {
                    files.Add(file);
                }
            }

            return files;
        }
    }
}
