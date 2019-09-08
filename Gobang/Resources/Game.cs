using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Class_Judge;
using Class_ChessBoard;
using Class_Test;

namespace Class_Game
{
    /// <summary>
    /// Game类-博弈类
    /// 用来封装一系列博弈算法及AI算法的类
    /// </summary>
    class Game
    {
        Judge JD_buff = new Judge();
        public ChessBoard CB_buff = new ChessBoard();
        public bool AI_player = false;//默认AI是白子
        public static int x_decision = 0;
        public static int y_decision = 0;
        public static int cut_depth = 14;
        public static int num_points = 0;
        public static int[,] Point_buff = new int[ChessBoard.chessboard_size * ChessBoard.chessboard_size, 2];
        public Game(ChessBoard CB1)
        {
            CB_buff.board = new int[ChessBoard.chessboard_size, ChessBoard.chessboard_size];
            for (int i = 0; i < ChessBoard.chessboard_size; i++)
            {
                for (int j = 0; j < ChessBoard.chessboard_size; j++)
                {
                    CB_buff.board[i, j] = CB1.board[i, j];
                }
            }
            CB_buff.player1 = CB1.player1;
        }
        //自己棋盘评分表
        public static long[,] Value_ziji = new long[ChessBoard.chessboard_size, ChessBoard.chessboard_size];
        public static long[,] Value_duishou = new long[ChessBoard.chessboard_size, ChessBoard.chessboard_size];
        public static long[,] Value_All = new long[ChessBoard.chessboard_size, ChessBoard.chessboard_size];

