using System;
using System.Collections.Generic;
using System.Xml.Xsl;
using System.IO;
using DjK.XslTransformUtility.Core.Services;

namespace DjK.XslTransformUtility.Core
{
    public class XslTransformEngine
    {
        readonly IMessageWriter messageWriter;
        readonly IInputReader inputReader;

        private string AppDirectory { get; }
        public string WorkingDirectory { get; private set; }
        public string CurrentXslFile { get; set; }
        public string CurrentXmlFile { get; set; }
        private string CurrentXslFileFullPath => Path.Combine(WorkingDirectory, CurrentXslFile);
        private string CurrentXmlFileFullPath => Path.Combine(WorkingDirectory, CurrentXmlFile);

        public XslTransformEngine(IMessageWriter messageWriter, IInputReader inputReader)
        {
            this.messageWriter = messageWriter ?? throw new ArgumentNullException(nameof(messageWriter));
            this.inputReader = inputReader ?? throw new ArgumentNullException(nameof(inputReader));
            AppDirectory = Directory.GetCurrentDirectory();
            GetWorkingDirectory(ReadFromConfigFile());
        }

        public void SetXslFile()
        {
            CurrentXslFile = GetFileName("Provide input XSL file:");
        }

        public void SetXmlFile()
        {
            CurrentXmlFile = GetFileName("Provide input XML file:");
        }

        public void Transform()
        {
            var xslt = new XslCompiledTransform();

            try
            {
                xslt.Load(CurrentXslFileFullPath);
            }
            catch (Exception e)
            {
                messageWriter.Write(e.ToString());
                inputReader.GetInput();
            }
            
            try
            {
                xslt.Transform(CurrentXmlFileFullPath, CurrentXmlFileFullPath + "_out");
                messageWriter.Write("Success!");
            }
            catch (Exception e)
            {
                messageWriter.Write(e.ToString());
                inputReader.GetInput();
            }
        }

        private string GetFileName(string inputMessage)
        {
            messageWriter.Write(inputMessage);
            string fileName = inputReader.GetInput();
            while (!File.Exists(Path.Combine(WorkingDirectory, fileName)))
            {
                messageWriter.Write("Provided file does not exist in the specified directory. Try again:");
                fileName = inputReader.GetInput();
            }
            return fileName;
        }

        private string GetWorkingDirectory(List<string> configLines)
        {
            string workingDirectory = "";

            foreach (string line in configLines)
            {
                if(line.Substring(0, 16) == "WorkingDirectory")
                {
                    workingDirectory = line.Substring(17);
                    WorkingDirectory = workingDirectory;
                    return workingDirectory;
                }
            }

            if (workingDirectory == "")
            {
                messageWriter.Write("Working directory has not been specified so far.");
                SetWorkingDirectory();
            }

            return workingDirectory;
        }


        public void SetWorkingDirectory()
        {
            messageWriter.Write("Provide working directory:");
            string path = inputReader.GetInput();

            while (!Directory.Exists(path))
            {
                messageWriter.Write("Specified path does not exist. Do you want to create it? (y/n):");
                string response = inputReader.GetInput(); //UserInterface.GetResponse(new List<string> { "y", "n" });
                if (response == "y")
                {
                    try
                    {
                        Directory.CreateDirectory(path);
                    }
                    catch (Exception e)
                    {
                        messageWriter.Write("Could not create directory at specified path:");
                        messageWriter.Write(path + Environment.NewLine);
                        messageWriter.Write(e.ToString());
                        inputReader.GetInput();
                    }
                }
                else
                {
                    messageWriter.Write("Specify different directory:");
                    path = inputReader.GetInput();
                }
            }
            
            WorkingDirectory = path;
            WriteToConfigFile();
        }


        private void WriteToConfigFile()
        {
            string configFile = Path.Combine(AppDirectory, "config.dat");

            using (StreamWriter file = new StreamWriter(configFile))
            {
                try
                {
                    file.WriteLine($"WorkingDirectory={WorkingDirectory}");
                }
                catch (Exception e)
                {
                    messageWriter.Write("Writing to config file failed!" + Environment.NewLine);
                    messageWriter.Write(e.ToString());
                    inputReader.GetInput();
                }
            }
        }
        
        private List<string> ReadFromConfigFile()
        {
            var configLines = new List<string>();
            string configFile = Path.Combine(AppDirectory, "config.dat");

            if (File.Exists(configFile))
            {
                using (StreamReader file = new StreamReader(configFile))
                {
                    string line;
                    while((line = file.ReadLine()) != null)
                    {
                        configLines.Add(line);
                    }
                }
            }

            return configLines;
        }
    }
    
}