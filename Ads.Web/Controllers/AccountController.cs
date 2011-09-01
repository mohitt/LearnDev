using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Ads.Helper.Constants;
using Ads.Model.Repositories;
using Ads.Model.ViewModels;
using Ads.Services;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.RelyingParty;
using DotNetOpenAuth.Messaging;
using Microsoft.Web.Mvc;

namespace Ads.Web.Controllers
{
    public class AccountController : Controller
    {
        public AccountController(IFormsAuthenticationService formsService
                                , IMembershipService membershipService
                                , IUserRepository userRepo
                                , IOpenIdService openIdService
                                ) {
            this.FormsService = formsService;
            this.MembershipService = membershipService;
            this.UserRepository = userRepo;
            this.OpenIdService = openIdService;
        }

        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }
        public IUserRepository UserRepository { get; set; }
        public IOpenIdService OpenIdService { get; set; }



        /// <summary>
        /// This Method is used to show the Open ID OverLay
        /// </summary>
        /// <returns></returns>
        public ActionResult OpenIdPopup() {

            if (Request.IsAjaxRequest())
                return PartialView("_OpenIdHtml");

            return View();

        }

        /// <summary>
        /// Authentication/Login post action.
        /// Original code concept from
        /// DotNetOpenAuth/Samples/OpenIdRelyingPartyMvc/Controller/UserController
        /// for demo purposes I took out the returnURL, for this demo I
        /// always want to login to the home page.
        /// </summary>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult LogOn(LogOnModel model) {

            Identifier id;
            Session[SessionConstants.OpenId] = null;
            //make sure your users openid_identifier is valid.
            if (!Identifier.TryParse(model.OpenIdIdentifier, out id)) {
                ViewBag.StatusMessage = "Invalid identifier";
                return View();
            }
            try {
                //request openid_identifier
                return OpenIdService
                    .CreateRequest(model.OpenIdIdentifier)
                    .RedirectingResponse
                    .AsActionResult();
            }
            catch (ProtocolException ex) {
                ViewBag.StatusMessage = ex.Message;
                return View();
            }
        }

        public ActionResult LogOn() {
            //display initial login page with form to call Authenticate Action
            if (Session[SessionConstants.OpenId] != null) {
                //ViewBag.UserLoggedIn = true;
                return this.RedirectToAction<HomeController>(controller => controller.Index()); //("Index", "Home");
            }

            var response = OpenIdService.GetResponse();
            if (response != null) {
                var statusMessage = "";
                //check the response status
                switch (response.Status) {
                    //success status
                    case AuthenticationStatus.Authenticated:
                        Session[SessionConstants.OpenId] = response.ClaimedIdentifier;
                        var user = UserRepository.FindByOpenId(response.ClaimedIdentifier);
                        if (user != null) {
                            Session[SessionConstants.DisplayName] = user.DisplayName;
                            FormsService.SetAuthCookie(response.ClaimedIdentifier, false);
                            return this.RedirectToAction<HomeController>(controller => controller.Index());
                        }
                        return this.RedirectToAction<UserController>(ctrl => ctrl.Register());
                    case AuthenticationStatus.Canceled:
                        statusMessage = "Canceled at provider";
                        ViewBag.StatusMessage = statusMessage;
                        return View();

                    case AuthenticationStatus.Failed:
                        statusMessage = response.Exception.Message;
                        ViewBag.StatusMessage = statusMessage;
                        return View();
                }
            }
            return View();
        }


        public ActionResult LogOff() {
            FormsService.SignOut();
            Session[SessionConstants.DisplayName] = null;
            Session[SessionConstants.OpenId] = null;
            return this.RedirectToAction<HomeController>(controller => controller.Index());
        }

        //// **************************************
        //// URL: /Account/Register
        //// **************************************

        //public ActionResult Register() {
        //    ViewBag.PasswordLength = MembershipService.MinPasswordLength;
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Register(RegisterModel model) {
        //    if (ModelState.IsValid) {
        //        // Attempt to register the user
        //        MembershipCreateStatus createStatus = MembershipService.CreateUser(model.UserName, model.Password, model.Email);

        //        if (createStatus == MembershipCreateStatus.Success) {
        //            FormsService.SignIn(model.UserName, false /* createPersistentCookie */);
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else {
        //            ModelState.AddModelError("", AccountValidation.ErrorCodeToString(createStatus));
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    ViewBag.PasswordLength = MembershipService.MinPasswordLength;
        //    return View(model);
        //}

        //// **************************************
        //// URL: /Account/ChangePassword
        //// **************************************

        //[Authorize]
        //public ActionResult ChangePassword() {
        //    ViewBag.PasswordLength = MembershipService.MinPasswordLength;
        //    return View();
        //}

        //[Authorize]
        //[HttpPost]
        //public ActionResult ChangePassword(ChangePasswordModel model) {
        //    if (ModelState.IsValid) {
        //        if (MembershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword)) {
        //            return RedirectToAction("ChangePasswordSuccess");
        //        }
        //        else {
        //            ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    ViewBag.PasswordLength = MembershipService.MinPasswordLength;
        //    return View(model);
        //}

        //// **************************************
        //// URL: /Account/ChangePasswordSuccess
        //// **************************************

        //public ActionResult ChangePasswordSuccess() {
        //    return View();
        //}

    }

    
}
