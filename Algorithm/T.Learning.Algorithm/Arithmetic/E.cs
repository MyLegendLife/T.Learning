using System;

namespace T.Learning.Arithmetic.Arithmetic
{
    class Program
    {
        static void MainE(string[] args)
        {
            BinaryTreeList();
            Console.Read();
        }

        //单链表
        static void SingleLinkTest()
        {
            //添加Link
            var LinkTable = new LinkTable();
            LinkTable.AddClassLink(new ClassLink("A"));
            LinkTable.AddClassLink(new ClassLink("B"));
            LinkTable.AddClassLink(new ClassLink("C"));
            LinkTable.AddClassLink(new ClassLink("D"));
            LinkTable.AddClassLink(new ClassLink("D"));

            //删除Link
            LinkTable.MinusClassLink("B");
            LinkTable.DisplayClassLink();
        }

        //折半查找
        static void BinarySearch()
        {
            int[] i = { 1, 5, 8, 10, 12, 15, 20, 22 };
            var BinarySearch = new BinarySearch();
            var Inti = BinarySearch.Search(i, 22);
            Console.WriteLine("Position=" + Inti.ToString() + ",Value=" + i[Inti].ToString());
        }

        //快速排序
        static void QuickSort()
        {
            int[] i = { 0, 19, 58, 8, 10, 112, 15, 20, 222 };
            var QuickSort = new QuickSort();
            QuickSort.QKSort(i, 1, i.Length - 1);
            QuickSort.DisplayData(i);
        }

        //二叉树遍历
        static void BinaryTreeList()
        {
            var BinaryTree = new BinaryTree();
            BinaryTree.InitBinaryTree();
            BinaryTree.LastOrderTraverse(@"C:\Tree");
        }

    }
}