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
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    
    public partial class AdminPage : Window
    {
        public string FullName;
        Database_Manager objDB = new Database_Manager();

        public AdminPage(string fullname)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            FullName = fullname;
             
        }

        public AdminPage()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dgv_Books.ItemsSource = objDB.ListBooks("%").DefaultView;
        }

        private void AddBookbtn_Click(object sender, RoutedEventArgs e)
        {
            if(bookNameText.Text != "" && AuthorText.Text != "")
            {
                bool bookAdded = objDB.AddBooks(bookNameText.Text, AuthorText.Text);

                if (bookAdded== true)
                {

                    MessageBox.Show("Book Added");
                    bookNameText.Clear();
                    AuthorText.Clear();
                    dgv_Books.ItemsSource = objDB.ListBooks("%").DefaultView;
                }
            }
            else
            {
                MessageBox.Show("Enter Book Detials");
            }
        }

        private void Searchbtn_Click(object sender, RoutedEventArgs e)
        {
            dgv_Books.ItemsSource = objDB.ListBooks(Textbox.Text).DefaultView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(dgv_Books.SelectedIndex >= 1)
            {

                MessageBoxResult dialogResult = MessageBox.Show("Are you sure you want to delete the book?", "Hamilton_Library", MessageBoxButton.YesNo);
                if(dialogResult.ToString()=="Yes")
                {
                    DataRowView row = (DataRowView)dgv_Books.SelectedItems[0];
                    Int32 bookID = Convert.ToInt32(row["BookID"]);
                    if (Convert. ToString(row["Available"])!= "No")
                    {

                        objDB.DeleteBooks(bookID);
                        MessageBox.Show("Book Deleted");
                        bookNameText.Clear();
                        AuthorText.Clear();
                        dgv_Books.ItemsSource = objDB.ListBooks(Textbox.Text).DefaultView;

                    }
                    else
                    {
                        MessageBox.Show("Unable to delete, book currently out");
                        bookNameText.Clear();
                        AuthorText.Clear();
                        dgv_Books.ItemsSource = objDB.ListBooks(Textbox.Text).DefaultView;
                    }
                }
            }
            else
            {
                MessageBox.Show("Select book to delete");
            }
        }

        private void dgv_Books_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgv_Books.SelectedIndex > -1)
            {
                DataRowView row = (DataRowView)dgv_Books.SelectedItems[0];
               // bookNameText.Text = @BookName;
               // AuthorText.Text = @Author;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (dgv_Books.SelectedIndex >= 1)
            { 
                if (bookNameText.Text != "" && AuthorText.Text != "")
            {
                    DataRowView row = (DataRowView)dgv_Books.SelectedItems[0];
                    String bookname = bookNameText.Text;
                    String author = AuthorText.Text;
                    int bookID = Convert.ToInt16(row["BookID"]);
                    objDB.EditBooks(bookname, author, bookID);

                    bool bookEdited = objDB.EditBooks(bookNameText.Text, AuthorText.Text, Convert.ToInt32(bookID));

                    if(bookEdited ==true)
                    { 
                    MessageBox.Show("Book Edited");
                    bookNameText.Clear();
                    AuthorText.Clear();
                    dgv_Books.ItemsSource = objDB.ListBooks("%").DefaultView;
                }
            }
            else
            {
                MessageBox.Show("Complete All fields");
            }
        }
            else
            {
                MessageBox.Show("Select book to edit");
            }
    }

        private void Issuebookbtn_Click(object sender, RoutedEventArgs e)
        {
            IssueB issueBook = new IssueB();

            Hide();

            issueBook.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ReturnBook returnBook = new ReturnBook();

            Hide();

            returnBook.Show();
        }
    }
}
