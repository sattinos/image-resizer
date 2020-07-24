using System;
using System.Text;
using System.IO;
using MatthiWare.CommandLine;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using ImageResizer.Options;
using ImageResizer.Extensions.IO;

namespace ImageResizer
{
    public class Application
    {
        public readonly static string[] ImageExtensions = new string[] { ".png", ".gif", "tiff", ".tif", ".jpeg", ".jpg", ".bmp", ".svg" };
        public Application(string[] args)
        {
            Setup(args);
        }
        public ApplicationSettings Settings { get; set; }
        public string Error { get; private set; }
        public void ProcessFiles()
        {
            if( Settings == null )
            {
                return;
            }

            if (Settings.ScaleFactor == 100)
            {
                return;
            }

            var samePath = String.Equals(Settings.SourcePath, Settings.TargetPath, StringComparison.OrdinalIgnoreCase);

            var sourceFiles = new DirectoryInfo(Settings.SourcePath).EnumerateFiles(ImageExtensions, samePath ? null : Settings.TargetPath);
            var factor = 100.0 / Settings.ScaleFactor;

            StringBuilder stringBuilder = new StringBuilder();
            double totalSourceSizeMB = 0;
            double totalTargetSizeMB = 0;
            foreach (var sourceFile in sourceFiles)
            {
                stringBuilder.AppendLine("===================");
                stringBuilder.AppendLine(sourceFile.FullName);
                var sourceFileSizeMB = Math.Round(sourceFile.Length / (1024.0 * 1024.0), 2);
                totalSourceSizeMB += sourceFileSizeMB;
                stringBuilder.AppendLine($"size: {sourceFileSizeMB} mb");
                using var image = Image.Load(sourceFile.FullName);
                stringBuilder.AppendLine($"{image.Width} * {image.Height}");

                image.Mutate(x => x.Resize((int)(image.Width / factor), (int)(image.Height / factor)));

                var finalImageFile = sourceFile.FullName;
                var finalPath = sourceFile.DirectoryName;
                if (!samePath)
                {
                    var relativePath = Path.GetRelativePath(Settings.TargetPath, sourceFile.DirectoryName);
                    var relativeFolderPath = relativePath.Substring(3);
                    finalPath = Path.Combine(Settings.TargetPath, relativeFolderPath);
                    Directory.CreateDirectory(finalPath);
                }

                var copyToken = "";
                if (Settings.Behaviour == Behaviour.Copy)
                {
                    copyToken = $"_{image.Width}_{image.Height}";
                }
                finalImageFile = Path.Combine(finalPath, $"{Path.GetFileNameWithoutExtension(sourceFile.Name)}{copyToken}{sourceFile.Extension}");

                if (Settings.Behaviour == Behaviour.Replace)
                {
                    File.Delete(sourceFile.FullName);
                }

                image.Save(finalImageFile);
                stringBuilder.AppendLine(" => ");
                stringBuilder.AppendLine(finalImageFile);
                stringBuilder.AppendLine($"{image.Width} * {image.Height}");

                var outputFileInfo = new FileInfo(finalImageFile);
                var targetFileSizeMB = Math.Round(outputFileInfo.Length / (1024.0 * 1024.0), 2);
                totalTargetSizeMB += targetFileSizeMB;
                stringBuilder.AppendLine($"size: {targetFileSizeMB} mb");
                Console.WriteLine(stringBuilder.ToString());
                stringBuilder.Clear();

            }
            stringBuilder.AppendLine("===================");
            stringBuilder.AppendLine($"total source files size: {Math.Round(totalSourceSizeMB, 2)} mb");
            stringBuilder.AppendLine($"total target files size: {Math.Round(totalTargetSizeMB, 2)} mb");
            stringBuilder.AppendLine($"total change: {Math.Round(Math.Abs(totalSourceSizeMB - totalTargetSizeMB), 2)} mb");
            stringBuilder.AppendLine("===================");
            Console.WriteLine(stringBuilder.ToString());
        }
        private void Setup(string[] args)
        {
            var parser = new CommandLineParser<ApplicationSettings>();
            var result = parser.Parse(args);
            if (args.Length == 0)
            {
                return;
            }
            var errors = new StringBuilder();

            if (result.HasErrors)
            {
                foreach (var errorItem in result.Errors)
                {
                    errors.Append(errorItem);
                }
                Error = errors.ToString();
                Settings = null;
                return;
            }
            Error = null;
            Settings = result.Result;
            PostProcess(Settings);
        }
        /// <summary>
        /// Should be called after the parse process is done.
        /// </summary>
        /// <param name="options"></param>
        private static void PostProcess(ApplicationSettings options)
        {
            if (string.IsNullOrWhiteSpace(options.SourcePath))
            {
                options.SourcePath = Environment.CurrentDirectory;
            }

            if (string.IsNullOrWhiteSpace(options.TargetPath))
            {
                options.TargetPath = Environment.CurrentDirectory;
            }

            if (options.Behaviour == Behaviour.Replace)
            {
                options.TargetPath = options.SourcePath;
            }
        }
    }
}
