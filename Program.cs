﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BruteForceMedian
{
    class Program
    {

        /// <summary>
        /// Returns the median value in a given array A of n numbers. 
        /// This is the kth element, where k = n/2, if the array was sorted.
        /// </summary>
        static int BruteForceMedian(int[] array)
        {
            int operations = 0;
            int k = (int)array.Length / 2;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            for (int a = 0; a <= array.Length - 1; a++)
            {
                int numSmaller = 0;
                int numEqual = 0;
                for (int b = 0; b <= array.Length - 1; b++)
                {
                    operations += 1;
                    if (array[b] < array[a]) numSmaller += 1;
                    else
                    {
                        if (array[b] == array[a]) numEqual += 1;
                    }
                }
                if (numSmaller < k && k <= (numSmaller + numEqual)) return array[a];
            }
            watch.Stop();
            return 0; // Haven't implemented operations.
        }
        /// <summary>
        /// Returns the median value in a given array A of n numbers.
        /// </summary>
        static int Median(int[] array)
        {
            int length = array.Length;
            if (length == 1) return array[0];
            else return Select(array, 0, (int)length / 2, length - 1);
        }
        /// <summary>
        /// Returns the value at index m in array slice A[l..h], if the slice 
        /// were sorted into nondecreasing order.
        /// </summary>
        static int Select(int[] array, int l, int m, int h)
        {
            int operations = 0;
            int pos = Partition(array, l, h);
            if (pos == m) return array[pos];
            if (pos > m) return Select(array, l, m, pos - 1);
            if (pos < m) return Select(array, pos + 1, m, h);
            return 0; // Haven't implemented operations.
        }
        /// <summary>
        /// Partitions array slic A[l..h] by moving element A[l} to the position
        /// it would have if the array slice was sorted, and by moving all
        /// values in the slice smaller than A[l] to earlier positions, and all values 
        /// larger than ore
        ///
        /// </summary>
        static int Partition(int[] array, int l, int h)
        {
            int pivotal = array[l];
            int pivotloc = l;
            for (int a = l + 1; a < h; a++)
            {
                if (array[a] < pivotal)
                {
                    pivotloc += 1;
                    Swap(array, pivotloc, a); // Swap elements around pivot
                }
            }
            Swap(array, l, pivotloc); // Put pivot element in place.
            return pivotloc;
        }
        /// <summary>
        /// Swaps a value a in an array with value b.
        /// </summary>
        static void Swap(int[] array, int a, int b)
        {
            int temp = array[a];
            array[a] = array[b];
            array[b] = temp;
        }

        static void Main(string[] args)
        {



            int testCases = 100;
            int[] array = new int[] { 10, 50, 100, 500, 1000, 2500, 5000, 7500, 10000, 50000, 100000 };
            int median = Median(array);
            int tests = array.Length;
            double[,] time = new double[tests, testCases];

            for (int a = 0; a < testCases; a++)
            {
                Console.WriteLine("Test Case: " + a);
                foreach (int number in array)
                {
                    int[] subarray = new int[number + 1];
                    for (int i = number / 2; i >= -(number / 2); i--)
                    {
                        array[i + (number / 2)] = i;
                    }
                    time[Array.IndexOf(array, number), a] = Median(array);
                }
            }


            for (int a = 0; a < tests; a++)
            {
                double totalTime = 0;
                for (int i = 0; i < testCases; i++)
                {
                    totalTime += time[a, i];
                }
                totalTime /= testCases;
                Console.WriteLine(a + "    :    " + totalTime);
            }

        }
    }
}

