using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Class_ChessBoard;
using System.Text.RegularExpressions;//使用正则表达式来做状态判断检测

namespace Class_Judge
{
    public class Judge
    {
        public static bool player_AI = false;
        ///数组下标表示：0->行状态 1->列状态 2->主斜状态 3->副斜状态
        ///数组的值为：0->一着状态 1->死二状态 2->死三状态 3->死四状态 4->眠二状态 5->活二状态  
        ///6->眠三状态 7->活三状态 8->冲四状态 9->活四状态 10->长连状态 11->五连状态
        public int[] player1_state = new int[4] { 0, 0, 0, 0 };
        /// 0->一着状态 1->活三状态 2->活四状态 3->冲四状态 4->五连状态
        public int[] player2_state = new int[4] { 0, 0, 0, 0 };
        ///玩家的胜负状态
        public string player1_situation = "胜负未定";
        public string player2_situation = "胜负未定";
        ///0->空白,1->同样颜色的子 2->不能落子 .->任意子
        ///各种棋形的状态表，正则表达式写法，序号的对应关系在上面 

        public static Regex[] state_h1  = new Regex[2] { new Regex("...2112.."), new Regex("..2112...")};
        public static Regex[] state_h2  = new Regex[3] { new Regex(".21112..."), new Regex("..21112.."), new Regex("...21112.") };
        public static Regex[] state_h3  = new Regex[4] { new Regex("211112..."), new Regex(".211112.."), new Regex("..211112."), new Regex("...211112") };
        public static Regex[] state_h4  = new Regex[12]{ new Regex("000112..."), new Regex(".000112.."), new Regex("001012..."), new Regex("..001012."), new Regex("010012..."), new Regex("...010012"), new Regex("10001...."), new Regex("....10001"), new Regex("2010102.."), new Regex("..2010102"), new Regex(".2011002."), new Regex("..2011002")};
        public static Regex[] state_h5  = new Regex[6] { new Regex(".00110..."), new Regex("..00110.."), new Regex(".01010..."), new Regex("...01010."), new Regex("...010010"), new Regex("010010...") };
        public static Regex[] state_h6  = new Regex[18]{ new Regex("001112..."), new Regex(".001112.."), new Regex("..001112."), new Regex("...010112"), new Regex(".010112.."), new Regex("010112..."), new Regex("...011012"),new Regex("..011012."), new Regex("011012..."), new Regex("....10011"), new Regex(".10011..."), new Regex("10011...."), new Regex("....10101"), new Regex("..10101.."), new Regex("10101...."), new Regex("..2011102"), new Regex(".2011102."), new Regex("2011102..")};
        public static Regex[] state_h7  = new Regex[12]{ new Regex("...010110"), new Regex("...011010"), new Regex("...011100"), new Regex("..001110."), new Regex("..011010."), new Regex("..011100."), new Regex(".001110.."), new Regex(".010110.."), new Regex(".011100.."), new Regex("001110..."), new Regex("010110..."), new Regex("011010...") };
        public static Regex[] state_h8  = new Regex[12]{ new Regex("....011112."), new Regex("...011112.."), new Regex("..011112..."), new Regex(".011112...."), new Regex("....0101110"), new Regex("..0101110.."), new Regex(".0101110..."), new Regex("0101110...."), new Regex("....0110110"), new Regex("...0110110."), new Regex(".0110110..."), new Regex("0110110....") };
        public static Regex[] state_h9  = new Regex[4] { new Regex("...011110"), new Regex("..011110."), new Regex(".011110.."), new Regex("011110...") };
        public static Regex[] state_h10 = new Regex[4] { new Regex("...111111"), new Regex("..111111."), new Regex(".111111.."), new Regex("111111...") };
        public static Regex[] state_h11 = new Regex[5] { new Regex("....11111"), new Regex("...11111."), new Regex("..11111.."), new Regex(".11111..."), new Regex("11111....") };
        

        public string state_now = "";//当前八子状态
        private string state_pre = "";//用于冲四判断
        private string state_behind = "";//用于冲四判断

