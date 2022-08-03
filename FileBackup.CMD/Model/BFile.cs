using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileBackup.CMD.Model
{
    public class BFile
    {
        //public string Name { get; set; }
        public string Path { get; set; }

        public BFile(string path)
        {
            Path = path;
        }

        public string Name()
        {
            return System.IO.Path.GetFileName(Path);
        }

    }
}
