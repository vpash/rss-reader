using RssWebApp.EF;
using RssWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RssWebApp.Controllers
{
    public class HomeController : Controller
    {
        private const int PageSize = 10;

        private DbOperations dbOperations;

        public HomeController()
        {
            dbOperations = new DbOperations();
        }

        [HttpGet]
        public async Task<ActionResult> Index(int page = 1)
        {
            return View(await dbOperations.GetIndexModel(PageSize, page, null, NewsItemOrder.DateDesc));
        }

        [HttpPost]
        public async Task<ActionResult> Index(int page = 1, int? providerId = null, NewsItemOrder order = NewsItemOrder.None)
        {
            return View(await dbOperations.GetIndexModel(PageSize, page, providerId, order));
        }

        [HttpGet]
        public async Task<ActionResult> IndexAjax(int page = 1)
        {
            return View(await dbOperations.GetIndexModel(PageSize, page, null, NewsItemOrder.DateDesc));
        }

        [HttpPost]
        public async Task<PartialViewResult> Table(int page = 1, int? providerId = null, NewsItemOrder order = NewsItemOrder.None)
        {
            return PartialView("_Table", await dbOperations.GetIndexModel(PageSize, page, providerId, order));
        }
    }
}