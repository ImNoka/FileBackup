using FileBackup.CMD.Model;
using FileBackup.CMD.Services;
using System.Configuration;
using System.Diagnostics;
using System.Text;

namespace FileBackup.CMD
{
    internal class Program
    {
        static ConfigProvider configProvider;
        static List<BFolder> sourceFolders;
        static void Main(string[] args)
        {
            try
            {
                configProvider = new ConfigProvider();
                Console.WriteLine("Конфигурация считана.");
                Trace.Listeners.Add(new TextWriterTraceListener(Path.Combine(configProvider.Config.LoggerPath, DateTime.Now.ToString("hh_mm_ss dd.MM.yyyy")))
                {
                    Filter = new EventTypeFilter(configProvider.Config.LogLevel),
                    
                });
                Trace.AutoFlush = true;
                Trace.TraceInformation("Старт.");
                if (configProvider.Config.SourceFolders == null)
                {
                    Trace.TraceInformation("Неверно задано имя конфигурации каталогов или указанные каталоги не существуют.");
                    Environment.Exit(0);
                }
                sourceFolders = new List<BFolder>();
                StringBuilder stringFolders = new StringBuilder();
                stringFolders.Append("Найдены источники:");
                foreach (var folderPath in configProvider.Config.SourceFolders)
                {
                    sourceFolders.Add(new BFolder(folderPath.FolderPath));
                    stringFolders.Append("\n"+folderPath.FolderPath);
                }
                Trace.TraceInformation(stringFolders.ToString());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                Environment.Exit(1);
            }

            Console.WriteLine("Источники: ");
            foreach (BFolder folder in sourceFolders)
            {
                Console.WriteLine(folder.FolderPath);
            }
            DateTime startTime = DateTime.Now;
            DateTime endTime;
            if (FileService.SaveFiles(configProvider.Config))
            {
                endTime = DateTime.Now;
                Console.WriteLine("\nПрограмма завершила задачу без ошибок.");
            }
            else
            {
                endTime = DateTime.Now;
                Console.WriteLine("\nПрограмма завершила работу с ошибками.");
            }
            Console.WriteLine("Время выполнения (с) : " + Math.Round((endTime - startTime).TotalSeconds));
            Console.WriteLine("Общий объём зарезервированных данных (MB) : "+FileService.byteSum/1024);
            Trace.TraceInformation("Конец.");
            Console.ReadLine();
            

        }
    }
}