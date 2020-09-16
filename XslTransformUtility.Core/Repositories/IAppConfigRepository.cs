using System.Collections.Generic;
using DjK.XslTransformUtility.Core.Config;

namespace DjK.XslTransformUtility.Core.Repositories
{
    public interface IAppConfigRepository
    {
        bool DirectoryExists(string path);
        bool FileExists(string path, string fileName);
        (bool, string) CreateDirectory(string path);
        List<string> ReadConfig();
        (bool, string) SaveConfig(AppConfig appConfig);
        string CombinePath(string path, string fileName);
    }
}