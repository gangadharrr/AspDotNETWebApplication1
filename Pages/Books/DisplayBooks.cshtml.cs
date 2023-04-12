using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace AspDotNETWebApplication1.Pages.Books
{
    public class DisplayBooks : PageModel
    {
        public List<Books> BookList= new List<Books>();
        public void OnGet()
        {
            if (Request.Query["bookCode"]!="")
            {
                DeleteBook(Request.Query["bookCode"]);
            }
            try 
            {
                using (SqlConnection sqlConnection = new SqlConnection(SQLServerConnect.conn))
                {
                    sqlConnection.Open();
                    using (SqlCommand cmd = sqlConnection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT BOOK_CODE, BOOK_TITLE, AUTHOR, PUBLICATION, PRICE " +
                                            "FROM LMS_BOOK_DETAILS";
                        SqlDataReader reader =cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Books book = new Books();
                            book.Id = (string)reader["BOOK_CODE"];
                            book.Title = (string)reader["BOOK_TITLE"];
                            book.Author = (string)reader["AUTHOR"];
                            book.Publication = (string)reader["PUBLICATION"];
                            book.Price = (double)reader[4];
                            BookList.Add(book);
                        }
                    }

                }
            }
            catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void OnPost()
        {

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SQLServerConnect.conn))
                {
                    sqlConnection.Open();
                    using (SqlCommand cmd = sqlConnection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT BOOK_CODE, BOOK_TITLE, AUTHOR, PUBLICATION, PRICE " +
                                            "FROM LMS_BOOK_DETAILS";
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Books book = new Books();
                            book.Id = reader[0].ToString();
                            book.Title = reader[1].ToString();
                            book.Author = reader[2].ToString();
                            book.Publication = reader[3].ToString();
                            book.Price = (double)reader[4];
                            BookList.Add(book);
                        }
                    }

                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        public static void DeleteBook(string bookCode)
        {
            try 
            { 
                using(SqlConnection sqlConnection=new SqlConnection(SQLServerConnect.conn))
                {
                    sqlConnection.Open();
                    using(SqlCommand cmd = sqlConnection.CreateCommand())
                    {
                        cmd.CommandText = $"DELETE FROM LMS_BOOK_DETAILS WHERE BOOK_CODE='{bookCode}'";
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        
        }
    }

    public class Books
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }
        public string Publication { get; set; }
        public double Price { get; set; }
    }
}
