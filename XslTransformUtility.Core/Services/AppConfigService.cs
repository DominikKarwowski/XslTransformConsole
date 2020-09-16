using System;
using System.Collections.Generic;
using DjK.XslTransformUtility.Core.Config;
using DjK.XslTransformUtility.Core.Repositories;

namespace DjK.XslTransformUtility.Core.Services
{
    public class AppConfigService : IAppConfigService
    {
        readonly IAppConfigRepository appConfigRepository;
        readonly IMessageWriter messageWriter;
        readonly IInputReader inputReader;

        private AppConfig AppConfig { get; set; }

        public string WorkingDirectory => AppConfig.WorkingDirectory;
        public string XslFile => appConfigRepository.CombinePath(WorkingDirectory, AppConfig.CurrentXslFile);
        public string XmlFile => appConfigRepository.CombinePath(WorkingDirectory, AppConfig.CurrentXmlFile);

        public event EventHandler AppConfigChanged;

        public AppConfigService(IAppConfigRepository appConfigRepository, IMessageWriter messageWriter, IInputReader inputReader)
        {
            this.appConfigRepository = appConfigRepository ?? throw new ArgumentNullException(nameof(appConfigRepository));
            this.messageWriter = messageWriter ?? throw new ArgumentNullException(nameof(messageWriter));
            this.inputReader = inputReader ?? throw new ArgumentNullException(nameof(inputReader));
            Initialize();
        }

        private void Initialize()
        {
            AppConfig = LoadAppConfiguration();
        }

        public AppConfig LoadAppConfiguration()
        {
            var appConfig = new AppConfig();

            List<string> configLines = appConfigRepository.ReadConfig();

            appConfig.WorkingDirectory = GetWorkingDirectory(configLines);
            appConfig.CurrentXslFile = "";
            appConfig.CurrentXmlFile = "";

            return appConfig;
        }

        public (bool, string) SaveAppConfiguration(AppConfig appConfig)
        {
            (bool, string) saveStatus = appConfigRepository.SaveConfig(appConfig);
            AppConfigChanged?.Invoke(this, new EventArgs());
            return saveStatus;
        }


        private string GetWorkingDirectory(List<string> configLines)
        {
            string workingDirectory = "";

            foreach (string line in configLines)
            {
                if(line.Substring(0, 16) == "WorkingDirectory")
                {
                    workingDirectory = line.Substring(17);
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

            while (!appConfigRepository.DirectoryExists(path))
            {
                messageWriter.Write("Specified path does not exist. Do you want to create it? (y/n):");
                string response = inputReader.GetInput(); //UserInterface.GetResponse(new List<string> { "y", "n" });
                if (response == "y")
                {
                    try
                    {
                        appConfigRepository.CreateDirectory(path);
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
            
            AppConfig.WorkingDirectory = path;
            SaveAppConfiguration(AppConfig);
        }


        public void SetXslFile()
        {
            AppConfig.CurrentXslFile = GetFileName("Provide input XSL file:");
            SaveAppConfiguration(AppConfig);
        }

        public void SetXmlFile()
        {
            AppConfig.CurrentXmlFile = GetFileName("Provide input XML file:");
            SaveAppConfiguration(AppConfig);
        }

        private string GetFileName(string inputMessage)
        {
            messageWriter.Write(inputMessage);
            string fileName = inputReader.GetInput();
            while (!appConfigRepository.FileExists(AppConfig.WorkingDirectory, fileName))
            {
                messageWriter.Write("Provided file does not exist in the specified directory. Try again:");
                fileName = inputReader.GetInput();
            }
            return fileName;
        }

    }
}
