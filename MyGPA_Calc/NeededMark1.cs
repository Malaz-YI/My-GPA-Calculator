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

namespace MyGPA_Calc
{
    public partial class NeededMark1 : Form
    {
        static int openUser;

        #region // Initializing The SQLite Connection \\
        //[MALAZ IMPORTANT]: The following conncection string will result in 
        //either the 'Debug' or 'Release' folder
        /* static string myConString = System.IO.Directory.GetCurrentDirectory(); */
        // For currently testing the original directory:
        static string myConString = System.IO.Path.GetFullPath(@"..\..\") + "\\GPAbase.db";

        SQLiteConnection conn = new SQLiteConnection($"data source ={myConString}");
        #endregion

        public NeededMark1()
        {
            Login theform = new Login();
            openUser = comboBox1.SelectedIndex;
            InitializeComponent();
            comboBox1.Items.Add("Midterm");
            comboBox1.Items.Add("Final");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        public decimal NeededScore(int theUserID)
        {
            decimal result = 0;
            // Getting midTermPercentValue
            DataTable MidtermPercentageT = new DataTable();
            SQLiteDataAdapter MidTermPercentAdapter = new SQLiteDataAdapter("SELECT VizePercentage FROM Users WHERE ID=@selectedUserID", conn);
            MidTermPercentAdapter.SelectCommand.Parameters.AddWithValue("@selectedUserID", theUserID);
            MidTermPercentAdapter.Fill(MidtermPercentageT);
            decimal midTermPercentValue = decimal.Parse(MidtermPercentageT.Rows[0][0].ToString());

            // Getting finalPercentValue
            DataTable FinalPercentageT = new DataTable();
            SQLiteDataAdapter FinalPercentAdapter = new SQLiteDataAdapter("SELECT FinalPercentageFinal FROM Users WHERE ID=@selectedUserID", conn);
            FinalPercentAdapter.SelectCommand.Parameters.AddWithValue("@selectedUserID", theUserID);
            FinalPercentAdapter.Fill(FinalPercentageT);
            decimal finalPercentValue = decimal.Parse(FinalPercentageT.Rows[0][0].ToString());

            if (comboBox1.SelectedItem.ToString() == "Midterm")
            {
                decimal vize = decimal.Parse(label2.Text) * midTermPercentValue;
                return result = (decimal.Parse(label1.Text) - vize) / finalPercentValue;
            }
            else
            {
                decimal final = decimal.Parse(label2.Text) * finalPercentValue;
                return result = (decimal.Parse(label1.Text) - final) / midTermPercentValue;

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString()=="Midterm")
            {
                label3.Text = "then I need to get on final:";
            }
            else if (comboBox1.SelectedItem.ToString() == "Final")
            {
                label3.Text = "then I need to get on midterm:";
            }
            label3.Text = NeededScore(openUser).ToString();
        }

        private void mzTxtBox2_TextChanged(object sender, EventArgs e)
        {
            label3.Text = NeededScore(openUser).ToString();
        }

        private void mzTxtBox1_TextChanged(object sender, EventArgs e)
        {
            label3.Text = NeededScore(openUser).ToString();
        }
    }
}
