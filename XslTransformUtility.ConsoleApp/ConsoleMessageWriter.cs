using System;

using DjK.XslTransformUtility.Core.Services;

namespace DjK.XslTransformUtility.ConsoleApp
{
    public class ConsoleMessageWriter : IMessageWriter
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}