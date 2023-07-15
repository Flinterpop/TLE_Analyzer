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

        protected bool lineOneGoodChecksum, lineTwoGoodChecksum;
        protected int Line1CSum, Line2CSum;

        protected void CheckAge()
        {
            //Check age of TLE
            int iDayOfYear = System.DateTime.UtcNow.DayOfYear;
            int iYear = System.DateTime.UtcNow.Year - 2000;
            tb_Today.Text = "Day " + iDayOfYear.ToString() + " of year "+ iYear.ToString();

            int ii;
            if (int.TryParse(tb_year.Text, out ii))
            {
                debug(ii.ToString());
            }
            if (ii != iYear)
            {
                debug("TLE from last calendar year");
                label_lastYear.Text = "TLE from last calendar year";
                tb_Age.Text = "";
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


        protected void parseLines()
        {
            Line1 = rtb_Input.Lines[0];
            Line2 = rtb_Input.Lines[1];
            Line3 = rtb_Input.Lines[2];

            ClearDecodedLines();

            if (Line2.Length > 69)
            {
                debug("Line 1 is too long: Length is " + Line2.Length.ToString() + " but should be 69.");
                labelL1OK.Text = "Line 1 too long: Length is " + Line2.Length.ToString() + " but should be 69.";
                labelL1OK.ForeColor = Color.Red;
            }
            else if (Line2.Length < 69)
            {
                debug("Line 1 is too short: Length is " + Line2.Length.ToString() + " but should be 69.");
                labelL1OK.Text = "Line 1 is too short: Length is " + Line2.Length.ToString() + " but should be 69.";
                labelL1OK.ForeColor = Color.Red;
            }
            else
            {
                labelL1OK.Text = "Okay: Line length " + Line2.Length.ToString() + " chars";
                labelL1OK.ForeColor = Color.Black;
            }

            try //always try to parse
            {

                tbSatNum.Text = Line2.Substring(2, 5);
                string dbSatName = lookupSatName(tbSatNum.Text);
                rtb_SatName.Text = dbSatName;

                int ccSum = CalcChecksum(Line2);
                tb_CheckSumCalc.Text = ccSum.ToString() + " -> " + (ccSum % 10).ToString();

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

                lineOneGoodChecksum = true;
                lineTwoGoodChecksum = true;



                int ii;
                if (int.TryParse(tb_CheckSumFromFile.Text, out ii))
                    if (ccSum % 10 != ii)
                    {
                        Line1CSum = ccSum % 10;

                        lineOneGoodChecksum = false;
                        debug("Line 1 CheckSum does not match" + ii.ToString());
                        label_Line1CheckSum.Text = "CheckSum does not match";
                        label_Line1CheckSum.ForeColor = Color.Red;
                        telGridView1.Rows[1].Cells[68].Style.BackColor = Color.Red;
                    }
                    else label_Line1CheckSum.Text = "";

            }
            catch (Exception e)  {}


            if (Line3.Length < 69)
            {
                debug("Line 2 is too short: Length is " + Line3.Length.ToString() + " but should be 69.");
                labelL2OK.Text = "Line 2 is too short: Length is " + Line3.Length.ToString() + " but should be 69.";
                labelL2OK.ForeColor = Color.Red;
            }
            else if (Line3.Length < 69)
            {
                debug("Line 2 is too short: Length is " + Line3.Length.ToString() + " but should be 69.");
                labelL2OK.Text = "Line 2 is too short: Length is " + Line3.Length.ToString() + " but should be 69.";
                labelL2OK.ForeColor = Color.Red;
            }
            else
            {
                labelL2OK.Text = "Okay: Line length " + Line3.Length.ToString() + " chars";
                labelL2OK.ForeColor = Color.Black;
            }

            try
            {
                int ccSum = CalcChecksum(Line3);
                tb_CheckSumCalc2.Text = ccSum.ToString() + " -> " + (ccSum % 10).ToString();

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

                int ii;
                if (int.TryParse(tb_CheckSumFromFile2.Text, out ii))
                    if (ccSum % 10 != ii)
                    {
                        Line2CSum = ccSum % 10;
                        lineTwoGoodChecksum = false;
                        debug("Line 2 CheckSum does not match" + ii.ToString());
                        label_Line2CheckSum.Text = "CheckSum does not match";
                        label_Line2CheckSum.ForeColor = Color.Red;
                        telGridView1.Rows[2].Cells[68].Style.BackColor = Color.Red;
                    }
                    else label_Line2CheckSum.Text = "";
            }
            catch (Exception e) { }

            if ((lineOneGoodChecksum==false) || (lineTwoGoodChecksum==false))
            {
                bn_Fix.Visible = true;
            }
            else bn_Fix.Visible = false;



        }


        protected void ClearDecodedLines()
        {
            tbSatNum.Text = "";
            tb_Classification.Text = "";
            tb_Launch.Text = "";
            tb_LaunchNum.Text = "";
            tb_LaunchPiece.Text = "";
            tb_year.Text = "";
            tb_Day.Text = "";
            tb_FirstTimeD.Text = "";
            tb_SecTimeD.Text = "";
            tb_BSTAR.Text = "";
            tb_AlwaysZero.Text = "";
            tb_ElementSet.Text = "";
            tb_CheckSumFromFile.Text = "";
            rtb_SatName.Text = ""; ;
            tb_CheckSumCalc.Text = "";
            label_Line1CheckSum.Text = "";


            labelL2OK.Text = "";
         
            tbSatNum.Text = "";
            tb_satnum2.Text = "";
            tb_inclination.Text = "";
            tb_rightAscension.Text = "";
            tb_Eccentricity.Text = "";
            tb_argument.Text = "";
            tb_meanAnomaly.Text = "";
            tb_meanMotion.Text = "";
            tb_revNumber.Text = "";
            tb_CheckSumFromFile2.Text = "";

            tb_CheckSumCalc2.Text = "";
            label_Line2CheckSum.Text = "";
        }



        protected int CalcChecksum(string lineIn)
        {
            int charCount = 0;
            int sum = 0;
            string line = lineIn;
            if (lineIn.Length==69)
                line = lineIn.Remove(lineIn.Length - 1, 1);//Don't count the last character because it should be the checksum

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

            debug("Summed this many characters:" + charCount.ToString());
            return sum;

        }


        protected void ValidateLine1()
        {
            debug(Line2);

            CheckAge();

            telGridView1.CheckValidLocations(tb_debug);
        }

    }
}


