using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tic_Tac_Toe_Game.Properties;

namespace Tic_Tac_Toe_Game
{
    public partial class frmXOGame : Form
    {
        stGameStatus GameStatus;
        enPlayar PlayerTurn = enPlayar.Player1;

        enum enPlayar
        {
            Player1,Player2
        }

        enum enWinner
        {
            Player1,
            Player2,
            Draw,
            GameInProgress
        }

        struct stGameStatus
        {
            public enWinner Winner;
            public bool GameOver;
            public short PlayCount;
        }


        public frmXOGame()
        {
            InitializeComponent();
        }

        void EndGame()
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;


            lblTurn.Text = "Game Over";

            switch (GameStatus.Winner)
            {
                case enWinner.Player1:
                    lblWinner.Text = "Player1";
                    break;

                case enWinner.Player2:
                    lblWinner.Text = "Player2";
                    break;

                default:
                    lblWinner.Text = "Draw";
                    break;

            }

            MessageBox.Show("Game Over", "Game End", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public bool CheckValues(Button btn1, Button btn2, Button btn3)
        {
            if(btn1.Tag.ToString() != "?" && btn1.Tag.ToString() == btn2.Tag.ToString() && btn1.Tag.ToString() == btn3.Tag.ToString())
            {

                btn1.BackColor = Color.GreenYellow; 
                btn2.BackColor = Color.GreenYellow;
                btn3.BackColor = Color.GreenYellow;

                if(btn1.Tag.ToString() == "X")
                {
                    GameStatus.Winner = enWinner.Player1;
                    GameStatus.GameOver = true;
                    EndGame();
                    return true;
                }
                else
                {
                    GameStatus.Winner = enWinner.Player2;
                    GameStatus.GameOver = true;
                    EndGame();
                    return true;
                }
            }


            GameStatus.GameOver = false;
            return false;
        }

        void CheckWinner()
        {
            if (CheckValues(button1, button2, button3))
                return;

            if (CheckValues(button4, button5, button6))
                return;

            if (CheckValues(button7, button8, button9))
                return;

            if (CheckValues(button1, button4, button7))
                return;

            if (CheckValues(button2, button5, button8))
                return;

            if (CheckValues(button3, button6, button9))
                return;

            if (CheckValues(button1, button5, button9))
                return;

            if (CheckValues(button3, button5, button7))
                return;
        }

        void ChangeImage(Button btn)
        {
            if(btn.Tag.ToString() == "?")
            {
                switch (PlayerTurn)
                {
                    case enPlayar.Player1:
                        btn.Image = Resources.X;
                        PlayerTurn = enPlayar.Player2;
                        lblTurn.Text = "Player2";
                        GameStatus.PlayCount++;
                        btn.Tag = "X";
                        CheckWinner();
                        break;

                    case enPlayar.Player2:
                        btn.Image = Resources.O;
                        PlayerTurn = enPlayar.Player1;
                        lblTurn.Text = "Player1";
                        GameStatus.PlayCount++;
                        btn.Tag = "O";
                        CheckWinner();
                        break;
                }

            }

            else
            {
                MessageBox.Show("Wrong Choice", "Wrong!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (GameStatus.PlayCount == 9)
            {
                GameStatus.GameOver = true;
                GameStatus.Winner = enWinner.Draw;
                EndGame();
            }
        }



        private void frmXOGame_Paint(object sender, PaintEventArgs e)
        {
            Color myWhite = Color.White;

            Pen myPen = new Pen(myWhite);
            myPen.Width = 7;

            myPen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            myPen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            e.Graphics.DrawLine(myPen, 400, 300, 1050, 300);
            e.Graphics.DrawLine(myPen, 400, 460, 1050, 460);

            e.Graphics.DrawLine(myPen, 610, 140, 610, 620);
            e.Graphics.DrawLine(myPen, 840, 140, 840, 620);

        }

        private void button_Click(object sender, EventArgs e)
        {
            ChangeImage((Button)sender);
        }

        void ReseteButton(Button btn)
        {
            btn.Image = Resources.question_mark_96;
            btn.Tag = "?";
            btn.BackColor = Color.Transparent;
            btn.Enabled = true;
        }

        void ResetGame()
        {
            ReseteButton(button1);
            ReseteButton(button2);
            ReseteButton(button3);
            ReseteButton(button4);
            ReseteButton(button5);
            ReseteButton(button6);
            ReseteButton(button7);
            ReseteButton(button8);
            ReseteButton(button9);

            GameStatus.Winner = enWinner.GameInProgress;
            lblWinner.Text = "In Progress";
            GameStatus.PlayCount = 0;
            GameStatus.GameOver = false;
            lblTurn.Text = "Player1";
            PlayerTurn = enPlayar.Player1;
        }

        private void btnRestartGame_Click(object sender, EventArgs e)
        {
            ResetGame();
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit_MouseEnter(object sender, EventArgs e)
        {
            btnExit.BackColor = Color.OrangeRed;
        }

        private void btnExit_MouseLeave(object sender, EventArgs e)
        {
            btnExit.BackColor = Color.Maroon;
        }
    }
}
