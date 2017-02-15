using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using fortune_teller_mvc.Models;

namespace fortune_teller_mvc.Controllers
{
    public class CustomersController : Controller
    {
        private FortuneTellerMVCEntities db = new FortuneTellerMVCEntities();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            //retirement fortune
            ViewBag.RetireYears = 0;

            if (customer.Age % 2 == 0)  //figure out if the age is even or odd
            {
                ViewBag.RetireYears = 22; //even number
            }
            else
            {
                ViewBag.RetireYears = 35;
            }
            //vacation fortune 
            ViewBag.UserVaca = "";

            if (customer.NumberOfSiblings == 0)
            {
                ViewBag.UserVaca = ("a beach house in Cancun");
            }

            else if (customer.NumberOfSiblings == 1)
            {
                ViewBag.UserVaca = ("a vacation home in Colorado");
            }
            else if (customer.NumberOfSiblings == 2)
            {
                ViewBag.UserVaca = ("a vacation home in Spain");
            }
            else if (customer.NumberOfSiblings == 3)
            {
                ViewBag.UserVaca = ("a beach house in Croatia");
            }
            else if (customer.NumberOfSiblings > 3)
            {
                ViewBag.UserVaca = ("a beach house in Australia");
            }
            else
            {
                ViewBag.UserVaca = ("a trailer in Crappy Ohio");
            }
            //fortune for mode of transportation
           ViewBag.userTransport = "";

            switch (customer.FavoriteColor)
            {
                case "RED":
                    ViewBag.userTransport = ("a boat");
                    break;
                case "ORANGE":
                    ViewBag.userTransport = ("a Ford SUV");
                    break;
                case "YELLOW":
                    ViewBag.userTransport = ("a motorcycle");
                    break;
                case "GREEN":
                    ViewBag.userTransport = ("a Rolls Royce");
                    break;
                case "BLUE":
                    ViewBag.userTransport = ("a bike");
                    break;
                case "INDIGO":
                    ViewBag.userTransport = ("a yacht");
                    break;
                case "VIOLET":
                    ViewBag.userTransport = ("an airplane");
                    break;
                default:
                    ViewBag.userTransport = ("a squeaky shopping cart");
                    break;
            }
            //method for money
            ViewBag.money=0;

            if (customer.BirthMonth >= 1 && customer.BirthMonth <= 4)
            {
                ViewBag.money = 3.45d;
            }
            else if (customer.BirthMonth >= 5 && customer.BirthMonth <= 8)
            {
                ViewBag.money = 4.6d;
            }
            else if (customer.BirthMonth >= 9 && customer.BirthMonth <= 12)
            {
                ViewBag.money = 500.56d;
            }
            else
            {
                ViewBag.money = 0.0d;
            }


            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,FirstName,LastName,Age,BirthMonth,FavoriteColor,NumberOfSiblings")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,FirstName,LastName,Age,BirthMonth,FavoriteColor,NumberOfSiblings")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
