using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Class_Test
{
    class Test
    {
        [DllImport("kernel32.dll")]
        public static extern Boolean AllocConsole();
        [DllImport("kernel32.dll")]
        public static extern Boolean FreeConsole();

        /// <summary>
        /// 打印数组
        /// </summary> 
        public static void printarr(int[,] arr, int c, int r,string title)
        {
            string str_buff = title + Environment.NewLine;
            for (int i = 0; i < c; i++)
            {
                for (int j = 0; j < r; j++)
                {
                    str_buff += arr[i, j].ToString() + " ";
                }
                str_buff += Environment.NewLine;
            }
            Console.WriteLine(str_buff);
        }
        public static void printarr_long2(long[,] arr, int c, int r, string title)
        {
            string str_buff = title + Environment.NewLine;
            for (int i = 0; i < c; i++)
            {
                for (int j = 0; j < r; j++)
                {
                    str_buff += arr[i, j].ToString() + " ";
                }
                str_buff += Environment.NewLine;
            }
            Console.WriteLine(str_buff);
        }
        public static void printarr_1(int[] arr, int s, string title)
        {
            string str_buff = title + Environment.NewLine;
            for (int i = 0; i < s; i++)
            {
                str_buff += arr[i].ToString() + " ";
            }
            str_buff += Environment.NewLine;
            Console.WriteLine(str_buff);
        }
        public static void printarr_long1(long[] arr, int s, string title)
        {
            string str_buff = title + Environment.NewLine;
            for (int i = 0; i < s; i++)
            {
                str_buff += arr[i].ToString() + " ";
            }
            str_buff += Environment.NewLine;
            Console.WriteLine(str_buff);
        }

        public static void print_Point(int[,] Point,int[,] arr, int s, string title)
        {
            string str_buff = title + Environment.NewLine;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int k = 0;
                    for (k = 0; k < s; k++)
                    {
                        if (i == Point[k, 0] && j == Point[k, 1])
                        { 
                            str_buff += "x" + " ";
                            break;
                        }
                    }
                    if(k==s)
                        str_buff += "0" + " ";
                }
                str_buff += Environment.NewLine;
            }
            Console.WriteLine(str_buff);
        }

    }
}
