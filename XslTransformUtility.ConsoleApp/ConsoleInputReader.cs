using System;

using DjK.XslTransformUtility.Core.Services;

namespace DjK.XslTransformUtility.ConsoleApp
{
    public class ConsoleInputReader : IInputReader
    {
        public string GetInput()
        {
            return Console.ReadLine();
        }
    }
}