        public bool flag_victory_1 = false;
        public bool flag_victory_2 = false;
        /// <summary>
        /// 检测状态列表(正则表达式写法)
        /// 状态字符串state_now，状态表正则表达式字符串Rx，状态Rx数组的大小size
        /// </summary>
        public bool Check_State_List(string state_now, Regex[] Rx, int size)
        {
            for (int i = 0; i < size; i++)
            {
                if (Rx[i].IsMatch(state_now))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 检测某个方向是哪种状态
        /// 输入：index:0->检测行 1->检测列 2->主斜 3->副斜
        /// </summary>
        void Check_Type(string state_n, int index, bool player1,string pre,string behind)
        {
            int state_buff = 0;
            string state_n_chongsi = pre+state_n+behind;

            ///查表检测状态从11检测到1
            if (Check_State_List(state_n, state_h11, 5))
                state_buff = 11;
            else if (Check_State_List(state_n, state_h10, 4))
                state_buff = 10;
            else if (Check_State_List(state_n, state_h9, 4))
                state_buff = 9;
            else if (Check_State_List(state_n_chongsi, state_h8, 12))
                state_buff = 8;
            else if (Check_State_List(state_n, state_h7, 12))
                state_buff = 7;
            else if (Check_State_List(state_n, state_h6, 18))
                state_buff = 6;
            else if (Check_State_List(state_n, state_h5, 6))
                state_buff = 5;
            else if (Check_State_List(state_n, state_h4, 12))
                state_buff = 4;
            else if (Check_State_List(state_n, state_h3, 4))
                state_buff = 3;
            else if (Check_State_List(state_n, state_h2, 3))
                state_buff = 2;
            else if (Check_State_List(state_n, state_h1, 2))
                state_buff = 1;
            else
                state_buff = 0;

            if (player1)
                player1_state[index] = state_buff;
            else
                player2_state[index] = state_buff;
        }
        /// <summary>
        /// 检测落下某子后某玩家的着棋状态
        /// 输入：x,y:落子索引，player1->P1还是P2的标志
        /// </summary>
        public void Check_State(int x, int y, bool player1, int[,] board)
        {
            int judge_same = 0;
            if (player1)
                judge_same = 1;
            else
                judge_same = 2;

            ///列扫
            state_now = "";
            ///更新状态表
            for (int i = y - 4, j = 0; j < 9; i++, j++)
            {
                if (i < 0 || i >= ChessBoard.chessboard_size)
                    state_now += "2";
                else if (board[x, i] == 0)
                    state_now += "0";
                else if (board[x, i] == judge_same)
                    state_now += "1";
                else
                    state_now += "2";
            }
            ///补扫两个，用于冲四检测
            if (y - 5 < 0 || y - 5 >= ChessBoard.chessboard_size)
                state_pre = "2";
            else if (board[x, y - 5] == 0)
                state_pre = "0";
            else if (board[x, y - 5] == judge_same)
                state_pre = "1";
            else
                state_pre = "2";

            if (y + 5 < 0 || y + 5 >= ChessBoard.chessboard_size)
                state_behind = "2";
            else if (board[x, y + 5] == 0)
                state_behind = "0";
            else if (board[x, y + 5] == judge_same)
                state_behind = "1";
            else
                state_behind = "2";
            ///检测状态，一着、活三、活四、五连
            Check_Type(state_now, 1, player1,state_pre,state_behind);
            
            ///行扫
            state_now = "";
            ///更新状态表
            for (int i = x - 4, j = 0; j < 9; i++, j++)
            {
                if (i < 0 || i >= ChessBoard.chessboard_size)
                    state_now += "2";
                else if (board[i, y] == 0)
                    state_now += "0";
                else if (board[i, y] == judge_same)
                    state_now += "1";
                else
                    state_now += "2";
            }
            ///补扫两个，用于冲四检测
            if (x - 5 < 0 || x - 5 >= ChessBoard.chessboard_size)
                state_pre = "2";
            else if (board[x - 5, y] == 0)
                state_pre = "0";
            else if (board[x - 5, y] == judge_same)
                state_pre = "1";
            else
                state_pre = "2";
            if (x + 5 < 0 || x + 5 >= ChessBoard.chessboard_size)
                state_behind = "2";
            else if (board[x + 5, y] == 0)
                state_behind = "0";
            else if (board[x + 5, y] == judge_same)
                state_behind = "1";
            else
                state_behind = "2";
            ///检测状态，一着、活三、活四、五连
            Check_Type(state_now, 0, player1, state_pre, state_behind);

            ///↖↘扫(主斜)
            state_now = "";
            ///更新状态表
            for (int i = -4, j = 0; j < 9; i++, j++)
            {
                if (x + i < 0 || x + i >= ChessBoard.chessboard_size || y + i < 0 || y + i >= ChessBoard.chessboard_size)
                    state_now += "2";
                else if (board[x + i, y + i] == 0)
                    state_now += "0";
                else if (board[x + i, y + i] == judge_same)
                    state_now += "1";
                else
                    state_now += "2";
            }
            ///补扫两个，用于冲四检测
            if (x - 5 < 0 || x - 5 >= ChessBoard.chessboard_size || y - 5 < 0 || y - 5 >= ChessBoard.chessboard_size)
                state_pre = "2";
            else if (board[x - 5, y - 5] == 0)
                state_pre = "0";
            else if (board[x - 5, y - 5] == judge_same)
                state_pre = "1";
            else
                state_pre = "2";
            if (x + 5 < 0 || x + 5 >= ChessBoard.chessboard_size || y + 5 < 0 || y + 5 >= ChessBoard.chessboard_size)
                state_behind = "2";
            else if (board[x + 5, y + 5] == 0)
                state_behind = "0";
            else if (board[x + 5, y + 5] == judge_same)
                state_behind = "1";
            else
                state_behind = "2";
            ///检测状态，一着、活三、活四、五连
            Check_Type(state_now, 2, player1, state_pre, state_behind);

            ///↙↗扫(副斜)
            state_now = "";
            ///更新状态表
            for (int i = -4, j = 0; j < 9; i++, j++)
            {
                if (x - i < 0 || x - i >= ChessBoard.chessboard_size || y + i < 0 || y + i >= ChessBoard.chessboard_size)
                    state_now += "2";
                else if (board[x - i, y + i] == 0)
                    state_now += "0";
                else if (board[x - i, y + i] == judge_same)
                    state_now += "1";
                else
                    state_now += "2";
            }
            ///补扫两个，用于冲四检测
            if (x + 5 < 0 || x + 5 >= ChessBoard.chessboard_size || y - 5 < 0 || y - 5 >= ChessBoard.chessboard_size)
                state_pre = "2";
            else if (board[x + 5, y - 5] == 0)
                state_pre = "0";
            else if (board[x + 5, y - 5] == judge_same)
                state_pre = "1";
            else
                state_pre = "2";
            if (x - 5 < 0 || x - 5 >= ChessBoard.chessboard_size || y + 5 < 0 || y + 5 >= ChessBoard.chessboard_size)
                state_behind = "2";
            else if (board[x - 5, y + 5] == 0)
                state_behind = "0";
            else if (board[x - 5, y + 5] == judge_same)
                state_behind = "1";
            else
                state_behind = "2";
            ///检测状态，一着、活三、活四、五连
            Check_Type(state_now, 3, player1, state_pre, state_behind);
        }

        public bool Judge_victory(bool player1,ChessBoard  CB)
        {
            ///判断输赢
            ///检测禁手
            if (player1)///只有黑棋禁手
            {
                ///检测四-四禁手
                int num_count = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (player1_state[i] == 9)
                    {
                        num_count++;
                    }
                    if (num_count >= 2)
                    {
                        flag_victory_2 = true;
                        player1_situation = "四-四禁手";
                        player2_situation = "获胜";
                        break;
                    }
                }
                ///检测三-三禁手
                num_count = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (player1_state[i] == 7)
                    {
                        num_count++;
                    }
                    if (num_count >= 2)
                    {
                        flag_victory_2 = true;
                        player1_situation = "三-三禁手";
                        player2_situation = "获胜";
                        break;
                    }
                }
                ///检测长连禁手
                for (int i = 0; i < 4; i++)
                {
                    if (player1_state[i] == 10)
                    {
                        flag_victory_2 = true;
                        player1_situation = "长连禁手";
                        player2_situation = "获胜";
                        break;
                    }
                }
            }

            ///判断是否五连来判断是否获胜
            for (int i = 0; i < 4; i++)
            {
                if (player1_state[i] == 11)
                {
                    flag_victory_1 = true;
                    player1_situation = "获胜";
                    break;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                if (player2_state[i] == 11)
                {
                    flag_victory_2 = true;
                    player2_situation = "获胜";
                    break;
                }
            }

            ///更新下棋顺序
            CB.player1 = (!(CB.player1));
            if (player1 == true && flag_victory_1 == true)
                return true;
            else if (player1 == false && flag_victory_2 == true)
                return true;
            return false;
        }
    }
}