        /// <summary>
        /// 评估函数，用来给决策打分的，制定棋盘评分表的。
        /// 输入：当前棋盘状态：board，评估玩家：player1
        /// 输出：无
        /// </summary>
        public long Evalution(int[,] board, bool player1)
        {
            long Max = 0;
            ///保存原先棋盘状态
            Judge JD = new Judge();
            int board_save = 0;

            int[] state1_save = JD.player1_state;
            int[] state2_save = JD.player2_state;
            long[,] Value_buff = new long[ChessBoard.chessboard_size, ChessBoard.chessboard_size];


            ///遍历棋盘
            for (int i = 0; i < ChessBoard.chessboard_size; i++)
            {
                for (int j = 0; j < ChessBoard.chessboard_size; j++)
                {
                    ///只检测未落子的点
                    if (board[i, j] == 0)
                    {
                        ///保存原先状态
                        board_save = board[i, j];
                        ///假设在该点落子
                        if (player1)
                            board[i, j] = 1;
                        else
                            board[i, j] = 2;

                        ///判断棋形
                        JD.Check_State(i, j, player1, board);

                        ///计算该点分数
                        int[] state_buff = new int[4];
                        if (player1)
                            state_buff = JD.player1_state;
                        else
                            state_buff = JD.player2_state;

                        Value_buff[i, j] = 0;
                        for (int k = 0; k < 4; k++)///四个方向都检索一遍
                        {
                            switch (state_buff[k])
                            {
                                case 1:///死二-5
                                    Value_buff[i, j] += -5;
                                    break;
                                case 2:///死三-5
                                    Value_buff[i, j] += -5;
                                    break;
                                case 3:///死四-5
                                    Value_buff[i, j] += -5;
                                    break;
                                case 4:///眠二+3
                                    Value_buff[i, j] += 3;
                                    break;
                                case 5:///活二+5
                                    Value_buff[i, j] += 5;
                                    break;
                                case 6:///眠三+50
                                    Value_buff[i, j] += 50;
                                    break;
                                case 7:///活三+200
                                    Value_buff[i, j] += 200;
                                    break;
                                case 8:///冲四分值未知
                                    Value_buff[i, j] += 0;
                                    break;
                                case 9:///活四+10000
                                    Value_buff[i, j] += 10000;
                                    break;
                                case 10:///长连+10W
                                    Value_buff[i, j] += 100000;
                                    break;
                                case 11://五连+10W
                                    Value_buff[i, j] += 100000;
                                    break;
                                default://一着0分
                                    Value_buff[i, j] += 0;
                                    break;
                            }
                        }

                        ///还原状态
                        board[i, j] = board_save;
                        JD.player1_state = state1_save;
                        JD.player2_state = state2_save;
                        ///组合棋型加分
                        ///叠加上位置加分
                        Value_buff[i, j] += position[i, j];

                        //if (Value_buff[i, j] > Max)
                        //    Max = Value_buff[i, j];
                    }
                }
            }
            Value_ziji = Value_buff;
            return Max;
        }
        /// <summary>
        /// 数组赋值函数_int型
        /// </summary>
        public void arr_assign(int[,] arr1, int[,] arr2, int size_c, int size_r)
        {
            for (int i = 0; i < size_c; i++)
            {
                for (int j = 0; j < size_r; j++)
                {
                    arr1[i, j] = arr2[i, j];
                }
            }
        }
        /// <summary>
        /// 数组赋值函数_long型
        /// </summary>
        public void arr_assign_long(long[,] arr1, long[,] arr2, int size_c, int size_r)
        {
            for (int i = 0; i < size_c; i++)
            {
                for (int j = 0; j < size_r; j++)
                {
                    arr1[i, j] = arr2[i, j];
                }
            }
        }
        /// <summary>
        /// ab剪枝，negaMax
        /// </summary>
        public long negaABpuring(int[,] board, bool player, int depth, long alpha, long beta)
        {
            if (depth == 0)
            {
                long v = 0;
                Evalution(board, player);

                long[,] Value1 = new long[ChessBoard.chessboard_size, ChessBoard.chessboard_size];
                arr_assign_long(Value1, Game.Value_ziji, ChessBoard.chessboard_size, ChessBoard.chessboard_size);
                Evalution(board, !player);

                arr_assign_long(Game.Value_duishou, Game.Value_ziji, ChessBoard.chessboard_size, ChessBoard.chessboard_size);
                arr_assign_long(Game.Value_ziji, Value1, ChessBoard.chessboard_size, ChessBoard.chessboard_size);

                for (int i = 0; i < ChessBoard.chessboard_size; i++)
                {
                    for (int j = 0; j < ChessBoard.chessboard_size; j++)
                    {
                        Game.Value_All[i, j] = (1 * Game.Value_ziji[i, j] + 1 * Game.Value_duishou[i, j]) / 2;
                    }
                }
                long max = 0;
                ///找综合评分表最大的位置落子
                for (int i = 0; i < ChessBoard.chessboard_size; i++)
                {
                    for (int j = 0; j < ChessBoard.chessboard_size; j++)
                    {
                        if (Game.Value_All[i, j] > max)
                        {
                            max = Game.Value_All[i, j];
                        }
                    }
                }
                ///评估函数值赋给v
                v = max;
                return v;
            }
            else
            {
                gen(board, player);

                int[,] Point_use = new int[Game.num_points, 2];
                int num_points_buff = Game.num_points;
                arr_assign(Point_use, Game.Point_buff, num_points_buff, 2);
                int[,] board_save = new int[ChessBoard.chessboard_size, ChessBoard.chessboard_size];
                arr_assign(board_save, board, ChessBoard.chessboard_size, ChessBoard.chessboard_size);

                for (int i = 0; i < num_points_buff; i++)
                {
                    int chess = 0;
                    if (player)
                        chess = 1;
                    else
                        chess = 2;
                    ///尝试下一个子
                    board_save[Point_use[i, 0], Point_use[i, 1]] = chess;
                    long value = negaABpuring(board_save, !player, depth - 1, -alpha, -beta);
                    if (value > alpha)
                    {
                        alpha = value;
                        if (depth == cut_depth)
                        {
                            x_decision = Point_use[i, 0];
                            y_decision = Point_use[i, 1];
                        }
                    }
                    if (alpha >= beta)
                        break;
                    ///清除落子
                    board_save[Point_use[i, 0], Point_use[i, 1]] = 0;
                }
                return alpha;
            }
        }

