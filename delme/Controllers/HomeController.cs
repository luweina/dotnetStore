using delme.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace delme.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string message)
        {
            if (message == null)
            {
                message = "";
            }
            ViewData["Message"] = message;

            FoodStoreContext db = new FoodStoreContext();

            // 1st query.
            var gfsItems = from p in db.Products
                           where p.Vendor == "GFS"
                           select p;

            // Second query using first query.
            var inexpesiveGFSItems = from p in gfsItems
                                     where p.Price < 2.50M
                                     select p;

            return View(inexpesiveGFSItems);
        }



        public IActionResult Store()
        {
            FoodStoreContext context = new FoodStoreContext();
            //Select rows ordered by Region and then Branch decending
            var query = from s in context.Stores
                        orderby s.Region, s.Branch descending
                        select s;
            return View(query);

        }

        public IActionResult Details(int? id)
        {
            FoodStoreContext db = new FoodStoreContext();
            var products = (from p in db.Products
                            where p.ProductId == id
                            select p).FirstOrDefault();
            return View(products);
        }

        // GET: Home/Create
        public IActionResult Create()
        {
            FoodStoreContext db = new FoodStoreContext();

            ViewData["Mfg"] = new SelectList(db.Manufacturers, "Mfg", "Mfg");
            ViewData["Vendor"] = new SelectList(db.Suppliers, "Vendor", "Vendor");
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("ProductId,Name,Mfg,Vendor,Price")] Product product)
        {

            FoodStoreContext db = new FoodStoreContext();

            // Ensure data is valid.
            if (ModelState.IsValid)
            {
                db.Add(product);
                db.SaveChanges(); // Commit changes to database.

                // Save is successful so show updated listing.
                return RedirectToAction(nameof(Index));
            }

            // Data not valid so show form again with populated drop downs.
            ViewData["Mfg"] = new SelectList(db.Manufacturers, "Mfg", "Mfg", product.Mfg);
            ViewData["Vendor"] = new SelectList(db.Suppliers, "Vendor", "Vendor",
                                                product.Vendor);
            return View(product);
        }
        public IActionResult Edit(int? id)
        {
            FoodStoreContext db = new FoodStoreContext();
            var product = (from p in db.Products
                           where p.ProductId == id
                           select p).FirstOrDefault();
            // This code is used to populate the Mfg and Vendor dropdowns.
            // the last parameter is the selected item for the product.
            ViewData["Mfg"] = new SelectList(db.Manufacturers, "Mfg", "Mfg", product.Mfg);
            ViewData["Vendor"] = new SelectList(db.Suppliers, "Vendor",
                                                "Vendor", product.Vendor);
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(
   [Bind("ProductId,Name,Mfg,Vendor,Price")] Product product)
        {
            ViewData["Message"] = "";
            FoodStoreContext db = new FoodStoreContext();
            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(product);
                    db.SaveChanges();
                    ViewData["Message"] = "The update has been saved.";
                }
                catch (Exception ex)
                {
                    ViewData["Message"] = ex.Message;
                }
            }
            ViewData["Mfg"] = new SelectList(db.Manufacturers, "Mfg", "Mfg", product.Mfg);
            ViewData["Vendor"] = new SelectList(db.Suppliers, "Vendor",
                                                "Vendor", product.Vendor);
            return View(product);

        }

        public IActionResult Delete(int? id)
        {
            string deleteMessage = "Product Id: " + id
                                 + " deleted successfully";
            try
            {
                FoodStoreContext db = new FoodStoreContext();

                var product = (from p in db.Products
                               where p.ProductId == id
                               select p).FirstOrDefault();
                db.Remove(product);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                deleteMessage = e.Message + " "
                + "The product may not exist or "
                + "there could be a foreign key restriction.";
            }
            // Redirects to Index action method of Home controller with message parameter. 
            return RedirectToAction("Index", "Home", new { message = "** " + deleteMessage });
        }






        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
