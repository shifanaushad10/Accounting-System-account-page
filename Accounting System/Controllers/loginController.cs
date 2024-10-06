using Accounting_System.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccountingSystem.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login

        public ActionResult Index()
        {
            return View("~/Views/Admin/adminLogin.cshtml");
        }

        public ActionResult Login(FormCollection frm, string action)
        {
            if (action == "Submit")
            {
                loginModel model = new loginModel();
                string name = frm["txtUser"];
                string passw = frm["txtPassw"];

                DataTable dt = model.UserLogin(name, passw);
                if (dt.Rows.Count > 0)
                {
                    Session["Username"] = name;
                    return RedirectToAction("Welcome");
                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid username or password.";
                    return View("~/Views/Admin/adminLogin.cshtml"); // Return the view directly to retain the error message
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        public ActionResult Welcome()
        {
            if (Session["Username"] != null)
            {
                ViewBag.Username = Session["Username"];
                return View("~/Views/Admin/Welcome.cshtml");
            }
            else
            {
                return RedirectToAction("Index"); // Redirect to login if not authenticated
            }
        }


    }
}