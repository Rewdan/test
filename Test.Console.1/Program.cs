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
using Test.CustomArray.Sort;
using Test.FileCreator;
using Test.FileText.Sort;

namespace Test.App1
{

    class Program
    {

        static void Main(string[] args)
        {

            ScoreSeconds(() => FileWorker.Create("file.txt", 200), "Создание файла");

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

