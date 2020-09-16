using System;

using DjK.XslTransformUtility.Core.Repositories;
using DjK.XslTransformUtility.Core.Services;

namespace DjK.XslTransformUtility.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("XSL Transform App");

            var appConfigService = new AppConfigService(
                appConfigRepository : new AppConfigRepository(),
                messageWriter : new ConsoleMessageWriter(),
                inputReader : new ConsoleInputReader()
            );

            var xslTransformService = new XslTransformService(
                messageWriter : new ConsoleMessageWriter(),
                inputReader : new ConsoleInputReader(),
                appConfigService
                );

            UserInterface.MainLoop(appConfigService, xslTransformService);
        }
    }
}
