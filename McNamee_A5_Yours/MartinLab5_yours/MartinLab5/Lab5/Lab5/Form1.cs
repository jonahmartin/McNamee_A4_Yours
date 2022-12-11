using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5
{
    public partial class Form1 : Form
    {
      /*
      Jon McNamee
      Lab 5: Functions and Loops
      Due Date: 6 December 2022
      */
        public Form1()
        {
            InitializeComponent();
            /*sets name of form to include my name, hides groupboxes below login area, generates random number between 100k and 200k 
             places cursor on login code
             */
            this.Text += " " + PROGRAMMER;
            grpChoose.Hide();
            grpStats.Hide();
            grpText.Hide();
            txtCode.Focus();
            lblCode.Text=Convert.ToString(GetRandom(100000,200000));
        }

        //creates constant for my name to be used where needed
        const string PROGRAMMER = "Jon McNamee";

        //sets counter to 0 for the ensuing login count
        int count = 0;

        private int GetRandom(int min, int max) 
        { 
         // function to create random number generator
            Random rand = new Random();
            return rand.Next(min, max);
        }


        //resets text groupbox and values, and places cursor in top textbox
        private void ResetTextGrp()
        {
            txtString1.Text = "";
            txtString2.Text = "";
            chkSwap.Checked = false;
            lblResults.Text = "";
            txtString1.Focus();
        }

        //resets stat groupbox and values
        private void ResetStatsGrp()
        {
            numStats.Value = 10;
            lblSum.Text = "";
            lblMean.Text = "";
            lblOdd.Text = "";
            lstNumbers.Items.Clear();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            /*
             Checks that the input in the textbox matches random code in label, if it does login is successful and grants access to rest of program.
            If input does not match code, increments counter and displays msg with attempts shown. Closes program after 3 unsuccessful attempts. */

            if (txtCode.Text == lblCode.Text) 
            {
                grpLogin.Enabled=false;
                grpChoose.Show();

            }
            else
            {
                count++;

                MessageBox.Show(Convert.ToString(count) + " incorrect code(s) entered.\nTry again - only 3 attempts allowed", PROGRAMMER);
               
                if(count == 3)
                {
                    MessageBox.Show("3 attempts to log in\nAccount locked - closing program", PROGRAMMER);
                    this.Close();
                }
            }
        }
        //loads the appropriate grpboxes based on which radio button is checked by user
        private void SetupOption()
        {
            if (radText.Checked)
            {
                grpText.Show();
                ResetStatsGrp();
                grpStats.Hide();
                this.AcceptButton = btnJoin;
                this.CancelButton = btnReset;
            }
            else if(radStats.Checked)
            { 
                grpStats.Show();
                ResetTextGrp();
                grpText.Hide();
                this.AcceptButton = btnGenerate;
                this.CancelButton = btnClear;
            }

        }

        private void radText_CheckedChanged(object sender, EventArgs e)
        {
            SetupOption();
        }

        private void radStats_CheckedChanged(object sender, EventArgs e)
        {
            SetupOption();

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetTextGrp();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetStatsGrp();
        }
        //swaps values inputted into txtstring1 and 2, and redisplays label with new values
        private void Swap(ref string stringFirst, ref string stringSecond)
        {
            stringFirst = txtString1.Text;
            stringSecond = txtString2.Text;

            string storage = txtString1.Text;
            stringFirst = txtString2.Text;
            stringSecond = storage;

            txtString1.Text = stringFirst;
            txtString2.Text = stringSecond;
        }

        //function to check that there is text input in both textboxes. Returns false if either box has no input
        private bool CheckInput()
        {
            bool isValid;
            if(txtString1.Text == "" || txtString2.Text == "")
            {
                isValid = false;
            }
            else 
            {
                isValid = true; 
            }
            return isValid;
        }

        private void chkSwap_CheckedChanged(object sender, EventArgs e)
        {
            bool check = CheckInput();
            if(check==true) 
            {
                string stringFirst = txtString1.Text;
                string stringSecond = txtString2.Text;

                Swap(ref stringFirst, ref stringSecond);
                lblResults.Text = "strings have been Swapped!";
            }
        }

        //checks that there has been input into both textboxes, then displays results in label displaying values
        private void btnJoin_Click(object sender, EventArgs e)
        {
            string stringFirst = txtString1.Text;
            string stringSecond = txtString2.Text;

            bool check = CheckInput();
            if(check == true)
            {
                lblResults.Text = "First String = "+stringFirst+"\nSecond String = "+stringSecond+"\nJoined = "+stringFirst+"-->"+stringSecond;
            }
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            string stringFirst = txtString1.Text;
            string stringSecond = txtString2.Text;

            bool check = CheckInput();
            if(check == true)
            {
                lblResults.Text = "First String = " + stringFirst + "\nCharacters = "+stringFirst.Length+"\nSecond String = " + stringSecond + "\nCharacters = "+ stringSecond.Length;
            }
        }

        //performs a while loop to add up sum of listbox content, returns value for use in form
        private int AddList()
        {
            count = 0;
            int Sum = 0;

            while (lstNumbers.Items.Count>count)
            { 
                Sum += Convert.ToInt32(lstNumbers.Items[count++]);
            }

            return Sum;
        }

        //counts the number of odd numbers and returns the value for use in form
        private int CountOdd()
        {
            int count = 0;
            int odd = 0;
            
            do
            {
                if (Convert.ToInt32(lstNumbers.Items[count]) % 2 != 0)
                {
                    odd++;
                }
                count++;
            } while (count<lstNumbers.Items.Count);
            return odd;
        }
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            
            lstNumbers.Items.Clear();
            int value = Convert.ToInt32(numStats.Value);

            Random rand = new Random(733); //seed value 733

            for(int count = 0;  count<value; count ++)
            {
                lstNumbers.Items.Add(rand.Next(1000,5001));
            }
            double Sum = AddList();

            lblSum.Text = Sum.ToString("N0");

            double avg;

            avg = Sum / lstNumbers.Items.Count;

            lblMean.Text = avg.ToString("N");

            int odd=CountOdd();
            lblOdd.Text=odd.ToString();
        }
    }
}
