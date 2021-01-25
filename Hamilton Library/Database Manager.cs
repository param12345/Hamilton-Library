using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows;

namespace Hamilton_Library
{
    class Database_Manager
    {


        SqlConnection SqlConn = new SqlConnection(@"Data Source=VETASTW-01\SQLEXPRESS;Initial Catalog=LibrarySystem;Integrated Security=True");
        SqlCommand SqlStr = new SqlCommand();
        SqlDataReader SqlReader;
        string SqlStmt;

        public DataTable ListBooks(string bookname)
        {

            DataTable dt = new DataTable();
            SqlDataReader SqlReader;
            try
            {

                SqlStr.Connection = SqlConn;
                SqlStmt = "Select  * from  Books where BookName like  '%' +@BookName + '%'";
                using (SqlStr = new SqlCommand(SqlStmt, SqlConn))
                {
                    SqlStr.Parameters.AddWithValue("@BookName", bookname);
                    SqlConn.Open();
                    SqlReader = SqlStr.ExecuteReader();

                    if (SqlReader.HasRows)
                    {

                        dt.Load(SqlReader);
                    }
                    SqlConn.Close();
                    return dt;


                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Database Exception" + ex.Message);
                SqlConn.Close();
                return null;
            }
        }
        public bool AddBooks(string bookname, string author)
        {

            bool bookAdded = false;
            try
            {
                SqlStr.Connection = SqlConn;
                SqlStmt = "Insert Into Books(bookname, Author, Available) values (@BookName, @Author,@Available)";

                using (SqlStr = new SqlCommand(SqlStmt, SqlConn))
                {
                    SqlStr.Parameters.AddWithValue("@BookName", bookname);
                    SqlStr.Parameters.AddWithValue("@Author", author);
                    SqlStr.Parameters.AddWithValue("@Available", "Yes");
                    SqlStr.CommandText = SqlStmt;

                    SqlConn.Open();
                    Int32 affectedRecords = SqlStr.ExecuteNonQuery();

                    if (affectedRecords > 0)
                    {

                        bookAdded = true;
                    }
                    SqlConn.Close();
                }
            }

            catch (Exception ex)
            {

                MessageBox.Show("Database Exception" + ex.Message);
                SqlConn.Close();
            }
            return bookAdded;
        }


        public bool DeleteBooks(int bookID)
        {

            bool bookDeleted = false;
            try
            {
                SqlStr.Connection = SqlConn;
                SqlStmt = "Delete from Books where BookID =@BookID";
                using (SqlStr = new SqlCommand(SqlStmt, SqlConn))
                {
                    SqlStr.Parameters.AddWithValue("@BookID", bookID);
                    SqlConn.Open();
                    Int32 affectedRecords = SqlStr.ExecuteNonQuery();

                    if (affectedRecords > 0)
                    {

                        bookDeleted = true;
                    }
                    SqlConn.Close();
                }
                return bookDeleted;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Database Exception" + ex.Message);
                SqlConn.Close();
            }
            return bookDeleted;
        }
        public bool EditBooks(string bookname, string author, int bookID)
        {

            bool bookEdited = false;
            try
            {
                SqlStr.Connection = SqlConn;
                SqlStmt = "Update Books set BookName =@BookName, Author=@Author where BookID =@BookId";

                using (SqlStr = new SqlCommand(SqlStmt, SqlConn))
                {
                    SqlStr.Parameters.AddWithValue("@BookName", bookname);
                    SqlStr.Parameters.AddWithValue("@Author", author);
                    SqlStr.Parameters.AddWithValue("@BookID", bookID);

                    SqlConn.Open();
                    Int32 affectedRecords = SqlStr.ExecuteNonQuery();

                    if (affectedRecords > 0)
                    {

                        bookEdited = true;
                    }
                    SqlConn.Close();
                }
                SqlConn.Close();
            }

            catch (Exception ex)
            {

                MessageBox.Show("Database Exception" + ex.Message);
                SqlConn.Close();
            }
            return bookEdited;
        }


        public DataTable ListUsers(string userfullname)
        {
            DataTable dt = new DataTable();
            SqlDataReader SqlReader;
            try
            {
                SqlStr.Connection = SqlConn;
                SqlStmt = "Select * from  Users where FullName like '%"  +@userfullname + "%'";

                using (SqlStr = new SqlCommand(SqlStmt, SqlConn))
                {
                    SqlStr.Parameters.AddWithValue("@userfullname", userfullname);
                    SqlConn.Open();
                    SqlReader = SqlStr.ExecuteReader();
                }
                   

                    if (SqlReader.HasRows)
                    {

                    dt.Load(SqlReader);
                    }
                    SqlConn.Close();
                return dt;
                }
            

            catch (Exception ex)
            {

                MessageBox.Show("Database Exception" + ex.Message);
                SqlConn.Close();
            }
            return null;
        }
        public bool IssueBook(string bookname, string username)

        {

            bool bookIssued = false;

            int affectedRecords;



            try

            {

                SqlStr.Connection = SqlConn;

                SqlStmt = "Update Books set Borrower= '" + @username + "', available ='No' where BookName= '" + @bookname + "'";

                using (SqlCommand cmd = new SqlCommand(SqlStmt, SqlConn))

                {

                    cmd.Parameters.AddWithValue("@username", username);

                    cmd.Parameters.AddWithValue("@bookname", bookname);

                    SqlConn.Open();

                    affectedRecords = cmd.ExecuteNonQuery();

                }

                if (affectedRecords > 0)

                {

                    bookIssued = true;

                }
                SqlConn.Close();
                }

            catch (Exception ex)

            {

             MessageBox.Show("Database Exception" + ex.Message);

            SqlConn.Close();

            }
            return bookIssued;

        }
        public bool ReturnBook(int bookID)

        {

            bool bookReturned = false;

            int affectedRecords;



            try

            {

                SqlStr.Connection = SqlConn;

                SqlStmt = "Update Books set Borrower= ' NULL', available ='Yes' where BookID= '" + @bookID + "'";

                using (SqlCommand cmd = new SqlCommand(SqlStmt, SqlConn))

                {

                    cmd.Parameters.AddWithValue("@bookID", bookID);

                    SqlConn.Open();

                    affectedRecords = cmd.ExecuteNonQuery();

                }



                if (affectedRecords > 0)

                {

                    bookReturned = true;

                }



                SqlConn.Close();

            }

            catch (Exception ex)

            {

                MessageBox.Show("Database Exception" + ex.Message);

                SqlConn.Close();

            }

            return bookReturned;

        }
    }
}




            
    
