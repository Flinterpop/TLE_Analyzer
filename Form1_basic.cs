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

            telGridView1.PopulateGrid(rtb_Input);

            Line1 = rtb_Input.Lines[0];
            Line2 = rtb_Input.Lines[1];
            Line3 = rtb_Input.Lines[2];
            parseLines();
            CheckAge();
            ValidateLine1();
        }

        private void bn_Quit_Click(object sender, EventArgs e)
        {
            Close();
        }

     

        private void bn_map_Click(object sender, EventArgs e)
        {
            telGridView1.PopulateGrid(rtb_Input);
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
            else if (findme == "33275")
                return "AMC-21";
            else if (findme == "39127")
                return "ANIK-G1";
            else if (findme == "33055")
                return "Skynet5C";
            else if (findme == "20776")
                return "SkyNet4C";
            else if (findme == "25544")
                return "ISS (ZARYA)";
            return "Unknown";
        }



        private void bn_ClearDebug_Click(object sender, EventArgs e)
        {
            tb_debug.Clear();
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
            parseLines();
            CheckAge();
            ValidateLine1();
        }

        private void rtb_Input_TextChanged(object sender, EventArgs e)
        {
            if (!pasting)
            {
                telGridView1.PopulateGrid(rtb_Input);
                parseLines();
                CheckAge();
                ValidateLine1();
            }
            rtb_Input.SelectAll();
            rtb_Input.SelectionFont = new Font("Courier New", 10);
            rtb_Input.Select(0, 0);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string myItem = comboBox1.SelectedItem.ToString();
            debug(myItem);
            pasting = true;

            if (myItem=="AMC-21")
            {
                rtb_Input.Text = "AMC-21" + "\r\n";
                rtb_Input.AppendText("1 33275U 08038B   18051.47253781  .00000030  00000-0  00000-0 0  9994" + "\r\n");
                rtb_Input.AppendText("2 33275   0.0670 291.7242 0002687  48.1024 215.7178  1.00273287 34990" + "\r\n");
            }
            else if (myItem == "ANIK-G1")
            {
                rtb_Input.Text = "ANIK-G1" + "\r\n";
                rtb_Input.AppendText("1 39127U 13014A   18051.87922665 -.00000080  00000-0  00000+0 0  9999" + "\r\n");
                rtb_Input.AppendText("2 39127   0.0126 210.0460 0002977 101.2151  48.7132  1.00269766 17798" + "\r\n");
            }
            else if (myItem == "ANIK-G1 Test1")
            {
                rtb_Input.Text = "ANIK-G1 Test1" + "\r\n";
                rtb_Input.AppendText("1 39127U 13014A   18051.87922665 -.00000080  00000-0  00000+0 0  9998" + "\r\n");
                rtb_Input.AppendText("2 39127   0.0126 210.0460 0002977 101.2151  48.7132  1.00269766 17798" + "\r\n");
            }
            else if (myItem == "ANIK-G1 Test2")
            {
                rtb_Input.Text = "ANIK-G1 Test2" + "\r\n";
                rtb_Input.AppendText("1 39127U 13014A   18051.87922665 -.00000080  00000-0  00000+0 0  9999" + "\r\n");
                rtb_Input.AppendText("2 39127   0.0126 210.0460 0002977 101.2151  48.7132  1.00269766 17792" + "\r\n");
            }
            else if (myItem == "ANIK-G1 Test3")
            {
                rtb_Input.Text = "ANIK-G1 Test2" + "\r\n";
                rtb_Input.AppendText("1 39127U 13014A   18051.87922665 -.00000080  00000-0 00000+0 0 9999" + "\r\n");
                rtb_Input.AppendText("2 39127   0.0126 210.0460 0002977 101.2151  48.7132  1.00269766 17798" + "\r\n");
            }
            else if (myItem == "Skynet4C")
            {
                rtb_Input.Text = "Skynet4C" + "\r\n";
                rtb_Input.AppendText("1 20776U 90079A   18051.52709275  .00000127  00000-0  00000-0 0  9999" + "\r\n");
                rtb_Input.AppendText("2 20776  13.2973  24.2718 0002466  19.7004 265.1058  1.00273927 90166" + "\r\n");
            }

            else if (myItem == "Skynet5C")
            {
                rtb_Input.Text = "Skynet5C" + "\r\n";
                rtb_Input.AppendText("1 33055U 08030A   18051.90700338 -.00000146  00000-0  00000-0 0  9994" + "\r\n");
                rtb_Input.AppendText("2 33055   0.0620   3.0412 0003321 328.4265 127.9780  1.00271520 35498" + "\r\n");
            }
            else if (myItem == "ISS (ZARYA)")
            {
                rtb_Input.Text = "ISS (ZARYA)" + "\r\n";
                rtb_Input.AppendText("1 25544U 98067A   18051.96457625  .00002577  00000-0  46169-4 0  9990" + "\r\n");
                rtb_Input.AppendText("2 25544  51.6416 238.1089 0003437 117.8478 357.3261 15.54125912100440" + "\r\n");
            }

            pasting = false;

            telGridView1.PopulateGrid(rtb_Input);
            parseLines();
            CheckAge();
            ValidateLine1();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AboutBox1 Abox = new AboutBox1();
            Abox.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            telGridView1.clearGrid();
            ClearDecodedLines();
        }

        private void bn_ValidateLines_Click(object sender, EventArgs e)
        {
            ValidateLine1();
        }

        private void bn_Load_Click(object sender, EventArgs e)
        {
            // Displays an OpenFileDialog so the user can select a Cursor.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "TLE Files|*.tle";
            openFileDialog1.Title = "Select a TLE File";
            openFileDialog1.InitialDirectory = "C:\\tle";

            // Show the Dialog.
            // If the user clicked OK in the dialog and
            // a .CUR file was selected, open it.

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);

                //MessageBox.Show(sr.ReadToEnd());
                rtb_Input.Text = sr.ReadToEnd();

                sr.Close();
            }


        }

        private void bn_saveTLE_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog so the user can save the Image  
            // assigned to Button2.  
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "TLE File|*.tle|text file|*.txt";
            saveFileDialog1.Title = "Save a TLE File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (saveFileDialog1.FileName != "")
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFileDialog1.FileName);
                sw.Write(rtb_Input.Text.ToString());
                sw.Close();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (lineOneGoodChecksum == false)
            {
                Line2 = Line2.Substring(0, 68) + Line1CSum.ToString();
            }

            if (lineTwoGoodChecksum == false)
            {
                Line3 = Line3.Substring(0, 68) + Line2CSum.ToString();
            }
            rtb_Input.Text = Line1 + "\r\n" + Line2 + "\r\n"  + Line3 + "\r\n";


        }
    }
}
