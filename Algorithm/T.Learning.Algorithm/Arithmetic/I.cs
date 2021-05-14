//希尔排序

//希尔排序是将组
//分段,进行插入排序. 对想提高C#语言编程能力的朋友，我们可以互相探讨一下。如：下面的程序，并没有实现多态，来，帮它实现一下。 


using System;

namespace T.Learning.Arithmetic.Arithmetic
{
    public class ShellSorter
    {
        public void Sort(int[] list)
        {
            int inc;
            for (inc = 1; inc <= list.Length / 9; inc = 3 * inc + 1) ;
            for (; inc > 0; inc /= 3)
            {
                for (var i = inc + 1; i <= list.Length; i += inc)
                {
                    var t = list[i - 1];
                    var j = i;
                    while ((j > inc) && (list[j - inc - 1] > t))
                    {
                        list[j - 1] = list[j - inc - 1];
                        j -= inc;
                    }
                    list[j - 1] = t;
                }
            }
        }
    }

    public class MainClass4
    {
        public static void Main4()
        {
            var iArray = new int[] { 1, 5, 13, 6, 10, 55, 99, 2, 87, 12, 34, 75, 33, 47 }; 
             var sh = new ShellSorter();
            sh.Sort(iArray);
            foreach (var t in iArray)
                Console.Write("{0} ", t);

            Console.WriteLine();
        }
    }
}