using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace MyGPA_Calc
{
    public partial class Form2 : Form
    {

        #region // Moving The Panel - For the mouse down event \\
        /// <summary>
        /// For making the upper panel able to move the whole windows 
        /// when the mouse is held over
        /// </summary>
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        #endregion

        #region // Initializing The SQLite Connection \\
        //[MALAZ IMPORTANT]: The following conncection string will result in 
        //either the 'Debug' or 'Release' folder
        /* static string myConString = System.IO.Directory.GetCurrentDirectory(); */
        // For currently testing the original directory:
        static string myConString = Environment.CurrentDirectory + "\\GPAbase.db";


        SQLiteConnection conn = new SQLiteConnection($"data source ={myConString}");
        #endregion

        // Default values to pass as arguments in database queries:
        public int _currentUserID;

        public int currentUserID
        {
            get { return _currentUserID; }
            set { _currentUserID = value; }
        }
        int currentTerm = 1;

        public Form2()
        {
            InitializeComponent();
            panelNavigation.Height = button1.Height;
            panelNavigation.Top = button1.Top;
            panelNavigation.Left = button1.Left;
            button1.BackColor = Color.DarkTurquoise;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            fillTable(currentUserID, currentTerm);
            totalCredits_label.Text = "Total Credits: " + TotalCredits(currentUserID).ToString();
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();
        }


        #region Upper Panel + Exit, Max, Min Buttons:
        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        #endregion

        public void fillTable(int theUserID, int theTerm)
        {
            DataTable myDT = new DataTable();
            SQLiteDataAdapter myDA = new SQLiteDataAdapter("SELECT Name,Credits,Vize_Notu1,Vize_Notu2,Final_Notu FROM Lessons WHERE User_ID=@selectedUserID AND Term=@selectedTerm ORDER BY RowNum", conn);
            myDA.SelectCommand.Parameters.AddWithValue("@selectedUserID", theUserID);
            myDA.SelectCommand.Parameters.AddWithValue("@selectedTerm", theTerm);
            myDA.Fill(myDT);


            int k = 0; // Textboxes counter
            for (int i = 0; i < myDT.Rows.Count; i++)
            {
                for (int j = 0; j < myDT.Columns.Count; j++)
                {
                    k++;
                    if (myDT.Rows[i][j] != DBNull.Value)
                    {
                        this.tableLayoutPanel1.Controls["mzTxtBox" + k].Text = myDT.Rows[i][j].ToString();
                    }
                    if (k == 5 || k == 12 || k == 19 || k == 26 || k == 33 || k == 40 || k == 47 || k == 54 || k == 61 || k == 68)
                    {
                        k = k + 3;
                    }
                }
            }
            SetAvrgLetterFourScale();

        }

        #region // Terms Buttons (Click & Leave) \\

        private void button1_Click(object sender, EventArgs e)
        {
            panelNavigation.Height = button1.Height;
            panelNavigation.Top = button1.Top;
            panelNavigation.Left = button1.Left;
            button1.BackColor = Color.DarkTurquoise;
            currentTerm = 1;
            StopAllEvents();
            // Empty every box
            for (int i = 1; i <= 80; i++)
            {
                this.tableLayoutPanel1.Controls["mzTxtBox" + i.ToString()].ResetText();
            }
            fillTable(currentUserID, currentTerm);
            TurnOnAllEvents();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panelNavigation.Height = button2.Height;
            panelNavigation.Top = button2.Top;
            panelNavigation.Left = button2.Left;
            button2.BackColor = Color.DarkTurquoise;

            currentTerm = 2;
            StopAllEvents();
            // Empty every box
            for (int i = 1; i <= 80; i++)
            {
                this.tableLayoutPanel1.Controls["mzTxtBox" + i.ToString()].ResetText();
            }
            fillTable(currentUserID, currentTerm);
            TurnOnAllEvents();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            panelNavigation.Height = button3.Height;
            panelNavigation.Top = button3.Top;
            panelNavigation.Left = button3.Left;
            button3.BackColor = Color.DarkTurquoise;

            currentTerm = 3;
            StopAllEvents();
            // Empty every box
            for (int i = 1; i <= 80; i++)
            {
                this.tableLayoutPanel1.Controls["mzTxtBox" + i.ToString()].ResetText();
            }
            fillTable(currentUserID, currentTerm);
            TurnOnAllEvents();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            panelNavigation.Height = button4.Height;
            panelNavigation.Top = button4.Top;
            panelNavigation.Left = button4.Left;
            button4.BackColor = Color.DarkTurquoise;

            currentTerm = 4;
            StopAllEvents();
            // Empty every box
            for (int i = 1; i <= 80; i++)
            {
                this.tableLayoutPanel1.Controls["mzTxtBox" + i.ToString()].ResetText();
            }
            fillTable(currentUserID, currentTerm);
            TurnOnAllEvents();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panelNavigation.Height = button5.Height;
            panelNavigation.Top = button5.Top;
            panelNavigation.Left = button5.Left;
            button5.BackColor = Color.DarkTurquoise;

            currentTerm = 5;
            StopAllEvents();
            // Empty every box
            for (int i = 1; i <= 80; i++)
            {
                this.tableLayoutPanel1.Controls["mzTxtBox" + i.ToString()].ResetText();
            }
            fillTable(currentUserID, currentTerm);
            TurnOnAllEvents();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panelNavigation.Height = button6.Height;
            panelNavigation.Top = button6.Top;
            panelNavigation.Left = button6.Left;
            button6.BackColor = Color.DarkTurquoise;

            currentTerm = 6;
            StopAllEvents();
            // Empty every box
            for (int i = 1; i <= 80; i++)
            {
                this.tableLayoutPanel1.Controls["mzTxtBox" + i.ToString()].ResetText();
            }
            fillTable(currentUserID, currentTerm);
            TurnOnAllEvents();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panelNavigation.Height = button7.Height;
            panelNavigation.Top = button7.Top;
            panelNavigation.Left = button7.Left;
            button7.BackColor = Color.DarkTurquoise;

            currentTerm = 7;
            StopAllEvents();
            // Empty every box
            for (int i = 1; i <= 80; i++)
            {
                this.tableLayoutPanel1.Controls["mzTxtBox" + i.ToString()].ResetText();
            }
            fillTable(currentUserID, currentTerm);
            TurnOnAllEvents();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panelNavigation.Height = button8.Height;
            panelNavigation.Top = button8.Top;
            panelNavigation.Left = button8.Left;
            button8.BackColor = Color.DarkTurquoise;

            currentTerm = 8;
            StopAllEvents();
            // Empty every box
            for (int i = 1; i <= 80; i++)
            {
                this.tableLayoutPanel1.Controls["mzTxtBox" + i.ToString()].ResetText();
            }
            fillTable(currentUserID, currentTerm);
            TurnOnAllEvents();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panelNavigation.Height = button9.Height;
            panelNavigation.Top = button9.Top;
            panelNavigation.Left = button9.Left;
            button9.BackColor = Color.DarkTurquoise;

            currentTerm = 9;
            StopAllEvents();
            // Empty every box
            for (int i = 1; i <= 80; i++)
            {
                this.tableLayoutPanel1.Controls["mzTxtBox" + i.ToString()].ResetText();
            }
            fillTable(currentUserID, currentTerm);
            TurnOnAllEvents();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            panelNavigation.Height = button10.Height;
            panelNavigation.Top = button10.Top;
            panelNavigation.Left = button10.Left;
            button10.BackColor = Color.DarkTurquoise;

            currentTerm = 10;
            StopAllEvents();
            // Empty every box
            for (int i = 1; i <= 80; i++)
            {
                this.tableLayoutPanel1.Controls["mzTxtBox" + i.ToString()].ResetText();
            }
            fillTable(currentUserID, currentTerm);
            TurnOnAllEvents();
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(0, 106, 128);

        }

        private void button2_Leave(object sender, EventArgs e)
        {
            button2.BackColor = Color.FromArgb(0, 106, 128);

        }

        private void button3_Leave(object sender, EventArgs e)
        {
            button3.BackColor = Color.FromArgb(0, 106, 128);

        }

        private void button4_Leave(object sender, EventArgs e)
        {
            button4.BackColor = Color.FromArgb(0, 106, 128);

        }

        private void button5_Leave(object sender, EventArgs e)
        {
            button5.BackColor = Color.FromArgb(0, 106, 128);

        }

        private void button6_Leave(object sender, EventArgs e)
        {
            button6.BackColor = Color.FromArgb(0, 106, 128);

        }

        private void button7_Leave(object sender, EventArgs e)
        {
            button7.BackColor = Color.FromArgb(0, 106, 128);

        }

        private void button8_Leave(object sender, EventArgs e)
        {
            button8.BackColor = Color.FromArgb(0, 106, 128);

        }

        private void button9_Leave(object sender, EventArgs e)
        {
            button9.BackColor = Color.FromArgb(0, 106, 128);

        }

        private void button10_Leave(object sender, EventArgs e)
        {
            button10.BackColor = Color.FromArgb(0, 106, 128);

        }
        #endregion


        #region Course Name Column:
        private void mzTxtBox1_Validating(object sender, CancelEventArgs e)
        {
            if (mzTxtBox1.Text == "" || mzTxtBox1.Text == null)
            {
                return;
            }
            else
            {
                NameEntry(1, mzTxtBox1);
            }
        }

        private void mzTxtBox9_Validating(object sender, CancelEventArgs e)
        {
            if (mzTxtBox9.Text == "" || mzTxtBox9.Text == null)
            {
                return;
            }
            else
            {
                NameEntry(2, mzTxtBox9);
            }
        }

        private void mzTxtBox17_Validating(object sender, CancelEventArgs e)
        {
            if (mzTxtBox17.Text == "" || mzTxtBox17.Text == null)
            {
                return;
            }
            else
            {
                NameEntry(3, mzTxtBox17);
            }
        }

        private void mzTxtBox25_Validating(object sender, CancelEventArgs e)
        {
            if (mzTxtBox25.Text == "" || mzTxtBox25.Text == null)
            {
                return;
            }
            else
            {
                NameEntry(4, mzTxtBox25);
            }
        }

        private void mzTxtBox33_Validating(object sender, CancelEventArgs e)
        {
            if (mzTxtBox33.Text == "" || mzTxtBox33.Text == null)
            {
                return;
            }
            else
            {
                NameEntry(5, mzTxtBox33);
            }
        }

        private void mzTxtBox41_Validating(object sender, CancelEventArgs e)
        {
            if (mzTxtBox41.Text == "" || mzTxtBox41.Text == null)
            {
                return;
            }
            else
            {
                NameEntry(6, mzTxtBox41);
            }
        }

        private void mzTxtBox49_Validating(object sender, CancelEventArgs e)
        {
            if (mzTxtBox49.Text == "" || mzTxtBox49.Text == null)
            {
                return;
            }
            else
            {
                NameEntry(7, mzTxtBox49);
            }
        }

        private void mzTxtBox57_Validating(object sender, CancelEventArgs e)
        {
            if (mzTxtBox57.Text == "" || mzTxtBox57.Text == null)
            {
                return;
            }
            else
            {
                NameEntry(8, mzTxtBox57);
            }
        }

        private void mzTxtBox65_Validating(object sender, CancelEventArgs e)
        {
            if (mzTxtBox65.Text == "" || mzTxtBox65.Text == null)
            {
                return;
            }
            else
            {
                NameEntry(9, mzTxtBox65);
            }
        }

        private void mzTxtBox73_Validating(object sender, CancelEventArgs e)
        {
            if (mzTxtBox73.Text == "" || mzTxtBox73.Text == null)
            {
                return;
            }
            else
            {
                NameEntry(10, mzTxtBox73);
            }
        }

        #endregion

        public void InsertNewLessonName(mzTxtBox txtboxToInsert, int theUserID, int theTerm, int theRow)
        {
            SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Lessons (User_ID,Name,Term,RowNum) VALUES(@EnteredUser_ID,@EnteredName,@EnteredTerm,@EnteredRow)", conn);
            cmd.Parameters.AddWithValue("@EnteredName", txtboxToInsert.Text);
            cmd.Parameters.AddWithValue("@EnteredUser_ID", theUserID);
            cmd.Parameters.AddWithValue("@EnteredTerm", theTerm);
            cmd.Parameters.AddWithValue("@EnteredRow", theRow);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void UpdateLessonName(mzTxtBox txtboxToUpdate, int theUserID, int theTerm, int theRow)
        {
            SQLiteCommand cmd = new SQLiteCommand("UPDATE Lessons SET Name = @EnteredName WHERE User_ID=@EnteredUser_ID AND Term=@EnteredTerm AND RowNum=@EnteredRow", conn);
            cmd.Parameters.AddWithValue("@EnteredName", txtboxToUpdate.Text);
            cmd.Parameters.AddWithValue("@EnteredUser_ID", theUserID);
            cmd.Parameters.AddWithValue("@EnteredTerm", theTerm);
            cmd.Parameters.AddWithValue("@EnteredRow", theRow);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void NameEntry(int forRow, mzTxtBox _textBox)
        {
            if (DoesRowExist(forRow) == true)
            {
                UpdateLessonName(_textBox, currentUserID, currentTerm, forRow);
            }
            else if (DoesRowExist(forRow) == false)
            {
                InsertNewLessonName(_textBox, currentUserID, currentTerm, forRow);
            }
        }

        #region Credits Column:
        private void mzTxtBox2_Validating(object sender, CancelEventArgs e)
        {
            CreditsEntry(1, mzTxtBox2);
            totalCredits_label.Text = "Total Credits: " + TotalCredits(currentUserID).ToString();
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();

        }

        private void mzTxtBox10_Validating(object sender, CancelEventArgs e)
        {
            CreditsEntry(2, mzTxtBox10);
            totalCredits_label.Text = "Total Credits: " + TotalCredits(currentUserID).ToString();
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();

        }

        private void mzTxtBox18_Validating(object sender, CancelEventArgs e)
        {
            CreditsEntry(3, mzTxtBox18);
            totalCredits_label.Text = "Total Credits: " + TotalCredits(currentUserID).ToString();
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();

        }

        private void mzTxtBox26_Validating(object sender, CancelEventArgs e)
        {
            CreditsEntry(4, mzTxtBox26);
            totalCredits_label.Text = "Total Credits: " + TotalCredits(currentUserID).ToString();
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();

        }

        private void mzTxtBox34_Validating(object sender, CancelEventArgs e)
        {
            CreditsEntry(5, mzTxtBox34);
            totalCredits_label.Text = "Total Credits: " + TotalCredits(currentUserID).ToString();
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();

        }

        private void mzTxtBox42_Validating(object sender, CancelEventArgs e)
        {
            CreditsEntry(6, mzTxtBox42);
            totalCredits_label.Text = "Total Credits: " + TotalCredits(currentUserID).ToString();
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();

        }

        private void mzTxtBox50_Validating(object sender, CancelEventArgs e)
        {
            CreditsEntry(7, mzTxtBox50);
            totalCredits_label.Text = "Total Credits: " + TotalCredits(currentUserID).ToString();
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();

        }

        private void mzTxtBox58_Validating(object sender, CancelEventArgs e)
        {
            CreditsEntry(8, mzTxtBox58);
            totalCredits_label.Text = "Total Credits: " + TotalCredits(currentUserID).ToString();
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();

        }

        private void mzTxtBox66_Validating(object sender, CancelEventArgs e)
        {
            CreditsEntry(9, mzTxtBox66);
            totalCredits_label.Text = "Total Credits: " + TotalCredits(currentUserID).ToString();
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();

        }

        private void mzTxtBox74_Validating(object sender, CancelEventArgs e)
        {
            CreditsEntry(10, mzTxtBox74);
            totalCredits_label.Text = "Total Credits: " + TotalCredits(currentUserID).ToString();
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();

        }
        #endregion

        public void InsertNewCredit(mzTxtBox txtboxToInsert, int theUserID, int theTerm, int theRow)
        {
            SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Lessons (User_ID,Name,Credits,Term,RowNum) VALUES(@EnteredUser_ID,@TheNoName,@EnteredCredits,@EnteredTerm,@EnteredRow)", conn);
      
            //SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Lessons (User_ID,Credits,Term,RowNum) VALUES(@EnteredUser_ID,@EnteredCredits,@EnteredTerm,@EnteredRow)", conn);
            if (int.TryParse(txtboxToInsert.Text, out int txtBoxNumericValue))
            {
                cmd.Parameters.AddWithValue("@EnteredCredits", txtBoxNumericValue);
                cmd.Parameters.AddWithValue("@EnteredUser_ID", theUserID);
                cmd.Parameters.AddWithValue("@EnteredTerm", theTerm);
                cmd.Parameters.AddWithValue("@EnteredRow", theRow);
                cmd.Parameters.AddWithValue("@TheNoName", "No Name Entered");

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            else if (txtboxToInsert.Text == "" || txtboxToInsert.Text == null)
            {
                txtboxToInsert.Text = "0";
                SQLiteCommand cmdNull = new SQLiteCommand("INSERT INTO Lessons (User_ID,Credits,Term,RowNum) VALUES(@EnteredUser_ID,0,@EnteredTerm,@EnteredRow)", conn);
                cmdNull.Parameters.AddWithValue("@EnteredUser_ID", theUserID);
                cmdNull.Parameters.AddWithValue("@EnteredTerm", theTerm);
                cmdNull.Parameters.AddWithValue("@EnteredRow", theRow);
                conn.Open();
                cmdNull.ExecuteNonQuery();
                conn.Close();
            }
            else
            {
                MessageBox.Show("Credits can only be an integer whole number! \nYou cannot type in letters or decimal number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtboxToInsert.ResetText();
            }

        }

        public void UpdateCredit(mzTxtBox txtboxToInsert, int theUserID, int theTerm, int theRow)
        {
            if (int.TryParse(txtboxToInsert.Text, out int txtBoxNumericValue))
            {
                SQLiteCommand cmd = new SQLiteCommand("UPDATE Lessons SET Credits = @EnteredCredits WHERE User_ID=@EnteredUser_ID AND Term=@EnteredTerm AND RowNum=@EnteredRow", conn);

                //SQLiteCommand cmd = new SQLiteCommand("UPDATE Lessons SET Credits = @EnteredCredits WHERE User_ID=@EnteredUser_ID AND Term=@EnteredTerm AND RowNum=@EnteredRow", conn);
                cmd.Parameters.AddWithValue("@EnteredCredits", txtBoxNumericValue);
                cmd.Parameters.AddWithValue("@EnteredUser_ID", theUserID);
                cmd.Parameters.AddWithValue("@EnteredTerm", theTerm);
                cmd.Parameters.AddWithValue("@EnteredRow", theRow);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            else if (txtboxToInsert.Text == "" || txtboxToInsert.Text == null)
            {
                txtboxToInsert.Text = "0";
                SQLiteCommand cmd = new SQLiteCommand("UPDATE Lessons SET Credits = 0 WHERE User_ID=@EnteredUser_ID AND Term=@EnteredTerm AND RowNum=@EnteredRow", conn);
                cmd.Parameters.AddWithValue("@EnteredUser_ID", theUserID);
                cmd.Parameters.AddWithValue("@EnteredTerm", theTerm);
                cmd.Parameters.AddWithValue("@EnteredRow", theRow);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            else
            {
                MessageBox.Show("Credits can only be an integer whole number! \nYou cannot type in letters or decimal number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtboxToInsert.ResetText();
            }
        }

        public void CreditsEntry(int forRow, mzTxtBox _textBox)
        {
            if (DoesRowExist(forRow) == true)
            {
                UpdateCredit(_textBox, currentUserID, currentTerm, forRow);
            }
            else
            {
                InsertNewCredit(_textBox, currentUserID, currentTerm, forRow);
            }
        }

        #region Midterm1 Column:

        private void mzTxtBox3_TextChanged(object sender, EventArgs e)
        {
            MidTerm1Entry(1, mzTxtBox3);
            mzTxtBox6.Text = GetAverage(1, currentUserID, currentTerm).ToString();
            mzTxtBox7.Text = GetLetter(1, currentUserID, currentTerm).ToString();
            mzTxtBox8.Text = GetOutOfFour(1, currentUserID, currentTerm).ToString();
            AddFourScale(1, currentUserID, currentTerm);
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();

        }

        private void mzTxtBox11_TextChanged(object sender, EventArgs e)
        {
            MidTerm1Entry(2, mzTxtBox11);
            mzTxtBox14.Text = GetAverage(2, currentUserID, currentTerm).ToString();
            mzTxtBox15.Text = GetLetter(2, currentUserID, currentTerm).ToString();
            mzTxtBox16.Text = GetOutOfFour(2, currentUserID, currentTerm).ToString();
            AddFourScale(2, currentUserID, currentTerm);
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();

        }

        private void mzTxtBox19_TextChanged(object sender, EventArgs e)
        {
            MidTerm1Entry(3, mzTxtBox19);
            mzTxtBox22.Text = GetAverage(3, currentUserID, currentTerm).ToString();
            mzTxtBox23.Text = GetLetter(3, currentUserID, currentTerm).ToString();
            mzTxtBox24.Text = GetOutOfFour(3, currentUserID, currentTerm).ToString();
            AddFourScale(3, currentUserID, currentTerm);
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();

        }

        private void mzTxtBox27_TextChanged(object sender, EventArgs e)
        {
            MidTerm1Entry(4, mzTxtBox27);
            mzTxtBox30.Text = GetAverage(4, currentUserID, currentTerm).ToString();
            mzTxtBox31.Text = GetLetter(4, currentUserID, currentTerm).ToString();
            mzTxtBox32.Text = GetOutOfFour(4, currentUserID, currentTerm).ToString();
            AddFourScale(4, currentUserID, currentTerm);
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();

        }

        private void mzTxtBox35_TextChanged(object sender, EventArgs e)
        {
            MidTerm1Entry(5, mzTxtBox35);
            mzTxtBox38.Text = GetAverage(5, currentUserID, currentTerm).ToString();
            mzTxtBox39.Text = GetLetter(5, currentUserID, currentTerm).ToString();
            mzTxtBox40.Text = GetOutOfFour(5, currentUserID, currentTerm).ToString();
            AddFourScale(5, currentUserID, currentTerm);
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();

        }

        private void mzTxtBox43_TextChanged(object sender, EventArgs e)
        {
            MidTerm1Entry(6, mzTxtBox43);
            mzTxtBox46.Text = GetAverage(6, currentUserID, currentTerm).ToString();
            mzTxtBox47.Text = GetLetter(6, currentUserID, currentTerm).ToString();
            mzTxtBox48.Text = GetOutOfFour(6, currentUserID, currentTerm).ToString();
            AddFourScale(6, currentUserID, currentTerm);
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();

        }

        private void mzTxtBox51_TextChanged(object sender, EventArgs e)
        {
            MidTerm1Entry(7, mzTxtBox51);
            mzTxtBox54.Text = GetAverage(7, currentUserID, currentTerm).ToString();
            mzTxtBox55.Text = GetLetter(7, currentUserID, currentTerm).ToString();
            mzTxtBox56.Text = GetOutOfFour(7, currentUserID, currentTerm).ToString();
            AddFourScale(7, currentUserID, currentTerm);
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();

        }

        private void mzTxtBox59_TextChanged(object sender, EventArgs e)
        {
            MidTerm1Entry(8, mzTxtBox59);
            mzTxtBox62.Text = GetAverage(8, currentUserID, currentTerm).ToString();
            mzTxtBox63.Text = GetLetter(8, currentUserID, currentTerm).ToString();
            mzTxtBox64.Text = GetOutOfFour(8, currentUserID, currentTerm).ToString();
            AddFourScale(8, currentUserID, currentTerm);
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();

        }

        private void mzTxtBox67_TextChanged(object sender, EventArgs e)
        {
            MidTerm1Entry(9, mzTxtBox67);
            mzTxtBox70.Text = GetAverage(9, currentUserID, currentTerm).ToString();
            mzTxtBox71.Text = GetLetter(9, currentUserID, currentTerm).ToString();
            mzTxtBox72.Text = GetOutOfFour(9, currentUserID, currentTerm).ToString();
            AddFourScale(9, currentUserID, currentTerm);
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();

        }

        private void mzTxtBox75_TextChanged(object sender, EventArgs e)
        {
            MidTerm1Entry(10, mzTxtBox75);
            mzTxtBox78.Text = GetAverage(10, currentUserID, currentTerm).ToString();
            mzTxtBox79.Text = GetLetter(10, currentUserID, currentTerm).ToString();
            mzTxtBox80.Text = GetOutOfFour(10, currentUserID, currentTerm).ToString();
            AddFourScale(10, currentUserID, currentTerm);
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();

        }

        #endregion

        public void InsertNewMidTerm1(mzTxtBox txtboxToInsert, int theUserID, int theTerm, int theRow)
        {
            SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Lessons(User_ID, Name, Vize_Notu1, Term, RowNum) VALUES(@EnteredUser_ID, @TheNoName, @EnteredPoint, @EnteredTerm, @EnteredRow)", conn);         

            if (decimal.TryParse(txtboxToInsert.Text, out decimal txtBoxNumericValue))
            {
                cmd.Parameters.AddWithValue("@EnteredUser_ID", theUserID);
                cmd.Parameters.AddWithValue("@EnteredPoint", txtBoxNumericValue);
                cmd.Parameters.AddWithValue("@EnteredTerm", theTerm);
                cmd.Parameters.AddWithValue("@EnteredRow", theRow);
                cmd.Parameters.AddWithValue("@TheNoName", "No Name Added");
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            else if (txtboxToInsert.Text == "" || txtboxToInsert.Text == null)
            {
                cmd.Parameters.AddWithValue("@EnteredUser_ID", theUserID);
                cmd.Parameters.AddWithValue("@EnteredPoint", 0);
                cmd.Parameters.AddWithValue("@EnteredTerm", theTerm);
                cmd.Parameters.AddWithValue("@EnteredRow", theRow);
                cmd.Parameters.AddWithValue("@TheNoName", "No Name Added");
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            else
            {
                txtboxToInsert.Text = txtboxToInsert.Text.Remove(txtboxToInsert.Text.Length - 1);
                MessageBox.Show("Exan marks cannot contain letters!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void UpdateMidTerm1(mzTxtBox txtboxToInsert, int theUserID, int theTerm, int theRow)
        {
            if (decimal.TryParse(txtboxToInsert.Text, out decimal txtBoxNumericValue))
            {
                SQLiteCommand cmd = new SQLiteCommand("UPDATE Lessons SET Vize_Notu1 = @EnteredPoint WHERE User_ID=@EnteredUser_ID AND Term=@EnteredTerm AND RowNum=@EnteredRow", conn);

                cmd.Parameters.AddWithValue("@EnteredPoint", txtBoxNumericValue);
                cmd.Parameters.AddWithValue("@EnteredUser_ID", theUserID);
                cmd.Parameters.AddWithValue("@EnteredTerm", theTerm);
                cmd.Parameters.AddWithValue("@EnteredRow", theRow);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            else if (txtboxToInsert.Text == "" || txtboxToInsert.Text == null)
            {
                SQLiteCommand cmd = new SQLiteCommand("UPDATE Lessons SET Vize_Notu1 = @EnteredPoint WHERE User_ID=@EnteredUser_ID AND Term=@EnteredTerm AND RowNum=@EnteredRow", conn);

                cmd.Parameters.AddWithValue("@EnteredPoint", 0);
                cmd.Parameters.AddWithValue("@EnteredUser_ID", theUserID);
                cmd.Parameters.AddWithValue("@EnteredTerm", theTerm);
                cmd.Parameters.AddWithValue("@EnteredRow", theRow);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            else
            {
                txtboxToInsert.Text = txtboxToInsert.Text.Remove(txtboxToInsert.Text.Length - 1);
                MessageBox.Show("Exam marks cannot contain letters!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void MidTerm1Entry(int forRow, mzTxtBox _textBox)
        {
            if (DoesRowExist(forRow) == true)
            {
                UpdateMidTerm1(_textBox, currentUserID, currentTerm, forRow);
            }
            else
            {
                InsertNewMidTerm1(_textBox, currentUserID, currentTerm, forRow);
            }
        }

        #region Midterm2 Column:
        private void mzTxtBox4_TextChanged(object sender, EventArgs e)
        {
            MidTerm2Entry(1, mzTxtBox4);
            mzTxtBox6.Text = GetAverage(1, currentUserID, currentTerm).ToString();
            mzTxtBox7.Text = GetLetter(1, currentUserID, currentTerm).ToString();
            mzTxtBox8.Text = GetOutOfFour(1, currentUserID, currentTerm).ToString();
            AddFourScale(1, currentUserID, currentTerm);
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();
        }

        private void mzTxtBox12_TextChanged(object sender, EventArgs e)
        {
            MidTerm2Entry(2, mzTxtBox12);
            mzTxtBox14.Text = GetAverage(2, currentUserID, currentTerm).ToString();
            mzTxtBox15.Text = GetLetter(2, currentUserID, currentTerm).ToString();
            mzTxtBox16.Text = GetOutOfFour(2, currentUserID, currentTerm).ToString();
            AddFourScale(2, currentUserID, currentTerm);
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();
        }

        private void mzTxtBox20_TextChanged(object sender, EventArgs e)
        {
            MidTerm2Entry(3, mzTxtBox20);
            mzTxtBox22.Text = GetAverage(3, currentUserID, currentTerm).ToString();
            mzTxtBox23.Text = GetLetter(3, currentUserID, currentTerm).ToString();
            mzTxtBox24.Text = GetOutOfFour(3, currentUserID, currentTerm).ToString();
            AddFourScale(3, currentUserID, currentTerm);
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();

        }

        private void mzTxtBox28_TextChanged(object sender, EventArgs e)
        {
            MidTerm2Entry(4, mzTxtBox28);
            mzTxtBox30.Text = GetAverage(4, currentUserID, currentTerm).ToString();
            mzTxtBox31.Text = GetLetter(4, currentUserID, currentTerm).ToString();
            mzTxtBox32.Text = GetOutOfFour(4, currentUserID, currentTerm).ToString();
            AddFourScale(4, currentUserID, currentTerm);
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();
        }

        private void mzTxtBox36_TextChanged(object sender, EventArgs e)
        {
            MidTerm2Entry(5, mzTxtBox36);
            mzTxtBox38.Text = GetAverage(5, currentUserID, currentTerm).ToString();
            mzTxtBox39.Text = GetLetter(5, currentUserID, currentTerm).ToString();
            mzTxtBox40.Text = GetOutOfFour(5, currentUserID, currentTerm).ToString();
            AddFourScale(5, currentUserID, currentTerm);
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();
        }

        private void mzTxtBox44_TextChanged(object sender, EventArgs e)
        {
            MidTerm2Entry(6, mzTxtBox44);
            mzTxtBox46.Text = GetAverage(6, currentUserID, currentTerm).ToString();
            mzTxtBox47.Text = GetLetter(6, currentUserID, currentTerm).ToString();
            mzTxtBox48.Text = GetOutOfFour(6, currentUserID, currentTerm).ToString();
            AddFourScale(6, currentUserID, currentTerm);
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();

        }

        private void mzTxtBox52_TextChanged(object sender, EventArgs e)
        {
            MidTerm2Entry(7, mzTxtBox52);
            mzTxtBox54.Text = GetAverage(7, currentUserID, currentTerm).ToString();
            mzTxtBox55.Text = GetLetter(7, currentUserID, currentTerm).ToString();
            mzTxtBox56.Text = GetOutOfFour(7, currentUserID, currentTerm).ToString();
            AddFourScale(7, currentUserID, currentTerm);
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();

        }

        private void mzTxtBox60_TextChanged(object sender, EventArgs e)
        {
            MidTerm2Entry(8, mzTxtBox60);
            mzTxtBox62.Text = GetAverage(8, currentUserID, currentTerm).ToString();
            mzTxtBox63.Text = GetLetter(8, currentUserID, currentTerm).ToString();
            mzTxtBox64.Text = GetOutOfFour(8, currentUserID, currentTerm).ToString();
            AddFourScale(8, currentUserID, currentTerm);
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();

        }

        private void mzTxtBox68_TextChanged(object sender, EventArgs e)
        {
            MidTerm2Entry(9, mzTxtBox68);
            mzTxtBox70.Text = GetAverage(9, currentUserID, currentTerm).ToString();
            mzTxtBox71.Text = GetLetter(9, currentUserID, currentTerm).ToString();
            mzTxtBox72.Text = GetOutOfFour(9, currentUserID, currentTerm).ToString();
            AddFourScale(9, currentUserID, currentTerm);
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();

        }

        private void mzTxtBox76_TextChanged(object sender, EventArgs e)
        {
            MidTerm2Entry(10, mzTxtBox76);
            mzTxtBox78.Text = GetAverage(10, currentUserID, currentTerm).ToString();
            mzTxtBox79.Text = GetLetter(10, currentUserID, currentTerm).ToString();
            mzTxtBox80.Text = GetOutOfFour(10, currentUserID, currentTerm).ToString();
            AddFourScale(10, currentUserID, currentTerm);
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();

        }

        #endregion

        public void InsertNewMidTerm2(mzTxtBox txtboxToInsert, int theUserID, int theTerm, int theRow)
        {
            SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Lessons(User_ID, Name, Vize_Notu2, Term, RowNum) VALUES(@EnteredUser_ID, @TheNoName, @EnteredPoint, @EnteredTerm, @EnteredRow)", conn);

            if (decimal.TryParse(txtboxToInsert.Text, out decimal txtBoxNumericValue))
            {
                cmd.Parameters.AddWithValue("@EnteredUser_ID", theUserID);
                cmd.Parameters.AddWithValue("@EnteredPoint", txtBoxNumericValue);
                cmd.Parameters.AddWithValue("@EnteredTerm", theTerm);
                cmd.Parameters.AddWithValue("@EnteredRow", theRow);
                cmd.Parameters.AddWithValue("@TheNoName", "No Name Added");
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            else if (txtboxToInsert.Text == "" || txtboxToInsert.Text == null)
            {
                cmd.Parameters.AddWithValue("@EnteredUser_ID", theUserID);
                cmd.Parameters.AddWithValue("@EnteredPoint", 0);
                cmd.Parameters.AddWithValue("@EnteredTerm", theTerm);
                cmd.Parameters.AddWithValue("@EnteredRow", theRow);
                cmd.Parameters.AddWithValue("@TheNoName", "No Name Entered");
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            else
            {
                txtboxToInsert.Text = txtboxToInsert.Text.Remove(txtboxToInsert.Text.Length - 1);
                MessageBox.Show("Exan marks cannot contain letters!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void UpdateMidTerm2(mzTxtBox txtboxToInsert, int theUserID, int theTerm, int theRow)
        {
            if (decimal.TryParse(txtboxToInsert.Text, out decimal txtBoxNumericValue))
            {
                SQLiteCommand cmd = new SQLiteCommand("UPDATE Lessons SET Vize_Notu2 = @EnteredPoint WHERE User_ID=@EnteredUser_ID AND Term=@EnteredTerm AND RowNum=@EnteredRow", conn);

                cmd.Parameters.AddWithValue("@EnteredPoint", txtBoxNumericValue);
                cmd.Parameters.AddWithValue("@EnteredUser_ID", theUserID);
                cmd.Parameters.AddWithValue("@EnteredTerm", theTerm);
                cmd.Parameters.AddWithValue("@EnteredRow", theRow);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            else if (txtboxToInsert.Text == "" || txtboxToInsert.Text == null)
            {
                SQLiteCommand cmd = new SQLiteCommand("UPDATE Lessons SET Vize_Notu2 = @EnteredPoint WHERE User_ID=@EnteredUser_ID AND Term=@EnteredTerm AND RowNum=@EnteredRow", conn);

                cmd.Parameters.AddWithValue("@EnteredPoint", 0);
                cmd.Parameters.AddWithValue("@EnteredUser_ID", theUserID);
                cmd.Parameters.AddWithValue("@EnteredTerm", theTerm);
                cmd.Parameters.AddWithValue("@EnteredRow", theRow);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            else
            {
                txtboxToInsert.Text = txtboxToInsert.Text.Remove(txtboxToInsert.Text.Length - 1);
                MessageBox.Show("Exam marks cannot contain letters!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void MidTerm2Entry(int forRow, mzTxtBox _textBox)
        {
            if (DoesRowExist(forRow) == true)
            {
                UpdateMidTerm2(_textBox, currentUserID, currentTerm, forRow);
            }
            else
            {
                InsertNewMidTerm2(_textBox, currentUserID, currentTerm, forRow);
            }
        }

        #region Final Column:

        private void mzTxtBox5_TextChanged(object sender, EventArgs e)
        {
            FinalEntry(1, mzTxtBox5);
            mzTxtBox6.Text = GetAverage(1, currentUserID, currentTerm).ToString();
            mzTxtBox7.Text = GetLetter(1, currentUserID, currentTerm).ToString();
            mzTxtBox8.Text = GetOutOfFour(1, currentUserID, currentTerm).ToString();
            AddFourScale(1, currentUserID, currentTerm);
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();
        }

        private void mzTxtBox13_TextChanged(object sender, EventArgs e)
        {
            FinalEntry(2, mzTxtBox13);
            mzTxtBox14.Text = GetAverage(2, currentUserID, currentTerm).ToString();
            mzTxtBox15.Text = GetLetter(2, currentUserID, currentTerm).ToString();
            mzTxtBox16.Text = GetOutOfFour(2, currentUserID, currentTerm).ToString();
            AddFourScale(2, currentUserID, currentTerm);
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();

        }

        private void mzTxtBox21_TextChanged(object sender, EventArgs e)
        {
            FinalEntry(3, mzTxtBox21);
            mzTxtBox22.Text = GetAverage(3, currentUserID, currentTerm).ToString();
            mzTxtBox23.Text = GetLetter(3, currentUserID, currentTerm).ToString();
            mzTxtBox24.Text = GetOutOfFour(3, currentUserID, currentTerm).ToString();
            AddFourScale(3, currentUserID, currentTerm);
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();

        }

        private void mzTxtBox29_TextChanged(object sender, EventArgs e)
        {
            FinalEntry(4, mzTxtBox29);
            mzTxtBox30.Text = GetAverage(4, currentUserID, currentTerm).ToString();
            mzTxtBox31.Text = GetLetter(4, currentUserID, currentTerm).ToString();
            mzTxtBox32.Text = GetOutOfFour(4, currentUserID, currentTerm).ToString();
            AddFourScale(4, currentUserID, currentTerm);
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();

        }

        private void mzTxtBox37_TextChanged(object sender, EventArgs e)
        {
            FinalEntry(5, mzTxtBox37);
            mzTxtBox38.Text = GetAverage(5, currentUserID, currentTerm).ToString();
            mzTxtBox39.Text = GetLetter(5, currentUserID, currentTerm).ToString();
            mzTxtBox40.Text = GetOutOfFour(5, currentUserID, currentTerm).ToString();
            AddFourScale(5, currentUserID, currentTerm);
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();

        }

        private void mzTxtBox45_TextChanged(object sender, EventArgs e)
        {
            FinalEntry(6, mzTxtBox45);
            mzTxtBox46.Text = GetAverage(6, currentUserID, currentTerm).ToString();
            mzTxtBox47.Text = GetLetter(6, currentUserID, currentTerm).ToString();
            mzTxtBox48.Text = GetOutOfFour(6, currentUserID, currentTerm).ToString();
            AddFourScale(6, currentUserID, currentTerm);
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();

        }

        private void mzTxtBox53_TextChanged(object sender, EventArgs e)
        {
            FinalEntry(7, mzTxtBox53);
            mzTxtBox54.Text = GetAverage(7, currentUserID, currentTerm).ToString();
            mzTxtBox55.Text = GetLetter(7, currentUserID, currentTerm).ToString();
            mzTxtBox56.Text = GetOutOfFour(7, currentUserID, currentTerm).ToString();
            AddFourScale(7, currentUserID, currentTerm);
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();

        }

        private void mzTxtBox61_TextChanged(object sender, EventArgs e)
        {
            FinalEntry(8, mzTxtBox61);
            mzTxtBox62.Text = GetAverage(8, currentUserID, currentTerm).ToString();
            mzTxtBox63.Text = GetLetter(8, currentUserID, currentTerm).ToString();
            mzTxtBox64.Text = GetOutOfFour(8, currentUserID, currentTerm).ToString();
            AddFourScale(8, currentUserID, currentTerm);
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();

        }

        private void mzTxtBox69_TextChanged(object sender, EventArgs e)
        {
            FinalEntry(9, mzTxtBox69);
            mzTxtBox70.Text = GetAverage(9, currentUserID, currentTerm).ToString();
            mzTxtBox71.Text = GetLetter(9, currentUserID, currentTerm).ToString();
            mzTxtBox72.Text = GetOutOfFour(9, currentUserID, currentTerm).ToString();
            AddFourScale(9, currentUserID, currentTerm);
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();

        }

        private void mzTxtBox77_TextChanged(object sender, EventArgs e)
        {
            FinalEntry(10, mzTxtBox77);
            mzTxtBox78.Text = GetAverage(10, currentUserID, currentTerm).ToString();
            mzTxtBox79.Text = GetLetter(10, currentUserID, currentTerm).ToString();
            mzTxtBox80.Text = GetOutOfFour(10, currentUserID, currentTerm).ToString();
            AddFourScale(10, currentUserID, currentTerm);
            GPA_label.Text = "Overall GPA: " + GetGPA(currentUserID).ToString();

        }

        #endregion

        public void InsertNewFinal(mzTxtBox txtboxToInsert, int theUserID, int theTerm, int theRow)
        {
            SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Lessons (User_ID,Final_Notu,Term,RowNum) VALUES(@EnteredUser_ID,@EnteredPoint,@EnteredTerm,@EnteredRow)", conn);

            if (decimal.TryParse(txtboxToInsert.Text, out decimal txtBoxNumericValue))
            {
                cmd.Parameters.AddWithValue("@EnteredPoint", txtBoxNumericValue);
                cmd.Parameters.AddWithValue("@EnteredUser_ID", theUserID);
                cmd.Parameters.AddWithValue("@EnteredTerm", theTerm);
                cmd.Parameters.AddWithValue("@EnteredRow", theRow);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            else if (txtboxToInsert.Text == "" || txtboxToInsert.Text == null)
            {
                cmd.Parameters.AddWithValue("@EnteredPoint", 0);
                cmd.Parameters.AddWithValue("@EnteredUser_ID", theUserID);
                cmd.Parameters.AddWithValue("@EnteredTerm", theTerm);
                cmd.Parameters.AddWithValue("@EnteredRow", theRow);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            else
            {
                txtboxToInsert.Text = txtboxToInsert.Text.Remove(txtboxToInsert.Text.Length - 1);
                MessageBox.Show("Exan marks cannot contain letters!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        public void UpdateFinal(mzTxtBox txtboxToInsert, int theUserID, int theTerm, int theRow)
        {
            if (decimal.TryParse(txtboxToInsert.Text, out decimal txtBoxNumericValue))
            {
                SQLiteCommand cmd = new SQLiteCommand("UPDATE Lessons SET Final_Notu = @EnteredPoint WHERE User_ID=@EnteredUser_ID AND Term=@EnteredTerm AND RowNum=@EnteredRow", conn);

                cmd.Parameters.AddWithValue("@EnteredPoint", txtBoxNumericValue);
                cmd.Parameters.AddWithValue("@EnteredUser_ID", theUserID);
                cmd.Parameters.AddWithValue("@EnteredTerm", theTerm);
                cmd.Parameters.AddWithValue("@EnteredRow", theRow);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            else if (txtboxToInsert.Text == "" || txtboxToInsert.Text == null)
            {
                SQLiteCommand cmd = new SQLiteCommand("UPDATE Lessons SET Final_Notu = @EnteredPoint WHERE User_ID=@EnteredUser_ID AND Term=@EnteredTerm AND RowNum=@EnteredRow", conn);

                cmd.Parameters.AddWithValue("@EnteredPoint", 0);
                cmd.Parameters.AddWithValue("@EnteredUser_ID", theUserID);
                cmd.Parameters.AddWithValue("@EnteredTerm", theTerm);
                cmd.Parameters.AddWithValue("@EnteredRow", theRow);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            else
            {
                txtboxToInsert.Text = txtboxToInsert.Text.Remove(txtboxToInsert.Text.Length - 1);
                MessageBox.Show("Exam marks cannot contain letters!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void FinalEntry(int forRow, mzTxtBox _textBox)
        {
            if (DoesRowExist(forRow) == true)
            {
                UpdateFinal(_textBox, currentUserID, currentTerm, forRow);
            }
            else
            {
                InsertNewFinal(_textBox, currentUserID, currentTerm, forRow);
            }
        }


        // Actions:
        public void StopAllEvents()
        {
            // Names
            mzTxtBox1.Validating -= new CancelEventHandler(mzTxtBox1_Validating);
            mzTxtBox9.Validating -= new CancelEventHandler(mzTxtBox9_Validating);
            mzTxtBox17.Validating -= new CancelEventHandler(mzTxtBox17_Validating);
            mzTxtBox25.Validating -= new CancelEventHandler(mzTxtBox25_Validating);
            mzTxtBox33.Validating -= new CancelEventHandler(mzTxtBox33_Validating);
            mzTxtBox41.Validating -= new CancelEventHandler(mzTxtBox41_Validating);
            mzTxtBox49.Validating -= new CancelEventHandler(mzTxtBox49_Validating);
            mzTxtBox57.Validating -= new CancelEventHandler(mzTxtBox57_Validating);
            mzTxtBox65.Validating -= new CancelEventHandler(mzTxtBox65_Validating);
            mzTxtBox73.Validating -= new CancelEventHandler(mzTxtBox73_Validating);

            // Credits
            mzTxtBox2.Validating -= new CancelEventHandler(mzTxtBox2_Validating);
            mzTxtBox10.Validating -= new CancelEventHandler(mzTxtBox10_Validating);
            mzTxtBox18.Validating -= new CancelEventHandler(mzTxtBox18_Validating);
            mzTxtBox26.Validating -= new CancelEventHandler(mzTxtBox26_Validating);
            mzTxtBox34.Validating -= new CancelEventHandler(mzTxtBox34_Validating);
            mzTxtBox42.Validating -= new CancelEventHandler(mzTxtBox42_Validating);
            mzTxtBox50.Validating -= new CancelEventHandler(mzTxtBox50_Validating);
            mzTxtBox58.Validating -= new CancelEventHandler(mzTxtBox58_Validating);
            mzTxtBox66.Validating -= new CancelEventHandler(mzTxtBox66_Validating);
            mzTxtBox74.Validating -= new CancelEventHandler(mzTxtBox74_Validating);

            // MidTerm1
            mzTxtBox3.TextChanged -= new EventHandler(mzTxtBox3_TextChanged);
            mzTxtBox11.TextChanged -= new EventHandler(mzTxtBox11_TextChanged);
            mzTxtBox19.TextChanged -= new EventHandler(mzTxtBox19_TextChanged);
            mzTxtBox27.TextChanged -= new EventHandler(mzTxtBox27_TextChanged);
            mzTxtBox35.TextChanged -= new EventHandler(mzTxtBox35_TextChanged);
            mzTxtBox43.TextChanged -= new EventHandler(mzTxtBox43_TextChanged);
            mzTxtBox51.TextChanged -= new EventHandler(mzTxtBox51_TextChanged);
            mzTxtBox59.TextChanged -= new EventHandler(mzTxtBox59_TextChanged);
            mzTxtBox67.TextChanged -= new EventHandler(mzTxtBox67_TextChanged);
            mzTxtBox75.TextChanged -= new EventHandler(mzTxtBox75_TextChanged);

            // MidTerm2
            mzTxtBox4.TextChanged -= new EventHandler(mzTxtBox4_TextChanged);
            mzTxtBox12.TextChanged -= new EventHandler(mzTxtBox12_TextChanged);
            mzTxtBox20.TextChanged -= new EventHandler(mzTxtBox20_TextChanged);
            mzTxtBox28.TextChanged -= new EventHandler(mzTxtBox28_TextChanged);
            mzTxtBox36.TextChanged -= new EventHandler(mzTxtBox36_TextChanged);
            mzTxtBox44.TextChanged -= new EventHandler(mzTxtBox44_TextChanged);
            mzTxtBox52.TextChanged -= new EventHandler(mzTxtBox52_TextChanged);
            mzTxtBox60.TextChanged -= new EventHandler(mzTxtBox60_TextChanged);
            mzTxtBox68.TextChanged -= new EventHandler(mzTxtBox68_TextChanged);
            mzTxtBox76.TextChanged -= new EventHandler(mzTxtBox76_TextChanged);

            // Final
            mzTxtBox5.TextChanged -= new EventHandler(mzTxtBox5_TextChanged);
            mzTxtBox13.TextChanged -= new EventHandler(mzTxtBox13_TextChanged);
            mzTxtBox21.TextChanged -= new EventHandler(mzTxtBox21_TextChanged);
            mzTxtBox29.TextChanged -= new EventHandler(mzTxtBox29_TextChanged);
            mzTxtBox37.TextChanged -= new EventHandler(mzTxtBox37_TextChanged);
            mzTxtBox45.TextChanged -= new EventHandler(mzTxtBox45_TextChanged);
            mzTxtBox53.TextChanged -= new EventHandler(mzTxtBox53_TextChanged);
            mzTxtBox61.TextChanged -= new EventHandler(mzTxtBox61_TextChanged);
            mzTxtBox69.TextChanged -= new EventHandler(mzTxtBox69_TextChanged);
            mzTxtBox77.TextChanged -= new EventHandler(mzTxtBox77_TextChanged);

        }

        public void TurnOnAllEvents()
        {
            // Names
            mzTxtBox1.Validating += new CancelEventHandler(mzTxtBox1_Validating);
            mzTxtBox9.Validating += new CancelEventHandler(mzTxtBox9_Validating);
            mzTxtBox17.Validating += new CancelEventHandler(mzTxtBox17_Validating);
            mzTxtBox25.Validating += new CancelEventHandler(mzTxtBox25_Validating);
            mzTxtBox33.Validating += new CancelEventHandler(mzTxtBox33_Validating);
            mzTxtBox41.Validating += new CancelEventHandler(mzTxtBox41_Validating);
            mzTxtBox49.Validating += new CancelEventHandler(mzTxtBox49_Validating);
            mzTxtBox57.Validating += new CancelEventHandler(mzTxtBox57_Validating);
            mzTxtBox65.Validating += new CancelEventHandler(mzTxtBox65_Validating);
            mzTxtBox73.Validating += new CancelEventHandler(mzTxtBox73_Validating);

            // Credits
            mzTxtBox2.Validating += new CancelEventHandler(mzTxtBox2_Validating);
            mzTxtBox10.Validating += new CancelEventHandler(mzTxtBox10_Validating);
            mzTxtBox18.Validating += new CancelEventHandler(mzTxtBox18_Validating);
            mzTxtBox26.Validating += new CancelEventHandler(mzTxtBox26_Validating);
            mzTxtBox34.Validating += new CancelEventHandler(mzTxtBox34_Validating);
            mzTxtBox42.Validating += new CancelEventHandler(mzTxtBox42_Validating);
            mzTxtBox50.Validating += new CancelEventHandler(mzTxtBox50_Validating);
            mzTxtBox58.Validating += new CancelEventHandler(mzTxtBox58_Validating);
            mzTxtBox66.Validating += new CancelEventHandler(mzTxtBox66_Validating);
            mzTxtBox74.Validating += new CancelEventHandler(mzTxtBox74_Validating);

            // MidTerm1
            mzTxtBox3.TextChanged += new EventHandler(mzTxtBox3_TextChanged);
            mzTxtBox11.TextChanged += new EventHandler(mzTxtBox11_TextChanged);
            mzTxtBox19.TextChanged += new EventHandler(mzTxtBox19_TextChanged);
            mzTxtBox27.TextChanged += new EventHandler(mzTxtBox27_TextChanged);
            mzTxtBox35.TextChanged += new EventHandler(mzTxtBox35_TextChanged);
            mzTxtBox43.TextChanged += new EventHandler(mzTxtBox43_TextChanged);
            mzTxtBox51.TextChanged += new EventHandler(mzTxtBox51_TextChanged);
            mzTxtBox59.TextChanged += new EventHandler(mzTxtBox59_TextChanged);
            mzTxtBox67.TextChanged += new EventHandler(mzTxtBox67_TextChanged);
            mzTxtBox75.TextChanged += new EventHandler(mzTxtBox75_TextChanged);

            // MidTerm2
            mzTxtBox4.TextChanged += new EventHandler(mzTxtBox4_TextChanged);
            mzTxtBox12.TextChanged += new EventHandler(mzTxtBox12_TextChanged);
            mzTxtBox20.TextChanged += new EventHandler(mzTxtBox20_TextChanged);
            mzTxtBox28.TextChanged += new EventHandler(mzTxtBox28_TextChanged);
            mzTxtBox36.TextChanged += new EventHandler(mzTxtBox36_TextChanged);
            mzTxtBox44.TextChanged += new EventHandler(mzTxtBox44_TextChanged);
            mzTxtBox52.TextChanged += new EventHandler(mzTxtBox52_TextChanged);
            mzTxtBox60.TextChanged += new EventHandler(mzTxtBox60_TextChanged);
            mzTxtBox68.TextChanged += new EventHandler(mzTxtBox68_TextChanged);
            mzTxtBox76.TextChanged += new EventHandler(mzTxtBox76_TextChanged);

            // Final
            mzTxtBox5.TextChanged += new EventHandler(mzTxtBox5_TextChanged);
            mzTxtBox13.TextChanged += new EventHandler(mzTxtBox13_TextChanged);
            mzTxtBox21.TextChanged += new EventHandler(mzTxtBox21_TextChanged);
            mzTxtBox29.TextChanged += new EventHandler(mzTxtBox29_TextChanged);
            mzTxtBox37.TextChanged += new EventHandler(mzTxtBox37_TextChanged);
            mzTxtBox45.TextChanged += new EventHandler(mzTxtBox45_TextChanged);
            mzTxtBox53.TextChanged += new EventHandler(mzTxtBox53_TextChanged);
            mzTxtBox61.TextChanged += new EventHandler(mzTxtBox61_TextChanged);
            mzTxtBox69.TextChanged += new EventHandler(mzTxtBox69_TextChanged);
            mzTxtBox77.TextChanged += new EventHandler(mzTxtBox77_TextChanged);

        }

        public bool DoesRowExist(int rowNumber)
        {
            DataTable myDT = new DataTable();
            SQLiteDataAdapter myDA = new SQLiteDataAdapter("SELECT RowNum FROM Lessons WHERE User_ID=@selectedUserID AND Term=@selectedTerm", conn);
            myDA.SelectCommand.Parameters.AddWithValue("@selectedUserID", currentUserID);
            myDA.SelectCommand.Parameters.AddWithValue("@selectedTerm", currentTerm);
            myDA.Fill(myDT);

            bool rowExist = false;
            for (int i = 0; i < myDT.Rows.Count; i++)
            {
                int wantedRowNum = int.Parse(myDT.Rows[i][0].ToString());
                if (wantedRowNum == rowNumber)
                {
                    rowExist = true;
                    break;
                }
            }
            return rowExist;
        }

        public int TotalCredits(int UserID)
        {
            int sum;
            DataTable CreditsColumn = new DataTable();
            SQLiteDataAdapter creditsAdapter = new SQLiteDataAdapter("SELECT SUM(Credits) FROM Lessons WHERE User_ID=@selectedUserID", conn);
            creditsAdapter.SelectCommand.Parameters.AddWithValue("@selectedUserID", UserID);
            creditsAdapter.Fill(CreditsColumn);
            if (CreditsColumn.Rows[0][0] == DBNull.Value)
            {
                sum = 0;
            }
            else
            {
                sum = int.Parse(CreditsColumn.Rows[0][0].ToString());
            }

            return sum
;
        }

        public decimal GetGPA(int UserID)
        {
            decimal overallGPA;
            DataTable avrgANDcredits = new DataTable();
            SQLiteDataAdapter gradesAdapter = new SQLiteDataAdapter("SELECT Credits,FourScale FROM Lessons WHERE User_ID=@selectedUserID", conn);
            gradesAdapter.SelectCommand.Parameters.AddWithValue("@selectedUserID", UserID);
            gradesAdapter.Fill(avrgANDcredits);

            decimal[] gradesArray = new decimal[100];
            for (int i = 0; i < avrgANDcredits.Rows.Count; i++)
            {
                gradesArray[i] = decimal.Parse(avrgANDcredits.Rows[i][0].ToString()) * decimal.Parse(avrgANDcredits.Rows[i][1].ToString());
            }

            decimal gradesSum = gradesArray.Sum();
            if (TotalCredits(UserID) == 0)
            {
                overallGPA = 0;
            }
            else
            {
                overallGPA = gradesSum / TotalCredits(UserID);
                overallGPA = Math.Round(overallGPA, 2);
            }
            return overallGPA;
        }

        #region // THE AVERAGE - Letter - Score \\

        public decimal GetAverage(int forRow, int theUserID, int theTerm)
        {
            DataTable MidtermPercentageT = new DataTable();
            SQLiteDataAdapter MidTermPercentAdapter = new SQLiteDataAdapter("SELECT VizePercentage FROM Users WHERE ID=@selectedUserID", conn);
            MidTermPercentAdapter.SelectCommand.Parameters.AddWithValue("@selectedUserID", theUserID);
            MidTermPercentAdapter.Fill(MidtermPercentageT);
            decimal midTermPercentValue = decimal.Parse(MidtermPercentageT.Rows[0][0].ToString());


            DataTable FinalPercentageT = new DataTable();
            SQLiteDataAdapter FinalPercentAdapter = new SQLiteDataAdapter("SELECT FinalPercentageFinal FROM Users WHERE ID=@selectedUserID", conn);
            FinalPercentAdapter.SelectCommand.Parameters.AddWithValue("@selectedUserID", theUserID);
            FinalPercentAdapter.Fill(FinalPercentageT);
            decimal finalPercentValue = decimal.Parse(FinalPercentageT.Rows[0][0].ToString());

            // Average Doesn't Include Fucking CREDITS!
            // However, SCORE does (after deviding by the total credits)
            decimal finalGrade = 0;
            decimal vize1Grade = 0;
            decimal vize2Grade = 0;
            decimal average = finalGrade + vize1Grade + vize2Grade;

            // Getting the final point for this row
            DataTable FinalColumn = new DataTable();
            SQLiteDataAdapter FinalAdapter = new SQLiteDataAdapter("SELECT Final_Notu FROM Lessons WHERE User_ID=@selectedUserID AND Term=@selectedTerm AND RowNum=@currentRow", conn);
            FinalAdapter.SelectCommand.Parameters.AddWithValue("@selectedUserID", theUserID);
            FinalAdapter.SelectCommand.Parameters.AddWithValue("@selectedTerm", theTerm);
            FinalAdapter.SelectCommand.Parameters.AddWithValue("@currentRow", forRow);
            FinalAdapter.Fill(FinalColumn);
            if (DoesRowExist(forRow) == true)
            {
                if (FinalColumn.Rows[0][0] != DBNull.Value)
                {
                    finalGrade = finalPercentValue * decimal.Parse(FinalColumn.Rows[0][0].ToString());
                }
            }


            // Getting the vize1 point for this row
            DataTable MidTerm1Column = new DataTable();
            SQLiteDataAdapter MidTerm1Adapter = new SQLiteDataAdapter("SELECT Vize_Notu1 FROM Lessons WHERE User_ID=@selectedUserID AND Term=@selectedTerm AND RowNum=@currentRow", conn);
            MidTerm1Adapter.SelectCommand.Parameters.AddWithValue("@selectedUserID", theUserID);
            MidTerm1Adapter.SelectCommand.Parameters.AddWithValue("@selectedTerm", theTerm);
            MidTerm1Adapter.SelectCommand.Parameters.AddWithValue("@currentRow", forRow);
            MidTerm1Adapter.Fill(MidTerm1Column);

            
            if (DoesRowExist(forRow) == true)
            {
                if (MidTerm1Column.Rows[0][0] != DBNull.Value)
                {
                    vize1Grade = (midTermPercentValue) * (decimal.Parse(MidTerm1Column.Rows[0][0].ToString()));
                }
            }

            // Getting the vize2 point for this row
            DataTable MidTerm2Column = new DataTable();
            SQLiteDataAdapter MidTerm2Adapter = new SQLiteDataAdapter("SELECT Vize_Notu2 FROM Lessons WHERE User_ID=@selectedUserID AND Term=@selectedTerm AND RowNum=@currentRow", conn);
            MidTerm2Adapter.SelectCommand.Parameters.AddWithValue("@selectedUserID", theUserID);
            MidTerm2Adapter.SelectCommand.Parameters.AddWithValue("@selectedTerm", theTerm);
            MidTerm2Adapter.SelectCommand.Parameters.AddWithValue("@currentRow", forRow);
            MidTerm2Adapter.Fill(MidTerm2Column);

            if (DoesRowExist(forRow) == true)
            {
                if (MidTerm2Column.Rows[0][0] != DBNull.Value)
                {
                    vize2Grade = (midTermPercentValue) * (decimal.Parse(MidTerm2Column.Rows[0][0].ToString()));
                }
            }
            average = vize1Grade + vize2Grade+ finalGrade;
            return average;

        }

        public string GetLetter(int _forRow, int _theUserID, int _theTerm)
        {
            string letter = "";

            // Getting the MIN points table:
            DataTable minPoint = new DataTable();
            SQLiteDataAdapter minPointAdapter = new SQLiteDataAdapter("SELECT Min_Point FROM Grading_System WHERE User_ID=@selectedUserID", conn);
            minPointAdapter.SelectCommand.Parameters.AddWithValue("@selectedUserID", _theUserID);
            minPointAdapter.Fill(minPoint);

            // Getting the MAX points table:
            DataTable maxPoint = new DataTable();
            SQLiteDataAdapter maxPointAdapter = new SQLiteDataAdapter("SELECT Max_Point FROM Grading_System WHERE User_ID=@selectedUserID", conn);
            maxPointAdapter.SelectCommand.Parameters.AddWithValue("@selectedUserID", _theUserID);
            maxPointAdapter.Fill(maxPoint);

            // Getting the Letters table:
            DataTable letterPoint = new DataTable();
            SQLiteDataAdapter letterPointAdapter = new SQLiteDataAdapter("SELECT Letter FROM Grading_System WHERE User_ID=@selectedUserID", conn);
            letterPointAdapter.SelectCommand.Parameters.AddWithValue("@selectedUserID", _theUserID);
            letterPointAdapter.Fill(letterPoint);

            decimal _average = Math.Round(GetAverage(_forRow, _theUserID, _theTerm));

            for (int i = 0; i < letterPoint.Rows.Count; i++)
            {
                if (_average >= decimal.Parse(minPoint.Rows[i][0].ToString()) && _average <= decimal.Parse(maxPoint.Rows[i][0].ToString()))
                {
                    letter = letterPoint.Rows[i][0].ToString();
                    break;
                }
            }

            return letter;
        }

        public decimal GetOutOfFour(int _forRow, int _theUserID, int _theTerm)
        {
            decimal result = 0;

            // Getting the MIN points table:
            DataTable minPoint = new DataTable();
            SQLiteDataAdapter minPointAdapter = new SQLiteDataAdapter("SELECT Min_Point FROM Grading_System WHERE User_ID=@selectedUserID", conn);
            minPointAdapter.SelectCommand.Parameters.AddWithValue("@selectedUserID", _theUserID);
            minPointAdapter.Fill(minPoint);

            // Getting the MAX points table:
            DataTable maxPoint = new DataTable();
            SQLiteDataAdapter maxPointAdapter = new SQLiteDataAdapter("SELECT Max_Point FROM Grading_System WHERE User_ID=@selectedUserID", conn);
            maxPointAdapter.SelectCommand.Parameters.AddWithValue("@selectedUserID", _theUserID);
            maxPointAdapter.Fill(maxPoint);

            // Getting the FourScale table:
            DataTable outtaFour = new DataTable();
            SQLiteDataAdapter outtaFourAdapter = new SQLiteDataAdapter("SELECT OutOfFour FROM Grading_System WHERE User_ID=@selectedUserID", conn);
            outtaFourAdapter.SelectCommand.Parameters.AddWithValue("@selectedUserID", _theUserID);
            outtaFourAdapter.Fill(outtaFour);

            decimal _average = Math.Round(GetAverage(_forRow, _theUserID, _theTerm));

            for (int i = 0; i < outtaFour.Rows.Count; i++)
            {
                if (_average >= decimal.Parse(minPoint.Rows[i][0].ToString()) && _average <= decimal.Parse(maxPoint.Rows[i][0].ToString()))
                {
                    result = decimal.Parse(outtaFour.Rows[i][0].ToString());
                    break;
                }
            }

            return result;
        }

        public void AddFourScale(int _forRow, int _theUserID, int _theTerm)
        {
            
            SQLiteCommand cmd = new SQLiteCommand("UPDATE Lessons SET FourScale=@OuttaFour WHERE User_ID=@selectedUserID AND Term=@selectedTerm AND RowNum=@selectedRowNum", conn);
            cmd.Parameters.AddWithValue("@OuttaFour", GetOutOfFour(_forRow, _theUserID, _theTerm));
            cmd.Parameters.AddWithValue("selectedUserID", _theUserID);
            cmd.Parameters.AddWithValue("selectedTerm", _theTerm);
            cmd.Parameters.AddWithValue("selectedRowNum", _forRow);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void SetAvrgLetterFourScale()
        {
            if (mzTxtBox1.Text != "")
            {
                mzTxtBox6.Text = GetAverage(1, currentUserID, currentTerm).ToString();
                mzTxtBox7.Text = GetLetter(1, currentUserID, currentTerm).ToString();
                mzTxtBox8.Text = GetOutOfFour(1, currentUserID, currentTerm).ToString();
            }

            if (mzTxtBox9.Text != "")
            {
                mzTxtBox14.Text = GetAverage(2, currentUserID, currentTerm).ToString();
                mzTxtBox15.Text = GetLetter(2, currentUserID, currentTerm).ToString();
                mzTxtBox16.Text = GetOutOfFour(2, currentUserID, currentTerm).ToString();
            }

            if (mzTxtBox17.Text != "")
            {
                mzTxtBox22.Text = GetAverage(3, currentUserID, currentTerm).ToString();
                mzTxtBox23.Text = GetLetter(3, currentUserID, currentTerm).ToString();
                mzTxtBox24.Text = GetOutOfFour(3, currentUserID, currentTerm).ToString();
            }

            if (mzTxtBox25.Text != "")
            {
                mzTxtBox30.Text = GetAverage(4, currentUserID, currentTerm).ToString();
                mzTxtBox31.Text = GetLetter(4, currentUserID, currentTerm).ToString();
                mzTxtBox32.Text = GetOutOfFour(4, currentUserID, currentTerm).ToString();
            }

            if (mzTxtBox33.Text != "")
            {
                mzTxtBox38.Text = GetAverage(5, currentUserID, currentTerm).ToString();
                mzTxtBox39.Text = GetLetter(5, currentUserID, currentTerm).ToString();
                mzTxtBox40.Text = GetOutOfFour(5, currentUserID, currentTerm).ToString();
            }

            if (mzTxtBox41.Text != "")
            {
                mzTxtBox46.Text = GetAverage(6, currentUserID, currentTerm).ToString();
                mzTxtBox47.Text = GetLetter(6, currentUserID, currentTerm).ToString();
                mzTxtBox48.Text = GetOutOfFour(6, currentUserID, currentTerm).ToString();
            }

            if (mzTxtBox49.Text != "")
            {
                mzTxtBox54.Text = GetAverage(7, currentUserID, currentTerm).ToString();
                mzTxtBox55.Text = GetLetter(7, currentUserID, currentTerm).ToString();
                mzTxtBox56.Text = GetOutOfFour(7, currentUserID, currentTerm).ToString();
            }

            if (mzTxtBox57.Text != "")
            {
                mzTxtBox62.Text = GetAverage(8, currentUserID, currentTerm).ToString();
                mzTxtBox63.Text = GetLetter(8, currentUserID, currentTerm).ToString();
                mzTxtBox64.Text = GetOutOfFour(8, currentUserID, currentTerm).ToString();
            }

            if (mzTxtBox65.Text != "")
            {
                mzTxtBox69.Text = GetAverage(9, currentUserID, currentTerm).ToString();
                mzTxtBox70.Text = GetLetter(9, currentUserID, currentTerm).ToString();
                mzTxtBox71.Text = GetOutOfFour(9, currentUserID, currentTerm).ToString();
            }

            if (mzTxtBox73.Text != "")
            {
                mzTxtBox78.Text = GetAverage(10, currentUserID, currentTerm).ToString();
                mzTxtBox79.Text = GetLetter(10, currentUserID, currentTerm).ToString();
                mzTxtBox80.Text = GetOutOfFour(10, currentUserID, currentTerm).ToString();
            }

        }
        #endregion

        private void button11_Click(object sender, EventArgs e)
        {
            SettingsForm settingsWindow = new SettingsForm();
            settingsWindow.Show();
        }

        private void userNameLabel_Click(object sender, EventArgs e)
        {
            Login loginWindow = new Login();
            this.Hide();
            loginWindow.Show();
        }

        private void exitButton_MouseHover(object sender, EventArgs e)
        {
            exitButton.ForeColor = Color.Red;
        }

        private void exitButton_MouseLeave(object sender, EventArgs e)
        {
            exitButton.ForeColor = Color.Transparent;

        }

    }
}
