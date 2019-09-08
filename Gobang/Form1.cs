using System;
using System.Collections;
using System.Collections.Generic;///要使用哈希表
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.Media;
using Class_Judge;
using Class_ChessBoard;
using Class_Game;
using Class_Test;
using MaterialSkin;
using MaterialSkin.Controls;

namespace Gobang
{
    public partial class Form1 : MaterialForm
    {
        ChessBoard CB = new ChessBoard();
        OpenFileDialog ofDialog = new OpenFileDialog();
        SoundPlayer play = new SoundPlayer();
        Judge Jd = new Judge();
        Hashtable state_string = new Hashtable();
        private bool flag_start = false;
        public Form1()
        {
            InitializeComponent();
            CB.GetHandler = GetDebug;
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            Timer.Start();
            this.comboBox1.SelectedIndex = 1;

            ofDialog.AddExtension = true;
            ofDialog.CheckFileExists = true;
            ofDialog.CheckPathExists = true;
            this.MediaPlayer.URL = "BGM.mp3";
            this.MediaPlayer.settings.volume = VolumeBar.Value;
            play.SoundLocation = "Click.wav";
        }

        public bool GetDebug()
        {
            return CK_AIdebug.Checked;
        }
        
        #region 系统监视
        PerformanceCounter cpuUsage = new PerformanceCounter("Process", "% Processor Time", System.Diagnostics.Process.GetCurrentProcess().ProcessName);//性能计数器 
        /// <summary>
        /// 定时器刷新图表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (Ck_monitor.Checked)
            {
                double Mem = 0;
                Process[] process = Process.GetProcessesByName(System.Diagnostics.Process.GetCurrentProcess().ProcessName);
                foreach (Process pres in process)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("RAM:");
                    Mem = pres.WorkingSet64 / 1024 / 1024;
                    sb.Append(Mem);
                    sb.Append("M CPU:");
                    sb.Append((int)(cpuUsage.NextValue() / Environment.ProcessorCount));
                    sb.Append("%");
                    SystemMonitor.Text = sb.ToString();
                }
            }
            else
            {
                SystemMonitor.Text = "";
            }
        }
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            ChessBoard.chessboard_size = 15;
            CB.board = new int[ChessBoard.chessboard_size, ChessBoard.chessboard_size];
            Game.position_create(ChessBoard.chessboard_size);

            DrawChessBoard(CB);

            Test.AllocConsole();
            /// <summary>
            /// 哈希表初始化
            /// </summary>
            state_string.Add(0, "一着");
            state_string.Add(1, "死二");
            state_string.Add(2, "死三");
            state_string.Add(3, "死四");
            state_string.Add(4, "眠二");
            state_string.Add(5, "活二");
            state_string.Add(6, "眠三");
            state_string.Add(7, "活三");
            state_string.Add(8, "冲四");
            state_string.Add(9, "活四");
            state_string.Add(10, "长连");
            state_string.Add(11, "五连");
            int[,] board_sbuff = new int[ChessBoard.chessboard_size, ChessBoard.chessboard_size];
            ChessBoard.link.AddLast(board_sbuff);
        }
        /// <summary>
        /// 画棋盘(初始化)
        /// size为棋盘大小：size*size大小
        /// </summary>
        public void DrawChessBoard(ChessBoard CB)
        {
            CB.DrawChessBoard();
            int size = ChessBoard.chessboard_size;
            pictureBox1.Size = new System.Drawing.Size(size * 30 + 2, size * 30 + 2);
            pictureBox1.BackgroundImage = CB.bit;
        }

        /// <summary>
        /// 落子操作
        /// </summary>
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            play.Play();
            Retract_button.Enabled = true;
            if (flag_start)
            {
                int x_index = (e.X - 1) / 30;
                int y_index = (e.Y - 1) / 30;
                int state = CB.Refresh_State(x_index, y_index);

                if (state == 0)///重复落子
                {
                    //MessageBox.Show("下子重复啦!请重新落子哟~");
                    Situation.Text = "There is a CHESS ALREADY.";
                }
                else if (state == 1)///黑子落子
                {
                    ///绘制棋子
                    CB.DrawChessBoard();
                    pictureBox1.Refresh();
                }
                else if (state == 2)///白子落子
                {
                    ///绘制棋子
                    CB.DrawChessBoard();
                    pictureBox1.Refresh();
                }
                else;


                if (state != 0)
                {
                    ///判断棋形
                    Jd.Check_State(x_index, y_index, CB.player1, CB.board);
                    ///判断输赢
                    Jd.Judge_victory(CB.player1, CB);

                    Game Ge = new Game(CB);
                    Ge.Evalution(CB.board, CB.player1);

                    long[,] Value1 = new long[ChessBoard.chessboard_size, ChessBoard.chessboard_size];
                    Ge.arr_assign_long(Value1, Game.Value_ziji, ChessBoard.chessboard_size, ChessBoard.chessboard_size);
                    Ge.Evalution(CB.board, !CB.player1);
                    int s = ChessBoard.chessboard_size;
                    Ge.arr_assign_long(Game.Value_duishou, Game.Value_ziji, ChessBoard.chessboard_size, ChessBoard.chessboard_size);
                    Ge.arr_assign_long(Game.Value_ziji, Value1, ChessBoard.chessboard_size, ChessBoard.chessboard_size);

                    for (int i = 0; i < ChessBoard.chessboard_size; i++)
                    {
                        for (int j = 0; j < ChessBoard.chessboard_size; j++)
                        {
                            Game.Value_All[i, j] = (1 * Game.Value_ziji[i, j] + 1 * Game.Value_duishou[i, j]) / 2;
                        }
                    }

                    ///更新评分表绘制
                    CB.DrawChessBoard();
                    pictureBox1.Refresh();

                    if (Jd.flag_victory_1 == true)
                    {
                        flag_start = false;
                        //MessageBox.Show("黑棋获胜！");
                        Situation.Text = "黑棋获胜！";
                    }
                    else if (Jd.flag_victory_2 == true)
                    {
                        flag_start = false;
                        ///检测黑棋是否违反禁手规定
                        if (Jd.player1_situation == "四-四禁手")
                        {
                            //MessageBox.Show("黑棋：四-四禁手，白棋：获胜");
                            Situation.Text = "黑棋：四 - 四禁手，白棋：获胜";
                        }
                        else if (Jd.player1_situation == "三-三禁手")
                        {
                            //MessageBox.Show("黑棋：三-三禁手，白棋：获胜");
                            Situation.Text = "黑棋：三-三禁手，白棋：获胜";
                        }
                        else if (Jd.player1_situation == "长连禁手")
                        {
                            //MessageBox.Show("黑棋：长连禁手，白棋：获胜");
                            Situation.Text = "黑棋：长连禁手，白棋：获胜";
                        }
                        else
                        {
                            //MessageBox.Show("黑棋：失败，白棋：获胜");
                            Situation.Text = "黑棋：失败，白棋：获胜";
                        }
                    }
                    Console.WriteLine("P1状态：");
                    Console.WriteLine("行状态  ：{0}", state_string[Jd.player1_state[0]]);
                    Console.WriteLine("列状态  ：{0}", state_string[Jd.player1_state[1]]);
                    Console.WriteLine("主斜状态：{0}", state_string[Jd.player1_state[2]]);
                    Console.WriteLine("副斜状态：{0}", state_string[Jd.player1_state[3]]);
                    ///AI落子
                    long alpha = -10000;
                    long beta = 10000;
                    int x_d = 0, y_d = 0;
                    ///棋盘状态存入缓存
                    Ge.arr_assign(Ge.CB_buff.board, CB.board, ChessBoard.chessboard_size, ChessBoard.chessboard_size);
                    long value = Ge.negaABpuring(Ge.CB_buff.board, !Judge.player_AI, 14, alpha, beta);
                    Console.WriteLine("Value:{0}", value);
                    x_d = Game.x_decision;
                    y_d = Game.y_decision;

                    //必杀棋型必下
                    ///保存原先棋盘状态
                    Judge JD2 = new Judge();
                    int board_save2 = 0;

                    int[] state1_save = JD2.player1_state;
                    int[] state2_save = JD2.player2_state;

                    ///遍历棋盘
                    bool flag_enterfor = false;
                    bool flag_must = false;

                    for (int i = 0; i < ChessBoard.chessboard_size; i++)
                    {
                        for (int j = 0; j < ChessBoard.chessboard_size; j++)
                        {
                            ///只检测未落子的点
                            if (CB.board[i, j] == 0)
                            {
                                ///保存原先状态
                                board_save2 = CB.board[i, j];
                                ///假设在该点落子
                                if (CB.player1)
                                    CB.board[i, j] = 1;
                                else
                                    CB.board[i, j] = 2;

                                ///判断棋形
                                JD2.Check_State(i, j, CB.player1, CB.board);

                                int[] state_buff2 = new int[4];
                                if (CB.player1)
                                    state_buff2 = JD2.player1_state;
                                else
                                    state_buff2 = JD2.player2_state;
                                ///判断必杀棋型
                                int num_h8 = 0;//冲四
                                int num_h9 = 0;//活四
                                int num_h11 = 0;//五连
                                int num_h7 = 0;//活三
                                for (int k = 0; k < 4; k++)
                                {
                                    switch (state_buff2[k])
                                    {
                                        case 8:
                                            num_h8++;
                                            break;
                                        case 9:
                                            num_h9++;
                                            break;
                                        case 7:
                                            num_h7++;
                                            break;
                                        case 11:
                                            num_h11++;
                                            break;
                                        default:
                                            break;
                                    }
                                }


                                if (num_h11 >= 1)///五连
                                {
                                    flag_must = true;
                                    flag_enterfor = true;
                                }
                                else if (num_h9 >= 1)//活四
                                {
                                    flag_must = true;
                                    flag_enterfor = true;
                                }
                                else if (num_h8 >= 2)//双冲四
                                {
                                    flag_must = true;
                                    flag_enterfor = true;
                                }
                                else if (num_h8 >= 1 && num_h7 >= 1)//冲四活三
                                {
                                    flag_must = true;
                                    flag_enterfor = true;
                                }
                                else
                                {
                                    flag_must = false;
                                    flag_enterfor = false;
                                }

                                ///还原状态
                                CB.board[i, j] = board_save2;
                                JD2.player1_state = state1_save;
                                JD2.player2_state = state2_save;

                                if (flag_must)
                                {
                                    x_d = i;
                                    y_d = j;
                                }
                            }

                            if (flag_enterfor)
                                break;
                        }
                    }

                    //必杀棋型必防
                    ///保存原先棋盘状态
                    Judge JD3 = new Judge();
                    int board_save3 = 0;

                    int[] state1_save2 = JD3.player1_state;
                    int[] state2_save2 = JD3.player2_state;
                    bool player_buff = !CB.player1;
                    ///遍历棋盘
                    bool flag_enterfor2 = false;
                    bool flag_must2 = false;

                    for (int i = 0; i < ChessBoard.chessboard_size; i++)
                    {
                        for (int j = 0; j < ChessBoard.chessboard_size; j++)
                        {
                            ///只检测未落子的点
                            if (CB.board[i, j] == 0)
                            {
                                ///保存原先状态
                                board_save3 = CB.board[i, j];
                                ///假设在该点落子
                                if (player_buff)
                                    CB.board[i, j] = 1;
                                else
                                    CB.board[i, j] = 2;

                                ///判断棋形
                                JD3.Check_State(i, j, player_buff, CB.board);

                                int[] state_buff3 = new int[4];
                                if (player_buff)
                                    state_buff3 = JD3.player1_state;
                                else
                                    state_buff3 = JD3.player2_state;
                                ///判断必杀棋型
                                int num_h8 = 0;//冲四
                                int num_h9 = 0;//活四
                                int num_h11 = 0;//五连
                                int num_h7 = 0;//活三
                                for (int k = 0; k < 4; k++)
                                {
                                    switch (state_buff3[k])
                                    {
                                        case 8:
                                            num_h8++;
                                            break;
                                        case 9:
                                            num_h9++;
                                            break;
                                        case 7:
                                            num_h7++;
                                            break;
                                        case 11:
                                            num_h11++;
                                            break;
                                        default:
                                            break;
                                    }
                                }


                                if (num_h11 >= 1)///五连
                                {
                                    flag_must2 = true;
                                    flag_enterfor2 = true;
                                }
                                else if (num_h9 >= 1)//活四
                                {
                                    flag_must2 = true;
                                    flag_enterfor2 = true;
                                }
                                else if (num_h8 >= 2)//双冲四
                                {
                                    flag_must2 = true;
                                    flag_enterfor2 = true;
                                }
                                else if (num_h8 >= 1 && num_h7 >= 1)//冲四活三
                                {
                                    flag_must2 = true;
                                    flag_enterfor2 = true;
                                }
                                else
                                {
                                    flag_must2 = false;
                                    flag_enterfor2 = false;
                                }

                                ///还原状态
                                CB.board[i, j] = board_save3;
                                JD3.player1_state = state1_save2;
                                JD3.player2_state = state2_save2;

                                if (flag_must2)
                                {
                                    x_d = i;
                                    y_d = j;
                                }
                            }

                            if (flag_enterfor2)
                                break;
                        }
                    }

                    CB.Refresh_State(x_d, y_d);
                    ///绘制棋子
                    CB.DrawChessBoard();
                    pictureBox1.Refresh();
                    ///判断棋形
                    Jd.Check_State(x_d, y_d, Judge.player_AI, CB.board);

                    ///判断输赢
                    Jd.Judge_victory(Judge.player_AI, CB);
                    Console.WriteLine("P2状态：");
                    Console.WriteLine("行状态  ：{0}", state_string[Jd.player2_state[0]]);
                    Console.WriteLine("列状态  ：{0}", state_string[Jd.player2_state[1]]);
                    Console.WriteLine("主斜状态：{0}", state_string[Jd.player2_state[2]]);
                    Console.WriteLine("副斜状态：{0}", state_string[Jd.player2_state[3]]);

                    if (Jd.flag_victory_1 == true)
                    {
                        //MessageBox.Show("黑棋获胜！");
                        Situation.Text = "黑棋获胜！";
                        flag_start = false;
                    }
                    else if (Jd.flag_victory_2 == true)
                    {
                        flag_start = false;
                        ///检测黑棋是否违反禁手规定
                        if (Jd.player1_situation == "四-四禁手")
                        {
                            //MessageBox.Show("黑棋：四-四禁手，白棋：获胜");
                            Situation.Text = "黑棋：四 - 四禁手，白棋：获胜";
                        }
                        else if (Jd.player1_situation == "三-三禁手")
                        {
                            //MessageBox.Show("黑棋：三-三禁手，白棋：获胜");
                            Situation.Text = "黑棋：三-三禁手，白棋：获胜";
                        }
                        else if (Jd.player1_situation == "长连禁手")
                        {
                            //MessageBox.Show("黑棋：长连禁手，白棋：获胜");
                            Situation.Text = "黑棋：长连禁手，白棋：获胜";
                        }
                        else
                        {
                            //MessageBox.Show("黑棋：失败，白棋：获胜");
                            Situation.Text = "黑棋：失败，白棋：获胜";
                        }
                    }
                    Ge.Evalution(CB.board, Judge.player_AI);
                    ///更新评分表绘制
                    CB.DrawChessBoard();
                    pictureBox1.Refresh();
                }
            }
        }

        private void Retract_button_Click(object sender, EventArgs e)
        {
            Situation.Text = "Undo a Turn.";
            CB.retract();
            flag_start = true;
            Jd.flag_victory_1 = false;
            Jd.flag_victory_2 = false;
            for (int i = 0; i < 4; i++)
            {
                Jd.player1_state[i] = 0;
                Jd.player2_state[i] = 0;
            }

            if (CB.flag_retract_stop == true)
                Retract_button.Enabled = false;
            else
                Retract_button.Enabled = true;

            CB.DrawChessBoard();//棋盘绘制，需要更改
            pictureBox1.Refresh();
        }

        private void Start_button_Click(object sender, EventArgs e)
        {
            CB.ReStart();
            CB.DrawChessBoard();//棋盘绘制，需要更改
            pictureBox1.Refresh();
            Jd.flag_victory_1 = false;
            Jd.flag_victory_2 = false;
            for (int i = 0; i < 4; i++)
            {
                Jd.player1_state[i] = 0;
                Jd.player2_state[i] = 0;
            }
            ChessBoard.link = new LinkedList<int[,]>(); //定义链表
            ///初始化链表
            int[,] board_sbuff = new int[ChessBoard.chessboard_size, ChessBoard.chessboard_size];
            ChessBoard.link.AddLast(board_sbuff);

            Start_button.Text = "Start";
            Retract_button.Enabled = false;
            //Console.WriteLine("Index:{0}", comboBox1.SelectedIndex);
            if (comboBox1.SelectedIndex != -1)
                flag_start = true;
            else
                flag_start = false;
            ///设定AI先手还是后手
            if (comboBox1.SelectedIndex == 0)
                Judge.player_AI = false;
            else if (comboBox1.SelectedIndex == 1)
                Judge.player_AI = true;

            //DrawChessBoard(CB);

            //Console.WriteLine("player_AI:{0}", Judge.player_AI);
            if (Judge.player_AI)///电脑执黑子先下
            {
                ///更新执棋者
                CB.player1 = Judge.player_AI;
                ///AI落子
                int x_d = ChessBoard.chessboard_size / 2;
                int y_d = ChessBoard.chessboard_size / 2;

                CB.Refresh_State(x_d, y_d);
                ///绘制棋子
                CB.DrawChessBoard();
                pictureBox1.Refresh();
                ///判断棋形
                Jd.Check_State(x_d, y_d, false, CB.board);

                ///判断输赢
                Jd.Judge_victory(false, CB);

                if (Jd.flag_victory_1 == true)
                //MessageBox.Show("黑棋获胜！");
                Situation.Text = "黑棋获胜！";
                else if (Jd.flag_victory_2 == true)
                {
                    ///检测黑棋是否违反禁手规定
                    if (Jd.player1_situation == "四-四禁手")
                    {
                        //MessageBox.Show("黑棋：四-四禁手，白棋：获胜");
                        Situation.Text = "黑棋：四 - 四禁手，白棋：获胜";
                    }
                    else if (Jd.player1_situation == "三-三禁手")
                    {
                        //MessageBox.Show("黑棋：三-三禁手，白棋：获胜");
                        Situation.Text = "黑棋：三-三禁手，白棋：获胜";
                    }
                    else if (Jd.player1_situation == "长连禁手")
                    {
                        //MessageBox.Show("黑棋：长连禁手，白棋：获胜");
                        Situation.Text = "黑棋：长连禁手，白棋：获胜";
                    }
                    else
                    {
                        //MessageBox.Show("黑棋：失败，白棋：获胜");
                        Situation.Text = "黑棋：失败，白棋：获胜";
                    }
                }
                CB.DrawChessBoard();//棋盘绘制，需要更改
                pictureBox1.Refresh();
            }
            else
            {
                CB.player1 = true;
            }
            Start_button.Text = "Restart";
            if (comboBox1.SelectedIndex == 0)
            {
                Situation.Text = "Player is FIRST now.";
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                Situation.Text = "AI is FIRST now.";
            }
        }

        private void Player_First_button_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            Player_First_button.BackColor = Color.Teal;
            Player_First_button.Enabled = false;
            Computer_First_button.BackColor = ColorTranslator.FromHtml("#37474F");
            Computer_First_button.Enabled = true;
            Start_button.PerformClick();
            Situation.Text = "Player is FIRST now.";
        }

        private void Computer_First_button_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 1;
            Player_First_button.BackColor = ColorTranslator.FromHtml("#37474F");
            Player_First_button.Enabled = true;
            Computer_First_button.BackColor = Color.Teal;
            Computer_First_button.Enabled = false;
            Start_button.PerformClick();
            Situation.Text = "AI is FIRST now.";
        }

        private void CK_AIdebug_CheckedChanged(object sender, EventArgs e)
        {
            CB.GetHandler = GetDebug;
            CB.DrawChessBoard();
        }

        private void Volume_a_Click(object sender, EventArgs e)
        {
            VolumeBar.Value = Math.Min(VolumeBar.Value + 10, 100);
            this.MediaPlayer.settings.volume = VolumeBar.Value;
        }

        private void Volume_s_Click(object sender, EventArgs e)
        {
            VolumeBar.Value = Math.Max(VolumeBar.Value - 10, 0);
            this.MediaPlayer.settings.volume = VolumeBar.Value;
        }

        private void CK_Music_CheckedChanged(object sender, EventArgs e)
        {
            if(CK_Music.Checked)
            {
                VolumeBar.Enabled = true;
                Volume_a.Enabled = true;
                Volume_s.Enabled = true;
                VolumeBar.Visible = true;
                Volume_a.Visible = true;
                Volume_s.Visible = true;
                MediaPlayer.settings.volume = VolumeBar.Value;
                MediaPlayer.Ctlcontrols.play();
            }
            else
            {
                VolumeBar.Enabled = false;
                Volume_a.Enabled = false;
                Volume_s.Enabled = false;
                VolumeBar.Visible = false;
                Volume_a.Visible = false;
                Volume_s.Visible = false;
                MediaPlayer.Ctlcontrols.stop();
            }
        }
    }

}
