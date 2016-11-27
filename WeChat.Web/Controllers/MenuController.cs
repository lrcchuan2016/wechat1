using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeChat.Api.Client;

namespace WeChat.Web.Controllers
{
    //[Authorize]
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
            ViewBag.Result = string.Empty;
            return View();
        }

        [HttpPost]
        [Route("Create")]
        public ActionResult Create(FormCollection collection)
        {
            string json = collection["menu"].Replace("\r", string.Empty).Replace("\n", string.Empty);
            ViewBag.Result = ApiClient.CreateMenu(json);           
            return View();
        }

        [Route("Delete")]
        public ActionResult Delete()
        {
            string json = ApiClient.QueryMenu();
            ViewBag.Json = json;
            ViewBag.Result = string.Empty;
            return View();
        }

        [HttpPost]
        [Route("Delete")]
        public ActionResult Delete(FormCollection collection)
        {
            ViewBag.Result = ApiClient.DeleteMenu();
            return View();
        }


    }
}