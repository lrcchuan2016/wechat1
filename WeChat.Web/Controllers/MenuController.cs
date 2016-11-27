using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeChat.Api.Client;

namespace WeChat.Web.Controllers
{
    [RoutePrefix("Admin/Menu")]
    public class MenuController : Controller
    {
        protected ApiClient ApiClient = ApiClient.Create();

        [Route("Query")]
        public ActionResult Query()
        {
            string json = ApiClient.QueryMenu();
            ViewBag.Json = json;
            return View();
        }

        [Route("Create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Create")]
        public ActionResult Create(FormCollection collection)
        {
            return View();
        }

        [Route("Delete")]
        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        [Route("Delete")]
        public ActionResult Delete(FormCollection collection)
        {
            return View();
        }


    }
}