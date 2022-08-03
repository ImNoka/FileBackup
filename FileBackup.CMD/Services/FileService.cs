using FileBackup.CMD.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileBackup.CMD.Services
{
    public static class FileService
    {
        public static bool SaveFiles(Config config)
        {
            string path = Path.Combine(config.DestinationFolder, DateTime.Now.ToString("hh_mm_ss dd.MM.yyyy"));
            Directory.CreateDirectory(path);
            foreach(var folder in config.SourceFolders)
                foreach(BFile file in folder.Files())
                {
                    FileInfo fileInfo = new FileInfo(file.Path);
                    fileInfo.CopyTo(Path.Combine(path,file.Name()));
                }

            return true;
        }

    }
}
