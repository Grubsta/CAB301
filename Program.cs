using System;
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
            
            return 0; 
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
            return 0; 
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
            int testCases = 2;

            // Array for the sizes of array to test
            int[] sizeArray = new int[] { 10, 50, 100, 500, 1000, 2500, 5000, 7500, 10000, 50000};

            // List containing values
            List<int> testValues =  new List<int>();

            // number of tests
            int testCount = sizeArray.Length;
            double[,] BFtime = new double[testCount, testCases+1];
            double[,] Mtime = new double[testCount, testCases+1];

            Console.WriteLine("[status] Commencing Testing...");

            // Runs for the amount of tests
            for (int a = 0; a < testCases; a++)
            {
                Console.WriteLine("[status] Test number " + (a + 1) + "...");

                Random rand = new Random();
                // Runs for ever number in Array
                for(int index = 0; index < testCount; index++)
                {
                    
                    int size = sizeArray[index];
                    Console.WriteLine("[status] Testing array of size " + size + "...");
                    for (int i = 0; i < size; i++)
                    {
                        testValues.Add(rand.Next(1,size + 1));
                    }
                    int[] values = testValues.ToArray();

                    testValues.Clear();

                    // Adds the time performance of each alogrithm to an array[Index, test case] = time in milliseconds
                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    BruteForceMedian(values);
                    watch.Stop();

                    BFtime[Array.IndexOf(sizeArray, size), a] = watch.Elapsed.TotalMilliseconds;

                    watch.Reset();
                    watch.Start();
                    Median(values);
                    watch.Stop();

                    Mtime[Array.IndexOf(sizeArray, size), a] = watch.Elapsed.TotalMilliseconds;

                }
            }
            /// <summary>
            /// Iterates through the sizeArray averaging the time of each size and outputs it to the console.
            /// </summary>
            for (int a = 0; a < testCount; a++)
            {
                double BFtotalTime = 0;
                double MtotalTime = 0;
                
                for (int i = 0; i < testCases; i++)
                {
                    BFtotalTime += BFtime[a, i];
                    MtotalTime += Mtime[a, i];

                }
                BFtotalTime /= testCases;
                MtotalTime /= testCases;

                Console.WriteLine("Average Time, set:  " + sizeArray[a] + "  Time(BF  :  M):  " + BFtotalTime + "  :  " + MtotalTime + " ms");
            }
            Console.ReadKey();
        }
    }
}