        /// <summary>
        /// ab剪枝
        /// </summary>
        public long maxminsearch(int[,] board, int depth)
        {
            long best = -100000;
            gen(board, false);

            int[,] Point_use = new int[Game.num_points, 2];
            int num_points_buff = Game.num_points;
            arr_assign(Point_use, Game.Point_buff, num_points_buff, 2);
            int[,] board_save = new int[ChessBoard.chessboard_size, ChessBoard.chessboard_size];
            arr_assign(board_save, board, ChessBoard.chessboard_size, ChessBoard.chessboard_size);

            Test.printarr(board_save, ChessBoard.chessboard_size, ChessBoard.chessboard_size, "board:");
            Test.print_Point(Game.Point_buff, board, Game.num_points, "Point:");

            for (int i = 0; i < num_points_buff; i++)
            {
                ///尝试下一个子
                board_save[Point_use[i, 0], Point_use[i, 1]] = 2;

                long v = minsearch_cut(board_save, depth - 1, -10000, 10000);//min();//找最大值

                if (v > best)///找到了一个更好的
                {
                    best = v;
                    x_decision = Point_use[i, 0];
                    y_decision = Point_use[i, 1];
                }
                ///清除落子
                board_save[Point_use[i, 0], Point_use[i, 1]] = 0;
            }
            return 0;
        }

