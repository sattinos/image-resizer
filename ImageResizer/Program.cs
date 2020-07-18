using System;

namespace ImageResizer
{
    class Program
    {
        static int Main(string[] args)
        {
            var appInstance = new Application(args);
            Console.WriteLine(appInstance.Settings);
            return 0;
        }
    }
}
