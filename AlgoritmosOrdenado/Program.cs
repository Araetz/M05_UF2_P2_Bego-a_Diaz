using System;
using System.Diagnostics;

namespace AlgoritmosOrdenado
{
    public class ArraySort
    {
        public int[] array;
        public int[] arrayIncreasing;
        public int[] arrayDecreasing;

        public ArraySort(int elements, Random random)
        {
            array = new int[elements];
            arrayIncreasing = new int[elements];
            arrayDecreasing = new int[elements];
            for (int i = 0; i < elements; i++)
            {
                array[i] = random.Next();
            }
            Array.Copy(array, arrayIncreasing, elements);
            Array.Sort(arrayIncreasing);

            Array.Copy(arrayIncreasing, arrayDecreasing, elements);
            Array.Reverse(arrayDecreasing);
        }

        public void Benchmark(Action<int[]> function)
        {
            int[] temp = new int[array.Length];
            Array.Copy(array, temp, array.Length);
            Stopwatch stopwatch = new Stopwatch();
            Console.WriteLine(function.Method.Name);

            stopwatch.Start();
            function(temp);
            stopwatch.Stop();
            Console.WriteLine("Random: " + stopwatch.ElapsedMilliseconds + "ms " + stopwatch.ElapsedTicks + "ticks");
            stopwatch.Reset();

            stopwatch.Start();
            function(temp);
            stopwatch.Stop();
            Console.WriteLine("Increasing: " + stopwatch.ElapsedMilliseconds + "ms " + stopwatch.ElapsedTicks + "ticks");
            stopwatch.Reset();

            Array.Reverse(temp);
            stopwatch.Start();
            function(temp);
            stopwatch.Stop();
            Console.WriteLine("Decreasing: " + stopwatch.ElapsedMilliseconds + "ms " + stopwatch.ElapsedTicks + "ticks");
        }

        public void BubbleSort(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = 0; j < arr.Length - 1; j++)
                {
                    if(arr[j] > arr[j + 1])
                    {
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }
        }
        public void BubbleSortEarlyExit(int[] arr)
        {
            bool isOrdered = true;
            for (int i = 0; i < arr.Length - 1; i++)
            {
                isOrdered = true;
                for (int j = 0; j < arr.Length - 1; j++)
                {
                    if(arr[j] > arr[j + 1])
                    {
                        isOrdered = false;
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
                if (isOrdered)
                    return;
            }
        }
        public void QuickSort(int[] arr)
        {
            QuickSort(arr, 0, arr.Length - 1);
        }
        public void QuickSort(int[] arr, int left, int right)
        {
            if(left < right)
            {
                int pivot = QuickSortIndex(arr, left, right);
                QuickSort(arr, left, pivot);
                QuickSort(arr, pivot + 1, right);
            }
        }
        public int QuickSortIndex(int[] arr, int left, int right)
        {
            int pivot = arr[(left + right) / 2];

            while (true)
            {
                while(arr[left] < pivot)
                {
                    left++;
                }
                while(arr[right] > pivot)
                {
                    right--;
                }
                if(right <= left)
                {
                    return right;
                }
                else
                {
                    int temp = arr[left];
                    arr[left] = arr[right];
                    arr[right] = temp;
                    right--;left++;
                }
            }
        }




       
       public void merge(int[] arr, int p, int q, int r)
        {
            int i, j, k;
            int n1 = q - p + 1;
            int n2 = r - q;
            int[] L = new int[n1];
            int[] R = new int[n2];
            for (i = 0; i < n1; i++)
            {
                L[i] = arr[p + i];
            }

            for (j = 0; j < n2; j++)
            {
                R[j] = arr[q + 1 + j];
            }
            i = 0;
            j = 0;
            k = p;
            while (i < n1 && j < n2)
            {
                if (L[i] <= R[j])
                {
                    arr[k] = L[i];
                    i++;
                }
                else
                {
                    arr[k] = R[j];
                    j++;
                }
                k++;
            }
            while (i < n1)
            {
                arr[k] = L[i];
                i++;
                k++;
            }
            while (j < n2)
            {
                arr[k] = R[j];
                j++;
                k++;
            }
        }
       public void mergeSort(int[] arr, int p, int r)
        {
            if (p < r)
            {
                int q = (p + r) / 2;
                mergeSort(arr, p, q);
                mergeSort(arr, q + 1, r);
                merge(arr, p, q, r);
            }
        }
        public void MergeShort(int[] arr)
        {
            mergeSort(arr,0,arr.Length-1);
        }




        static void heapSort(int[] arr, int n)
        {
            for (int i = n / 2 - 1; i >= 0; i--)
                heapify(arr, n, i);
            for (int i = n - 1; i >= 0; i--)
            {
                int temp = arr[0];
                arr[0] = arr[i];
                arr[i] = temp;
                heapify(arr, i, 0);
            }
        }
        static void heapify(int[] arr, int n, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            if (left < n && arr[left] > arr[largest])
                largest = left;
            if (right < n && arr[right] > arr[largest])
                largest = right;
            if (largest != i)
            {
                int swap = arr[i];
                arr[i] = arr[largest];
                arr[largest] = swap;
                heapify(arr, n, largest);
            }
        }





        public void HeapShort(int[] arr)
        {
            heapSort(arr, arr.Length); 
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Cuantos numeros quieres ordenar?");
            int elements = int.Parse(Console.ReadLine());
            Console.WriteLine("Que semilla quieres usar?");
            int seed = int.Parse(Console.ReadLine());
            Random random = new Random(seed);
            ArraySort arraySort = new ArraySort(elements, random);

            //arraySort.Benchmark(arraySort.BubbleSort);
            arraySort.Benchmark(arraySort.BubbleSortEarlyExit);
            arraySort.Benchmark(arraySort.QuickSort);
            arraySort.Benchmark(arraySort.MergeShort);
            arraySort.Benchmark(arraySort.HeapShort);

        }
    }
}
