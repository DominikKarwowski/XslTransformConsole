using System;

using DjK.XslTransformUtility.Core;
using DjK.XslTransformUtility.Core.Services;

namespace DjK.XslTransformUtility.ConsoleApp
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
