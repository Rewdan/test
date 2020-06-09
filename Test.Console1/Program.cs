using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Test.FileCreator;
using Test.FileText.Sort;

namespace Test.Console
{

  


    class Program
    {
        static BlockingCollection<string> _concurentLine = new BlockingCollection<string>();

        static void Main(string[] args)
        {

                        DirectoryInfo d = new DirectoryInfo(@"C:\Users\mainUser\source\repos\test\test.console\bin\Debug\netcoreapp3.1");//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles(); //Getting Text files
            foreach (FileInfo file in Files)
            {
                if (file.Name.Contains(".tmp"))
                    File.Delete(file.Name);
            }

            ConcurrentBag<string> strs = new ConcurrentBag<string>();
            int limitMb = 60;
            Stopwatch SW = new Stopwatch(); // Создаем объект
            SW.Start(); // 
            FileWorker.Create("file.txt", limitMb);
            Console.WriteLine($"{null} | Время создания = {SW.Elapsed.TotalSeconds} сек");

            File.Delete("file_sort.txt");

                Tree tree1 = new Tree("file.txt");

            // ScoreSeconds(new Action(() => {
            QuickSortFile.Begin("file.txt", tree1, true);
            // }),"Сортировка файла");
            Console.WriteLine($"{null} | Время сортировки = {SW.Elapsed.TotalSeconds} сек");


            FileInfo fileInfo = new FileInfo("file.txt");

            // ScoreSeconds(new Action(() => {
            RecursiveTree(tree1);
            Console.WriteLine($"{null} | Время Объединения файлов = {SW.Elapsed.TotalSeconds} сек");

            SW.Stop(); //Останавливаем

            //}), "Соединение файлов в 1 файл");
            return;

        }

        static void ScoreSeconds(Action action,string textInfo)
        {
            Stopwatch SW = new Stopwatch(); // Создаем объект
            SW.Start(); // Запускаем

            action.Invoke();
            Console.WriteLine($"{textInfo} | Время выполнения = {SW.Elapsed.TotalSeconds} сек");

            SW.Stop(); //Останавливаем

        }
        static void RecursiveTree(Tree tree)
        {


            if(tree.Children.Count==1)
            {
                var item = tree.Children.ElementAt(0);
                //if (new FileInfo(item.FileName).Length > 72428800)
                //{
                //  using (FileStream fs = File.Open(item.FileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                //  using (BufferedStream bs = new BufferedStream(fs))
                //  using (var redaer = new StreamReader(bs))
                using (StreamReader redaer = File.OpenText(item.FileName))
                using (var writer1 = new StreamWriter("file_sort.txt", append: true))
                    {
                        while (!redaer.EndOfStream)
                            writer1.WriteLine(redaer.ReadLine());

                    }
                //}
                //else
                //{
                //    File.AppendAllText("file_sort.txt", File.ReadAllText(item.FileName));
                //}
                    File.Delete(item.FileName);
                return;
            }
            foreach (var item in tree.Children)
            {
                RecursiveTree(item);
            }

           // if (tree!=null && tree.Children.Count==0)

        }


    }
}
