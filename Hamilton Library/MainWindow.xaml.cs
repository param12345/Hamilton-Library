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
        public string FullName;
        string usertype;
        
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
                SqlStmt = "Select FullName, usertype from Users u Inner Join Login l  on u.Username = l.username where u.Username = @UserName and password = @Password ";
                using (SqlCommand cmd = new SqlCommand(SqlStmt, SqlConn))

                {

                    cmd.Parameters.AddWithValue("@UserName", UserName);

                    cmd.Parameters.AddWithValue("@Password", Pass);

                    SqlConn.Open();

                    SqlReader = cmd.ExecuteReader();

                }

                if (SqlReader.HasRows)
                {
                    MessageBox.Show("Login Successful");
                    SqlReader.Read();
                    FullName = SqlReader[0].ToString();
                    usertype = SqlReader[1].ToString();

                    if (usertype == "Admin")

                    {

                        AdminPage admin = new AdminPage(FullName);

                        admin.Show();

                    }

                    else

                    {

                        Dashboard dash = new Dashboard(UserName);
                        dash.userName = UserName;
                        dash.Show();
                        
                    }

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

 