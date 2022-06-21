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
        //declare all string variables
        string char1;
        string char2;
        string char3;
        string char4;
        string char5;
        string answer;
        string gameState;
        string guessAnswer;

        //string to hold path to file of words
        string path = "Resources/WordList2.txt";

        //declare all integer variables
        int guess = 0;
        int index;
        int scoreSum;
        int gamesPlayed;
        int gamesWon;
        int streak;
        int bestScore;
        int previousScore;
        int lockedIndex;

        //double is needed so it doesn't round to 0% or 100%
        double winPercentage;


        //list of the 30 textboxs in the guessing grid
        List<System.Windows.Forms.TextBox> Inputs = new List<System.Windows.Forms.TextBox>();
        //List of the 5 textboxs used to pick an answer word
        List<System.Windows.Forms.TextBox> AnswerInputs = new List<System.Windows.Forms.TextBox>();
        //List of buttons that represent the buttons below the guessing grid that indicate which letters have been used
        List<System.Windows.Forms.Button> LetterButtons = new List<System.Windows.Forms.Button>();
        //2 lists that track the guess distribution of each game played
        List<System.Windows.Forms.Label> GuessDistributions = new List<System.Windows.Forms.Label>();
        List<System.Windows.Forms.Label> GuessStatlabels = new List<System.Windows.Forms.Label>();

        //Lists of strings
        List<string> AnswerCharacters = new List<string>();
        List<string> Letters = new List<string>();
        //readText is used to read the dictionary file and make the word list
        List<string> readText;

        //List of int lists to track scores and number of guesses
        List<int> Scores = new List<int>();
        List<int> Guesses = new List<int>();

        //random generator for letting the computer pick a word randomly from the word list
        Random randGen = new Random();

        public Background()
        {
            InitializeComponent();

            // Open the file to read from.
            readText = File.ReadAllLines(path).ToList();

            //Call upon GameStart which makes the main menu screen visible
            GameStart();

            //Add to most of the lists when the program is made
            //AnswerInputs is made of the textboxs for inputting your own guess word
            AnswerInputs.Add(answerInput1);
            AnswerInputs.Add(answerInput2);
            AnswerInputs.Add(answerInput3);
            AnswerInputs.Add(answerInput4);
            AnswerInputs.Add(answerInput5);

            //Letters is a list of the letters of the alphabet in order
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

            //Scores is a list of scores that is inversely proportional to how often a letter is used in the English language
            //In alphabetical order (The first index is A, the second is B, etc...)
            //Most common letter: E has a score of 1 since it shows up so often
            //Least common letter: Q has a score of 26 since it is so rare
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

            //add the labels that are used to track guess distributions
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

            //LetterButtons is a list of the buttons below the guessing grid that show which letters have or have not been guessed
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
        public void GameStart()
        {
            //GameStart is for setting up the main menu elements
            //Needs to be a function since it will be called upon on its own later

            dictionaryButton.Size = new Size(123, 35);
            statsButton.Size = new Size(123, 35);
            cpuChooseButton.Size = new Size(220, 98);
            playerChooseButton.Size = new Size(220, 98);
            backtomenuButton.Size = new Size(100, 45);
            blackbackgroundLabel.Size = new Size(845, 604);
            yellowbackgroundLabel.Size = new Size(871, 698);
        }

        private void playerChooseButton_Click(object sender, EventArgs e)
        {
            //When the button that lets the player choose a word is pressed call upon GameInitialize
            //GameInitialize gets rid of main menu and changes focus to answerInput1
            GameInitialize();
            
            //Clear the lists to make sure it starts from 0 and doesn't add more items into the lists
            Inputs.Clear();
            AnswerCharacters.Clear();

            //Maximize the form, it just looks better and provides some visual feedback that it works
            this.WindowState = FormWindowState.Maximized;

            //Make these elements visible 
            backgroundLabel.Size = new Size(1316, 1050);
            enterAnswerButton.Size = new Size(300, 54);
            enterLabel.Size = new Size(310, 55);

            //change the sizes of all the input textboxs from (0,0) to their correct sizes to be seen
            answerInput1.Size = new Size(45, 47);
            answerInput2.Size = new Size(45, 47);
            answerInput3.Size = new Size(45, 47);
            answerInput4.Size = new Size(45, 47);
            answerInput5.Size = new Size(45, 47);
            
            //clear the textboxs to make sure there isn't any text from the previous game
            answerInput1.Clear();
            answerInput2.Clear();
            answerInput3.Clear();
            answerInput4.Clear();
            answerInput5.Clear();

            //Make these elements not visible as the screen changes
            cpuChooseAnotherButton.Visible = false;
            backtomenuButton.Visible = true;
            statsLabel.Visible = false;
            statspromptsLabel.Visible = false;

            //Make the guess distribution elements not visible as they could still be on screen from a previous game
            for (int i = 0; i < GuessDistributions.Count; i++)
            {
                GuessDistributions[i].Visible = false;
                GuessStatlabels[i].Visible = false;
            }
        }
        private void answerInput1_KeyDown(object sender, KeyEventArgs e)
        {
            //Make a textbox that tracks the tabindex of the answerInput that has been pressed
            TextBox answerTb = (TextBox)sender;
            //If statement that checks if a letter has been pressed
            //prevents random characters from being pressed
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.B || e.KeyCode == Keys.C || e.KeyCode == Keys.D || e.KeyCode == Keys.E || e.KeyCode == Keys.F || e.KeyCode == Keys.G || e.KeyCode == Keys.H || e.KeyCode == Keys.I || e.KeyCode == Keys.J || e.KeyCode == Keys.K || e.KeyCode == Keys.L || e.KeyCode == Keys.M || e.KeyCode == Keys.N || e.KeyCode == Keys.O || e.KeyCode == Keys.P || e.KeyCode == Keys.Q || e.KeyCode == Keys.R || e.KeyCode == Keys.S || e.KeyCode == Keys.T || e.KeyCode == Keys.U || e.KeyCode == Keys.V || e.KeyCode == Keys.W || e.KeyCode == Keys.X || e.KeyCode == Keys.Y || e.KeyCode == Keys.Z)
            {   
                //set the index to the tab index - 133 to get it back to zero
                index = answerTb.TabIndex - 133;

                //for loop that expands the inputs size to make an animation
                for (int i = 0; i < 3; i++)
                {
                    answerTb.Size = new Size(answerTb.Width + i, answerTb.Height + i);
                    Thread.Sleep(1);
                    answerTb.Location = new Point(answerTb.Location.X - i, answerTb.Location.Y - i);
                    answerTb.Refresh();
                    Thread.Sleep(1);
                }
                //for loop that shrinks the inputs size to make an animation
                for (int i = 0; i < 3; i++)
                {
                    answerTb.Size = new Size(answerTb.Width - i, answerTb.Height - i);
                    Thread.Sleep(1);
                    answerTb.Location = new Point(answerTb.Location.X + i, answerTb.Location.Y + i);
                    answerTb.Refresh();
                    Thread.Sleep(1);
                }

                //check if the the tab index is less than the number of textboxs
                //If it is less than enable the next text box and focus on it
                if (index < AnswerInputs.Count)
                {
                    AnswerInputs[index].Enabled = true;
                    AnswerInputs[index].Focus();
                }
                //else do noting
                else
                {

                }

            }
            //check if the backspace button is pressed
            else if (e.KeyCode == Keys.Back)
            {
                answerLabel.Visible = false;
                //decrement the to go back to previous textbox
                index--;
                //If index becomes negative, focus on the first AnswerInput to avoid crashing the program
                if (index <= -1)
                {
                    AnswerInputs[0].Focus();
                }
                //else change focus on and clear the text of the textbox before
                else
                {
                    AnswerInputs[index].Focus();
                    AnswerInputs[index].Clear();
                }
            }
            //check if enter key is pressed
            else if (e.KeyCode == Keys.Enter)
            {
                //if it is pressed, call upon the button click method 
                enterAnswerButton_Click(sender, e);
            }
            //else clear the clear the current texbox and refresh
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
            //enable the first input textbox and changing focus to it for a smooth experience
            input1.Enabled = true;
            input1.Focus();

            //reset number of guesses to 0
            guess = 0;

            //if any of the text boxes are empty clear the answerInputs and focus back on the first textbox
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
            //Check if the textboxs are full
            else
            {
                //Set each of the answerInput text as the individual letters of the answer word
                char1 = answerInput1.Text;
                char2 = answerInput2.Text;
                char3 = answerInput3.Text;
                char4 = answerInput4.Text;
                char5 = answerInput5.Text;

                //combine the five letters to make the answer word
                answer = char1 + char2 + char3 + char4 + char5;

                //add those letters to a list to be checked later
                AnswerCharacters.Add(char1);
                AnswerCharacters.Add(char2);
                AnswerCharacters.Add(char3);
                AnswerCharacters.Add(char4);
                AnswerCharacters.Add(char5);

                //check the list of words that was read from the dictionary file if the answer word is in the readText list
                //change the answer to Lower because the word list is in lowercase
                if (readText.Contains(answer.ToLower()))
                {
                    //reset the index and lockedindex to zero because the game is resetting
                    index = 0;
                    lockedIndex = 0;

                    //add 1 to the gamesPlayed
                    gamesPlayed++;                    

                    //Add all of the input textboxes from the guessing grid into a list of textboxs called Inputs
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

                    //make all the answer input textboxes not visible and other elements on that page
                    answerInput1.Size = new Size(0, 0);
                    answerInput2.Size = new Size(0, 0);
                    answerInput3.Size = new Size(0, 0);
                    answerInput4.Size = new Size(0, 0);
                    answerInput5.Size = new Size(0, 0);
                    enterAnswerButton.Size = new Size(0, 0);
                    enterLabel.Size = new Size(0, 0);
                    backgroundLabel.Size = new Size(0, 0);
                   
                    
                    //change the visiblilty of some elements between screens
                    enterLabel.Visible = false;
                    answerLabel.Visible = false;
                    label1.Visible = false;

                    tryagainButton.Visible = true;
                    enterButton.Visible = true;
                    playerchooseAnotherButton.Visible = true;
                    cpuChooseAnotherButton.Visible = true;
                    backtomenuButton.Visible = true;

                    //change the focus to the first textbox of the guess grid to improve user experience
                    input1.Focus();
                }
                //if the word is not in the word list
                //keep the focus of the last input box
                //have a label show up that tells the user to enter a valid word
                else
                {
                    answerInput5.Focus();
                    answerLabel.Visible = true;
                    answerLabel.Text = $"Please enter a valid answer word";
                }
            }
        }
        private void cpuChooseButton_Click(object sender, EventArgs e)
        {
            //When the button that lets the computer choose the word is pressed, do the following:
            
            //Reset all variables that are incremented over time to keep track of guesses and tab index back to zero
            index = 0;
            lockedIndex = 0;
            guess = 0;

            //Add 1 to games played since a new game has started
            gamesPlayed++;

            //maximize the form since the whole screen is needed to fit the whole guessing grid
            this.WindowState = FormWindowState.Maximized;

            //make the try again and enter button visible as they can be used during the game
            tryagainButton.Visible = true;
            enterButton.Visible = true;

            //make buttons visible when the screen changes
            playerchooseAnotherButton.Visible = true;
            cpuChooseButton.Visible = true;
            backtomenuButton.Visible = true;

            //make these label not visible when the screen changes
            enterLabel.Visible = false;
            statsLabel.Visible = false;
            statspromptsLabel.Visible = false;

            //clear all guess distribution elements that could still be on the screen from a previous game
            for (int i = 0; i < GuessDistributions.Count; i++)
            {
                GuessDistributions[i].Visible = false;
                GuessStatlabels[i].Visible = false;
            }

            //reduce the size of the elements used to pick the guess word to (0, 0) in case they are still on screen
            backgroundLabel.Size = new Size(0, 0);
            enterLabel.Size = new Size(0, 0);
            answerInput1.Size = new Size(0, 0);
            answerInput2.Size = new Size(0, 0);
            answerInput3.Size = new Size(0, 0);
            answerInput4.Size = new Size(0, 0);
            answerInput5.Size = new Size(0, 0);
            enterAnswerButton.Size = new Size(0, 0);
            enterLabel.Size = new Size(0, 0);

            //call upon GameInitialize to get rid of all the main menu elements
            GameInitialize();

            //make a variable that is randomly generated from the list of over 2000 five words
            int guessValue = randGen.Next(0, readText.Count);

            //set the answer variable equal to index of the word list(readText) that is chosen by the randomly generated number (guessValue)
            //convert to upper for simplicity's sake later on, as the textboxes are set to make all text uppercase 
            //answer will be a five letter string
            answer = readText[guessValue].ToUpper();

            //split the 5 letter string into 5 individual letter strings (char 1-5) to be used later  
            char1 = Convert.ToString(answer[0]);
            char2 = Convert.ToString(answer[1]);
            char3 = Convert.ToString(answer[2]);
            char4 = Convert.ToString(answer[3]);
            char5 = Convert.ToString(answer[4]);

            //clear list to make sure the last game's data is not used
            AnswerCharacters.Clear();

            //then add the individual letters from the answer word into the list called AnswerCharacters
            AnswerCharacters.Add(char1);
            AnswerCharacters.Add(char2);
            AnswerCharacters.Add(char3);
            AnswerCharacters.Add(char4);
            AnswerCharacters.Add(char5);

            //clear the textboxes in the guessing grid of text
            Inputs.Clear();

            //Re-add all the text boxes to create the grid again
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

            //troubleshooting to find what the answer word is
            //this.Text = $"{answer}";

            
            //Now that the whole guessing grid has been set up, enable the first textbox and change focus to it
            //The cursor and change of focus helps show the user where to start if they are new
            input1.Enabled = true;
            input1.Focus();
        }

        public void GameInitialize()
        {
            //Gets rid of the main menu elements and shifts focus to the first input of the textboxs used to type in a word
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

        private void input1_KeyDown(object sender, KeyEventArgs e)
        {
            //Check if letters are pressed and the index is less than the 30 textboxesand than index does not get 4 larger than indeix
            if ((e.KeyCode == Keys.A || e.KeyCode == Keys.B || e.KeyCode == Keys.C || e.KeyCode == Keys.D || e.KeyCode == Keys.E || e.KeyCode == Keys.F || e.KeyCode == Keys.G || e.KeyCode == Keys.H || e.KeyCode == Keys.I || e.KeyCode == Keys.J || e.KeyCode == Keys.K || e.KeyCode == Keys.L || e.KeyCode == Keys.M || e.KeyCode == Keys.N || e.KeyCode == Keys.O || e.KeyCode == Keys.P || e.KeyCode == Keys.Q || e.KeyCode == Keys.R || e.KeyCode == Keys.S || e.KeyCode == Keys.T || e.KeyCode == Keys.U || e.KeyCode == Keys.V || e.KeyCode == Keys.W || e.KeyCode == Keys.X || e.KeyCode == Keys.Y || e.KeyCode == Keys.Z)
               && (index < 30)
               && (index - lockedIndex < 4))
            {
                //make a textbox object that tracks the value of the textbox that is refrencing the KeyDown events
                TextBox tb = (TextBox)sender;
                
                //increment the index every time a key is pressed
                index = tb.TabIndex + 1;

                //then enable the input textbox that corelates to that index number and then focus on it
                Inputs[index].Enabled = true;
                Inputs[index].Focus();
            }
            //check if backspace is pressed
            else if (e.KeyCode == Keys.Back && index >= lockedIndex)
            {
                
                answerLabel.Visible = false;

                //Clear the input at the current index
                Inputs[index].Clear();
                //decrement the index
                index--;

                //if index becomes negative make it 0 to not crash the program and keep it on the first textbox
                if(index < 0)
                {
                    index = 0;
                }
                
                //Focus on the new index
                Inputs[index].Focus(); //Look at this line

            }
            //check if the enter key is pressed and the index is on the last textbox of each row
            else if ((e.KeyCode == Keys.Enter) && (index == 4 || index == 9 || index == 14 || index == 19 || index == 24 || index == 29))
            {
                //if yes, call upon button click method
                enterButton_Click(sender, e);
            }
        }
        private void enterButton_Click(object sender, EventArgs e)
        {
            //check the index of the textbox when the enter button is pressed
            //the textboxes will only be a value of 4,9,14,19,24,or 29, so only those need to be checked
            if (index == 4)
            {
                //if the index is the end of the first row
                //make the variable called guessAnswer which tracks the player guess words the sum of the letters in the first five textboxes
                guessAnswer = input1.Text + input2.Text + input3.Text + input4.Text + input5.Text;
            }
            else if (index == 9)
            {
                //same as above but for second row
                guessAnswer = input6.Text + input7.Text + input8.Text + input9.Text + input10.Text;
            }
            else if (index == 14)
            {
                //third row
                guessAnswer = input11.Text + input12.Text + input13.Text + input14.Text + input15.Text;
            }
            else if (index == 19)
            {
                //fourth row
                guessAnswer = input16.Text + input17.Text + input18.Text + input19.Text + input20.Text;
            }
            else if (index == 24)
            {
                //fifth row
                guessAnswer = input21.Text + input22.Text + input23.Text + input24.Text + input25.Text;
            }
            else if (index == 29)
            {
                //six and last row
                guessAnswer = input26.Text + input27.Text + input28.Text + input29.Text + input30.Text;
            }
            //then check if the guessAnswer word is in the dictionary list
            if (readText.Contains(guessAnswer.ToLower()))
            {
                //if it is that disable all the previous textboxes to "lock in" your answer
                for (int i = lockedIndex; i <= lockedIndex + 4; i++)
                {
                    Inputs[i].Enabled = false;
                }
                //call upon the functions InputChange and LetterButtonColours 
                InputChange();
                LetterButtonColours();
            }
            //if the word is not in the dictionary list make a label visible with text to tell user to enter a valid word
            else
            {
                answerLabel.Visible = true;
                answerLabel.Text = $"Please enter a valid word";
                //label1.Visible = true;
                //label1.Text = $"Please enter a valid word";
            }

        }
        public void InputChange()
        {
            //increment the guess variable to track how many guesses it take for the player to guess correctly
            //At this point we know the word is a legit word
            guess++;

            //again check the index to figure out what to focus on
            if (index == 4)
            {
                //if it is the first row
                //enable and focus on the first textbox of the second row
                input6.Enabled = true;
                input6.Focus();
            }
            else if (index == 9)
            {
                //same thing for second row to focus on third row
                input11.Enabled = true;
                input11.Focus();
            }
            else if (index == 14)
            {
                //third to fourth row
                input16.Enabled = true;
                input16.Focus();
            }
            else if (index == 19)
            {
                //fourth to fifth row
                input21.Enabled = true;
                input21.Focus();
            }
            else if (index == 24)
            {
                //fifth to last row
                input26.Enabled = true;
                input26.Focus();
            }
            //if it isn't any of the numbers above it has to be the last row
            //So now I call upon the CheckAnswer function to see if the guess is wrong to end the game
            else
            {
                CheckAnswer();
            }

            //Check the row that the guess is on
            
            for (int i = lockedIndex; i <= lockedIndex + 4; i++)
            {
                //if any of the inputs contain the first letter of the guess word make its background yellow
                //problems can arise if they are repeat letters but I did not have time to fix it
                if (Inputs[i].Text == AnswerCharacters[0])
                {
                    Inputs[i].BackColor = Color.Yellow;
                }
                //Same for the second letter of the answer word
                else if (Inputs[i].Text == AnswerCharacters[1])
                {
                    Inputs[i].BackColor = Color.Yellow;
                }
                //check third letter
                else if (Inputs[i].Text == AnswerCharacters[2])
                {
                    Inputs[i].BackColor = Color.Yellow;
                }
                //fourth letter
                else if (Inputs[i].Text == AnswerCharacters[3])
                {
                    Inputs[i].BackColor = Color.Yellow;
                }
                //fifth and last letter
                else if (Inputs[i].Text == AnswerCharacters[4])
                {
                    Inputs[i].BackColor = Color.Yellow;
                }
                //if the inputs have the same letter in the same index space than make the backcolor green to signify a correct guess
                if (Inputs[i].Text == AnswerCharacters[i - lockedIndex])
                {
                    Inputs[i].BackColor = Color.Green;
                }
            }

            //Check if the entire guess word equals the answer word
            if (guessAnswer == answer)
            {
                //change a variable to "Won" used later in paint function
                gameState = "Won";
                //add 1 to streak and gamesWon since the user got it right
                streak++;
                gamesWon++;

                //winpercentage tracks the percent of games the player wins
                //doesn't work properly, rounds to either 0 or 1 which doesn't work
                winPercentage = gamesWon / gamesPlayed * 100;

                //change element visibility
                tryagainButton.Visible = false;
                enterButton.Visible = false;

                //add the number of guesses it took the player to guess correctly into the Guesses list to use later
                Guesses.Add(guess);

                //Check what letters the answer contains
                //and assign a score based on how often it appears in the english language 
                //E has a low score because it is common, Q has a high score because it is rare
                for (int i = 0; i < Letters.Count; i++)
                {
                    if (answer.Contains(Letters[i]))
                    {
                        //add the scores from the list into a variable called scoreSum to be displayed later
                        scoreSum += Scores[i];
                    }
                }

                //check if the scoreSum of the current game is greater than the score from the last game
                
                if (scoreSum > previousScore)
                {
                    //if it is greater make the variable bestScore now equal to scoreSum since it was higher than the previous one
                    bestScore = scoreSum;
                }
                //make previousScore equal to the current scoreSum
                previousScore = scoreSum;

                //display all the stats on the from text, mostly used for troubeshooting
                this.Text = $"Score:{bestScore}, Streak: {streak}, Games:{gamesPlayed}, Win %: {winPercentage}%, #Guesses: {guess}";

                //call upon GameStats function which displays the charts of guess distribution
                GameStats();
            }
            //If the guess word is not the answer word and index is the last index of the guess grid
            else if (index == 29 && guessAnswer != answer)
            {
                //troubleshooting code to set the form text to the answer word
                //this.Text = $"The word was {answer}";
            }

            //lockedIndex now equals index plus 1
            lockedIndex = index + 1;

        }
        public void LetterButtonColours()
        {
            //This function is for the buttons below the guess grid which show which letters have been guessed yet
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
        }
        public void CheckAnswer()
        {
            //called on the last row of the guess grid
            //if any of the text from the textboxes don't equal the answer letters the game is lost
            if (input26.Text != char1 || input27.Text != char2 || input28.Text != char3 || input29.Text != char4 || input30.Text != char5)
            {
                tryagainButton.Visible = false;
                enterButton.Visible = false;

                //set gameState to Lost to be used later
                gameState = "Lost";

                //reset streak to 0 since the player lost
                streak = 0;

                //recalculate winPercentage
                winPercentage = gamesWon / gamesPlayed * 100;

                //display the new stats on the form text
                this.Text = $"Score:{bestScore}, Streak: {streak}, Games:{gamesPlayed}, Win %: {winPercentage}%, #Guesses: {guess}";

                //have text that shows up that tells the player what the answer word was
                guessedcorrectlyLabel.Text = $"\r\nTHE WORD WAS {answer}\r\n\r\n\r\nGUESS DISTRIBUTION";

                //add the number of guesses it took
                //will have to be 6 because that is the only way to lose, but it has to be added to the list
                Guesses.Add(guess);

                //display the game stats
                GameStats();
            }
        }

        private void tryagainButton_Click(object sender, EventArgs e)
        {
            //reset all the tracking variables back to 0 
            index = 0;
            lockedIndex = 0;
            guess = 0;

            //reset all the input backcolor back to black, disable them, and clear the text
            for (int i = 0; i < Inputs.Count; i++)
            {
                Inputs[i].Clear();
                Inputs[i].BackColor = Color.Black;
                Inputs[i].Enabled = false;
            }
            //reset all the letter buttons back to DarkGray
            for (int i = 0; i < LetterButtons.Count; i++)
            {
                LetterButtons[i].BackColor = Color.DarkGray;
            }

            //Enable the first input and focus on it 
            input1.Enabled = true;
            input1.Focus();

            answerLabel.Visible = false;
            answerLabel.Text = "";
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
            //dictionaryListOutput.Text = (Properties.Resources.WordList2);
            dictionaryListOutput.Text = "";

            //dictionaryListOutput.Text = File.AppendAllLines(path, Encoding.UTF8);
            dictionaryListOutput.Text = File.ReadAllText(path);

            //for (int i = 0; i < readText.Count(); i++)
            //{
            //    dictionaryListOutput.Text += $"\n {readText[i]}\n, ";
            //}

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
                if (readText.Contains(addWord))
                {
                    answerLabel.Size = new Size(500, 500);
                    answerLabel.Visible = true;
                    answerLabel.Text = "Word is already in dictionary";
                    this.Text = "Word is already in dictionary";

                }
                else
                {
                    readText.Add(addWord);
                    readText.Sort();
                    File.WriteAllLines(path, readText);

                    dictionaryaddInput.Clear();
                    dictionaryListOutput.Text = File.ReadAllText(path);
                }
               

                //readText = File.ReadAllLines(path, Encoding.UTF8).ToList();

                //readText = File.ReadAllLines(path, Encoding.UTF8).ToList();

                //dictionaryListOutput.Text = File.ReadAllLines(path).ToString();

                //dictionaryListOutput.Text = $"{File.ReadAllLines(path, Encoding.UTF8)}";
                Refresh();

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

            dictionaryListOutput.Text = File.ReadAllText(path);


            Refresh();
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
            //
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