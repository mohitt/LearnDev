using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.Web.Mvc;


namespace Ads.Web.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Logon() {
            return View();
        }

        [HttpPost]
        public ActionResult Logon(FormCollection form) {
            IDictionary<string, string> userPass =
                new Dictionary<string, string>()
                    {
                        {"mthakral", "ram123"}
                    };
            if (!userPass.ContainsKey(form["username"]))
                return View("Error");

            if (userPass[form["username"]] == form["password"]) {
                var adminSessionId = Guid.NewGuid();
                Session["adminSession"] = adminSessionId.ToString();
                Response.Cookies.Add(new HttpCookie("adminSession") {  Domain = Request.Url.Host.ToString(), Value = adminSessionId.ToString() });
                if (Session["redirectToUrl"] != null)
                {
                    
                    return RedirectToAction(Session["redirectToUrl"].ToString().Split(',')[1],
                                            Session["redirectToUrl"].ToString().Split(',')[0]);
                }
                return this.RedirectToAction<HomeController>(ctr => ctr.Index());
            }
            return View("Error");


        }
    }
}
