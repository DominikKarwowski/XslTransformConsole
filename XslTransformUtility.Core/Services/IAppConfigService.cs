using System;
using DjK.XslTransformUtility.Core.Config;

namespace DjK.XslTransformUtility.Core.Services
{
    public interface IAppConfigService
    {
        string WorkingDirectory { get; }
        string XslFile { get; }
        string XmlFile { get; }

        (bool, string) SaveAppConfiguration(AppConfig appConfig);
        AppConfig LoadAppConfiguration();
        void SetWorkingDirectory();
        void SetXslFile();
        void SetXmlFile();

        event EventHandler AppConfigChanged;
        
    }
}