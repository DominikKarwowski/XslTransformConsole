using System;

namespace XslTransformConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("XSL Transform App");

            var xlstEngine = new XslTransformEngine();
            UserInterface.MainLoop(xlstEngine);
        }
    }
}
