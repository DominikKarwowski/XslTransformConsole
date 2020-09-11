using System;

namespace DjK.Utilities.XslTransformConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("XSL Transform App");

            var consoleMessageWriter = new ConsoleMessageWriter();
            var xlstEngine = new XslTransformEngine(consoleMessageWriter);
            UserInterface.MainLoop(xlstEngine);
        }
    }
}
