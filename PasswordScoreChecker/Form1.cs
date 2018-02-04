using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasswordScoreChecker
{
    public partial class Form1 : Form
    {
        int Blank;
        int VeryWeak;
        int Weak;
        int Medium;
        int Strong;
        int VeryStrong;

        public Form1()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Create the ToolTip and associate with the Form container.
            ToolTip toolTip1 = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;

            // Set up the ToolTip text for the Button and Checkbox.
            toolTip1.SetToolTip(this.checkBox1, "use this when loading combos for example email:pass , or user:pass");
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Blank = 0;
            VeryWeak = 0;
            Weak = 0;
            Medium = 0;
            Strong = 0;
            VeryStrong = 0;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter =
               "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.InitialDirectory = "C:\\";
            dialog.Title = "Select a text file";
            dialog.Multiselect = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {

                foreach (String f in dialog.FileNames)
                {
                    RatePass(f);
                }


            }
        }



        public void RatePass(String filename)
        {

            // string data = File.ReadAllText(filename);
            string[] dataLine = File.ReadAllLines(filename);

            StringBuilder sb = new StringBuilder();





            for (int i = 0; i < dataLine.Length; i++)
            {
                if (checkBox1.Checked)
                {
                    String[] breakApart = dataLine[i].Split(':');

                    if (CheckStrength(breakApart[1]) == PasswordScore.Blank)
                    {
                        Blank++;
                    }
                    else if (CheckStrength(breakApart[1]) == PasswordScore.VeryWeak)
                    {
                        VeryWeak++;
                    }
                    else if (CheckStrength(breakApart[1]) == PasswordScore.Weak)
                    {
                        Weak++;
                    }
                    else if (CheckStrength(breakApart[1]) == PasswordScore.Medium)
                    {
                        Medium++;
                    }
                    else if (CheckStrength(breakApart[1]) == PasswordScore.Strong)
                    {
                        Strong++;
                    }
                    else if (CheckStrength(breakApart[1]) == PasswordScore.VeryStrong)
                    {
                        VeryStrong++;
                    }
                }
                else
                {

                    if (CheckStrength(dataLine[i]) == PasswordScore.Blank)
                    {
                        Blank++;
                    }
                    else if (CheckStrength(dataLine[i]) == PasswordScore.VeryWeak)
                    {
                        VeryWeak++;
                    }
                    else if (CheckStrength(dataLine[i]) == PasswordScore.Weak)
                    {
                        Weak++;
                    }
                    else if (CheckStrength(dataLine[i]) == PasswordScore.Medium)
                    {
                        Medium++;
                    }
                    else if (CheckStrength(dataLine[i]) == PasswordScore.Strong)
                    {
                        Strong++;
                    }
                    else if (CheckStrength(dataLine[i]) == PasswordScore.VeryStrong)
                    {
                        VeryStrong++;
                    }


                }



            }
            try
            {
                int total = Blank + VeryWeak + Weak + Medium + Strong + VeryStrong;
                int weak = Blank + VeryWeak + Weak;
                int strong = Strong + VeryStrong;
                int weakPercentage = weak / total * 100;
                int mediumPercentage = Medium / total * 100;
                int strongPercentage = strong / total * 100;

                label3.Text = "" + weakPercentage;
                label4.Text = "" + mediumPercentage;
                label6.Text = "" + strongPercentage;
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine(e.Message);
            }
            MessageBox.Show("Blank: " + Blank + "| " + "Very weak: " + VeryWeak + "| " + "weak: " + Weak + "| " + "Medium: " + Medium + "| " + "Strong: " + Strong + "| " + "Very Strong: " + VeryStrong, "Results");
        }



        public enum PasswordScore
        {
            Blank = 0,
            VeryWeak = 1,
            Weak = 2,
            Medium = 3,
            Strong = 4,
            VeryStrong = 5
        }

        public static PasswordScore CheckStrength(string password)
        {
            int score = 0;

            if (password.Length < 1)
                return PasswordScore.Blank;
            if (password.Length < 4)
                return PasswordScore.VeryWeak;

            if (password.Length >= 6)
                score++;
            if (password.Length >= 7)
                score++;
            if (Regex.Match(password, @"/\d+/", RegexOptions.ECMAScript).Success)
                score++;
            if (Regex.Match(password, @"/[a-z]/", RegexOptions.ECMAScript).Success &&
              Regex.Match(password, @"/[A-Z]/", RegexOptions.ECMAScript).Success)
                score++;
            if (Regex.Match(password, @"/.[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]/", RegexOptions.ECMAScript).Success)
                score++;

            return (PasswordScore)score;
        }




    }


}