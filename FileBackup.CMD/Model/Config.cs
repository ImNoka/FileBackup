using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace FileBackup.CMD.Model
{
    [Serializable]
    public class Config
    {
        public List<BFolder> SourceFolders { get; set; }
        public string DestinationFolder { get; set; }
        public string ErrorPath { get; set; }
        public string LoggerPath { get; set; }

    }
}
