using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace AspDotNETWebApplication1.Pages.Books
{
    public class CreateBook : PageModel
    {
        public string Message="";
        public bool Error=false;
        public void OnGet()
        {
        }
        public void OnPost() 
        {
            try
            { 
                Books book = new Books();
                book.Id = Request.Form["Id"];
                book.Title = Request.Form["Title"];
                book.Author = Request.Form["Author"];
                book.Publication = Request.Form["Publication"];
                book.Price = Convert.ToDouble(Request.Form["Price"].ToString());
                using (SqlConnection sqlConnection = new SqlConnection(SQLServerConnect.conn))
                {
                    sqlConnection.Open();
                    using (SqlCommand cmd = sqlConnection.CreateCommand())
                    {
                        cmd.CommandText = $"INSERT INTO LMS_BOOK_DETAILS (BOOK_CODE,BOOK_TITLE,AUTHOR, PUBLICATION, PRICE) VALUES('{book.Id}','{book.Title}','{book.Author}','{book.Publication}',{book.Price})";
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
            catch(Exception ex)
            {
                Error = true;
                Message = ex.Message;
            }
        }
    }
}