        public long minsearch_cut(int[,] board, int depth, long alpha, long beta)
        {
            long v = 0;
            //total++;
            if (depth <= 0)
            {
                Evalution(board, true);

                long[,] Value1 = new long[ChessBoard.chessboard_size, ChessBoard.chessboard_size];
                arr_assign_long(Value1, Game.Value_ziji, ChessBoard.chessboard_size, ChessBoard.chessboard_size);
                Evalution(board, false);

                arr_assign_long(Game.Value_duishou, Game.Value_ziji, ChessBoard.chessboard_size, ChessBoard.chessboard_size);
                arr_assign_long(Game.Value_ziji, Value1, ChessBoard.chessboard_size, ChessBoard.chessboard_size);

                for (int i = 0; i < ChessBoard.chessboard_size; i++)
                {
                    for (int j = 0; j < ChessBoard.chessboard_size; j++)
                    {
                        Game.Value_All[i, j] = (1 * Game.Value_ziji[i, j] + 1 * Game.Value_duishou[i, j]) / 2;
                    }
                }
                long max = 0;
                ///找综合评分表最大的位置落子
                for (int i = 0; i < ChessBoard.chessboard_size; i++)
                {
                    for (int j = 0; j < ChessBoard.chessboard_size; j++)
                    {
                        if (Game.Value_All[i, j] > max)
                        {
                            max = Game.Value_All[i, j];
                        }
                    }
                }
                ///评估函数值赋给v
                v = max;
                return v;
            }
            else if (false)///获胜
                return v;

            long best = 100000;

            gen(board, true);
            int[,] Point_use = new int[Game.num_points, 2];
            int num_points_buff = Game.num_points;
            arr_assign(Point_use, Game.Point_buff, num_points_buff, 2);

            for (int i = 0; i < num_points_buff; i++)
            {
                ///落子
                board[Point_use[i, 0], Point_use[i, 1]] = 1;

                long alpha_buff = 0;
                if (best < alpha)
                    alpha_buff = best;
                else
                    alpha_buff = alpha;

                v = maxsearch_cut(board, depth - 1, alpha_buff, beta);//max();
                if (v < best)
                    best = v;
                board[Point_use[i, 0], Point_use[i, 1]] = 0;
                if (v < beta)
                {
                    //ABcut++;
                    break;
                }

            }
            return best;
        }
        public long maxsearch_cut(int[,] board, int depth, long alpha, long beta)
        {
            long v = 0;
            //total++;
            if (depth <= 0)
            {
                Evalution(board, false);

                long[,] Value1 = new long[ChessBoard.chessboard_size, ChessBoard.chessboard_size];
                arr_assign_long(Value1, Game.Value_ziji, ChessBoard.chessboard_size, ChessBoard.chessboard_size);
                Evalution(board, true);

                arr_assign_long(Game.Value_duishou, Game.Value_ziji, ChessBoard.chessboard_size, ChessBoard.chessboard_size);
                arr_assign_long(Game.Value_ziji, Value1, ChessBoard.chessboard_size, ChessBoard.chessboard_size);

                for (int i = 0; i < ChessBoard.chessboard_size; i++)
                {
                    for (int j = 0; j < ChessBoard.chessboard_size; j++)
                    {
                        Game.Value_All[i, j] = (1 * Game.Value_ziji[i, j] + 1 * Game.Value_duishou[i, j]) / 2;
                    }
                }
                long max = 0;
                ///找综合评分表最大的位置落子
                for (int i = 0; i < ChessBoard.chessboard_size; i++)
                {
                    for (int j = 0; j < ChessBoard.chessboard_size; j++)
                    {
                        if (Game.Value_All[i, j] > max)
                        {
                            max = Game.Value_All[i, j];
                        }
                    }
                }
                ///评估函数值赋给v
                v = max;
                ///评估函数值赋给v
                return v;
            }
            else if (false)///获胜
                return v;

            long best = -10000;

            gen(board, false);
            int[,] Point_use = new int[Game.num_points, 2];
            int num_points_buff = Game.num_points;
            arr_assign(Point_use, Game.Point_buff, num_points_buff, 2);

            for (int i = 0; i < num_points_buff; i++)
            {
                ///落子
                board[Point_use[i, 0], Point_use[i, 1]] = 2;

                long beta_buff = 0;
                if (best > beta)
                    beta_buff = best;
                else
                    beta_buff = beta;

                v = minsearch_cut(board, depth - 1, alpha, beta_buff);//min();
                if (v > best)
                    best = v;
                board[Point_use[i, 0], Point_use[i, 1]] = 0;
                if (v > alpha)
                {
                    //ABcut++;
                    break;
                }
            }
            return best;
        }
        /// <summary>
        /// 启发式节点选择函数
        /// </summary>
        public void gen(int[,] board, bool player1)
        {
            int[,] Point = new int[ChessBoard.chessboard_size * ChessBoard.chessboard_size, 2];
            int num_neighbors = 0;
            int num_point = 0;

            for (int i = 0; i < ChessBoard.chessboard_size; i++)
            {
                for (int j = 0; j < ChessBoard.chessboard_size; j++)
                {
                    num_neighbors = 0;
                    if (board[i, j] == 0)
                    {
                        for (int ii = i - 2; ii <= i + 2; ii++)
                        {
                            if (ii < 0 || ii >= ChessBoard.chessboard_size)
                                continue;
                            for (int jj = j - 2; jj <= j + 2; jj++)
                            {
                                if (jj < 0 || jj >= ChessBoard.chessboard_size)
                                    continue;
                                if (board[ii, jj] != 0)
                                    num_neighbors++;
                            }
                        }
                        if (num_neighbors > 0)//有邻居的点
                        {
                            Point[num_point, 0] = i;
                            Point[num_point, 1] = j;
                            num_point++;
                        }
                    }
                }
            }
            for (int i = 0; i < num_point; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Game.Point_buff[i, j] = Point[i, j];
                }
            }
            num_points = num_point;
            ///按评估矩阵排序
            long[] Value_Point = new long[num_point];

            ///评估
            ///保存原先棋盘状态
            Judge JD = new Judge();
            int board_save = 0;

