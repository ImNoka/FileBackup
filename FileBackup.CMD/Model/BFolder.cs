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
        public string FolderPath { get; set; }

        public BFolder()
        {

        }

        public BFolder(string path)
        {
            FolderPath = path;
        }

        public List<BChild> Files()
        {
            List<BChild> childs = new List<BChild>();
            foreach(var child in Directory.GetDirectories(FolderPath))
                childs.Add(new BChild(child));
            foreach (var file in Directory.GetFiles(FolderPath))
                childs.Add(new BChild(file));
            return childs;
        }

        public string Name()
        {
            return Path.GetFileName(FolderPath);
        }

        
    }
}
