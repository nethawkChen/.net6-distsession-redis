using DistSession.Lib;
using DistSession.ViewModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DistSession.Controllers {
    public class LoginController : Controller {
        private Utils utils;

        public LoginController(Utils utils) {
            this.utils = utils;
        }

        #region Login & Logout
        public IActionResult Login() {
            string svrhost = utils.GetHostName();
            ViewData["SvrHost"] = svrhost;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserData userData) {
            userData.LoginTime = DateTime.Now;
            HttpContext.Session.Set<UserData>("User", userData);

            #region 不使用 ASP.NET Core Identity 的 Cookie 驗證
            var claims = new List<Claim> {
                        new Claim(ClaimTypes.Name,userData.AgentId),  //登入者帳號
                        new Claim("FullName",userData.AgentName),     //登入者姓名
                        new Claim("LoginTime",userData.LoginTime.ToString()),  //登入時間
                        new Claim(ClaimTypes.Role,userData.RoleID)
                    };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties {

            };

            //登入
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
            #endregion

            return RedirectToAction("Index", "Home", null);
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Logout() {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}
