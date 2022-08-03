using FileBackup.CMD.Model;
using FileBackup.CMD.Services;
using System.Configuration;

namespace FileBackup.CMD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConfigProvider configProvider = new ConfigProvider();


            BFolder destFolder = new BFolder(configProvider.Config.DestinationFolder);
            List<BFolder> sourceFolders = new List<BFolder>();
            foreach(var folderPath in configProvider.Config.SourceFolders)
                sourceFolders.Add(new BFolder(folderPath.FolderPath));

            

            foreach(BFolder folder in sourceFolders)
            {
                Console.WriteLine(folder.FolderPath);
                foreach(var file in folder.Files())
                Console.WriteLine(file.Path);
            }
            Console.Read();

            if (FileService.SaveFiles(configProvider.Config))
                Console.WriteLine("Success");
            else 
                Console.WriteLine("Error");
            Console.ReadLine();


        }
    }
}