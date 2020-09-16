using System;
using System.Collections.Generic;
using System.IO;
using DjK.XslTransformUtility.Core.Config;

namespace DjK.XslTransformUtility.Core.Repositories
{
    public class AppConfigRepository : IAppConfigRepository
    {
        private string AppDirectory => Directory.GetCurrentDirectory();

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public bool FileExists(string path, string fileName)
        {
            return File.Exists(Path.Combine(path, fileName));
        }

        public (bool, string) CreateDirectory(string path)
        {
            bool operationStatus = false;
            string operationStatusInfo = "Directory not created.";

            try
            {
                Directory.CreateDirectory(path);
                operationStatus = true;
                operationStatusInfo = "Directory successfully created.";
            }
            catch (Exception e)
            {
                operationStatus = false;
                operationStatusInfo = "Could not create directory at specified path:" +
                    Environment.NewLine + path + Environment.NewLine + e.ToString();
            }

            return (operationStatus, operationStatusInfo);
        }

        public List<string> ReadConfig()
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


        public (bool, string) SaveConfig(AppConfig appConfig)
        {
            string configFile = Path.Combine(AppDirectory, "config.dat");

            bool saveStatus = false;
            string operationStatusInfo = "Config not saved.";

            using (StreamWriter file = new StreamWriter(configFile))
            {
                try
                {
                    file.WriteLine($"WorkingDirectory={appConfig.WorkingDirectory}");
                    saveStatus = true;
                    operationStatusInfo = "Config saved.";
                }
                catch (Exception e)
                {
                    operationStatusInfo = "Writing to config file failed!" + Environment.NewLine + e.ToString();
                }
            }

            return (saveStatus, operationStatusInfo);
        }
        
        public string CombinePath(string path, string fileName)
        {
            return Path.Combine(path, fileName);
        }
    }
}