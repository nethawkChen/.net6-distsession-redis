using DistSession.Lib;
using DistSession.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DistSession.Controllers {
    [Authorize]
    public class Page2Controller : Controller {
        private Utils utils;
        public Page2Controller(Utils utils) {
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
    }
}
