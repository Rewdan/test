using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Test.CustomArray.Sort;
using Test.FileText.Sort;

namespace Test.App2
{
    class Program
    {
        static  void Main(string[] args)
        {
            System.IO.File.Delete("file_sort.txt");
            foreach (var item in Directory.GetFiles(Directory.GetCurrentDirectory(),"*.tmp"))
                System.IO.File.Delete(item);
            //ScoreSeconds(() =>
            //{
            Stopwatch SW = new Stopwatch(); // Создаем объект
                 SW.Start(); 
            QuickSortFile.Begin("file.txt",  "file_sort.txt", true, partSizeByte: 154857600);//.GetAwaiter().GetResult();
                 System.Console.WriteLine($"{null} | Время выполнения = {SW.Elapsed.TotalSeconds} сек");
                 SW.Stop(); //Останавливаем
             //}
             //   , "Сортировка");
            
        }

        static void ScoreSeconds(Action action, string textInfo)
        {
            Stopwatch SW = new Stopwatch(); 
            SW.Start(); 
            action.Invoke();
            System.Console.WriteLine($"{textInfo} | Время выполнения = {SW.Elapsed.TotalSeconds} сек");
            SW.Stop();

        }


    }
}
