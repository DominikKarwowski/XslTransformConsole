using System;
using System.Collections.Generic;

using DjK.XslTransformUtility.Core.Services;

namespace DjK.XslTransformUtility.ConsoleApp
{
    public static class UserInterface
    {
        public static void MainLoop(IAppConfigService appConfigService, XslTransformService xslTransformService)
        {
            string response = "";

            do
            {
                Console.Clear();
                Console.WriteLine("Welcome to XSL Transform App");
                Console.WriteLine($"Current working directory: {appConfigService.WorkingDirectory}");
                Console.WriteLine($"Current stylesheet: {appConfigService.XslFile}");
                Console.WriteLine($"Current xml file to transform: {appConfigService.XmlFile}");
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("1. Change working directory.");
                Console.WriteLine("2. Change currentstylesheet.");
                Console.WriteLine("3. Change current xml file.");
                Console.WriteLine("4. Transform!");
                Console.WriteLine("Q. Quit");
                response = GetResponse(new List<string> { "1", "2", "3", "4", "Q", "q" });

                switch (response)
                {
                    case "1":
                        appConfigService.SetWorkingDirectory();
                        break;
                    case "2":
                        appConfigService.SetXslFile();
                        break;
                    case "3":
                        appConfigService.SetXmlFile();
                        break;
                    case "4":
                        xslTransformService.Transform();
                        break;
                    default:
                        break;
                }

            } while (response != "Q" && response != "q");
        }

        public static string GetResponse(List<string> possibleAnswers)
        {
            string userResponse = Console.ReadLine();

            while (!possibleAnswers.Contains(userResponse))
            {
                Console.WriteLine("Incorrect input. Try again.");
                userResponse = Console.ReadLine();
            }

            return userResponse;
        }
    }
}
