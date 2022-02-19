using LoginPage.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginPage.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Welcome()
        {
            return View();
        }

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Remove("key");
            return RedirectToAction("Login");
        }

        // GET: Login/Create
        public ActionResult Login()
        {
            //Session.Abandon();
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=BasitDB; Integrated Security = True";
            cn.Open();

            SqlCommand cmdSelect = new SqlCommand();
            cmdSelect.Connection = cn;
            cmdSelect.CommandType = System.Data.CommandType.Text;
            cmdSelect.CommandText = "Select LoginName, Password from Users2";
            SqlDataReader dr = cmdSelect.ExecuteReader();
            while (dr.Read())
            {   
                if(dr.GetString(0) == user.LoginName && dr.GetString(1) == user.Password)
                {
                    Session["key"] = user.LoginName ;
                   // Session["key1"] = user.UserId;
                    dr.Close();
                    return View("Welcome");
                } 
            }
            dr.Close();
            return View("Error");

        }

        public ActionResult RegisterCreate()
        {
            List<SelectListItem> objDepts = new List<SelectListItem>
            {
                new SelectListItem{Text= ""},
                new SelectListItem{Text= "Mumbai"},
                new SelectListItem{Text= "Pune"},
                new SelectListItem{Text= "Kolkata"},
            };
            
            ViewBag.City= objDepts;

            return View();
        }


        // POST: Login/Create
        [HttpPost]
        public ActionResult RegisterCreate(User user)
        {
            
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BasitDB;Integrated Security=True";
            cn.Open();
            
            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = cn;
            cmdInsert.CommandType = System.Data.CommandType.Text;

            cmdInsert.CommandText = "insert into Users2(LoginName, Password, Fullname, EmailId, City, Phone) values(@LoginName, @Password, @Fullname, @EmailId, @City, @Phone)";

            
            cmdInsert.Parameters.AddWithValue("@LoginName", user.LoginName);
            cmdInsert.Parameters.AddWithValue("@Password", user.Password);
            cmdInsert.Parameters.AddWithValue("@Fullname", user.Fullname);
            cmdInsert.Parameters.AddWithValue("@EmailId", user.EmailId);
            cmdInsert.Parameters.AddWithValue("@City", user.City);
            cmdInsert.Parameters.AddWithValue("@Phone", user.Phone);
            string name = user.LoginName;
            try
            {
                // TODO: Add insert logic here
                cmdInsert.ExecuteNonQuery();
                cn.Close();
                return RedirectToAction("Login");
            }
            catch
            {
                cn.Close();
                return View("Error");
            }
        }



        // GET: Login/Edit/5
        public ActionResult Edit()
        {
            var id = Session["key"];
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BasitDB;Integrated Security=True";
            cn.Open();

            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = cn;
            cmdInsert.CommandType = System.Data.CommandType.Text;
            cmdInsert.CommandText = "select * from Users2 where LoginName = @Id";
            cmdInsert.Parameters.AddWithValue("@Id", id);
            SqlDataReader dr = cmdInsert.ExecuteReader();
            User obj = null;
            if (dr.Read())
                obj = new User { LoginName = dr.GetString(1), Password = dr.GetString(2), Fullname = dr.GetString(3), EmailId = dr.GetString(4), City = dr.GetString(5), Phone = dr.GetString(6) };
            else
            {
                //not found
                ViewBag.ErrorMessage = "Not found";
            }
            dr.Close();
            cn.Close();
            return View(obj);
        }

        // POST: Login/Edit/5
        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BasitDB;Integrated Security=True";
            cn.Open();
            var id = Session["key"];
            try
            {
                string LoginName = collection["LoginName"];
                string Fullname = collection["Fullname"];
                string EmailId = collection["EmailId"];
                string City = collection["City"];
                string Phone = collection["Phone"];

                

                SqlCommand cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;
                cmdInsert.CommandType = System.Data.CommandType.Text;
                cmdInsert.CommandText = "update Users2 set LoginName = @LoginName, Fullname = @Fullname, EmailId = @EmailId, City = @City, Phone =@Phone where LoginName = @Id";
                cmdInsert.Parameters.AddWithValue("@Id", id);
                cmdInsert.Parameters.AddWithValue("@LoginName", LoginName);
                cmdInsert.Parameters.AddWithValue("@Fullname", Fullname);
                cmdInsert.Parameters.AddWithValue("@EmailId", EmailId);
                cmdInsert.Parameters.AddWithValue("@City", City);
                cmdInsert.Parameters.AddWithValue("@Phone", Phone);

                cmdInsert.ExecuteNonQuery();
                cn.Close();

                return RedirectToAction("Welcome");
            }
            catch
            {
                cn.Close();
                return View();
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
