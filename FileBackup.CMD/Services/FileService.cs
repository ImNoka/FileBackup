using FileBackup.CMD.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace FileBackup.CMD.Services
{
    public static class FileService
    {
        static string path;
        public static long byteVolume=0;
        public static long byteSum=0;
        public static bool SaveFiles(Config config)
        {
            bool ok = true;
            
            try
            {
                path = Path.Combine(config.DestinationFolder, DateTime.Now.ToString("hh_mm_ss dd.MM.yyyy"));
                Directory.CreateDirectory(path);
            }
            catch(Exception ex)
            {
                Trace.TraceError("Не удалось создать каталог сохранения. Трассировка: "+ex.Message);
                ok= false;
                return ok;
            }
            Trace.TraceInformation("Каталог сохранения создан.");
            foreach (BFolder folder in config.SourceFolders)
            {
                DirectoryInfo fInfo = new DirectoryInfo(folder.FolderPath);
                byteVolume += CheckSize(fInfo);
            }
            byteVolume /= 1024;
            
            Console.WriteLine("Общий объём копируемых данных (MB): "+byteVolume/1024);
            Trace.TraceInformation("Общий объём копируемых данных (MB): " + byteVolume / 1024);
            Console.WriteLine("\n");
            Console.WriteLine("\n");
            foreach (BFolder folder in config.SourceFolders)
            {
                Trace.TraceInformation("Работа с источником "+folder.Name());
                if(folder.Files().Count<=0)
                {
                    Trace.TraceInformation("Источник "+folder.Name()+" пуст.");
                }
                foreach (BChild file in folder.Files())
                {
                    try
                    {
                        /*FileInfo fileInfo = new FileInfo(file.Path);
                        fileInfo.CopyTo(Path.Combine(path, file.Name()));
                        
                        Trace.TraceInformation(file.Name() + ": сохранён");*/
                        RecursiveSave(file, path);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("\n"+file.Name() + ": " + ex.Message);
                        Trace.TraceError(file.Name() + ": " + ex.Message);
                        ok = false;
                    }
                }
            }
            return ok;
        }

        private static void RecursiveSave(BChild item, string _path)
        {
            try
            {
                string childPath = Path.Combine(_path, item.Name());
                if (Directory.Exists(item.Path))
                {
                    Directory.CreateDirectory(childPath);
                    foreach (string child in Directory.GetFiles(item.Path))
                    {
                        RecursiveSave(new BChild(child),childPath);
                    }
                    foreach (string child in Directory.GetDirectories(item.Path))
                    {
                        string childDirName = Path.GetFileName(child);
                        Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                        Console.WriteLine("Работа с каталогом " + childDirName);
                        Trace.TraceInformation("Работа с папкой " + childDirName);
                        RecursiveSave(new BChild(child), childPath);
                    }
                }
                else
                {
                    FileInfo fileInfo = new FileInfo(item.Path);
                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 2);
                    //Console.WriteLine();
                    //Console.WriteLine();
                    Console.Write(new String(' ', Console.BufferWidth));
                    Console.WriteLine(new String(' ', Console.BufferWidth));
                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 2);
                    Console.WriteLine("Завершено: "+ byteSum*100/byteVolume+" (%)");
                    Console.WriteLine(fileInfo.Name+" "+fileInfo.Length / (1024)+" (KB)");
                    fileInfo.CopyTo(childPath);
                    byteSum += fileInfo.Length / (1024);
                }

                
            }
            catch(Exception ex)
            {
                Console.WriteLine(item.Name() + ": " + ex.Message);
                Trace.TraceError(item.Name() + ": " + ex.Message);
            }
        }

        private static long CheckSize(DirectoryInfo folderInfo)
        {
            long size = 0;
            FileInfo[] fInfos = folderInfo.GetFiles();
            foreach(FileInfo fileInfo in fInfos)
            {
                size+=fileInfo.Length;
            }
            DirectoryInfo[] childFolderInfos = folderInfo.GetDirectories();
            foreach(DirectoryInfo child in childFolderInfos)
            {
                size+=CheckSize(child);
            }

            return size;
        }
        
    }
}
