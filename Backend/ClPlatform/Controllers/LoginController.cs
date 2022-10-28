using ClPlatform.DataModels;
using ClPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ClPlatform.Controllers
{
    public class LoginController : Controller
    {
        public ClPlatformContext _db = new ClPlatformContext();

        #region Login
        public IActionResult Login()
        {
            var loginModel = new LoginModel();
            loginModel.imgDesc = GetImgDesc();

            return View(loginModel);
        }
        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email.Equals(model.user.Email.ToLower()) && u.Password.Equals(model.user.Password));

            if (user == null)
            {
                var loginModel = new LoginModel();
                loginModel.imgDesc = GetImgDesc();
                TempData["ErrorMes"] = "Email or password is incorrect";

                return View(loginModel);
            }
            return RedirectToAction("Index", "Home");
        }
        #endregion Login

        #region Register
        public IActionResult Register()
        {
            var loginModel = new LoginModel();
            loginModel.imgDesc = GetImgDesc();
            return View(loginModel);
        }

        [HttpPost]
        public IActionResult Register(LoginModel model)
        {
            var user = _db.Users.Add(model.user);
            _db.SaveChanges();

            return RedirectToAction("Login", "Login");
        }
        #endregion Register

        #region LostPassword
        public IActionResult LostPassword()
        {
            var loginModel = new LoginModel();
            loginModel.imgDesc = GetImgDesc();
            return View(loginModel);
        }
        [HttpPost]
        public IActionResult LostPassword(LoginModel model)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email.Equals(model.user.Email.ToLower()));

            if(user == null) {
                TempData["ErrorMes"] = "Email Not exist";
                return View();
            }

            var encryptedId = Convert.ToBase64String(Encoding.ASCII.GetBytes(user.Id.ToString()));

            return RedirectToAction("ResetPassword","Login",new {id = encryptedId});
        }
        #endregion LostPassword

        #region ResetPassword
        public IActionResult ResetPassword()
        {
            var loginModel = new LoginModel();
            loginModel.imgDesc = GetImgDesc();
            return View(loginModel);
        }        
        
        [HttpPost]
        public IActionResult ResetPassword(LoginModel model,string id)
        {
            id = Encoding.UTF8.GetString(Convert.FromBase64String(id));
            var user = _db.Users.FirstOrDefault(u => u.Id == int.Parse(id));
            user.Password = model.user.Password;
            _db.Users.Update(user);
            _db.SaveChanges();

            return RedirectToAction("Login", "Login");
        }
        #endregion ResetPassword

        #region Get Carousel 
        public List<ImgDesc> GetImgDesc()
        {
            return _db.ImgDescs.ToList();
        }
        #endregion Get Carousel 
    }
}
