using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using FileBackup.CMD.Model;
using System.Text.Json.Serialization;

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
                JsonSerializerOptions options = new JsonSerializerOptions()
                {
                    Converters = { new JsonStringEnumConverter() }
                };

                _config = JsonSerializer.Deserialize<Config>(fs, options);
            }
        }




    }
}
