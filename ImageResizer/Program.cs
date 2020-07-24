namespace ImageResizer
{
    class Program
    {
        static int Main(string[] args)
        {
            var appInstance = new Application(args);
            appInstance.ProcessFiles();
            return 0;
        }
    }
}
