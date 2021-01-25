using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace Hamilton_Library
{
    /// <summary>
    /// Interaction logic for IssueB.xaml
    /// </summary>
    public partial class IssueB : Window
    {
        Database_Manager objDB = new Database_Manager();

        public IssueB()
        {
            InitializeComponent();
            SqlConnection SqlConn = new SqlConnection(@"Data Source=VETASTW-01\SQLEXPRESS;Initial Catalog=LibrarySystem;Integrated Security=True");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)

        {
            Datagd.ItemsSource = objDB.ListBooks("%").DefaultView;

            Datagd2.ItemsSource = objDB.ListUsers("%").DefaultView;

        }
       
   

        private void IssueBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Datagd.SelectedIndex > -1 && Datagd2.SelectedIndex > -1)

            {

                DataRowView bookrow = (DataRowView)Datagd.SelectedItems[0];

                string bookname = Convert.ToString(bookrow["BookName"]);



                if (Convert.ToString(bookrow["Available"]) != "No")

                {

                    DataRowView userrow = (DataRowView)Datagd2.SelectedItems[0];

                    string username = Convert.ToString(userrow["FullName"]);



                    objDB.IssueBook(bookname, username);



                    bool bookIssued = objDB.IssueBook(bookname, username);



                    if (bookIssued == true)

                    {

                        MessageBox.Show("Book Issued");

                        Datagd.ItemsSource = objDB.ListBooks("%").DefaultView;

                        Datagd2.ItemsSource = objDB.ListUsers("%").DefaultView;

                    }

                }

                else

                {

                    MessageBox.Show("Unable to issue, book currently out");

                    Datagd.ItemsSource = objDB.ListBooks("%").DefaultView;

                }

                TextBookName.Clear();

                textfullname.Clear();

            }

            else

            {

                MessageBox.Show("Select a book and user to issue");

            }

        }

        private void ReturnToMM_Click(object sender, RoutedEventArgs e)
       {

            AdminPage admin = new AdminPage();

           Hide();

            admin.Show();
        }

        private void TextBookName_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            Datagd.ItemsSource = objDB.ListBooks(TextBookName.Text).DefaultView;
        }

        private void textfullname_TextChanged(object sender, TextChangedEventArgs e)
        {
            Datagd2.ItemsSource = objDB.ListUsers(textfullname.Text).DefaultView;
        }
    }
    }

