using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ads.Helper.Mvc;
using Ads.Model.Entities;
using Ads.Model.Repositories;

namespace Ads.Web.Controllers
{
    public class AdsController : Controller
    {
        private readonly IAdRepository adRepository;

        // If you are using Dependency Injection, you can delete the following constructor
        //public AdsController() : this(new AdRepository())
        //{
        //}

        public AdsController(IAdRepository adRepository, ICategoryRepository categoryRepository) {
            this.adRepository = adRepository;
            this.CategoryRepository = categoryRepository;
        }

        protected ICategoryRepository CategoryRepository { get; set; }

        //
        // GET: /Ads/

        public ViewResult Index() {
            return View(adRepository.All);
        }

        //
        // GET: /Ads/Details/5

        public ViewResult Details(int id) {
            return View(adRepository.Find(id));
        }

        //
        // GET: /Ads/Create

        public ActionResult Create()
        {
            ViewBag.Categories = CategoryRepository.All.Select(k => new {k.DisplayValue, k.Id});
            if (Request.IsAjaxRequest())
                return PartialView("_CreateOrEdit");

            return View();
        }

        public ActionResult InitCreate() {
            return PartialView("_PostAd");
        }

        //
        // POST: /Ads/Create

        [HttpPost]
        public ActionResult Create(Ad ad)
        {
            if (ModelState.IsValid) {
                ad.UserId = 1;
                ad.LastUpdatedBy = 1;
                ad.CreatedBy = 1;
                ad.AdTypeId = 1;
                adRepository.InsertOrUpdate(ad);
                adRepository.Save();
                return RedirectToAction("Index");
            }
            return View();
        }

        //
        // GET: /Ads/Edit/5

        public ActionResult Edit(int id) {
            return View(adRepository.Find(id));
        }

        //
        // POST: /Ads/Edit/5

        [HttpPost]
        public ActionResult Edit(Ad ad) {
            if (ModelState.IsValid) {
                adRepository.InsertOrUpdate(ad);
                adRepository.Save();
                return RedirectToAction("Index");
            }
            else {
                return View();
            }
        }

        //
        // GET: /Ads/Delete/5

        public ActionResult Delete(int id) {
            return View(adRepository.Find(id));
        }

        //
        // POST: /Ads/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id) {
            adRepository.Delete(id);
            adRepository.Save();

            return RedirectToAction("Index");
        }

        public ActionResult ListPartial(int? catId = null) {
            if (catId == null)
                return PartialView("_ListPerCategory", CategoryRepository.AllIncluding(cat => cat.Ads));
            else
                return PartialView("_ListPerCategory", CategoryRepository.AllIncluding(cat => cat.Ads).Where(cat => cat.Id == catId.Value));

        }
        public ActionResult List(int? catId = null)
        {
            return View(catId == null ? CategoryRepository.AllIncluding(cat => cat.Ads) : CategoryRepository.AllIncluding(cat => cat.Ads).Where(cat => cat.Id == catId.Value));
        }
    }


}

