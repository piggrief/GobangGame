using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Class_Game;
using Class_Test;
using Class_Judge;
using Gobang;

namespace Class_ChessBoard
{
    public delegate bool GetHandler();
    public class ChessBoard
    {
        private GetHandler getHandler;
        public GetHandler GetHandler
        {
            set { getHandler = value; }
        }
        
        public int[,] board = new int[5, 5]; ///棋盘状态数组 0->空 1->P1 2->P2
        public bool player1 = true;///黑白子落子判断位
        public static int chessboard_size = 5;
        public static int chessboard_wide = 450;
        public Color chessboardcolor1 = ColorTranslator.FromHtml("#A0A0A0");
        public Color chessboardcolor2 = ColorTranslator.FromHtml("#DEDEDE");
        public static int num_step = 0;//第几步
        public static LinkedList<int[,]> link = new LinkedList<int[,]>(); //定义链表
        /// <summary>
        /// 更新棋盘状态
        /// 输入：x_index，y_index -> 落子的横纵坐标，player1->是否是黑子落子
        /// </summary>
        public int Refresh_State(int x_index, int y_index)
        {
            ///检测有没有下标越界
            if (x_index >= chessboard_size || x_index < 0 || y_index >= chessboard_size || y_index < 0)
                return 0;

            ///检测是否在已有棋子位置落子
            if (board[x_index, y_index] != 0)
            {
                return 0;
            }
            else
            {
                if (player1)
                    ///更新棋盘状态
                    board[x_index, y_index] = 1;
                else
                    ///更新棋盘状态
                    board[x_index, y_index] = 2;
                int [,]board_buff2 = new int [chessboard_size,chessboard_size];
                for (int i = 0; i < chessboard_size; i++)
                {
                    for (int j = 0; j < chessboard_size; j++)
                    {
                        board_buff2[i, j] = board[i,j];
                    }
                }
                link.AddLast(board_buff2);//保存棋盘状态到链表
                num_step++;
//                Console.WriteLine("num_step = {0}", num_step);
//                Test.printarr(link.Last.Value, 15, 15, "link_lask:");

                return board[x_index, y_index];
            }
        }

