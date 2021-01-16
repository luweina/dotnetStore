using delme.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace delme.Controllers
{
    public class StoreController : Controller
    {
        public IActionResult AllStore()
        {
            FoodStoreContext db = new FoodStoreContext();

            // 1st query.
            var province = from p in db.Stores
                           where p.Region == "BC"
                           select p;

            // Second query using first query.
            
            var selectedBranch = from p in province
                                     where p.Branch.StartsWith("K")||
                                     p.Branch.StartsWith("L")||
                                     p.Branch.StartsWith("M")
                                 select p;

            return View(selectedBranch);
        }
        [HttpGet("/store/Details/{branchName}")]
        public IActionResult Details(String? branchName)
        {
            FoodStoreContext db = new FoodStoreContext();
            var store = (from s in db.Stores
                         where s.Branch == branchName
                         select s).FirstOrDefault();
            return View(store);
        }
    }
}
