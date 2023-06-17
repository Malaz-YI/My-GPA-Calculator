using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Runtime.InteropServices;

namespace MyGPA_Calc
{
    public partial class CreateProfile2 : Form
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

        static string myConString = Environment.CurrentDirectory + "\\GPAbase.db";

        SQLiteConnection conn = new SQLiteConnection($"data source ={myConString}");

        CreateProfile1 ProfileSetup1 = new CreateProfile1();
        Form1 oneVizeForm = new Form1();
        Form2 twoVizeForm = new Form2();
        public CreateProfile2()
        {
            InitializeComponent();
        }

        private void CreateProfile2_Load(object sender, EventArgs e)
        {
            ConfirmButton.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mzTxtBox2_Enter(object sender, EventArgs e)
        {
            mzTxtBox2.Text = "";
            mzTxtBox2.ForeColor = Color.FromArgb(64,64,64);
            mzTxtBox2.Font = new Font("Microsoft YaHei", 14.25f);
            mzTxtBox2.Enter -= new EventHandler(mzTxtBox2_Enter);
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            DataTable allUserIDs = new DataTable();
            SQLiteDataAdapter allUserIDsAdapter = new SQLiteDataAdapter("SELECT ID FROM Users ORDER BY ID DESC LIMIT 1", conn);
            allUserIDsAdapter.Fill(allUserIDs);
            int lastUser = int.Parse(allUserIDs.Rows[0][0].ToString());

            if (decimal.TryParse(mzTxtBox1.Text, out decimal enteredMin) && decimal.TryParse(mzTxtBox2.Text, out decimal enteredMax) && decimal.TryParse(mzTxtBox4.Text, out decimal enteredOuttaFour))
            {
                SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Grading_System(User_ID,Min_Point,Max_Point,Letter,OutOfFour) VALUES(@theUserID,@theMinPoint,@theMaxPoint,@theLetter,@theOuttaFour)", conn);
                cmd.Parameters.AddWithValue("@theUserID", lastUser);
                cmd.Parameters.AddWithValue("@theMinPoint", enteredMin);
                cmd.Parameters.AddWithValue("@theMaxPoint", enteredMax);
                cmd.Parameters.AddWithValue("@theLetter", mzTxtBox3.Text);
                cmd.Parameters.AddWithValue("@theOuttaFour", enteredOuttaFour);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                // Reset:
                mzTxtBox1.Text = "";
                mzTxtBox2.Text = "";
                mzTxtBox3.Text = "";
                mzTxtBox4.Text = "";
                ConfirmButton.Visible = true;
                mzTxtBox1.ReadOnly = false;
                mzTxtBox3.ReadOnly = false;
                mzTxtBox4.ReadOnly = false;
            }
            else
            {
                MessageBox.Show("You Can Only Enter Numbers!","Error",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
 
        }

        private void CreateProfile2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            if (mzTxtBox1.Text != "" && mzTxtBox2.Text != "" && mzTxtBox3.Text != "" && mzTxtBox4.Text != "")
            {
                DataTable allUserIDs = new DataTable();
                SQLiteDataAdapter allUserIDsAdapter = new SQLiteDataAdapter("SELECT ID FROM Users ORDER BY ID DESC LIMIT 1", conn);
                allUserIDsAdapter.Fill(allUserIDs);
                int lastUser = int.Parse(allUserIDs.Rows[0][0].ToString());

                if (decimal.TryParse(mzTxtBox1.Text, out decimal enteredMin) && decimal.TryParse(mzTxtBox2.Text, out decimal enteredMax) && decimal.TryParse(mzTxtBox4.Text, out decimal enteredOuttaFour))
                {
                    SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Grading_System(User_ID,Min_Point,Max_Point,Letter,OutOfFour) VALUES(@theUserID,@theMinPoint,@theMaxPoint,@theLetter,@theOuttaFour)", conn);
                    cmd.Parameters.AddWithValue("@theUserID", lastUser);
                    cmd.Parameters.AddWithValue("@theMinPoint", enteredMin);
                    cmd.Parameters.AddWithValue("@theMaxPoint", enteredMax);
                    cmd.Parameters.AddWithValue("@theLetter", mzTxtBox3.Text);
                    cmd.Parameters.AddWithValue("@theOuttaFour", enteredOuttaFour);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    MessageBox.Show("You Can Only Enter Numbers!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    ConfirmButton.Click -= new EventHandler(ConfirmButton_Click);
                }

            }

            DataTable _allUserIDs = new DataTable();
            SQLiteDataAdapter _allUserIDsAdapter = new SQLiteDataAdapter("SELECT ID,NumOfMidterms FROM Users ORDER BY ID DESC LIMIT 1", conn);
            _allUserIDsAdapter.Fill(_allUserIDs);
            int _lastUser = int.Parse(_allUserIDs.Rows[0][0].ToString());
            int numOfVize = int.Parse(_allUserIDs.Rows[0][1].ToString());

            if (numOfVize == 1)
            {
                oneVizeForm.currentUserID = _lastUser;
                oneVizeForm.Show();
                this.Hide();
            }
            else
            {
                twoVizeForm.currentUserID = _lastUser;
                twoVizeForm.Show();
                this.Hide();
            }
        }
    }
}
