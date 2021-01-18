using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace Hamilton_Library
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }
        public string UserName;
        string Pass;
        SqlConnection SqlConn = new SqlConnection(@"Data Source=VETASTW-01\SQLEXPRESS;Initial Catalog=LibrarySystem;Integrated Security=True");

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {

            UserName = username.Text;
            Pass = Password.Password;
            LoginCheck();

        }
        private void LoginCheck()
        {
            SqlCommand SqlStr = new SqlCommand();
            SqlDataReader SqlReader;
            string SqlStmt;

            try
            {
                SqlStr.Connection = SqlConn;
                SqlStmt = "Select * from Login where username = '" + UserName + "' and password = '" + Pass + "'";
                SqlStr.CommandText = SqlStmt;
                SqlConn.Open();
                SqlReader = SqlStr.ExecuteReader();

                if (SqlReader.HasRows)
                {
                    MessageBox.Show("Login Successful");

                    Dashboard dash = new Dashboard(UserName);
                    dash.userName = UserName;
                    dash.Show();
                    SqlConn.Close();
                    Hide();
                }
                else
                {
                    MessageBox.Show("Check Username/Password");
                    username.Clear();
                    Password.Clear();
                    SqlConn.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Exception" + ex.Message);
                SqlConn.Close();
            }

           

    }
}
}

 