//选择排序
//本人用了C#开发出选择排序算法。希望能为C#语言的学习者带来一些益处。不要忘了，学语言要花大力气学数据结构和算法。 

using System;

namespace T.Learning.Arithmetic.Arithmetic
{
    public class SelectionSorter
    {
        private int min;
        public void Sort(int[] list)
        { 
            for(var i = 0; i<list.Length-1;i++)
            { 
                min=i; 
                for(var j = i + 1; j<list.Length;j++)
                { 
                    if(list[j]<list[min])
                    min=j; 
                }
                var t = list[min];
                list[min]=list[i]; 
                list[i]=t; 
            }
        } 
    } 
    public class MainClass2
    {
        public static void Main2()
        { 
            var iArrary = new int[] { 1, 5, 3, 6, 10, 55, 9, 2, 87, 12, 34, 75, 33, 47 };
            var ss = new SelectionSorter();
            ss.Sort(iArrary); 
            for(var m = 0; m<iArrary.Length;m++)
            Console.Write("{0} ",iArrary[m]); 
            Console.WriteLine();
        } 
    } 
}