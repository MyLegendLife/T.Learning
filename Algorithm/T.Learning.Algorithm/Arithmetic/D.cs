using System;

namespace T.Learning.Arithmetic.Arithmetic
{
    //单链表操作
    public class ClassLink
    {
        //成员变量
        private string StrValue;
        private ClassLink Next = null;

        //构造函数1
        public ClassLink(string value)
        {
            this.StrValue = value;
            this.Next = null;
        }

        //构造函数2
        public ClassLink(string value, ClassLink next)
        {
            this.StrValue = value ?? throw new ArgumentNullException(nameof(value));
            this.Next = next;
        }

        /// <summary>
        /// 返回数据值
        /// </summary>
        public string DataValue
        {
            get
            {
                return StrValue;
            }

            set
            {
                StrValue = value;
            }
        }

        /// <summary>
        /// 返回连接值
        /// </summary>
        public ClassLink NextLinkValue
        {
            get
            {
                return Next;
            }

            set
            {
                Next = value;
            }
        }

    }

    public class LinkTable
    {
        public int IntCnt = 0;
        public ClassLink Head = null;
        public ClassLink Last = null;


        public void DisplayClassLink()
        {
            var DisplayHead = Head;
            while (DisplayHead != null)
            {
                Console.WriteLine("Value=" + DisplayHead.DataValue.ToString());
                DisplayHead = DisplayHead.NextLinkValue;
            }
        }

        public bool AddClassLink(ClassLink ClsLink)
        {
            var BlnRet = false;
            if (ClsLink != null)
            {
                if (IntCnt == 0)
                {
                    Head = ClsLink;
                    Last = ClsLink;
                }
                else
                {
                    Last.NextLinkValue = ClsLink;
                    this.Last = ClsLink;
                }
                IntCnt++;
                BlnRet = true;
            }
            else
            {
                BlnRet = false;
            }
            return BlnRet;
        }

        public void MinusClassLink(String StrValue)
        {
            if (StrValue.Trim() != "")
            {
                var NewHead = Head;
                ClassLink NewNext = null;
                while (NewHead != null)
                {
                    NewNext = NewHead.NextLinkValue;
                    if (NewHead == Head && NewHead.DataValue.Trim() == StrValue.Trim())
                    {
                        NewHead = NewNext;
                        Head = NewNext;
                    }
                    else if (NewNext != null && NewNext.DataValue.Trim() == StrValue.Trim())
                    {
                        NewHead.NextLinkValue = NewNext.NextLinkValue;
                    }
                    else
                    {
                        NewHead = NewHead.NextLinkValue;
                    }
                }
            }
        }

    }

}