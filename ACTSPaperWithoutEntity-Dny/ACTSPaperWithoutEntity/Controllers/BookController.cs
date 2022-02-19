using ACTSPaperWithoutEntity.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACTSPaperWithoutEntity.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            return View();
        }

        // GET: Book/Details/5
        public ActionResult Details(int id)
        {

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JkJan22;Integrated Security=True";
            cn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select * from tbl_books where BookID=" + id;
            BookModel book;
            try
            {
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                book = new BookModel { BookName = dr["BookName"].ToString(), BookAuthor = dr["BookAuthor"].ToString(), BookPrice = (decimal)dr["BookPrice"] };
                return View(book);

            }
            catch (Exception ex)
            {
               
                Console.WriteLine(ex.Message);
                return View();
            }
            cn.Close();
           

        }

        public ActionResult ShowBooks()
        {

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JkJan22;Integrated Security=True";
            cn.Open();

            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = cn;
            cmdInsert.CommandType = System.Data.CommandType.Text;
            cmdInsert.CommandText = "select * from tbl_books ";
            List<BookModel> books = new List<BookModel>();
            try
            {
                SqlDataReader dr = cmdInsert.ExecuteReader();
                while (dr.Read())
                {
                    books.Add(new BookModel {BookID =(int) dr["BookID"], BookName = dr["BookName"].ToString(), BookAuthor = dr["BookAuthor"].ToString(), BookPrice = (decimal)dr["BookPrice"] });
                }
                dr.Close();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            cn.Close();
            return View(books);

        }


        // GET: Book/Create
        public ActionResult AddBook()
        {
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        public ActionResult AddBook(BookModel book)
        {
            try
            {
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JkJan22;Integrated Security=True";
                cn.Open();
                SqlCommand cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;
                cmdInsert.CommandType = System.Data.CommandType.Text;
                cmdInsert.CommandText = "insert into tbl_books(BookName,BookAuthor,BookPrice) values(@BookName,@BookAuthor,@BookPrice)";

                cmdInsert.Parameters.AddWithValue("@BookName", book.BookName);
                cmdInsert.Parameters.AddWithValue("@BookAuthor", book.BookAuthor);
                cmdInsert.Parameters.AddWithValue("@BookPrice", book.BookPrice);
                cmdInsert.ExecuteNonQuery();
                cn.Close();


                return RedirectToAction("ShowBooks");
            }
            catch
            {
                return View();
            }
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JkJan22;Integrated Security=True";
            cn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select * from tbl_books where BookID=" + id;
            BookModel book;
            try
            {
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                book = new BookModel { BookID =(int) dr["BookID"], BookName = dr["BookName"].ToString(), BookAuthor = dr["BookAuthor"].ToString(), BookPrice = (decimal)dr["BookPrice"] };
                return View(book);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return View();
            }
            cn.Close();
        }

        // POST: Book/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, BookModel book)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JkJan22;Integrated Security=True";
            cn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "update tbl_books set BookName = @BookName, BookAuthor = @BookAuthor,BookPrice=@BookPrice where  BookID=@BookID";
            cmd.Parameters.AddWithValue("@BookID", id);
            cmd.Parameters.AddWithValue("@BookName", book.BookName);
            cmd.Parameters.AddWithValue("@BookAuthor", book.BookAuthor);
            cmd.Parameters.AddWithValue("@BookPrice", book.BookPrice);
            SqlDataReader dr = cmd.ExecuteReader();

            try
            {
                dr.Read();

                return RedirectToAction("ShowBooks");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View();
            }
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JkJan22;Integrated Security=True";
            cn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select * from tbl_books where BookID=" + id;
            BookModel book;
            try
            {
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                book = new BookModel { BookID = (int)dr["BookID"], BookName = dr["BookName"].ToString(), BookAuthor = dr["BookAuthor"].ToString(), BookPrice = (decimal)dr["BookPrice"] };
                return View(book);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return View();
            }
            cn.Close();
            
        }

        // POST: Book/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {

                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JkJan22;Integrated Security=True";
                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "delete from tbl_books  where BookID=" + id;

                SqlDataReader dr = cmd.ExecuteReader();
                
                return RedirectToAction("ShowBooks");
            }
            catch
            {
                return View();
            }
        }
    }
}
