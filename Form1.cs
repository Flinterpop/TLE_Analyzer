using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Wintellect.PowerCollections;
using PIEBALD.Types;//https://www.codeproject.com/Articles/16459/A-Set-class


namespace TLE_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            telGridView1.clearGrid();
            telGridView1.PopulateGrid(rtb_Input);

        }

        private void bn_Quit_Click(object sender, EventArgs e)
        {
            Close();
        }

     

        private void bn_map_Click(object sender, EventArgs e)
        {
            telGridView1.PopulateGrid(rtb_Input);
        }




        private void parseLine1()
        {
            if (Line2.Length < 69)
            { 
                debug("Line 1 too short: Length is " + Line2.Length.ToString() + " but should be 70.");
                labelL1OK.Text = "Line 1 too short: Length is " + Line2.Length.ToString() + " but should be 70.";
                labelL1OK.ForeColor = Color.Red;

            }

            else
            {
                labelL1OK.Text = "Okay";
                labelL1OK.ForeColor = Color.Black;
                tbSatNum.Text = Line2.Substring(2, 5);
                tb_Classification.Text = Line2.Substring(7, 1);
                tb_Launch.Text = Line2.Substring(9, 2);
                tb_LaunchNum.Text = Line2.Substring(11, 3);
                tb_LaunchPiece.Text = Line2.Substring(14, 3);
                tb_year.Text = Line2.Substring(18, 2);
                tb_Day.Text = Line2.Substring(20, 12);
                tb_FirstTimeD.Text = Line2.Substring(33, 10);
                tb_SecTimeD.Text = Line2.Substring(44, 8);
                tb_BSTAR.Text = Line2.Substring(53, 8);
                tb_AlwaysZero.Text = Line2.Substring(62, 1);
                tb_ElementSet.Text = Line2.Substring(64, 4);
                tb_CheckSumFromFile.Text = Line2.Substring(68, 1);

                string dbSatName = lookupSatName(tbSatNum.Text);
                rtb_SatName.Text = dbSatName;

                int ccSum = CalcChecksum(Line2);
                tb_CheckSumCalc.Text = ccSum.ToString() + " -> " + (ccSum % 10).ToString();

                int ii;
                if (int.TryParse(tb_CheckSumFromFile.Text, out ii))
                    if (ccSum%10 != ii)
                    {
                        debug("Line 1 CheckSum does not match" + ii.ToString());
                        label_Line1CheckSum.Text = "CheckSum does not match";
                        label_Line1CheckSum.ForeColor = Color.Red;
                        telGridView1.Rows[1].Cells[68].Style.BackColor = Color.Red;

                    }
                    else label_Line1CheckSum.Text = "";

            }


            if (Line3.Length < 69)
            { 
                debug("Line 2 too short: Length is " + Line3.Length.ToString() + " but should be 70.");
                labelL2OK.Text = "Line 2 too short: Length is " + Line3.Length.ToString() + " but should be 70.";
                labelL2OK.ForeColor = Color.Red;

            }

            else
            {
                labelL2OK.Text = "Okay";
                labelL2OK.ForeColor = Color.Black;
                tbSatNum.Text = Line2.Substring(2, 5);
                tb_satnum2.Text = Line3.Substring(2, 5);
                tb_inclination.Text = Line3.Substring(8, 8);
                tb_rightAscension.Text = Line3.Substring(17, 8);
                tb_Eccentricity.Text = Line3.Substring(26, 7);
                tb_argument.Text = Line3.Substring(34, 8);
                tb_meanAnomaly.Text = Line3.Substring(43, 8);
                tb_meanMotion.Text = Line3.Substring(52, 11);
                tb_revNumber.Text = Line3.Substring(63, 5);
                tb_CheckSumFromFile2.Text = Line3.Substring(68, 1);

                int ccSum = CalcChecksum(Line3);
                tb_CheckSumCalc2.Text = ccSum.ToString() + " -> " + (ccSum % 10).ToString();

                int ii;
                if (int.TryParse(tb_CheckSumFromFile2.Text, out ii))
                    if (ccSum % 10 != ii)
                    {
                        debug("Line 2 CheckSum does not match" + ii.ToString());
                        label_Line2CheckSum.Text = "CheckSum does not match";
                        label_Line2CheckSum.ForeColor = Color.Red;
                        telGridView1.Rows[2].Cells[68].Style.BackColor = Color.Red;
                    }
                    else label_Line2CheckSum.Text = "";
            }
        }
        string lookupSatName(string findme)
        {
            debug("Looking for " + findme);

            if (findme == "32258")
                return "WGS1";
            else if (findme == "34713")
                return "WGS2";
            else if (findme == "36108")
                return "WGS3";
            else if (findme == "38070")
                return "WGS4";
            else if (findme == "39168")
                return "WGS5";
            else if (findme == "39222")
                return "WGS6";
            else if (findme == "40746")
                return "WGS7";
            else if (findme == "33055")
                return "Skynet5C";
            else if (findme == "20776")
                return "SkyNet4C";
            else if (findme == "25544")
                return "ISS (ZARYA)";
            return "Unknown";
        }


        void debug(string a)
        {
            if(checkBox1.Checked)
                tb_debug.AppendText(a + "\r\n");
        }

        void debugNoCR(string a)
        {

            if(checkBox1.Checked)
                tb_debug.AppendText(a);
        }

        private void bn_ParseLine1_Click(object sender, EventArgs e)
        {
            ParseLine1Sub();

        }

        void ParseLine1Sub()
        {
            parseLine1();

            int iDayOfYear = System.DateTime.UtcNow.DayOfYear;
            int iYear = System.DateTime.UtcNow.Year-2000;

            int ii;
            if (int.TryParse(tb_year.Text, out ii))
            {
                debug(ii.ToString());
            }
            if (ii!=iYear)
            {
                debug("TLE from last calendar year");
                label_lastYear.Text= "TLE from last calendar year";
            }

            else
            {
                label_lastYear.Text = "TLE from this calendar year";
                float f = 0;
                if (float.TryParse(tb_Day.Text, out f))
                {
                    int x = (int)f;

                    // you know that the parsing attempt
                    // was successful
                    debug("Day of year from TLE:" + x.ToString());
                    debug("Age of TLE: " + (iDayOfYear - x).ToString());
                    tb_Age.Text = (iDayOfYear - x).ToString();
                }
                else
                    debug("not a number");
            }

        }

        
        private int CalcChecksum(string lineIn)
        {
            int charCount = 0;
            int sum = 0;
            string line = lineIn.Remove(lineIn.Length - 1, 1);//Don't count the last character because it should be the checksum
            foreach (char c in line)
            {
                charCount++;

                debugNoCR(charCount.ToString() + " is " + c.ToString() + " add ");

                double d = Char.GetNumericValue(c);
                if (d < 0) d = 0;
                if (d > 9) d = 0;
                if (c == '-') d = 1;
                sum += (int)d;

                debug(d.ToString() + "    -    " + sum.ToString());
            }

            debug("Summed this many characters:"+ charCount.ToString());
            return sum;

        }

        private void rtb_Input_TextChanged(object sender, EventArgs e)
        {
            if (!pasting)
            {
                telGridView1.PopulateGrid(rtb_Input);
                //telGridView1.ParseLine1Sub();

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string myItem = comboBox1.SelectedItem.ToString();
            debug(myItem);
            pasting = true;

            if (myItem=="WGS1")
            {
                rtb_Input.Text = "WGS1" + "\r\n";
                rtb_Input.AppendText("1 32258U 07046A   16117.29927109 +.00000013 +00000-0 +00000-0 0  0685" + "\r\n");
                rtb_Input.AppendText("2 32258  00.0044 174.9093 0000421 156.2113 166.3363  1.00273548 03119" + "\r\n");
            }

            else if (myItem == "WGS3")
            {
                rtb_Input.Text = "WGS3" + "\r\n";
                rtb_Input.AppendText("1 36108U 09068A 17212.84279005 -.00000109 +00000-0 +00000-0 0 0001" + "\r\n");
                rtb_Input.AppendText("2 36108 000.0176 076.6473 0000158 034.2273 130.1363 01.0027308501409" + "\r\n");
            }

            else if (myItem == "WGS5")
            {
                rtb_Input.Text = "WGS5" + "\r\n";
                rtb_Input.AppendText("1 39168U 13024A   16117.82588814 -.00000289 +00000-0 +00000-0 0  0672" + "\r\n");
                rtb_Input.AppendText("2 39168  00.0207 240.4123 0000452 154.9994 064.6452  1.00272202	01069" + "\r\n");
            }
            else if (myItem == "WGS6")
            {
                rtb_Input.Text = "WGS6" + "\r\n";
                rtb_Input.AppendText("1 39222U 13041A   16112.23022950 +.00000077 +00000-0 +00000-0 0  0001" + "\r\n");
                rtb_Input.AppendText("2 39222  00.0180 249.7826 0000492 205.7339 061.8882  1.00271618 00886" + "\r\n");
            }

            else if (myItem == "Skynet4C")
            {
                rtb_Input.Text = "Skynet4C" + "\r\n";
                rtb_Input.AppendText("1 20776U 90079A   15119.25938941 -.00000038  00000-0  00000+0 0  9996" + "\r\n");
                rtb_Input.AppendText("2 20776  13.2973  24.2718 0002466  19.7004 265.1058  1.00273927 90166" + "\r\n");
            }

            else if (myItem == "Skynet5C")
            {
                rtb_Input.Text = "Skynet5C" + "\r\n";
                rtb_Input.AppendText("1 33055U 08030A   16280.84125925 -.00000148  00000-0  00000+0 0  9993" + "\r\n");
                rtb_Input.AppendText("2 33055   0.0616   7.0820 0004093 187.4976 106.3979  1.00272692 30395" + "\r\n");
            }
            else if (myItem == "ISS")
            {
                rtb_Input.Text = "ISS (ZARYA)" + "\r\n";
                rtb_Input.AppendText("1 25544U 98067A   08264.51782528 -.00002182  00000-0 -11606-4 0  2927" + "\r\n");
                rtb_Input.AppendText("2 25544  51.6416 247.4627 0006703 130.5360 325.0288 15.72125391563537" + "\r\n");
            }

            pasting = false;

            telGridView1.PopulateGrid(rtb_Input);
            //telGridView1.ParseLine1Sub();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AboutBox1 Abox = new AboutBox1();
            Abox.ShowDialog();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            telGridView1.clearGrid();
        }

        private void bn_ClearDebug_Click(object sender, EventArgs e)
        {
            tb_debug.Clear();
        }

        private void bn_ValidateLines_Click(object sender, EventArgs e)
        {
            ValidateLine1();
        }


        void ValidateLine1()
        {
            debug(Line2);
            for (int x =0;x<Line2.Length;x++)
            {
                if (Line1Blanks.Contains(x))
                {
                    if (Line2[x-1] != ' ')
                        debug("Expected space at location " + x.ToString() + " is not blank");
                }
            }

            debug(Line3);
            for (int x = 0; x < Line3.Length; x++)
            {
                if (Line2Blanks.Contains(x))
                {
                    if (Line3[x - 1] != ' ')
                        debug("Expected space at location " + x.ToString() + " is not blank");
                }
            }
        }

    }
}
