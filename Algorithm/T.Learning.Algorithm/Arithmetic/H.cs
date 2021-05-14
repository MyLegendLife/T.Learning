//插入排序
//插入排序算法。对想提高C#语言编程能力的朋友，我们可以互相探讨一下。如：下面的程序，并没有实现多态，来，帮它实现一下。 

using System;

namespace T.Learning.Arithmetic.Arithmetic
{
    public class InsertionSorter
    {
        public void Sort(int[] list)
        { 
            for(var i = 1; i<list.Length;i++)
            { 
                var t = list[i];
                var j = i; 
                while((j>0)&&(list[j - 1]>t))
                { 
                    list[j]=list[j - 1]; 
                    --j; 
                }
                list[j]=t; 
            }
        } 
    } 
    public class MainClass3
    {
        public static void Main3()
        { 
            var iArrary = new int[] { 1, 13, 3, 6, 10, 55, 98, 2, 87, 12, 34, 75, 33, 47 };
            var ii = new InsertionSorter();
            ii.Sort(iArrary); 
            for(var m = 0; m<iArrary.Length;m++)
            Console.Write("{0}",iArrary[m]); 
            Console.WriteLine(); 
        } 
    } 
}