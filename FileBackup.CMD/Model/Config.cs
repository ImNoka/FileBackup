using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Diagnostics;

namespace FileBackup.CMD.Model
{
    [Serializable]
    public class Config
    {
        public List<BFolder> SourceFolders { get; set; }
        public string DestinationFolder { get; set; }
        public string LoggerPath { get; set; }

        public SourceLevels LogLevel { get; set; }

    }
}
