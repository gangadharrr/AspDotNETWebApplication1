using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;

namespace AspDotNETWebApplication1.Pages.Books
{
    
    public class UpdateBook : PageModel
    {
        public List<Books> BookList = new List<Books>();
        public string Message = "";
        public bool Error=false;
        public void OnGet()
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
        public void OnPost() 
        {
            
            try
            {
                Books book = new Books();
                book.Id = Request.Query["bookCode"];
                book.Title = Request.Form["Title"];
                book.Author = Request.Form["Author"];
                book.Publication = Request.Form["Publication"];
                book.Price = Convert.ToDouble(Request.Form["Price"].ToString());
                using (SqlConnection sqlConnection = new SqlConnection(SQLServerConnect.conn))
                {
                    sqlConnection.Open();
                    using (SqlCommand cmd = sqlConnection.CreateCommand())
                    {
                        cmd.CommandText = $"UPDATE LMS_BOOK_DETAILS " +
                                        $"SET BOOK_TITLE='{book.Title}',AUTHOR='{book.Author}', PUBLICATION='{book.Publication}', PRICE={book.Price} " +
                                        $"WHERE BOOK_CODE='{book.Id}'";
                        cmd.ExecuteNonQuery();
                        Message = "Added Successfully";
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Error = true;
                Message = sqlEx.Message;
            }
            catch (Exception ex)
            {
                Error = true;
                Message = ex.Message;
            }
            Response.Redirect("/Books/DisplayBooks");
        }
    }
}

