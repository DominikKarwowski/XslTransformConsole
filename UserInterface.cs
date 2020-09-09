using System;
using System.Collections.Generic;

namespace XslTransformConsole
{
    public static class UserInterface
    {
        public static void MainLoop(XslTransformEngine xsltEngine)
        {
            string response = "";

            do
            {
                Console.Clear();
                Console.WriteLine("Welcome to XSL Transform App");
                Console.WriteLine($"Current working directory: {xsltEngine.WorkingDirectory}");
                Console.WriteLine($"Current stylesheet: {xsltEngine.CurrentXslFile}");
                Console.WriteLine($"Current xml file to transform: {xsltEngine.CurrentXmlFile}");
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
                        xsltEngine.SetWorkingDirectory();
                        break;
                    case "2":
                        xsltEngine.SetXslFile();
                        break;
                    case "3":
                        xsltEngine.SetXmlFile();
                        break;
                    case "4":
                        xsltEngine.Transform();
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