            int[] state1_save = JD.player1_state;
            int[] state2_save = JD.player2_state;
            long[] Value_buff = new long[num_point];

            ///遍历棋盘
            for (int i = 0; i < num_point; i++)
            {
                ///只检测未落子的点
                if (board[Game.Point_buff[i, 0], Game.Point_buff[i, 1]] == 0)
                {
                    ///保存原先状态
                    board_save = board[Game.Point_buff[i, 0], Game.Point_buff[i, 1]];
                    ///假设在该点落子
                    if (player1)
                        board[Game.Point_buff[i, 0], Game.Point_buff[i, 1]] = 1;
                    else
                        board[Game.Point_buff[i, 0], Game.Point_buff[i, 1]] = 2;

                    ///判断棋形
                    JD.Check_State(Game.Point_buff[i, 0], Game.Point_buff[i, 1], player1, board);

                    ///计算该点分数
                    int[] state_buff = new int[4];
                    if (player1)
                        state_buff = JD.player1_state;
                    else
                        state_buff = JD.player2_state;
                    Value_buff[i] = 0;
                    for (int k = 0; k < 4; k++)///四个方向都检索一遍
                    {
                        switch (state_buff[k])
                        {
                            case 1:///死二-5
                                Value_buff[i] += -5;
                                break;
                            case 2:///死三-5
                                Value_buff[i] += -5;
                                break;
                            case 3:///死四-5
                                Value_buff[i] += -5;
                                break;
                            case 4:///眠二+3
                                Value_buff[i] += 3;
                                break;
                            case 5:///活二+5
                                Value_buff[i] += 5;
                                break;
                            case 6:///眠三+50
                                Value_buff[i] += 50;
                                break;
                            case 7:///活三+200
                                Value_buff[i] += 200;
                                break;
                            case 8:///冲四分值未知
                                Value_buff[i] += 0;
                                break;
                            case 9:///活四+10000
                                Value_buff[i] += 10000;
                                break;
                            case 10:///长连+10W
                                Value_buff[i] += 100000;
                                break;
                            case 11://五连+10W
                                Value_buff[i] += 100000;
                                break;
                            default://一着0分
                                Value_buff[i] += 0;
                                break;
                        }
                    }
                    ///还原状态
                    board[Game.Point_buff[i, 0], Game.Point_buff[i, 1]] = board_save;
                    JD.player1_state = state1_save;
                    JD.player2_state = state2_save;
                    ///叠加上位置加分
                    Value_buff[i] += position[Game.Point_buff[i, 0], Game.Point_buff[i, 1]];
                }
            }

            Value_Point = Value_buff;
            ///排序
            long temp = 0;
            int temp2 = 0;

            for (int i = 0; i < num_point - 1; i++)
            {
                for (int j = 0; j < num_point - i - 1; j++)
                {
                    if (Value_Point[j] < Value_Point[j + 1])
                    {
                        temp = Value_Point[j + 1];
                        Value_Point[j + 1] = Value_Point[j];
                        Value_Point[j] = temp;
                        temp2 = Game.Point_buff[j + 1, 0];
                        Game.Point_buff[j + 1, 0] = Game.Point_buff[j, 0];
                        Game.Point_buff[j, 0] = temp2;
                        temp2 = Game.Point_buff[j + 1, 1];
                        Game.Point_buff[j + 1, 1] = Game.Point_buff[j, 1];
                        Game.Point_buff[j, 1] = temp2;

                    }
                }
            }
        }
        private static int[,] position = new int[15, 15];

        public static void position_create(int s)
        {
            position = new int[s, s];
            for (int k = 1; k <= s / 2; k++)
            {
                for (int i = k; i < s - k; i++)
                {
                    for (int j = k; j < s - k; j++)
                    {
                        position[i,j] += 1;
                    }
                }
            }
        }
    }
}
