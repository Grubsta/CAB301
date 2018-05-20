using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BruteForceMedian
{
    static class Counter
    {
        public static int BFcounter;
        public static int Mcounter;
    }

    class Program
    {
        /// <summary>
        /// Returns the median value in a given array A of n numbers. 
        /// This is the kth element, where k = n/2, if the array was sorted.
        /// </summary>

        static int BruteForceMedian(int[] array)
        {
            int k = (int)array.Length / 2;

            for (int a = 0; a <= array.Length - 1; a++)
            {
                int numSmaller = 0;
                int numEqual = 0;
                for (int b = 0; b <= array.Length - 1; b++)
                {
                    if (array[b] < array[a]) numSmaller += 1;
                    else
                    {
                        if (array[b] == array[a])
                        {
                            numEqual += 1;
                            Counter.BFcounter++;
                        }
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
            else
            {
                int testVar = Select(array, 0, (int)length / 2, length - 1);
                return testVar;
            }
        }
        /// <summary>
        /// Returns the value at index m in array slice A[l..h], if the slice 
        /// were sorted into nondecreasing order.
        /// </summary>
        static int Select(int[] array, int l, int m, int h)
        {
            Counter.Mcounter++;
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
            for (int j = l + 1; j <= h; j++)
            {
                if (array[j] < pivotal)
                {
                    pivotloc += 1;
                    Swap(array, pivotloc, j); // Swap elements around pivot
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


        static void Testing(int testCases)
        {
            
            // Array for the sizes of array to test
            int[] sizeArray = new int[] { 10, 50, 100, 500, 1000, 2500, 5000, 7500, 10000, 15000, 30000, 50000};

            // List containing values
            List<int> testValues = new List<int>();

            // Number of tests
            int testCount = sizeArray.Length;

            // Measurement variables for storing time and basic operations
            // dependent on test case number.
            double[,] BFtime = new double[testCount, testCases];
            double[,] Mtime = new double[testCount, testCases];

            double[,] BFops = new double[testCount, testCases];
            double[,] Mops = new double[testCount, testCases];

            // Watch class for timing algorithms
            var watch = System.Diagnostics.Stopwatch.StartNew();

            Console.WriteLine("[status] Commencing Testing...");
            // Runs for the amount of tests
            for (int a = 0; a < testCases; a++)
            {
                Console.WriteLine("[status] Test number " + (a + 1) + "...");

                Random rand = new Random();
                // Runs for every number in Array
                for (int index = 0; index < testCount; index++)
                {
                    int size = sizeArray[index];
                    for (int i = 0; i < size; i++)
                    {
                        testValues.Add(rand.Next(1, 10000000));
                    }
                    int[] values = testValues.ToArray();

                    testValues.Clear();

                    // Adds the time performance of each alogrithm to an array[Index, test case] = time in milliseconds
                    // Tesings brute force median algorithm
                    Console.WriteLine("[status] Testing unorder bfmed of size: \t" + size + "...");
                    Counter.BFcounter = 0;

                    watch.Reset();
                    watch.Start();
                    BruteForceMedian(values);
                    watch.Stop();

                    BFops[Array.IndexOf(sizeArray, size), a] = Counter.BFcounter;
                    BFtime[Array.IndexOf(sizeArray, size), a] = watch.Elapsed.TotalMilliseconds;

                    // Testing median algoritm
                    Console.WriteLine("[status] Testing unorder median of size:\t" + size + "...");
                    Counter.Mcounter = 0;

                    watch.Reset();
                    watch.Start();
                    Median(values);
                    watch.Stop();

                    Mops[Array.IndexOf(sizeArray, size), a] = Counter.Mcounter;
                    Mtime[Array.IndexOf(sizeArray, size), a] = watch.Elapsed.TotalMilliseconds;

                }

                
            }

            Console.WriteLine("[status]");
            Console.WriteLine("[status] RESULTS");
            Console.WriteLine("[status]");
            /// <summary>
            /// Iterates through the sizeArray averaging the time of each size and outputs it to the console.
            /// </summary>
            double BFtotalTime, MtotalTime, BFopsTotal, MopsTotal;
            for (int a = 0; a < testCount; a++)
            {
                BFtotalTime = 0;
                MtotalTime = 0;

                BFopsTotal = 0;
                MopsTotal = 0;

                // Total all the times dependent on test cases
                for (int i = 0; i < testCases; i++)
                {
                    // Total time (Unordered)
                    BFtotalTime += BFtime[a, i];
                    MtotalTime += Mtime[a, i];

                    // Total operations (unordered)
                    BFopsTotal += BFops[a, i];
                    MopsTotal += Mops[a, i];


                }
                // Averaging results
                BFtotalTime /= testCases;
                MtotalTime /= testCases;


                BFopsTotal /= testCases;
                MopsTotal /= testCases;


                // Output results
                Console.WriteLine("[result] Average Time for unorder set:\t" + sizeArray[a] + "\tTime(BF  :  M):\t" + BFtotalTime + " ms : " + MtotalTime + " ms");
                Console.WriteLine("[result] Average Ops for unorder set:\t" + sizeArray[a] + "\tOps (BF  :  M):\t" + BFopsTotal + "ops : " + MopsTotal + "ops");
            }
            // Close program
            Console.ReadKey();
        }

        static void FunctionalTesting()
        {
            // Watch class for timing algorithms
            var watch = System.Diagnostics.Stopwatch.StartNew();

            double BFtime, Mtime;
            int BFvalue, Mvalue;
            int[] OrderedEven = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int[] UnorderedEven = { 9, 5, 2, 3, 4, 1, 6, 7, 8, 0 };
            int[] UnorderedOdd = { 9, 5, 2, 3, 4, 1, 6, 7, 8 };


            Console.WriteLine("[Status] Validation testing for set: { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }");
            //Testing Bruteforce Median
            Counter.BFcounter = 0;
            watch.Reset();
            watch.Start();
            BFvalue = BruteForceMedian(OrderedEven);
            watch.Stop();
            BFtime = watch.Elapsed.TotalMilliseconds;

            // Testing median algorithm
            Counter.Mcounter = 0;

            watch.Reset();
            watch.Start();
            Mvalue = Median(OrderedEven);
            watch.Stop();
            Mtime = watch.Elapsed.TotalMilliseconds;

            Console.WriteLine("[result] \tValue (BF  :  M):\t" + BFvalue + " / " + Mvalue);
            Console.WriteLine("[result] \tTime  (BF  :  M):\t" + BFtime + " ms : " + Mtime + " ms");
            Console.WriteLine("[result] \tOps   (BF  :  M):\t" + Counter.BFcounter + "ops : " + Counter.Mcounter + "ops");


            Console.WriteLine("[Status] Validation testing for set: { 9, 5, 2, 3, 4, 1, 6, 7, 8, 0 }");
            //Testing Bruteforce Median
            Counter.BFcounter = 0;
            watch.Reset();
            watch.Start();
            BFvalue = BruteForceMedian(UnorderedEven);
            watch.Stop();
            BFtime = watch.Elapsed.TotalMilliseconds;

            // Testing median algorithm
            Counter.Mcounter = 0;

            watch.Reset();
            watch.Start();
            Mvalue = Median(UnorderedEven);
            watch.Stop();
            Mtime = watch.Elapsed.TotalMilliseconds;

            Console.WriteLine("[result] \tValue (BF  :  M):\t" + BFvalue + " / " + Mvalue);
            Console.WriteLine("[result] \tTime  (BF  :  M):\t" + BFtime + " ms : " + Mtime + " ms");
            Console.WriteLine("[result] \tOps   (BF  :  M):\t" + Counter.BFcounter + "ops : " + Counter.Mcounter + "ops");


            Console.WriteLine("[Status] Validation testing for set: { 9, 5, 2, 3, 4, 1, 6, 7, 8 }");
            //Testing Bruteforce Median
            Counter.BFcounter = 0;
            watch.Reset();
            watch.Start();
            BFvalue = BruteForceMedian(UnorderedOdd);
            watch.Stop();
            BFtime = watch.Elapsed.TotalMilliseconds;

            // Testing median algorithm
            Counter.Mcounter = 0;

            watch.Reset();
            watch.Start();
            Mvalue = Median(UnorderedOdd);
            watch.Stop();
            Mtime = watch.Elapsed.TotalMilliseconds;

            Console.WriteLine("[result] \tValue (BF  :  M):\t" + BFvalue + " / " + Mvalue);
            Console.WriteLine("[result] \tTime  (BF  :  M):\t" + BFtime + " ms : " + Mtime + " ms");
            Console.WriteLine("[result] \tOps   (BF  :  M):\t" + Counter.BFcounter + "ops : " + Counter.Mcounter + "ops");


            Console.ReadKey();
            

        }
        static void Main(string[] args)
        {
            //FunctionalTesting();
            Testing(100);
        }
    }
}

