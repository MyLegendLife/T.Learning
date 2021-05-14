//冒泡排序

//希望能为C#语言的学习者带来一些益处。不要忘了，学语言要花大力气学数据结构和算法。 

using System;

namespace T.Learning.Arithmetic.Arithmetic
{
    public class BubbleSorter
    {
        public void Sort(int[] list)
        {
            int i, j, temp;
            var done = false;
            j = 1;
            while ((j<list.Length) && (!done))
            {
                done = true;
                for (i = 0; i<list.Length - j; i++)
                {
                    if (list[i]>list[i + 1])
                    {
                        done = false;
                        temp = list[i];
                        list[i] = list[i + 1];
                        list[i + 1] = temp;
                    }
                }
                j++;
            }
        }
    }

    public class MainClass1
    {
        public static void Main1()
        {
            var iArrary = new[] { 1, 5, 13, 6, 10, 55, 99, 2, 87, 12, 34, 75, 33, 47 };
            var sh = new BubbleSorter();
            sh.Sort(iArrary);
            for(var m = 0; m<iArrary.Length; m++)
            Console.Write("{0} ", m);
            Console.WriteLine();
        }
    }
}