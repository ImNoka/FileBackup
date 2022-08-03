using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using FileBackup.CMD.Model;

namespace FileBackup.CMD.Services
{
    public class ConfigProvider
    {
        Config _config;
        public Config Config
        {
            get
            {
                return _config;
            }
            set
            {
                _config = value;
            }
        }
        public ConfigProvider(string jsonPath = "appsettings.json")
        {
            _config=new Config();
            using(FileStream fs = new FileStream(jsonPath, FileMode.Open))
            {
                _config = JsonSerializer.Deserialize<Config>(fs);
            }
        }




    }
}
