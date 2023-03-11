using DistSession.Lib;
using DistSession.Models;
using DistSession.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DistSession.Controllers {
    [Authorize]
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private Utils utils;

        public HomeController(ILogger<HomeController> logger,Utils utils) {
            _logger = logger;
            this.utils = utils;
        }

        public IActionResult Index() {
            UserData user = null;
            string svrip = utils.GetServerIP();
            string svrhost = utils.GetHostName();

            ViewData["SvrIp"] = svrip;
            ViewData["SvrHost"] = svrhost;

            if (HttpContext.Session.Get<UserData>("User") != null) {
                user = HttpContext.Session.Get<UserData>("User");
            }

            return View(user);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}