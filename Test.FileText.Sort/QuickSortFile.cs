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
using Test.File.Format;


namespace Test.FileText.Sort
{

    public static class QuickSortFile
    {

        static Test.Text.Format.StringComparer _stringComp = new Test.Text.Format.StringComparer();
        static QuickSortFile()
        {
            _stringComp = new Test.Text.Format.StringComparer();
        }

        static public  void Begin(string fileNameForSort, Tree tree, string fileNameDataSort = "file_sort.txt", bool saveFile = true, int partSizeByte = 104857600)
        {
            Do(fileNameForSort, tree, saveFile, partSizeByte);
           CombineSortFilesRecursive(tree, fileNameDataSort);
        }

        /// <summary>
        /// объединить все файлы
        /// </summary>
        /// <param name="tree"></param>
        private static  void CombineSortFilesRecursive(Tree tree,string fileNameSort)
        {

            if (tree.Children == null || tree.Children.Length == 0)
                return;
          

            if (tree.Children.Length == 1)
            {
                var item = tree.Children[0];
                using (StreamReader reader = System.IO.File.OpenText(item.FileName))
                using (StreamWriter writer1 = new StreamWriter(fileNameSort, append: true))
                {
                    while (!reader.EndOfStream)
                        writer1.WriteLine(reader.ReadLine());
                }
                System.IO.File.Delete(item.FileName);
                return;
            }

            foreach (var item in tree.Children)
                CombineSortFilesRecursive(item, fileNameSort);

        }
     

        /// <summary>
        /// Сортирует файл по формату {num. text} с использование промежуточных файлов
        /// </summary>
        /// <param name="fileName">файл для сортировки</param>
        /// <param name="tree"> дерево вызовов для сохранения порядка</param>
        /// <param name="saveFile">сохранять ли исходный файл</param>
        /// <param name="partSizeByte"> размер сортированного блока файла</param>
        static private void  Do(string fileName, Tree tree, bool saveFile = true, int partSizeByte = 104857600)
        {

            string fileName1 = $"{Task.CurrentId}_1.tmp";
            string fileName2 = $"{Task.CurrentId}_2.tmp";
            ///разбить на файлы размером partSizeByte
            if ((new FileInfo(fileName)).Length > partSizeByte)
            {
                using (var redaer = new StreamReader(fileName))
                using (var writer1 = new StreamWriter(fileName1))
                using (var writer2 = new StreamWriter(fileName2))
                {
                    var str =  redaer.ReadLine();
                    while (!redaer.EndOfStream)
                    {
                        var line =  redaer.ReadLine();
                        if (_stringComp.Compare(str, line) >= 0)
                             writer1.WriteLine(line);
                        else
                             writer2.WriteLine(line);
                    }
                     writer1.WriteLine(str);

                }

                Tree tree1 = new Tree(fileName1, tree);
                Tree tree2 = new Tree(fileName2, tree);
                tree.SetChild(new Tree[] { tree1, tree2 });
                if (!saveFile)
                    System.IO.File.Delete(fileName);
                Parallel.Invoke(
                           () => Do(fileName1, tree1, false, partSizeByte),
                               () => Do(fileName2, tree2, false, partSizeByte));

            }
            else //если файл меньше указ размера сортируем часть
            {
                var strs = System.IO.File.ReadAllLines(fileName).Select(x => new LineData(x, '.')).ToArray();
                if (!saveFile)
                    System.IO.File.Delete(fileName);
                PQuickSort.ParallelQuickSort(strs);
                Array.Reverse(strs);
                 System.IO.File.WriteAllLines(fileName2, strs.Select(x => x.Str));
                strs = null;
                Tree tree1 = new Tree(fileName2, tree);
                tree.FileName = null;
                tree.SetChild(new Tree[] { tree1});

            }


        }


    }

}
