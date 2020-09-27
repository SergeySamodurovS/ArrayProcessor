using System;
using System.IO;

namespace ArrayProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFile = new StreamReader("input.txt");
            int arraySize = Convert.ToInt32(inputFile.ReadLine().Trim());

            int[] array = new int[arraySize];
            int count = 0;
            string temp = inputFile.ReadLine().Trim();
            while (temp.Contains(' '))
            {
                array[count] = Convert.ToInt32(temp.Substring(0, temp.IndexOf(' ')));
                temp = temp.Substring(temp.IndexOf(' ') + 1);
                count++;
            }
            array[count] = Convert.ToInt32(temp);

            int sampleSize = Convert.ToInt32(inputFile.ReadLine().Trim());
            inputFile.Close();

            if (sampleSize >= arraySize)
            {
                Array.Sort(array);
                Array.Reverse(array);
                PrintToFile(array);
            }
            else if (sampleSize <= Math.Log2(arraySize))
            {
                PrintToFile(GetArraySmallSample(array, sampleSize));
            }
            else
            {
                PrintToFile(GetArrayBigSample(array, sampleSize));
            }
            Console.ReadKey();
        }

        private static int[] GetArraySmallSample(int[] array, int sampleNumber)
        {
            int[] outArray = new int[sampleNumber];
            int max = array[0];
            int indexMax = 0;
            for (int i = 0; i < sampleNumber; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    if (array[j] > max)
                    {
                        max = array[j];
                        indexMax = j;
                    }
                }
                outArray[i] = max;
                Array.Clear(array, indexMax , 1);
                max = array[0];
                indexMax = 0;
            }
            return outArray;
        }

        private static int[] GetArrayBigSample(int[] array, int sampleNumber)
        {
            Array.Sort(array);
            int[] outArray = new int[sampleNumber];
            Array.Copy(array, array.Length - sampleNumber, outArray, 0, sampleNumber);
            Array.Reverse(outArray);
            return outArray;
        }

        private static void PrintToFile(int[] array)
        {
            using var file = new StreamWriter("output.txt");
            foreach (var item in array)
            {
                file.Write(item + " ");
            }
        }
    }
}
