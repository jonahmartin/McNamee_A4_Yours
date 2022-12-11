using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       /* Name: Jonah Martin
        * Date: December 6, 2022
        * Description: This form performs 3 main tasks. Firstly it generates
        * a randomized login code for the user to input so they can access the rest of the form.
        * Then there is a string-swapping groupbox where the user inputs 2 strings and the program
        * swaps them. And finally there is a stats groupbox where the user inputs a number and
        * the program runs some calculations for sum, mean, and the amount of odd numbers */

        /* Name: GetRandom
         * Description: generates random number between a min & max values (no seed)
         * In: 2 ints (min, max)
         * Out: int */
        private int GetRandom(int MIN, int MAX)
        {
            Random randNum = new Random();
            return randNum.Next(MIN, MAX + 1);
        }

        /* This event will run on form load, and it sets the PROGRAMMER constant
         * to display in the form title, hides the choose, stats, and text groupboxes,
         * and generates a random number that the user will use in the following event. */
        const string PROGAMER = "Jonah Martin";
        private void Form1_Load(object sender, EventArgs e)
        {
             this.Text += " " + PROGRAMMER;

             grpChoose.Hide();
             grpStats.Hide();
             grpText.Hide();
             txtCode.Focus();
             const int MIN = 100000;
             const int MAX = 200000;

             int num = GetRandom(MIN, MAX + 1);
             lblCode.Text = num.ToString();
        }

        /* Checks if the number entered is equal to the randomly generated code.
         * Also will track how many times the button has been clicked so that after the maximum
         * amount of attempts has been reached, the program will shut off. Otherwise if the input
         * matches the randomly generated code, the choose group box will be shown.*/
        int attemptCount = 0;
        private void btnLogin_Click(object sender, EventArgs e)
        {
            attemptCount++;
            const int ATTEMPTS = 3;
            if (Convert.ToInt32(lblCode.Text) != Convert.ToInt32(txtCode.Text))
            {

                if (attemptCount == 1)
                {
                    MessageBox.Show("3 attempts to login\nAccount locked - closing program", PROGRAMMER);
                    this.Close();
                }
                else
                {
                    MessageBox.Show(attemptCount + " incorrect code(s) entered\nTry again - only " + ATTEMPTS + " attempts allowed", PROGRAMMER);
                    txtCode.Focus();
                }
            }
            else
            {
                grpChoose.Show();
                grpLogin.Enabled = false;
            }
        }
        /* Name: ResetTextGrp
         * Description: Resets all controls within the text groupbox to default values.
         * Also changes the accept/cancel buttons of the form to be associated with the text groupbox.
         * In: nothing
         * Out: void */
        private void ResetTextGrp()
        {
            txtString1.Text = "";
            txtString1.Focus();
            txtString2.Text = "";
            chkSwap.Checked = false;
            lblResults.Text = "";
            this.AcceptButton = btnJoin;
            this.CancelButton = btnReset;
        }
        /* Name: ResetStatsGrp
         * Description: Resets all controls within the stats groupbox to default values.
         * Also changes the accept/cancel buttons of the form to be associated with the stats groupbox.
         * In: nothing
         * Out: void */
        private void ResetStatsGrp()
        {
            nudNumber.Value = 10;
            lblSum.Text = "";
            lblMean.Text = "";
            lblOdd.Text = "";
            lstNumbers.Items.Clear();
            this.AcceptButton = btnGenerate;
            this.CancelButton = btnClear;
        }
        /* Name: SetupOption
         * Description: displays/hides the text and stats groupboxes based on which radio button has been checked.
         * In: nothnig
         * Out: void */
        private void SetupOption()
        {
            switch (radText.Checked)
            {
                case true:
                        ResetTextGrp();
                        grpText.Show();
                        txtString1.Focus();
                        grpStats.Hide();
                        break;
                    

                case false:
                        ResetStatsGrp();
                        grpStats.Show();
                        grpText.Hide();
                        break;
            }
        }
        // runs the SetupOption function when Text radio button has been checked
        private void radText_CheckedChanged(object sender, EventArgs e)
        {
            SetupOption();
        }
        // runs the SetupOption function when Stats radio button has been checked
        private void radStats_CheckedChanged(object sender, EventArgs e)
        {
            SetupOption();
        }
        // runs the ResetTextGrp funtion when the Reset button is clicked
        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetTextGrp();
        }
        // runs the ResetTextGrp funtion when the Clear button is clicked
        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetStatsGrp();
        }

        /* Name: Swap
         * Description: swaps the location of the inputed strings in RAM memory
         * In: 2 strings (string1, string2)
         * Out: void */
        private void Swap(ref string string1, ref string string2)
        {
            string temp = txtString1.Text;
            string1 = txtString2.Text;
            string2 = temp;
        }
        /* Name: CheckInput
         * Description: checks whether data has been entered into textboxes
         * In: Nothing
         * Out: Boolean */
        private bool CheckInput()
        {
            bool input;
            if (txtString1.Text != "" && txtString2.Text != "")
            {
                input = true;
            }
            else
            {
                input = false;
            }

            return input;
        }
        /* runs CheckInput function to see if textboxes have data. If they do
         * it runs the Swap function to swap the 2 strings and displays a
         * message in the results label. All of this is done upon checking
         * the swap checkbox*/
        private void chkSwap_CheckedChanged(object sender, EventArgs e)
        {
            bool input = CheckInput();
            if (input)
            {
                string string1 = txtString1.Text;
                string string2 = txtString2.Text;
                Swap(ref string1, ref string2);
                txtString1.Text = string1;
                txtString2.Text = string2;
                lblResults.Text = "Strings have been swapped!";
            }
        }
        // joins the 2 strings in the results label if there is data upon activating the Join button
        private void btnJoin_Click(object sender, EventArgs e)
        {
            bool input = CheckInput();
            if (input)
            {
                lblResults.Text = "First string = " + txtString1.Text + "\nSecond String = " + txtString2.Text + "\nJoined = " + txtString1.Text + "-->" + txtString2.Text;
            }
        }
        // if there is input, analyzes the 2 strings and displays how many characters are in each
        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            bool input = CheckInput();
            if (input)
            {
                lblResults.Text = "First string = " + txtString1.Text + "\n Characters = " + txtString1.TextLength + "\nSecond string = " + txtString2.Text + "\n Characters = " + txtString2.TextLength;
            }
        }
        /* Generates new random numbers (with a seed & between a min/max value) and 
         * displays them in the listbox. The amount of numbers in the list is
         * equal to the number in the numeric updown control. Then it calculates
         * the sum, the mean, and the amount of odd numbers in the list and displays
         * them in their respective labels */
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            lstNumbers.Items.Clear();
            const int MAX = 5000;
            const int MIN = 1000

            // generates random number 
            Random rand = new Random(733);

            // adds the random numbers to the listbox
            for (int i = 0; i < nudNumber.Value; i++)
            {
                lstNumbers.Items.Add(rand.Next(MIN, MAX + 1));
            }
            // calls AddList function which sums up the numbers in listbox 
            int sum = AddList();
            // displays sum in respective label //
            lblSum.Text = sum.ToString("#,###");
            // calculates and displays mean in respective label
            lblMean.Text = (sum / nudNumber.Value).ToString("#,###.#0");
            //calls oddNumbers function to calculate how many odd #s there are and display them
            int oddNumbers = CountOdd();
            lblOdd.Text = oddNumbers.ToString();
        }

        /* Name: AddList
         * Description: sums up the numbers in the listbox and returns the value
         * In: Nothing
         * Out: int */
        private int AddList()
        {
            int i = 0;
            int sum = 0;
            
            while (i < lstNumbers.Items.Count)
            {
                sum += Convert.ToInt32(lstNumbers.Items[i]);
                i++;
            }
            return sum;
        }
        /* Name: CountOdd
         * Description: calculates the number of odd numbers in the listbox and returns the value
         * In: Nothing
         * Out: int */
        private int CountOdd()
        {
            int oddNumbers = 0;
            do
            {
                if (Convert.ToInt32(lstNumbers.Items[i]) % 2 == 1)
                {
                    oddNumbers++;
                }

                i++;
            }
            while (i < lstNumbers.Items.Count);
            return oddNumbers;
        }
    }

}

