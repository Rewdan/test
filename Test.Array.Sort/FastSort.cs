using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Test.CustomArray.Sort
{
    public class QuickSortBase
    {
        public static int Threshold = 500; // array length to use InsertionSort instead of SequentialQuickSort

        protected static void InsertionSort<T>(T[] array, int from, int to) where T : IComparable<T>
        {
            for (int i = from + 1; i < to; i++)
            {
                var a = array[i];
                int j = i - 1;

                //while (j >= from && array[j] > a)
                while (j >= from && array[j].CompareTo(a) == -1)
                {
                    array[j + 1] = array[j];
                    j--;
                }
                array[j + 1] = a;
            }
        }


        static void Swap<T>(T[] array, int i, int j) where T : IComparable<T>
        {
            var temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        protected static int Partition<T>(T[] array, int from, int to, int pivot) where T : IComparable<T>
        {
            var arrayPivot = array[pivot];  
            Swap(array, pivot, to - 1); 
            var newPivot = from; 
            for (int i = from; i < to - 1; i++) 
            {
                
                if (array[i].CompareTo(arrayPivot) != -1)
                {
                    Swap(array, newPivot, i);  
                    newPivot++;
                }
            }
            Swap(array, newPivot, to - 1); 
            return newPivot; 
        }

        public static void SequentialQuickSort<T>(T[] array) where T : IComparable<T>
        {
                SequentialQuickSort(array, 0, array.Length);
        }


        protected static void SequentialQuickSort<T>(T[] array, int from, int to) where T : IComparable<T>
        {
            if (to - from <= Threshold)
            {
                InsertionSort<T>(array, from, to);
            }
            else
            {
                int pivot = from + (to - from) / 2; // could be anything, use middle
                pivot = Partition<T>(array, from, to, pivot);
                // Assert: forall i < pivot, array[i] <= array[pivot]  && forall i > ...
                SequentialQuickSort(array, from, pivot);
                SequentialQuickSort(array, pivot + 1, to);
            }
        }

    }

    public class PQuickSort : QuickSortBase
    {
        /// <summary>
        /// Сортировка с использованием plinq
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        public static void ParallelQuickSort<T>(T[] array) where T : IComparable<T>
        {

                ParallelQuickSort(array, 0, array.Length,
               (int)Math.Log(Environment.ProcessorCount, 2) + 4);

        }

        static void ParallelQuickSort<T>(T[] array, int from, int to, int depthRemaining) where T : IComparable<T>
        {
            if (to - from <= Threshold)
            {
                InsertionSort<T>(array, from, to);
            }
            else
            {
                int pivot = from + (to - from) / 2; // could be anything, use middle
                pivot = Partition<T>(array, from, to, pivot);
                if (depthRemaining > 0)
                {
                    Parallel.Invoke(
                        () => ParallelQuickSort(array, from, pivot, depthRemaining - 1),
                        () => ParallelQuickSort(array, pivot + 1, to, depthRemaining - 1));
                }
                else
                {
                    SequentialQuickSort(array, from, pivot);
                    SequentialQuickSort(array, pivot + 1, to);
                }
            }
        }
    }

}
