using System;
using System.Web.Mvc;
using Ads.Helper.Constants;
using Ads.Model.Entities;
using Ads.Model.Repositories;
using System.Linq;
using Ads.Services;
using Microsoft.Web.Mvc;

namespace Ads.Web.Controllers
{
    public class UserController : Controller
    {
        public UserController(IUserRepository userRepository,IFormsAuthenticationService formsAuthenticationService) {
            this.UserRepo = userRepository;
            this.FormsAuthServ = formsAuthenticationService;
        }

        protected IFormsAuthenticationService FormsAuthServ { get; set; }

        protected IUserRepository UserRepo { get; set; }

        public ActionResult Register() {
            return View();
        }

        [HttpPost]
        [ValidateInput(true)]
        public ActionResult Register(User user) {
            // Change this to a constant
            var adminUser = UserRepo.All.Where(u => u.DisplayName == "Admin").FirstOrDefault();
            var adminId = adminUser == default(User) ? 1 : adminUser.Id;

            user.OpenId = Session[SessionConstants.OpenId].ToString();
            user.CreatedBy = adminId;
            user.LastUpdatedBy = adminId;
            user.LastUpdatedOn = DateTime.Now;
            user.CreatedOn = DateTime.Now;

            UserRepo.InsertOrUpdate(user);
            UserRepo.Save();
            //TODO:: Move following 2 lines to a seperate method, Since it is used
            //Y:\~Projects\Ads\Ads.Web\Controllers\AccountController.cs:99 as well
            Session[SessionConstants.DisplayName] = user.DisplayName;
            FormsAuthServ.SetAuthCookie(user.OpenId,false);
            return this.RedirectToAction(k => k.Details(user.Id, true));

        }

        public ActionResult Details(int userId, bool newAccount)
        {
            ViewBag.NewAccount = newAccount;
            ViewData.Model = UserRepo.All.Where(u => u.Id == userId).FirstOrDefault();
            return View();
            
        }
    }
}