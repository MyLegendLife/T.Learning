using System;

namespace T.Learning.Arithmetic.Arithmetic
{
    //快速排序
    public class QuickSort
    {
        public int Partition(int[] array, int min, int max)
        {
            array[0] = array[min];
            var value = array[min];
            while (min < max)
            {
                while (min < max && array[max] >= value) max--;
                array[min] = array[max];
                while (min < max && array[min] <= value) min++;
                array[max] = array[min];
            }
            array[min] = array[0];
            return min;
        }


        public void QKSort(int[] array, int min, int max)
        {
            if (min < max)
            {
                var IntPar = Partition(array, min, max);
                QKSort(array, min, IntPar - 1);
                QKSort(array, IntPar + 1, max);
            }
        }

        public void DisplayData(int[] array)
        {
            for (var i = 1; i <= array.Length - 1; i++)
            {
                Console.Write("\t" + array[i]);
            }
        }
    }
}