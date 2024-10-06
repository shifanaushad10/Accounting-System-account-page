using System;
using System.Web.Mvc;
using System.Data;
using Accounting_System.Models;

namespace Accounting_System.Controllers
{
    public class AccountController : Controller
    {
        private AccountModel accountModel = new AccountModel();

        // GET: /Account/Index
        public ActionResult Index()
        {
            DataTable dt = accountModel.GetAllAccounts();
            return View(dt); // Passes the DataTable to the Index view
        }

        // GET: /Account/Details/{id}
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                ViewBag.ErrorMessage = "Invalid account ID.";
                return View("Error"); // Handle invalid or missing ID
            }

            DataTable dt = accountModel.GetAccountById(id.Value);
            if (dt.Rows.Count == 0)
            {
                ViewBag.ErrorMessage = "The requested account could not be found.";
                return View("Error"); // Handle account not found
            }

            return View(dt); // Pass account details to the Details view
        }

        // GET: /Account/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Account/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DateTime date, string description, decimal credit, decimal debit, decimal balance)
        {
            if (ModelState.IsValid)
            {
                accountModel.AddAccount(date, description, credit, debit, balance);
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: /Account/Edit/{id}
        // GET: /Account/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            DataTable dt = accountModel.GetAccountById(id.Value);
            if (dt.Rows.Count == 0)
            {
                return HttpNotFound();
            }

            // Map the DataRow to the ViewModel
            var accountViewModel = new AccountViewModel
            {
                AccountID = Convert.ToInt32(dt.Rows[0]["AccountID"]),
                Date = Convert.ToDateTime(dt.Rows[0]["Date"]),
                Description = dt.Rows[0]["Description"].ToString(),
                Credit = Convert.ToDecimal(dt.Rows[0]["Credit"]),
                Debit = Convert.ToDecimal(dt.Rows[0]["Debit"]),
                Balance = Convert.ToDecimal(dt.Rows[0]["Balance"])
            };

            return View(accountViewModel);
        }

        // POST: /Account/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                accountModel.UpdateAccount(model.AccountID, model.Date, model.Description, model.Credit, model.Debit, model.Balance);
                return RedirectToAction("Index");
            }
            return View(model); // Pass the model back to the view if validation fails
        }


        // GET: /Account/Delete/{id}
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            DataTable dt = accountModel.GetAccountById(id.Value);
            if (dt.Rows.Count == 0)
            {
                return HttpNotFound(); // If the account is not found, return a 404
            }

            // Set ViewBag properties with the account details to display
            ViewBag.AccountID = dt.Rows[0]["AccountID"]; // Ensure this is set
            ViewBag.Date = dt.Rows[0]["Date"];
            ViewBag.Description = dt.Rows[0]["Description"];
            ViewBag.Credit = dt.Rows[0]["Credit"];
            ViewBag.Debit = dt.Rows[0]["Debit"];
            ViewBag.Balance = dt.Rows[0]["Balance"];

            return View(); // Return the Delete view
        }


        // POST: /Account/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            accountModel.DeleteAccount(id);
            return RedirectToAction("Index");
        }

    }
}
