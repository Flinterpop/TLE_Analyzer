using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Drawing;

using PIEBALD.Types;//https://www.codeproject.com/Articles/16459/A-Set-class



namespace TLE_Analyzer
{
    class TELGridView : DataGridView
    {
        
        string Line1;
        string Line2;
        string Line3;

        Set<int> Line1Blanks;
        Set<int> Line1Green;
        Set<int> Line2Blanks;
        Set<int> Line2Green;

        public TELGridView()
        {

            this.ColumnCount = 69;
            Line1Blanks = new Set<int>(2, 9, 18, 33, 44, 53, 62, 64);
            Line1Green = new Set<int>(8, 12, 13, 14, 19, 20, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 54, 55, 56, 57, 58, 59, 60, 61, 65, 66, 67, 68);

            Line2Blanks = new Set<int>(2, 8, 17, 26, 34, 43, 52);
            Line2Green = new Set<int>(9, 10, 11, 12, 13, 14, 15, 16, 27, 28, 29, 30, 31, 32, 33, 44, 45, 46, 47, 48, 49, 50, 51, 64, 65, 66, 67, 68);

            BuildHeader();
            ShadeCells();
        }


        public void BuildHeader()
        {
            this.ColumnCount = 69;
            for (int x = 1; x < 70; x++)
            {
                this.Columns[x - 1].Width = 20;
                string s = x.ToString();
                this.Columns[x - 1].Name = s;
            }

            string[] row = new string[] { "" };
            this.Rows.Add(row);
            this.Rows.Add(row);
            this.Rows.Add(row);
        }

        public void ShadeCells()
        {
            //Shade the cells
            //Line 2 of TLE (Line 1)
            for (int x = 0; x < 69; x++)
            {
                if (Line1Blanks.Contains(x)) this.Rows[1].Cells[x - 1].Style.BackColor = Color.LightGray;
                else if (Line1Green.Contains(x)) this.Rows[1].Cells[x - 1].Style.BackColor = Color.PaleGreen;
                else
                {
                    if (x > 1) this.Rows[1].Cells[x - 1].Style.BackColor = Color.Cornsilk;
                }
            }

            //Line 3 of TLE (Line 2)
            for (int x = 0; x < 69; x++)
            {
                if (Line2Blanks.Contains(x)) this.Rows[2].Cells[x - 1].Style.BackColor = Color.LightGray;
                else if (Line2Green.Contains(x)) this.Rows[2].Cells[x - 1].Style.BackColor = Color.PaleGreen;
                else
                {
                    if (x > 1) this.Rows[2].Cells[x - 1].Style.BackColor = Color.Cornsilk;
                }
            }
            this.Rows[1].Cells[69 - 1].Style.BackColor = Color.Cornsilk;
            this.Rows[2].Cells[69 - 1].Style.BackColor = Color.Cornsilk;
        }


        public void PopulateGrid(RichTextBox rtb_Input) //create grid header with numbers 1 to 69
        {
            clearGrid();
            Line1 = rtb_Input.Lines[0];
            Line2 = rtb_Input.Lines[1];
            Line3 = rtb_Input.Lines[2];

            int x = 0;
            foreach (char c in Line1)
            {
                this.CurrentCell = this.Rows[0].Cells[x];
                this.CurrentCell.Value = Line1[x];
                x++;

            }

            x = 0;
            foreach (char c in Line2)
            {
                this.CurrentCell = this.Rows[1].Cells[x];
                this.CurrentCell.Value = Line2[x];
                x++;

            }

            x = 0;
            foreach (char c in Line3)
            {
                this.CurrentCell = this.Rows[2].Cells[x];
                this.CurrentCell.Value = Line3[x];
                x++;

            }

            this.CurrentRow.Selected = false;//this gets rid of the blue selected cell


            ShadeCells();
        }


        public void clearGrid()
        {
            for (int x = 0; x < 69; x++)
            {
                this.CurrentCell = this.Rows[0].Cells[x];
                this.CurrentCell.Value = ' ';
                this.CurrentCell.Style.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
                this.CurrentCell.Style.ForeColor = Color.Black;

                this.CurrentCell = this.Rows[1].Cells[x];
                this.CurrentCell.Value = ' ';
                this.CurrentCell.Style.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
                this.CurrentCell.Style.ForeColor = Color.Black;

                this.CurrentCell = this.Rows[2].Cells[x];
                this.CurrentCell.Value = ' ';
                this.CurrentCell.Style.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
                this.CurrentCell.Style.ForeColor = Color.Black;

                this.CurrentRow.Selected = false; //this gets rid of the blue selected cell
            }
        }

        public void CheckValidLocations(TextBox tb_debug)
        {
            tb_debug.AppendText("Validating...\r\n");
      
            tb_debug.AppendText("Num Columns: " + this.ColumnCount.ToString() + "\r\n");

            for (int x = 0; x < Line2.Length; x++)
            {
                if (Line1Blanks.Contains(x))
                {
                    if (Line2[x - 1] != ' ')
                    {
                        tb_debug.AppendText("Expected space at location Line 1, Column " + x.ToString() + " is not a space\r\n");
                        this.Rows[1].Cells[x - 1].Style.ForeColor = Color.Red;
                        this.Rows[1].Cells[x - 1].Style.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);

                    }
                }
                if (x == 63)
                {
                    if (Line2[x - 1] != '0')
                    {
                        tb_debug.AppendText("Expected '0' at location Line 1, Column " + x.ToString() + "\r\n");
                        this.Rows[1].Cells[x - 1].Style.ForeColor = Color.Red;
                        this.Rows[1].Cells[x - 1].Style.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);
                    }
                }

            }


            for (int x = 0; x < Line3.Length; x++)
            {
                if (Line2Blanks.Contains(x))
                {
                    if (Line3[x - 1] != ' ')
                    {
                        tb_debug.AppendText("Expected space at location Line 2 Column " + x.ToString() + " is not a space\r\n");
                        this.Rows[2].Cells[x - 1].Style.ForeColor = Color.Red;
                        this.Rows[2].Cells[x - 1].Style.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);

                    }
                }
            }
        }



    }
}
