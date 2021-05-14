using System;
using System.IO;

namespace T.Learning.Arithmetic.Arithmetic
{
    public class BinaryTree
    {

        public void InitBinaryTree()
        {
            if (!Directory.Exists(@"C:\Tree"))
            {
                Directory.CreateDirectory(@"C:\Tree");
            }

            if (!Directory.Exists(@"C:\Tree\a"))
            {
                Directory.CreateDirectory(@"C:\Tree\a");
            }

            if (!Directory.Exists(@"C:\Tree\a\a1"))
            {
                Directory.CreateDirectory(@"C:\Tree\a\a1");
            }

            if (!Directory.Exists(@"C:\Tree\a\a2"))
            {
                Directory.CreateDirectory(@"C:\Tree\a\a2");
            }

            if (!Directory.Exists(@"C:\Tree\b"))
            {
                Directory.CreateDirectory(@"C:\Tree\b");
            }

            if (!Directory.Exists(@"C:\Tree\b\c"))
            {
                Directory.CreateDirectory(@"C:\Tree\b\c");
            }

            if (!Directory.Exists(@"C:\Tree\b\c\e"))
            {
                Directory.CreateDirectory(@"C:\Tree\b\c\e");
            }

            if (!Directory.Exists(@"C:\Tree\b\d"))
            {
                Directory.CreateDirectory(@"C:\Tree\b\d");
            }

        }


        /// <summary>
        /// 先根遍历二叉树
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public void PreOrderTraverse(string path)
        {
            if (path.Trim() != "")
            {
                var directory = new DirectoryInfo(path);
                if (directory.Exists)
                {
                    Console.WriteLine(directory.FullName);
                    var directories = directory.GetDirectories();
                    if (directories.Length > 0)
                        PreOrderTraverse(directories[0].FullName);
                    if (directories.Length > 1)
                        PreOrderTraverse(directories[1].FullName);

                }
            }
        }

        /// <summary>
        /// 中根遍历二叉树
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public void MidOrderTraverse(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (path.Trim() != "")
            {
                var directory = new DirectoryInfo(path);
                if (directory.Exists)
                {
                    var directories = directory.GetDirectories();
                    if (directories.Length > 0)
                        MidOrderTraverse(directories[0].FullName);

                    Console.WriteLine(directory.FullName);

                    if (directories.Length > 1)
                        MidOrderTraverse(directories[1].FullName);
                }
            }
        }

        /// <summary>
        /// 后根遍历二叉树
        /// </summary>
        /// <param name="StrPath"></param>
        /// <returns></returns>
        public void LastOrderTraverse(string path)
        {
            if (path.Trim() != "")
            {
                var directory = new DirectoryInfo(path);
                if (directory.Exists)
                {

                    var directories = directory.GetDirectories();
                    if (directories.Length > 0)
                        LastOrderTraverse(directories[0].FullName);

                    if (directories.Length > 1)
                        LastOrderTraverse(directories[1].FullName);

                    Console.WriteLine(directory.FullName);
                }
            }
        }
    }
}