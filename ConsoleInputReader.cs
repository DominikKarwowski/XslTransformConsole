using System;

namespace DjK.Utilities.XslTransformConsole
{
    public class ConsoleInputReader : IInputReader
    {
        public string GetInput()
        {
            return Console.ReadLine();
        }
    }
}