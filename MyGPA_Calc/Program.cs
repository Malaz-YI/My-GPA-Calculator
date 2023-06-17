using System;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data;

namespace MyGPA_Calc
{
    static class Program
    {
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            string myConString = Environment.CurrentDirectory + "\\GPAbase.db";

            SQLiteConnection conn = new SQLiteConnection($"data source ={myConString}");

            DataTable allUserIDs = new DataTable();
            SQLiteDataAdapter allUserIDsAdapter = new SQLiteDataAdapter("SELECT ID FROM Users", conn);
            allUserIDsAdapter.Fill(allUserIDs);
            int UserCount = allUserIDs.Rows.Count;
            //
            //
            //
            //
            //

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (UserCount > 0)
            {
                Application.Run(new Login());

            }
            else
            {
                Application.Run(new CreateProfile1());
            }
        }
    }
}
