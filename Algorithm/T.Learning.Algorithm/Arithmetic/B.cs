namespace T.Learning.Arithmetic.Arithmetic
{
    //折半查找
    public class BinarySearch
    {
        public int Search(int[] array, int value)
        {
            if (array == null || array.Length <= 0) return -1;

            var min = 0;
            var max = array.Length - 1;
            var mid = (max + min) / 2;
            while (min <= max)
            {
                if (array[mid] == value)
                {
                    return mid;
                }
                else if (array[mid] > value)
                {
                    max = mid - 1;
                }
                else if (array[mid] < value)
                {
                    min = mid + 1;
                }
                mid = (max + min) / 2;
            }
            return -1;
        }
    }
}