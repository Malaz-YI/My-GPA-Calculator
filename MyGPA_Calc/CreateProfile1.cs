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
    public partial class CreateProfile1 : Form
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

        public CreateProfile1()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            CreateProfile2 ProfileSetup2 = new CreateProfile2();
            if (mzTxtBox1.Text == "" || mzTxtBox2.Text == "" || mzTxtBox3.Text == "" || mzTxtBox4.Text == "")
            {
                MessageBox.Show("All Fields Are Required!");
            }
            else
            {
                if (int.TryParse(mzTxtBox2.Text, out int vizeNum) == true &&
                decimal.TryParse(mzTxtBox3.Text, out decimal vizePercentage) == true &&
                decimal.TryParse(mzTxtBox4.Text, out decimal finalPercentage) == true)
                {
                    if (vizeNum > 0 && vizeNum < 3)
                    {
                        SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Users(Name,NumOfMidterms,VizePercentage,FinalPercentageFinal) VALUES(@enteredName,@enteredMidtermsCount,@enteredVizePercent,@enteredFinalPercent)", conn);
                        cmd.Parameters.AddWithValue("@enteredName", mzTxtBox1.Text);
                        cmd.Parameters.AddWithValue("@enteredMidtermsCount", vizeNum);
                        cmd.Parameters.AddWithValue("@enteredVizePercent", vizePercentage/100);
                        cmd.Parameters.AddWithValue("@enteredFinalPercent", finalPercentage/100);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        ProfileSetup2.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Midterms count cannot be any number other than 1 or 2");
                    }
                }
                else
                {
                    MessageBox.Show("Enter Numbers Only!");
                }
            }

        }

        private void mzTxtBox1_Enter(object sender, EventArgs e)
        {
            mzTxtBox1.Text = "";
            mzTxtBox1.ForeColor = Color.FromArgb(64, 64, 64);
            mzTxtBox1.Enter -= new EventHandler(mzTxtBox1_Enter);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mzTxtBox2_Enter(object sender, EventArgs e)
        {
            mzTxtBox2.Text = "";
            mzTxtBox2.ForeColor = Color.FromArgb(64, 64, 64);
            mzTxtBox2.Enter -= new EventHandler(mzTxtBox2_Enter);
        }

        private void mzTxtBox3_Enter(object sender, EventArgs e)
        {
            mzTxtBox3.Text = "";
            mzTxtBox3.ForeColor = Color.FromArgb(64, 64, 64);
            mzTxtBox3.Enter -= new EventHandler(mzTxtBox3_Enter);
        }

        private void mzTxtBox4_Enter(object sender, EventArgs e)
        {
            mzTxtBox4.Text = "";
            mzTxtBox4.ForeColor = Color.FromArgb(64, 64, 64);
            mzTxtBox4.Enter -= new EventHandler(mzTxtBox4_Enter);
        }

        private void CreateProfile1_MouseDown(object sender, MouseEventArgs e)
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
    }
}
