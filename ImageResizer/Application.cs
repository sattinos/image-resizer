using System;
using System.Text;
using ImageResizer.Options;
using MatthiWare.CommandLine;

namespace ImageResizer
{
    public class Application
    {
        public Application(string[] args)
        {
            Setup(args);
        }
        public ApplicationSettings Settings { get; set; }
        public string Error { get; private set; }
        private void Setup(string[] args)
        {          
            var parser = new CommandLineParser<ApplicationSettings>();
            var result = parser.Parse(args);
            var errors = new StringBuilder();

            if( result.HasErrors )
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
            if (options.SourcePath == "")
            {
                options.SourcePath = Environment.CurrentDirectory;
            }

            if (options.TargetPath == "")
            {
                options.TargetPath = Environment.CurrentDirectory;
            }
        }
    }
}
