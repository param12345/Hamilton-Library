using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class ReturnBook: Window
    {
        Database_Manager objDB = new Database_Manager();
        private void Window_Loaded(object sender, RoutedEventArgs e)

        {

            datagrd3.ItemsSource = objDB.ListBooks("%").DefaultView;

        }

        private void ReturntoMM_Click(object sender, RoutedEventArgs e)
        {
            AdminPage admin = new AdminPage();

            Hide();

            admin.Show();
        }
      
        private void BookN_TextChanged(object sender, TextChangedEventArgs e)

        {

            datagrd3.ItemsSource = objDB.ListBooks(BookN.Text).DefaultView;

        }
        private void Returnbtn1_Click(object sender, RoutedEventArgs e)
        {
            if (datagrd3.SelectedIndex > -1)

            {

                DataRowView bookrow = (DataRowView)datagrd3.SelectedItems[0];



                if (Convert.ToString(bookrow["Available"]) == "No")

                {

                    int bookID = Convert.ToInt32(bookrow["BookID"]);

                    objDB.ReturnBook(bookID);

                    bool bookReturned = objDB.ReturnBook(bookID);



                    if (bookReturned == true)

                    {

                        MessageBox.Show("Book Returned");

                        datagrd3.ItemsSource = objDB.ListBooks("%").DefaultView;

                    }

                }

                else

                {

                    MessageBox.Show("Book not out");

                }

                BookN.Clear();

            }

            else

            {

                MessageBox.Show("Select a book to return");

            }

        }


    }
}
    

