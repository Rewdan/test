using System;
using System.Diagnostics;
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
            Tree tree1 = new Tree("file.txt");
             //ScoreSeconds(() =>
             //{
                 Stopwatch SW = new Stopwatch(); // Создаем объект
                 SW.Start(); // Запускаем
            QuickSortFile.Begin("file.txt", tree1, "file_sort.txt", true, partSizeByte: 154857600);//.GetAwaiter().GetResult();
                 System.Console.WriteLine($"{null} | Время выполнения = {SW.Elapsed.TotalSeconds} сек");
                 SW.Stop(); //Останавливаем
             //}
             //   , "Сортировка");
            tree1 = null;
        }

        static void ScoreSeconds(Action action, string textInfo)
        {
            Stopwatch SW = new Stopwatch(); // Создаем объект
            SW.Start(); // Запускаем
            action.Invoke();
            System.Console.WriteLine($"{textInfo} | Время выполнения = {SW.Elapsed.TotalSeconds} сек");
            SW.Stop(); //Останавливаем

        }


    }
}
