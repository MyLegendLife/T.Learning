using System;
using System.Collections.Generic;

namespace T.Learning.Arithmetic
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(Algorithm.F1(1010));
            //Console.WriteLine(Algorithm.F2(1010));
            //Console.WriteLine(Algorithm.F3(1010));


            var df = new ListEx<string>();

            //var dfd = df[2];

            //var temp = 0;
            //int[] arr = { 23, 44, 66, 76, 98, 11, 3, 9, 7 };
            //#region 该段与排序无关
            //Console.WriteLine("排序前的数组：");
            //foreach (var item in arr)
            //{
            //    Console.Write(item + "");
            //}
            //Console.WriteLine();
            //#endregion
            //for (var i = 0; i < arr.Length - 1; i++)
            //{
            //    #region 将大的数字移到数组的arr.Length-1-i
            //    for (var j = 0; j < arr.Length - 1 - i; j++)
            //    {
            //        if (arr[j] > arr[j + 1])
            //        {
            //            temp = arr[j + 1];
            //            arr[j + 1] = arr[j];
            //            arr[j] = temp;
            //        }
            //    }
            //    #endregion
            //}
            //Console.WriteLine("排序后的数组：");
            //foreach (var item in arr)
            //{
            //    Console.Write(item + "");
            //}
            //Console.WriteLine();
            //Console.ReadKey();




            //int[] data = new int[] { 42, 20, 17, 27, 13, 8, 17, 48 };
            //SelectSort(data);
            //foreach (var temp1 in data)
            //{
            //    Console.Write(temp1 + " ");
            //}
            //Console.ReadKey();

            //var data = new[] { 42, 20, 17, 27, 13, 8, 17, 48 };

            //QuickSort(data, 0, data.Length - 1);

            //foreach (var temp in data)
            //{
            //    Console.Write(temp + " ");
            //}
            //Console.ReadKey();


            //var extend = new Extend() { 42, 20, 17, 27, 13, 8, 17, 48 };
            //var dd = QuickSort2(extend);

            //foreach (var temp in dd)
            //{
            //    Console.Write(temp + " ");
            //}
            //Console.ReadKey();

            var data = new List<int> { 42, 20, 17, 27, 13, 8, 17, 48 };
            QuickSort1(ref data, 0, data.Count - 1);
            foreach (var temp in data)
            {
                Console.Write(temp + " ");
            }
            Console.ReadKey();
        }

        static void SelectSort(int[] dataArray)
        {
            for (var i = 0; i < dataArray.Length - 1; i++)
            {
                var min = dataArray[i];
                var minIndex = i;//最小值所在索引
                for (var j = i + 1; j < dataArray.Length; j++)
                {
                    if (dataArray[j] < min)
                    {
                        min = dataArray[j];
                        minIndex = j;
                    }
                }
                if (minIndex != i)
                {
                    var temp = dataArray[i];
                    dataArray[i] = dataArray[minIndex];
                    dataArray[minIndex] = temp;
                }
            }
        }

        static void QuickSort(int[] dataArray, int left, int right)
        {
            if (left < right)
            {
                var x = dataArray[left];//基准数， 把比它小或者等于它的 放在它的左边，然后把比它大的放在它的右边
                var i = left;
                var j = right;//用来做循环的标志位

                while (true && i < j)//当i==j的时候，说明我们找到了一个中间位置，这个中间位置就是基准数应该所在的位置 
                {

                    //从后往前比较(从右向左比较) 找一个比x小（或者=）的数字，放在我们的坑里 坑位于i的位置
                    while (true && i < j)
                    {
                        if (dataArray[j] <= x) //找到了一个比基准数 小于或者等于的数子，应该把它放在x的左边
                        {
                            dataArray[i] = dataArray[j];
                            break;
                        }
                        else
                        {
                            j--;//向左移动 到下一个数字，然后做比较
                        }
                    }

                    //从前往后（从左向右）找一个比x大的数字，放在我们的坑里面 现在的坑位于j的位置
                    while (true && i < j)
                    {
                        if (dataArray[i] > x)
                        {
                            dataArray[j] = dataArray[i];
                            break;
                        }
                        else
                        {
                            i++;
                        }
                    }

                }

                //跳出循环 现在i==j i是中间位置
                dataArray[i] = x;// left -i- right

                QuickSort(dataArray, left, i - 1);
                QuickSort(dataArray, i + 1, right);
            }
        }

        static void QuickSort1(ref List<int> nums, int left, int right)
        {
            if (left < right)
            {
                var i = left;
                var j = right;
                var middle = nums[(left + right) / 2];
                while (true)
                {
                    while (i < right && nums[i] < middle) { i++; };
                    while (j > 0 && nums[j] > middle) { j--; };
                    if (i == j) break;
                    var temp = nums[i];
                    nums[i] = nums[j];
                    nums[j] = temp;
                    if (nums[i] == nums[j]) j--;
                }
                QuickSort1(ref nums, left, i);
                QuickSort1(ref nums, i + 1, right);
            }
        }

        static Extend QuickSort2(Extend nums)
        {
            if (nums.Count < 2)
            {
                return nums;
            }
            else
            {
                var minList = new Extend();//小于基准数的集合
                var maxList = new Extend();//大于基准数的集合
                var f = nums[0];
                for (var i = 1; i < nums.Count; i++)
                {
                    if (nums[i] <= f) minList.Add(nums[i]);
                    else maxList.Add(nums[i]);
                }
                return QuickSort2(minList) + new Extend() { f } + QuickSort2(maxList);//递归，并且使用+运算符
            }
        }
    }

    public class Extend : List<int>
    {
        private static Extend k = new Extend();

        public static Extend operator +(Extend L1, Extend L2)
        {
            if (L1.Count == 1) k.Add(L1[0]);
            if (L2.Count == 1) k.Add(L2[0]);
            return k;
            //L1.AddRange(L2);
            //return L1;
        }
    }
}