        /// <summary>
        /// 绘图用系列变量
        /// </summary>
        public Bitmap bit = new Bitmap(700, 700);
        public Graphics grp;
        public Image image_White;
        public Image image_Black;
        /// <summary>
        /// 构造函数
        /// 初始化棋盘大小，棋子的图案
        /// </summary>
        public ChessBoard()
        {
            grp = Graphics.FromImage(bit);
            int[,] board = new int[chessboard_size, chessboard_size];
        }
        public void DrawChessBoard()
        {
            grp.Clear(Gobang.Form1.DefaultBackColor);//清空绘画图
           ///绘制棋盘
            for (int i = 1; i < chessboard_wide; i += 60)
                for (int j = 1; j < chessboard_wide; j += 60)
                    grp.FillRectangle(new SolidBrush(chessboardcolor1), i, j, 2 * chessboard_size, 2 * chessboard_size);
            for (int i = 31; i < chessboard_wide; i += 60)
                for (int j = 31; j < chessboard_wide; j += 60)
                    grp.FillRectangle(new SolidBrush(chessboardcolor1), i, j, 2 * chessboard_size, 2 * chessboard_size);
            for (int i = 1; i < chessboard_wide; i += 60)
                for (int j = 31; j < chessboard_wide; j += 60)
                    grp.FillRectangle(new SolidBrush(chessboardcolor2), i, j, 2 * chessboard_size, 2 * chessboard_size);
            for (int i = 31; i < chessboard_wide; i += 60)
                for (int j = 1; j < chessboard_wide; j += 60)
            grp.FillRectangle(new SolidBrush(chessboardcolor2), i, j, 2 * chessboard_size, 2 * chessboard_size);
            grp.DrawLine(new Pen(new SolidBrush(ColorTranslator.FromHtml("#37474F")), 2), new Point(0, 0), new Point(0, 452));
            grp.DrawLine(new Pen(new SolidBrush(ColorTranslator.FromHtml("#37474F")), 2), new Point(0, 0), new Point(452, 0));
            grp.DrawLine(new Pen(new SolidBrush(ColorTranslator.FromHtml("#37474F")), 2), new Point(452, 452), new Point(0, 452));
            grp.DrawLine(new Pen(new SolidBrush(ColorTranslator.FromHtml("#37474F")), 2), new Point(452, 452), new Point(452, 0));
            //for (int i = 1; i <= 30 * chessboard_size + 1; i += 30)
            //{
            //    grp.DrawLine(new Pen(new SolidBrush(ColorTranslator.FromHtml("#00343F")), 2), new Point(i, 1), new Point(i, 30 * chessboard_size + 1));
            //    grp.DrawLine(new Pen(new SolidBrush(ColorTranslator.FromHtml("#00343F")), 2), new Point(1, i), new Point(30 * chessboard_size + 1, i));
            //}
            ///绘制棋子
            for (int x = 0; x < chessboard_size; x++)
            {
                for (int y = 0; y < chessboard_size; y++)
                {
                    if (board[x, y] == 1)
                    {
                        grp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        Brush bush = new SolidBrush(Color.Black);//填充的颜色
                        grp.FillEllipse(bush, (x * 30) + 1, (y * 30) + 1, 30, 30);//画填充椭圆的方法，x坐标、y坐标、宽、高，如果是100，则半径为50
                        //grp.DrawImage(image_Black, (x * 30) + 2, (y * 30) + 2, 26, 26);
                    }
                    else if (board[x, y] == 2)
                    {
                        grp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        Brush bush = new SolidBrush(Color.White);//填充的颜色
                        grp.FillEllipse(bush, (x * 30) + 1, (y * 30) + 1, 30, 30);//画填充椭圆的方法，x坐标、y坐标、宽、高，如果是100，则半径为50
                        Pen pen = new Pen(Color.Black);//画笔颜色     
                        grp.DrawEllipse(pen, (x * 30) + 1, (y * 30) + 1, 30, 30);//画椭圆的方法，x坐标、y坐标、宽、高，如果是100，则半径为50
                        //grp.DrawImage(image_White, (x * 30) + 2, (y * 30) + 2, 26, 26);
                    }
                    ///绘制评分
                    if(getHandler != null)
                    {
                        if(getHandler())
                            grp.DrawString(Game.Value_All[x, y].ToString(), new Font("Arial", 10), new SolidBrush(Color.White), new PointF((x * 30) + 2, (y * 30) + 2));
                    }
                }
            }
        }

        /// <summary>
        /// 重新开始，初始化棋盘状态
        /// </summary>
        public void ReStart()
        {
            for (int i = 0; i < chessboard_size; i++)
            {
                for (int j = 0; j < chessboard_size; j++)
                {
                    board[i, j] = 0;
                }
            }
        }
        public bool flag_retract_stop = false;
        /// <summary>
        /// 悔棋（链表写法）
        /// 可以一直悔棋直到初始棋盘状态
        /// </summary>
        public void retract()
        {
            //Test.printarr(link.Last.Value, 15, 15, "Now:");

            if (link.First != link.Last)
            {
                if (Judge.player_AI)
                {
                    if (num_step > 1)
                    {
                        link.RemoveLast();
                        num_step--;
                    }
                    else
                    {
                        flag_retract_stop = true;
                    }
                    if (num_step > 1)
                    {
                        link.RemoveLast();
                        num_step--;
                    }
                    else
                    {
                        flag_retract_stop = true;
                    }
                }
                else
                {
                    flag_retract_stop = false;
                    link.RemoveLast();
                    num_step--;
                    link.RemoveLast();
                    num_step--;
                }
            }
            else 
            {
                flag_retract_stop = true;
            }
            //Test.printarr(link.Last.Value, 15, 15, "Last:");
            for (int i = 0; i < chessboard_size; i++)
            {
                for (int j = 0; j < chessboard_size; j++)
                {
                    board[i, j] = link.Last.Value[i, j];
                }
            } 
            DrawChessBoard();///棋盘绘制，需要更改
        }
    }
}
