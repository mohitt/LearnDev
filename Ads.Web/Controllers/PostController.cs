using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Ads.Model.Entities;
using Ads.Services;

namespace Ads.Web.Controllers
{
    public class PostController : Controller
    {
        public PostController(IAdProcessorService adProcessorService)
        {
            this.AdProcessor = adProcessorService;
        }

        public IAdProcessorService AdProcessor { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Index(FormCollection form)
        {
            Ad adTobePopulated=new Ad();
            AdProcessor.Process(adTobePopulated, form["searchPost"]);
            return View();
        }       

       
    }
}
