using CRUD_application_2.Models;
using System;
using System.Linq;
using System.Web.Mvc;
 
namespace CRUD_application_2.Controllers
{
    public class UserController : Controller
    {
        public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();
        // GET: User
        public ActionResult Index()
        {
            // Return the Index view with the list of users
            return View(userlist);
        }
 
        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            // Find the user by ID
            var user = userlist.FirstOrDefault(u => u.Id == id);

            // If no user is found, return HttpNotFound (Not Found response)
            if (user == null)
            {
                return HttpNotFound();
            }

            // If a user is found, return the Details view with the user object
            return View("Details", user);
        }
 
        // GET: User/Create
        public ActionResult Create()
        {
            // If a user is found, pass the user to the Edit view
            return View("Create");

        }
 
      // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            try
            {
                // Check if a user with the same name or email already exists
                if (userlist.Any(u => u.Name.Equals(user.Name, StringComparison.OrdinalIgnoreCase)
                                      || u.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase)))
                {
                    ModelState.AddModelError("", "A user with the same name or email already exists.");
                    return View("Create", user);
                }

                // Check if the userlist is empty. If so, start IDs at 1; otherwise, increment the highest ID by 1.
                user.Id = userlist.Any() ? userlist.Max(u => u.Id) + 1 : 1;

                // Add the user to the user list
                userlist.Add(user);

                // Redirect to the Index action to display the updated list of users
                return RedirectToAction("Index");
            }
            catch (System.Exception ex)
            {
                // Log error message to the console
                System.Diagnostics.Debug.WriteLine($"Failed to create user. Error: {ex.Message}");

                // Return to the Create view with the user object to display any errors
                return View("Create", user);
            }
        }
 
        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            // Find the user by ID
            var user = userlist.FirstOrDefault(u => u.Id == id);

            // If no user is found, return HttpNotFound (Not Found response)
            if (user == null)
            {
                return HttpNotFound();
            }

            // If a user is found, pass the user to the Edit view
            return View("Edit", user);
        }
 
      // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            var userToUpdate = userlist.FirstOrDefault(u => u.Id == id);

            if (userToUpdate == null)
            {
                return HttpNotFound();
            }

            try
            {
                // Check if another user with the same name or email already exists
                if (userlist.Any(u => (u.Name.Equals(user.Name, StringComparison.OrdinalIgnoreCase)
                                       || u.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase))
                                       && u.Id != id))
                {
                    ModelState.AddModelError("", "Another user with the same name or email already exists.");
                    return View("Edit", user);
                }

                // Update the user's information
                userToUpdate.Name = user.Name;
                userToUpdate.Email = user.Email;
                // Add other fields to update as necessary

                // Redirect to the Index action to display the updated list of users
                return RedirectToAction("Index");
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message, "Failed to edit user with ID {Id}.", id);

                // Return to the Edit view with the user object to display any validation errors
                return View("Edit", user);
            }
        }
 
        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            // Find the user by ID
            var user = userlist.FirstOrDefault(u => u.Id == id);

            // If no user is found, return HttpNotFound (Not Found response)
            if (user == null)
            {
                return HttpNotFound();
            }

            // If a user is found, pass the user to the Edit view
            return View("Delete", user);
        }
 
        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            // Find the user by ID
            var userToDelete = userlist.FirstOrDefault(u => u.Id == id);

            if (userToDelete != null)
            {
                // Remove the user from the list
                userlist.Remove(userToDelete);

                // Redirect to the Index action to display the updated list of users
                return RedirectToAction("Index");
            }
            else
            {
                // If no user is found, return HttpNotFound (Not Found response)
                return HttpNotFound();
            }
        }
    }
}