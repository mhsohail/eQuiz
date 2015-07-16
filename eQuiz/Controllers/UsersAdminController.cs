using eQuiz.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace VdoValley.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UsersAdminController : Controller
    {
        public UserManager<ApplicationUser> UserManager { get; private set; }
        public RoleManager<IdentityRole> RoleManager { get; private set; }
        public eQuizContext db { get; private set; }
        public UsersAdminController()
        {
            db = new eQuizContext();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
        }

        public UsersAdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }



        //
        // GET: /Users/
        public ActionResult Index()
        {
            return View(UserManager.Users.ToList());
        }

        //
        // GET: /Users/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            var r = UserManager.GetRoles(user.Id);
            var roles = r.ToList();
            if (roles.Count() > 0)
            {
                ViewBag.RolesForThisUser = roles;
            }
            return View(user);
        }

        //
        // GET: /Users/Create
        public ActionResult Create()
        {
            //Get the list of Roles
            //ViewBag.RoleId = new SelectList(RoleManager.Roles.ToList(), "Id", "Name");
            TempData["roles"] = RoleManager.Roles.ToList();
            return View();
        }

        //
        // POST: /Users/Create
        [HttpPost]
        public async Task<ActionResult> Create(RegisterViewModel userViewModel, string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser();
                user.UserName = userViewModel.Email;
                user.Email = userViewModel.Email;
                user.FirstName = userViewModel.FirstName;
                user.LastName = userViewModel.LastName;
                user.IpAddress = "000.000.111.111";
                user.MacAddress = "000000000000";

                //user.PhoneNumber = userViewModel.PhoneNo;
                //user.LockoutEnabled = true;
                var adminresult = await UserManager.CreateAsync(user, userViewModel.Password);

                //Add User Admin to Role Admin
                if (adminresult.Succeeded)
                {
                    /*
                    if (selectedRoles.Count() > 0)
                    {
                        //Find Role and assign it to user
                        foreach (var r in selectedRoles)
                        {
                            var role = await RoleManager.FindByIdAsync(r);
                            var result = await UserManager.AddToRoleAsync(user.Id, role.Name);
                            if (!result.Succeeded)
                            {
                                ModelState.AddModelError("", result.Errors.First().ToString());
                                TempData["roles"] = RoleManager.Roles.ToList();
                                return View();
                            }
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("", "Select roles for User!");
                        TempData["roles"] = RoleManager.Roles.ToList();
                        return View();
                    }
                    */
                }
                else
                {
                    ModelState.AddModelError("", adminresult.Errors.First().ToString());
                    TempData["roles"] = RoleManager.Roles.ToList();
                    return View();

                }
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.RoleId = new SelectList(RoleManager.Roles, "Id", "Name");
                return View();
            }
        }

        //
        // GET: /Users/Edit/1
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            TempData["roles"] = RoleManager.Roles.ToList();
            return View(user);
        }

        //
        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UserName,Email,PhoneNumber,Id")] ApplicationUser formuser, string id, string[] selectedRoles)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempData["roles"] = RoleManager.Roles.ToList();
            var user = await UserManager.FindByIdAsync(id);
            user.UserName = formuser.UserName;
            user.Email = formuser.Email;
            user.PhoneNumber = formuser.PhoneNumber;

            if (ModelState.IsValid)
            {
                //Update the user details
                await UserManager.UpdateAsync(user);

                //If user has existing Role then remove the user from the role
                // This also accounts for the case when the Admin selected Empty from the drop-down and
                // this means that all roles for the user must be removed
                var rolesForUser = await UserManager.GetRolesAsync(id);
                /*
                if (rolesForUser.Count() > 0)
                {
                    foreach (var item in rolesForUser)
                    {
                        var result = await UserManager.RemoveFromRoleAsync(id, item);
                    }
                }

                if (selectedRoles.Count() > 0)
                {
                    //Find Role and assign it to user
                    foreach (var r in selectedRoles)
                    {
                        var role = await RoleManager.FindByIdAsync(r);
                        var result = await UserManager.AddToRoleAsync(user.Id, role.Name);
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", result.Errors.First().ToString());
                            TempData["roles"] = RoleManager.Roles.ToList();
                            return View();
                        }
                    }
                }
                */
                return RedirectToAction("Index");
            }
            else
            {
                TempData["roles"] = RoleManager.Roles.ToList();
                return View();
            }
        }

        //
        // GET: /Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var user = db.Users.Find(id);
                var logins = user.Logins;
                foreach (var login in logins)
                {
                    db.UserLogins.Remove(login);
                }
                ClearUserRoles(user.Id);
                db.Users.Remove(user);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        public void ClearUserRoles(string userId)
        {
            var user = UserManager.FindById(userId);
            var currentRoles = new List<IdentityUserRole>();

            currentRoles.AddRange(user.Roles);
            foreach (var role in currentRoles)
            {
                UserManager.RemoveFromRole(userId, this.RoleManager.FindById(role.RoleId).Name);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (UserManager != null)
                {
                    UserManager.Dispose();
                    UserManager = null;
                }
                if (RoleManager != null)
                {
                    RoleManager.Dispose();
                    RoleManager = null;
                }
            }
            base.Dispose(disposing);
        }

    }
}