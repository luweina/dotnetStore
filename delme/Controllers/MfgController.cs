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
    public class MfgController : Controller
    {
        public IActionResult Index(string message)
        {
            if (message == null)
            {
                message = "";
            }
            ViewData["Message"] = message;

            FoodStoreContext db = new FoodStoreContext();
            return View(db.Manufacturers.ToList());
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]

        public IActionResult Create([Bind("Mfg,MfgDiscount")] Manufacturer manufacturer)
        {

            FoodStoreContext db = new FoodStoreContext();

            // Ensure data is valid.
            if (ModelState.IsValid)
            {
                db.Add(manufacturer);
                db.SaveChanges(); // Commit changes to database.

                // Save is successful so show updated listing.
                return RedirectToAction(nameof(Index));
            }

            // Data not valid so show form again with populated drop downs.
          
            return View(manufacturer);
        }
        [HttpGet("/mfg/edit/{name}")]
        public IActionResult Edit(String? name)
        {
            FoodStoreContext db = new FoodStoreContext();
            var manufacturer = (from m in db.Manufacturers
                           where m.Mfg == name
                                select m).FirstOrDefault();
           
            return View(manufacturer);
        }


        [HttpPost]
        public IActionResult Edit(
        [Bind("Mfg,MfgDiscount")] Manufacturer manufacturer)
        {
            ViewData["Message"] = "";
            FoodStoreContext db = new FoodStoreContext();
            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(manufacturer);
                    db.SaveChanges();
                    ViewData["Message"] = "The update has been saved.";
                }
                catch (Exception ex)
                {
                    ViewData["Message"] = ex.Message;
                }
            }
         
            return View(manufacturer);

        }
      [HttpDelete("/mfg/Delete/{name}")]
        public IActionResult Delete(string? name)
        {
            string deleteMessage = "Product Id: " + name
                                 + " deleted successfully";
            try
            {
                FoodStoreContext db = new FoodStoreContext();

                var manu = (from m in db.Manufacturers
                               where m.Mfg == name
                               select m).FirstOrDefault();
                db.Remove(manu);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                deleteMessage = e.Message + " "
                + "The product may not exist or "
                + "there could be a foreign key restriction.";
            }
            // Redirects to Index action method of Home controller with message parameter. 
            return RedirectToAction("Index", "Mfg", new { message = "** " + deleteMessage });
        }
    }

   



}
