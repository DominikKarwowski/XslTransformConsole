using System;

namespace DjK.Utilities.XslTransformConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("XSL Transform App");

            var xlstEngine = new XslTransformEngine(
                messageWriter : new ConsoleMessageWriter(),
                inputReader : new ConsoleInputReader()
                );
            UserInterface.MainLoop(xlstEngine);
        }
    }
}
