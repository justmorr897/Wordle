using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Test
{
    public partial class Background : Form
    {
        string char1;
        string char2;
        string char3;
        string char4;
        string char5;
        string answer;
        string gameState;
        string guessAnswer1;
        string path = "Resources/WordList2.txt";

        int guess = 0;
        int index;
        int scoreSum;
        int gamesPlayed;
        int gamesWon;
        int winPercentage;
        int streak;
        int bestScore;
        int previousScore;
        int lockedIndex;

        List<System.Windows.Forms.TextBox> Inputs = new List<System.Windows.Forms.TextBox>();
        List<System.Windows.Forms.TextBox> AnswerInputs = new List<System.Windows.Forms.TextBox>();
        List<System.Windows.Forms.Button> LetterButtons = new List<System.Windows.Forms.Button>();
        List<System.Windows.Forms.Label> GuessDistributions = new List<System.Windows.Forms.Label>();
        List<System.Windows.Forms.Label> GuessStatlabels = new List<System.Windows.Forms.Label>();

        List<string> AnswerCharacters = new List<string>(new string[] { });
        List<string> Letters = new List<string>();
        List<string> readText;
        List<int> Scores = new List<int>();
        List<int> Guesses = new List<int>();

        Random randGen = new Random();

        public Background()
        {
            InitializeComponent();

            // Open the file to read from.
            readText = File.ReadAllLines(path).ToList();

            GameStart();

            AnswerInputs.Add(answerInput1);
            AnswerInputs.Add(answerInput2);
            AnswerInputs.Add(answerInput3);
            AnswerInputs.Add(answerInput4);
            AnswerInputs.Add(answerInput5);

            Letters.Add("A");
            Letters.Add("B");
            Letters.Add("C");
            Letters.Add("D");
            Letters.Add("E");
            Letters.Add("F");
            Letters.Add("G");
            Letters.Add("H");
            Letters.Add("I");
            Letters.Add("J");
            Letters.Add("K");
            Letters.Add("L");
            Letters.Add("M");
            Letters.Add("N");
            Letters.Add("O");
            Letters.Add("P");
            Letters.Add("Q");
            Letters.Add("R");
            Letters.Add("S");
            Letters.Add("T");
            Letters.Add("U");
            Letters.Add("V");
            Letters.Add("W");
            Letters.Add("X");
            Letters.Add("Y");
            Letters.Add("Z");

            Scores.Add(2);
            Scores.Add(17);
            Scores.Add(10);
            Scores.Add(12);
            Scores.Add(1);
            Scores.Add(18);
            Scores.Add(16);
            Scores.Add(15);
            Scores.Add(4);
            Scores.Add(25);
            Scores.Add(21);
            Scores.Add(9);
            Scores.Add(14);
            Scores.Add(7);
            Scores.Add(5);
            Scores.Add(13);
            Scores.Add(26);
            Scores.Add(3);
            Scores.Add(8);
            Scores.Add(6);
            Scores.Add(11);
            Scores.Add(22);
            Scores.Add(20);
            Scores.Add(23);
            Scores.Add(19);
            Scores.Add(24);

            GuessDistributions.Add(guessdistributionLabel1);
            GuessDistributions.Add(guessdistributionLabel2);
            GuessDistributions.Add(guessdistributionLabel3);
            GuessDistributions.Add(guessdistributionLabel4);
            GuessDistributions.Add(guessdistributionLabel5);
            GuessDistributions.Add(guessdistributionLabel6);
            GuessDistributions.Add(guessdistributionLabel7);

            GuessStatlabels.Add(guessstatLabel1);
            GuessStatlabels.Add(guessstatLabel2);
            GuessStatlabels.Add(guessstatLabel3);
            GuessStatlabels.Add(guessstatLabel4);
            GuessStatlabels.Add(guessstatLabel5);
            GuessStatlabels.Add(guessstatLabel6);
            GuessStatlabels.Add(guessstatLabel7);

            LetterButtons.Add(qButton);
            LetterButtons.Add(wButton);
            LetterButtons.Add(eButton);
            LetterButtons.Add(rButton);
            LetterButtons.Add(tButton);
            LetterButtons.Add(yButton);
            LetterButtons.Add(uButton);
            LetterButtons.Add(iButton);
            LetterButtons.Add(oButton);
            LetterButtons.Add(pButton);
            LetterButtons.Add(aButton);
            LetterButtons.Add(sButton);
            LetterButtons.Add(dButton);
            LetterButtons.Add(fButton);
            LetterButtons.Add(gButton);
            LetterButtons.Add(hButton);
            LetterButtons.Add(jButton);
            LetterButtons.Add(kButton);
            LetterButtons.Add(lButton);
            LetterButtons.Add(zButton);
            LetterButtons.Add(xButton);
            LetterButtons.Add(cButton);
            LetterButtons.Add(vButton);
            LetterButtons.Add(bButton);
            LetterButtons.Add(nButton);
            LetterButtons.Add(mButton);
        }
        private void playerChooseButton_Click(object sender, EventArgs e)
        {
            GameInitialize();
            
            Inputs.Clear();
            AnswerCharacters.Clear();

            this.WindowState = FormWindowState.Maximized;
            enterLabel.Visible = true;
            backgroundLabel.Size = new Size(1316, 1050);
            enterLabel.Size = new Size(460, 82);

            answerInput1.Size = new Size(45, 47);
            answerInput2.Size = new Size(45, 47);
            answerInput3.Size = new Size(45, 47);
            answerInput4.Size = new Size(45, 47);
            answerInput5.Size = new Size(45, 47);
            answerInput1.Clear();
            answerInput2.Clear();
            answerInput3.Clear();
            answerInput4.Clear();
            answerInput5.Clear();


            enterAnswerButton.Size = new Size(300, 54);
            enterLabel.Size = new Size(310, 55);

            cpuChooseAnotherButton.Visible = false;
            backtomenuButton.Visible = true;
            statsLabel.Visible = false;
            statspromptsLabel.Visible = false;

            for (int i = 0; i < GuessDistributions.Count; i++)
            {
                GuessDistributions[i].Visible = false;
                GuessStatlabels[i].Visible = false;
            }
        }
        private void cpuChooseButton_Click(object sender, EventArgs e)
        {
            index = 0;
            lockedIndex = 0;
            guess = 0;
            gamesPlayed++;

            this.WindowState = FormWindowState.Maximized;

            tryagainButton.Visible = true;
            enterButton.Visible = true;

            enterLabel.Visible = false;
            statsLabel.Visible = false;
            statspromptsLabel.Visible = false;

            for (int i = 0; i < GuessDistributions.Count; i++)
            {
                GuessDistributions[i].Visible = false;
                GuessStatlabels[i].Visible = false;
            }

            backgroundLabel.Size = new Size(0, 0);
            enterLabel.Size = new Size(0, 0);
            answerInput1.Size = new Size(0, 0);
            answerInput2.Size = new Size(0, 0);
            answerInput3.Size = new Size(0, 0);
            answerInput4.Size = new Size(0, 0);
            answerInput5.Size = new Size(0, 0);
            enterAnswerButton.Size = new Size(0, 0);
            enterLabel.Size = new Size(0, 0);

            GameInitialize();
            int guessValue = randGen.Next(0, readText.Count);
            answer = readText[guessValue].ToUpper();

            char1 = Convert.ToString(answer[0]);
            char2 = Convert.ToString(answer[1]);
            char3 = Convert.ToString(answer[2]);
            char4 = Convert.ToString(answer[3]);
            char5 = Convert.ToString(answer[4]);

            AnswerCharacters.Clear();

            AnswerCharacters.Add(char1);
            AnswerCharacters.Add(char2);
            AnswerCharacters.Add(char3);
            AnswerCharacters.Add(char4);
            AnswerCharacters.Add(char5);

            Inputs.Clear();

            Inputs.Add(input1);
            Inputs.Add(input2);
            Inputs.Add(input3);
            Inputs.Add(input4);
            Inputs.Add(input5);
            Inputs.Add(input6);
            Inputs.Add(input7);
            Inputs.Add(input8);
            Inputs.Add(input9);
            Inputs.Add(input10);
            Inputs.Add(input11);
            Inputs.Add(input12);
            Inputs.Add(input13);
            Inputs.Add(input14);
            Inputs.Add(input15);
            Inputs.Add(input16);
            Inputs.Add(input17);
            Inputs.Add(input18);
            Inputs.Add(input19);
            Inputs.Add(input20);
            Inputs.Add(input21);
            Inputs.Add(input22);
            Inputs.Add(input23);
            Inputs.Add(input24);
            Inputs.Add(input25);
            Inputs.Add(input26);
            Inputs.Add(input27);
            Inputs.Add(input28);
            Inputs.Add(input29);
            Inputs.Add(input30);

            //this.Text = $"{answer}";

            playerchooseAnotherButton.Visible = true;
            cpuChooseButton.Visible = true;
            backtomenuButton.Visible = true;

            input1.Enabled = true;
            input1.Focus();
        }

        public void GameStart()
        {
            dictionaryButton.Size = new Size(123, 35);
            //streakButton.Size = new Size(123, 35);
            statsButton.Size = new Size(123, 35);
            cpuChooseButton.Size = new Size(220, 98);
            playerChooseButton.Size = new Size(220, 98);
            backtomenuButton.Size = new Size(100, 45);
            blackbackgroundLabel.Size = new Size(845, 604);
            yellowbackgroundLabel.Size = new Size(871, 698);
        }
        public void GameInitialize()
        {
            

            statsButton.Size = new Size(0, 0);
            streakButton.Size = new Size(0, 0);
            dictionaryButton.Size = new Size(0, 0);
            yellowbackgroundLabel.Size = new Size(0, 0);
            blackbackgroundLabel.Size = new Size(0, 0);
            playerChooseButton.Size = new Size(0, 0);
            cpuChooseButton.Size = new Size(0, 0);

            cpuChooseAnotherButton.Visible = true;
            answerInput1.Focus();
        }

        private void answerInput1_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox answerTb = (TextBox)sender;
            //answerTb.BackColor = Color.Red;

            if (e.KeyCode == Keys.A || e.KeyCode == Keys.B || e.KeyCode == Keys.C || e.KeyCode == Keys.D || e.KeyCode == Keys.E || e.KeyCode == Keys.F || e.KeyCode == Keys.G || e.KeyCode == Keys.H || e.KeyCode == Keys.I || e.KeyCode == Keys.J || e.KeyCode == Keys.K || e.KeyCode == Keys.L || e.KeyCode == Keys.M || e.KeyCode == Keys.N || e.KeyCode == Keys.O || e.KeyCode == Keys.P || e.KeyCode == Keys.Q || e.KeyCode == Keys.R || e.KeyCode == Keys.S || e.KeyCode == Keys.T || e.KeyCode == Keys.U || e.KeyCode == Keys.V || e.KeyCode == Keys.W || e.KeyCode == Keys.X || e.KeyCode == Keys.Y || e.KeyCode == Keys.Z)
            {
                index = answerTb.TabIndex - 133;

                for (int i = 0; i < 3; i++)
                {
                    answerTb.Size = new Size(answerTb.Width + i, answerTb.Height + i);
                    Thread.Sleep(1);
                    answerTb.Location = new Point(answerTb.Location.X - i, answerTb.Location.Y - i);
                    answerTb.Refresh();
                    Thread.Sleep(1);
                }
                for (int i = 0; i < 3; i++)
                {
                    answerTb.Size = new Size(answerTb.Width - i, answerTb.Height - i);
                    Thread.Sleep(1);
                    answerTb.Location = new Point(answerTb.Location.X + i, answerTb.Location.Y + i);
                    answerTb.Refresh();
                    Thread.Sleep(1);
                }

                if (index < AnswerInputs.Count)
                {
                    AnswerInputs[index].Enabled = true;
                    AnswerInputs[index].Focus();
                }
                else
                {

                }

            }
            else if (e.KeyCode == Keys.Back)
            {
                answerLabel.Visible = false;
                index--;
                if (index <= -1)
                {
                    AnswerInputs[0].Focus();
                }
                else
                {
                    AnswerInputs[index].Focus();
                    AnswerInputs[index].Clear();
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                enterAnswerButton_Click(sender, e);
            }
            else
            {
                answerTb.Clear();
                answerTb.Text = " ";
                answerTb.Text = answerTb.Text.Substring(0, 0);
                answerTb.Refresh();
            }
        }
        private void enterAnswerButton_Click(object sender, EventArgs e)
        {
            input1.Enabled = true;
            input1.Focus();
            guess = 0;
            if (answerInput1.Text == "" || answerInput2.Text == "" || answerInput3.Text == "" || answerInput4.Text == "" || answerInput5.Text == "")
            {
                label1.Size = new Size(379, 79);

                answerInput1.Clear();
                answerInput2.Clear();
                answerInput3.Clear();
                answerInput4.Clear();
                answerInput5.Clear();

                answerInput1.Focus();
            }
            else
            {
                char1 = answerInput1.Text;
                char2 = answerInput2.Text;
                char3 = answerInput3.Text;
                char4 = answerInput4.Text;
                char5 = answerInput5.Text;

                answer = char1 + char2 + char3 + char4 + char5;

                AnswerCharacters.Add(char1);
                AnswerCharacters.Add(char2);
                AnswerCharacters.Add(char3);
                AnswerCharacters.Add(char4);
                AnswerCharacters.Add(char5);

                if (readText.Contains(answer.ToLower()))
                {
                    index = 0;
                    lockedIndex = 0;

                    gamesPlayed++;

                    label1.Visible = false;
                    tryagainButton.Visible = true;
                    enterButton.Visible = true;

                    Inputs.Add(input1);
                    Inputs.Add(input2);
                    Inputs.Add(input3);
                    Inputs.Add(input4);
                    Inputs.Add(input5);
                    Inputs.Add(input6);
                    Inputs.Add(input7);
                    Inputs.Add(input8);
                    Inputs.Add(input9);
                    Inputs.Add(input10);
                    Inputs.Add(input11);
                    Inputs.Add(input12);
                    Inputs.Add(input13);
                    Inputs.Add(input14);
                    Inputs.Add(input15);
                    Inputs.Add(input16);
                    Inputs.Add(input17);
                    Inputs.Add(input18);
                    Inputs.Add(input19);
                    Inputs.Add(input20);
                    Inputs.Add(input21);
                    Inputs.Add(input22);
                    Inputs.Add(input23);
                    Inputs.Add(input24);
                    Inputs.Add(input25);
                    Inputs.Add(input26);
                    Inputs.Add(input27);
                    Inputs.Add(input28);
                    Inputs.Add(input29);
                    Inputs.Add(input30);

                    answerInput1.Size = new Size(0, 0);
                    answerInput2.Size = new Size(0, 0);
                    answerInput3.Size = new Size(0, 0);
                    answerInput4.Size = new Size(0, 0);
                    answerInput5.Size = new Size(0, 0);
                    enterAnswerButton.Size = new Size(0, 0);
                    enterLabel.Size = new Size(0, 0);
                    backgroundLabel.Size = new Size(0, 0);
                    enterLabel.Visible = false;
                    playerchooseAnotherButton.Visible = true;
                    cpuChooseAnotherButton.Visible = true;
                    backtomenuButton.Visible = true;

                    input1.Focus();
                    answerLabel.Visible = false;
                }
                else
                {
                    answerInput5.Focus();
                    answerLabel.Visible = true;
                    answerLabel.Text = $"Please enter a valid answer word";
                }
            }
        }

        private void input1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.A || e.KeyCode == Keys.B || e.KeyCode == Keys.C || e.KeyCode == Keys.D || e.KeyCode == Keys.E || e.KeyCode == Keys.F || e.KeyCode == Keys.G || e.KeyCode == Keys.H || e.KeyCode == Keys.I || e.KeyCode == Keys.J || e.KeyCode == Keys.K || e.KeyCode == Keys.L || e.KeyCode == Keys.M || e.KeyCode == Keys.N || e.KeyCode == Keys.O || e.KeyCode == Keys.P || e.KeyCode == Keys.Q || e.KeyCode == Keys.R || e.KeyCode == Keys.S || e.KeyCode == Keys.T || e.KeyCode == Keys.U || e.KeyCode == Keys.V || e.KeyCode == Keys.W || e.KeyCode == Keys.X || e.KeyCode == Keys.Y || e.KeyCode == Keys.Z)
               && (index < 30)
               && (index - lockedIndex < 4))
            {
                TextBox tb = (TextBox)sender;
                index = tb.TabIndex + 1;

                Inputs[index].Enabled = true;
                Inputs[index].Focus();
                //Inputs[index].BackColor = Color.Blue;
            }
            else if (e.KeyCode == Keys.Back && index >= lockedIndex)
            {
                answerLabel.Visible = false;
                Inputs[index].Clear();
                index--;
                if(index < 0)
                {
                    index = 0;
                }
                Inputs[index].Focus(); //Look at this line

            }
            else if ((e.KeyCode == Keys.Enter) && (index == 4 || index == 9 || index == 14 || index == 19 || index == 24 || index == 29))
            {
                enterButton_Click(sender, e);
            }
        }
        private void enterButton_Click(object sender, EventArgs e)
        {
            if (index == 4)
            {
                guessAnswer1 = input1.Text + input2.Text + input3.Text + input4.Text + input5.Text;
            }
            else if (index == 9)
            {
                guessAnswer1 = input6.Text + input7.Text + input8.Text + input9.Text + input10.Text;
            }
            else if (index == 14)
            {
                guessAnswer1 = input11.Text + input12.Text + input13.Text + input14.Text + input15.Text;
            }
            else if (index == 19)
            {
                guessAnswer1 = input16.Text + input17.Text + input18.Text + input19.Text + input20.Text;
            }
            else if (index == 24)
            {
                guessAnswer1 = input21.Text + input22.Text + input23.Text + input24.Text + input25.Text;
            }
            else if (index == 29)
            {
                guessAnswer1 = input26.Text + input27.Text + input28.Text + input29.Text + input30.Text;
            }

            if (readText.Contains(guessAnswer1.ToLower()))
            {
                for (int i = lockedIndex; i <= lockedIndex + 4; i++)
                {
                    Inputs[i].Enabled = false;
                }
                InputChange();
                LetterButtonColours();

            }
            else
            {
                answerLabel.Visible = true;
                answerLabel.Text = $"Please enter a valid word";
                label1.Visible = true;
                label1.Text = $"Please enter a valid word";
            }

        }
        public void InputChange()
        {
            guess++;

            if (index == 4)
            {
                input6.Enabled = true;
                input6.Focus();
            }
            else if (index == 9)
            {
                input11.Enabled = true;
                input11.Focus();
            }
            else if (index == 14)
            {
                input16.Enabled = true;
                input16.Focus();
            }
            else if (index == 19)
            {
                input21.Enabled = true;
                input21.Focus();
            }
            else if (index == 24)
            {
                input26.Enabled = true;
                input26.Focus();
            }
            else
            {
                CheckAnswer();
            }

            for (int i = lockedIndex; i <= lockedIndex + 4; i++)
            {
                if (Inputs[i].Text == AnswerCharacters[0])
                {
                    Inputs[i].BackColor = Color.Yellow;
                }
                else if (Inputs[i].Text == AnswerCharacters[1])
                {
                    Inputs[i].BackColor = Color.Yellow;
                }
                else if (Inputs[i].Text == AnswerCharacters[2])
                {
                    Inputs[i].BackColor = Color.Yellow;
                }
                else if (Inputs[i].Text == AnswerCharacters[3])
                {
                    Inputs[i].BackColor = Color.Yellow;
                }
                else if (Inputs[i].Text == AnswerCharacters[4])
                {
                    Inputs[i].BackColor = Color.Yellow;
                }
                if (Inputs[i].Text == AnswerCharacters[i - lockedIndex])
                {
                    Inputs[i].BackColor = Color.Green;
                }
            }

            if (guessAnswer1 == answer)
            {
                gameState = "Won";
                streak++;
                gamesWon++;
                winPercentage = gamesWon / gamesPlayed * 100;

                tryagainButton.Visible = false;
                enterButton.Visible = false;

                Guesses.Add(guess);

                for (int i = 0; i < Letters.Count; i++)
                {
                    if (answer.Contains(Letters[i]))
                    {
                        scoreSum += Scores[i];
                    }
                }

                if (scoreSum > previousScore)
                {
                    bestScore = scoreSum;
                }
                previousScore = scoreSum;

                this.Text = $"Score:{bestScore}, Streak: {streak}, Games:{gamesPlayed}, Win %: {winPercentage}%, #Guesses: {guess}";
                GameStats();
            }
            else if (index == 29 && guessAnswer1 != answer)
            {
                this.Text = $"The word was {answer}";
            }

            lockedIndex = index + 1;

        }
        public void LetterButtonColours()
        {
            for (int i = 0; i < Inputs.Count; i++)
            {
                if (Inputs[i].BackColor != SystemColors.InactiveCaptionText)
                {
                    for (int n = 0; n < LetterButtons.Count; n++)
                    {
                        if (LetterButtons[n].Text == (Inputs[i].Text) && Inputs[i].BackColor == Color.Green)
                        {
                            LetterButtons[n].BackColor = Color.Green;
                            break;
                        }
                        else if (LetterButtons[n].Text == (Inputs[i].Text) && Inputs[i].BackColor == Color.Yellow && LetterButtons[n].BackColor != Color.Green)
                        {
                            LetterButtons[n].BackColor = Color.Yellow;
                            break;
                        }
                        else if (LetterButtons[n].Text == (Inputs[i].Text) && Inputs[i].BackColor == Color.Black)
                        {
                            LetterButtons[n].BackColor = Color.DimGray;
                            break;
                        }
                    }
                }
            }

            //for(int i = 0; i < Inputs.Count; i++)
            //{
            //    if(Inputs[i].BackColor != Color.Green || Inputs[i].BackColor != Color.Yellow)
            //    {
            //       for(int n = 0; n < LetterButtons.Count; n++)
            //       {
            //            if (LetterButtons[n].Text == (Inputs[i].Text) && Inputs[i].BackColor == SystemColors.InactiveCaptionText)
            //            {
            //                LetterButtons[n].BackColor = Color.Pink;
            //                break;
            //            }
            //        }
            //    }
            //}

        }
        public void CheckAnswer()
        {
            if (input26.Text != char1 || input27.Text != char2 || input28.Text != char3 || input29.Text != char4 || input30.Text != char5)
            {
                tryagainButton.Visible = false;
                enterButton.Visible = false;

                gameState = "Lost";
                streak = 0;

                guessedcorrectlyLabel.Text = $"\r\nTHE WORD WAS {answer}\r\n\r\n\r\nGUESS DISTRIBUTION";
                //answerLabel.Visible = true;
                //answerLabel.Text = $"You Guessed Incorrectly \n:( \n the word was {answer} ";
                Guesses.Add(guess);

                GameStats();
            }
        }

        private void tryagainButton_Click(object sender, EventArgs e)
        {
            index = 0;
            lockedIndex = 0;
            guess = 0;

            for (int i = 0; i < Inputs.Count; i++)
            {
                Inputs[i].Clear();
                Inputs[i].BackColor = Color.Black;
                Inputs[i].Enabled = false;
            }
            for (int i = 0; i < LetterButtons.Count; i++)
            {
                LetterButtons[i].BackColor = Color.DarkGray;
            }

            input1.Enabled = true;
            input1.Focus();
            answerLabel.Visible = false;
            answerLabel.Text = "";
            enterButton.Text = "Enter";
        }
        private void chooseAnotherButton_Click(object sender, EventArgs e)
        {
            GameReset();

            AnswerCharacters.Clear();
            answerInput1.Text = "";
            answerInput2.Text = "";
            answerInput3.Text = "";
            answerInput4.Text = "";
            answerInput5.Text = "";

            playerchooseAnotherButton.Visible = false;
            cpuChooseAnotherButton.Visible = false;
            backtomenuButton.Visible = false;

            scoreSum = 0;

            playerChooseButton_Click(sender, e);
        }
        private void cpuChooseAnotherButton_Click(object sender, EventArgs e)
        {
            index = 0;
            lockedIndex = 0;
            guess = 0;
            scoreSum = 0;

            GameReset();
            cpuChooseButton_Click(sender, e);
        }
        private void backtomenuButton_Click(object sender, EventArgs e)
        {
            GameReset();

            this.WindowState = FormWindowState.Normal;

            dictionaryAddButton.Visible = false;
            dictionaryRemoveButton.Visible = false;
            dictionaryListOutput.Visible = false;
            dictionaryBackground.Visible = false;
            dictionaryaddInput.Visible = false;
            dictionaryremoveInput.Visible = false;
            playerchooseAnotherButton.Visible = false;
            cpuChooseAnotherButton.Visible = false;
            backtomenuButton.Visible = false;
            statsLabel.Visible = false;
            statspromptsLabel.Visible = false;
            GameStart();
        }

        public void GameReset()
        {
            guess = 0;

            for (int i = 0; i < Inputs.Count; i++)
            {
                Inputs[i].Clear();
                Inputs[i].BackColor = Color.Black;
                //Inputs[i].Text = "";
            }
            for (int i = 0; i < LetterButtons.Count; i++)
            {
                LetterButtons[i].BackColor = Color.DarkGray;
            }
            for (int i = 0; i < GuessDistributions.Count; i++)
            {
                GuessDistributions[i].Visible = false;
                GuessStatlabels[i].Visible = false;
            }

            statsLabel.Visible = false;
            statspromptsLabel.Visible = false;
            guessedcorrectlyLabel.Visible = false;
            answerLabel.Visible = false;
        }

        private void dictionaryButton_Click(object sender, EventArgs e)
        {
            dictionaryListOutput.Text = (Properties.Resources.WordList2);
            this.WindowState = FormWindowState.Maximized;
            
            for (int i = 0; i < GuessDistributions.Count; i++)
            {
                GuessDistributions[i].Visible = false;
                GuessStatlabels[i].Visible = false;
            }

            dictionaryListOutput.Visible = true;
            backtomenuButton.Visible = true;
            dictionaryaddInput.Visible = true;
            dictionaryremoveInput.Visible = true;
            dictionaryAddButton.Visible = true;
            dictionaryRemoveButton.Visible = true;
            dictionaryBackground.Visible = true;
            statsLabel.Visible = false;
            statspromptsLabel.Visible = false;
        }
        private void dictionaryAddButton_Click(object sender, EventArgs e)
        {
            if (dictionaryaddInput.TextLength == 5)
            {
                string addWord = dictionaryaddInput.Text.ToLower();

                readText.Add(addWord);
                readText.Sort();
                dictionaryaddInput.Clear();
                dictionaryListOutput.Text += $"{addWord}\n";

                File.WriteAllLines(path, readText);
                //readText = File.ReadAllLines(path, Encoding.UTF8).ToList();

                //dictionaryListOutput.Text = File.ReadAllLines(path).ToString();

                //dictionaryListOutput.Text = $"{File.ReadAllLines(path, Encoding.UTF8)}";
            }

        }
        private void dictionaryRemoveButton_Click(object sender, EventArgs e)
        {
            string removeWord = dictionaryremoveInput.Text.ToLower();

            for (int i = 0; i < readText.Count; i++)
            {
                if (readText.Contains(removeWord))
                {
                    readText.Remove(removeWord);
                    //readText.RemoveAt(i);

                    File.WriteAllLines(path, readText);
                    readText = File.ReadAllLines(path, Encoding.UTF8).ToList();
                }
            }

            dictionaryremoveInput.Clear();
        }

        private void streakButton_Click(object sender, EventArgs e)
        {

        }
        private void statsButton_Click(object sender, EventArgs e)
        {

            if(statsLabel.Visible == true)
            {
                statsLabel.Visible = false;
                statspromptsLabel.Visible = false;
                for(int i = 0; i < GuessDistributions.Count; i++)
                {
                    GuessDistributions[i].Visible = false;
                    GuessStatlabels[i].Visible = false ;
                }
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                GameStats();
                guessedcorrectlyLabel.Visible = false;
            }
        }

        public void GameStats()
        {
            //statsLabel.Text = $"\r\n\r\n{gamesPlayed} Games          {winPercentage}%              {streak}                   {scoreSum}\r\n\r\n";
            statsLabel.Text = $"\r\n\r\n    {gamesPlayed}                  {winPercentage}%              {streak}                  {bestScore}\r\n\r\n";
            guessedcorrectlyLabel.Visible = true;
            statsLabel.Visible = true;
            statspromptsLabel.Visible = true;

            for (int i = 0; i < Guesses.Count; i++)
            {
                GuessDistributions[i].Visible = true;
                GuessStatlabels[i].Visible = true;
                GuessDistributions[i].Text = $"{Guesses[i]}";
            }
            for (int i = 0; i < Guesses.Count; i++)
            {
                if (Guesses[i] == 1)
                {
                    GuessDistributions[i].Size = new Size(75, GuessDistributions[i].Height);
                }
                else if (Guesses[i] == 2)
                {
                    GuessDistributions[i].Size = new Size(150, GuessDistributions[i].Height);
                }
                else if (Guesses[i] == 3)
                {
                    GuessDistributions[i].Size = new Size(225, GuessDistributions[i].Height);
                }
                else if (Guesses[i] == 4)
                {
                    GuessDistributions[i].Size = new Size(300, GuessDistributions[i].Height);
                }
                else if (Guesses[i] == 5)
                {
                    GuessDistributions[i].Size = new Size(375, GuessDistributions[i].Height);
                }
                else if (Guesses[i] == 6)
                {
                    GuessDistributions[i].Size = new Size(450, GuessDistributions[i].Height);
                }
                else
                {
                    GuessDistributions[i].Size = new Size(25, GuessDistributions[i].Height);
                }

                if (gameState == "Lost")
                {
                    GuessDistributions[i].BackColor = Color.Red;
                }
            }
        }
    }
}
