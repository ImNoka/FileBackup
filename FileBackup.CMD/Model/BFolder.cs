using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileBackup.CMD.Model
{
    [Serializable]
    public class BFolder
    {
        //public string Name { get; set; }
        public string FolderPath { get; set; }

        public BFolder()
        {

        }

        public BFolder(string path)
        {
            FolderPath = path;
            //Name = Path.GetFileName(path);
        }

        public List<BFile> Files()
        {
            List<BFile> files = new List<BFile>();
            foreach (var file in Directory.GetFiles(FolderPath))
                files.Add(new BFile(file));
            return files;
        }

        
    }
}
