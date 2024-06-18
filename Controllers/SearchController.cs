using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_application_2.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            return View("~/Views/Search.cshtml");
        }

        // POST: Search
        [HttpPost]
        public ActionResult Search(string word)
        {
            var searchResult = UserController.userlist
                     .Where(user => user.Name.Contains(word) || user.Email.Contains(word))
                     .ToList().FirstOrDefault();

            // Check if any user was found
            if (searchResult != null)
            {
                // If users are found, pass the searchResult to the Details view
                return View("~/Views/User/Details.cshtml", searchResult);
            }
            else
            {
                // If no users are found, optionally redirect to a different view or display a message
                ViewBag.Message = "No users found.";
                return View("~/Views/Search.cshtml");
            }
        }
    }
}