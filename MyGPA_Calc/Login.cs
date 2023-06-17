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
    public partial class Login : Form
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

        Form1 OneVizeForm = new Form1();
        Form2 TwoVizeForm = new Form2();
        //Login LoginForm = new Login();

        public Login()
        {
            InitializeComponent();
            PopulateComboBox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void PopulateComboBox()
        {
            DataTable profileNames = new DataTable();
            SQLiteDataAdapter profileAdapter = new SQLiteDataAdapter("SELECT Name FROM USERS", conn);
            profileAdapter.Fill(profileNames);
            for (int i = 0; i < profileNames.Rows.Count; i++)
            {
                LoginComboBox.Items.Add(profileNames.Rows[i][0]);
            }
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Getting number of midterms:
                DataTable vizeCount = new DataTable();
                SQLiteDataAdapter vizeCountAdapter = new SQLiteDataAdapter("SELECT NumOfMidterms FROM Users WHERE Name=@selectedName", conn);
                vizeCountAdapter.SelectCommand.Parameters.AddWithValue("@selectedName", LoginComboBox.SelectedItem.ToString());
                vizeCountAdapter.Fill(vizeCount);

                if (int.Parse(vizeCount.Rows[0][0].ToString()) == 1)
                {
                    // Getting the user ID:
                    DataTable profileNumber = new DataTable();
                    SQLiteDataAdapter profileNumAdapter = new SQLiteDataAdapter("SELECT ID FROM Users WHERE Name=@selectedName", conn);
                    profileNumAdapter.SelectCommand.Parameters.AddWithValue("@selectedName", LoginComboBox.SelectedItem.ToString());
                    profileNumAdapter.Fill(profileNumber);

                    OneVizeForm.currentUserID = int.Parse(profileNumber.Rows[0][0].ToString());

                    this.Hide();
                    OneVizeForm.Show();
                }
                else
                {
                    // Getting the user ID:
                    DataTable profileNumber = new DataTable();
                    SQLiteDataAdapter profileNumAdapter = new SQLiteDataAdapter("SELECT ID FROM Users WHERE Name=@selectedName", conn);
                    profileNumAdapter.SelectCommand.Parameters.AddWithValue("@selectedName", LoginComboBox.SelectedItem.ToString());
                    profileNumAdapter.Fill(profileNumber);

                    TwoVizeForm.currentUserID = int.Parse(profileNumber.Rows[0][0].ToString());
                    this.Hide();
                    TwoVizeForm.Show();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("You Must Enter Username!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
          
        }

        private void LoginComboBox_Enter(object sender, EventArgs e)
        {
            LoginComboBox.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CreateProfile1 createWindow = new CreateProfile1();
            this.Hide();
            createWindow.Show();
        }

        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }
    }
}
