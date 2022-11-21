using CIPlatform.DataModels;
using CIPlatform.Models;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatform.Controllers
{
    public class AdminController : Controller
    {
        public CiPlatformContext _db = new CiPlatformContext();
        private readonly IWebHostEnvironment _hostEnvironment;

        #region Constructor
        public AdminController(IWebHostEnvironment _environment)
        {
            _hostEnvironment = _environment;
        }
        #endregion Constructor

        #region User DataTable
        [CheckSession]
        public IActionResult User()
        {
            var model = new AdminModel();
            model.users = _db.Users.Where(u => u.DeletedAt == null).AsEnumerable().ToList();
            return View(model);
        }
        #endregion User DataTable

        #region Add Admin
        [HttpGet]
        public IActionResult CreateAdmin()
        {
            Admin admin = new Admin();
            return PartialView("_AddAdminPartial", admin);
        }

        [HttpPost]
        public IActionResult CreateAdmin(Admin model)
        {
            var admin = _db.Admins.FirstOrDefault(u => u.Email.Equals(model.Email.ToLower()) && u.DeletedAt == null);

            if (admin == null)
            {

                var newAdmin = _db.Admins.Add(model);
                _db.SaveChanges();

                return RedirectToAction("User", "Admin");
            }

            TempData["ErrorMes"] = "User Alrady exist with same Email";
            return RedirectToAction("User", "Admin");
        }
        #endregion Add Admin

        #region Delete User
        public IActionResult DeleteUser(int id)
        {
            var user = _db.Users.FirstOrDefault(x => x.UserId == id);
            user.DeletedAt = DateTime.Now;
            _db.Users.Update(user);
            _db.SaveChanges();
            return RedirectToAction("User", "Admin");
        }
        #endregion Delete User

        #region CMS DataTable
        [CheckSession]
        public IActionResult CMSPage()
        {
            var model = new AdminModel();
            model.cmsPages = _db.CmsPages.Where(x => x.DeletedAt == null).AsEnumerable().ToList();
            return View(model);
        }
        #endregion CMS DataTable

        #region Add CMS
        [CheckSession]
        public IActionResult AddCMSPage()
        {
            return View();
        }
        #endregion Add CMS

        #region Mission DataTable
        [CheckSession]
        public IActionResult Mission()
        {
            var model = new AdminModel();
            model.missions = _db.Missions.Where(m => m.DeletedAt == null).AsEnumerable().ToList();
            return View(model);
        }
        #endregion Mission DataTable


        #region Add Mission
        [HttpGet]
        public IActionResult CreateMission()
        {
            AddMissionModel mission = new AddMissionModel();
            mission.countrys = _db.Countries.Where(x => x.DeletedAt == null).AsEnumerable().ToList();
            mission.citys = _db.Cities.Where(x => x.DeletedAt == null).AsEnumerable().ToList();
            mission.skills = _db.Skills.Where(s => s.DeletedAt == null).AsEnumerable().ToList();
            mission.themes = _db.Themes.Where(t => t.DeletedAt == null).AsEnumerable().ToList();

            return PartialView("_AddMissionPartial",mission);
        }


        #endregion Add Mission

        #region Theme DataTable
        [CheckSession]
        public IActionResult MissionTheme()
        {
            var model = new AdminModel();
            model.themes = _db.Themes.Where(t => t.DeletedAt == null).AsEnumerable().ToList();
            return View(model);
        }
        #endregion Theme DataTable

        #region Skill DataTable
        [CheckSession]
        public IActionResult MissionSkill()
        {
            var model = new AdminModel();
            model.skills = _db.Skills.Where(s => s.DeletedAt == null).AsEnumerable().ToList();
            return View(model);
        }
        #endregion Skill DataTable

        #region Mission Application DataTable
        [CheckSession]
        public IActionResult MissionApplication()
        {
            var model = new AdminModel();
            model.missionApplications = _db.MissionApplications.Where(s => s.DeletedAt == null).AsEnumerable().ToList();
            return View(model);
        }
        #endregion Mission Application DataTable

        #region Story List DataTable
        [CheckSession]
        public IActionResult StoryList()
        {
            var model = new AdminModel();
            model.stories = _db.Stories.Where(x => x.Status == 1 && x.DeletedAt == null).AsEnumerable().ToList();
            return View(model);
        }
        #endregion Story List DataTable

        #region Story Application DataTable
        [CheckSession]
        public IActionResult StoryApplication()
        {
            var model = new AdminModel();
            model.stories = _db.Stories.Where(x => x.Status == 0 && x.DeletedAt == null).AsEnumerable().ToList();
            return View(model);
        }
        #endregion Story Application DataTable

        #region Banner DataTable
        [CheckSession]
        public IActionResult BannerManagement()
        {
            var model = new AdminModel();
            model.banners = _db.Banners.Where(b => b.DeletedAt == null).AsEnumerable().ToList();
            return View(model);
        }
        #endregion Banner DataTable

        #region Add Banner
        [HttpGet]
        public IActionResult CreateBanner()
        {
            Banner banner = new Banner();
            return PartialView("_AddBannerPartial", banner);
        }

        [HttpPost]
        public IActionResult CreateBanner(Banner model, IFormFile? carouselImg)
        {
            if (model.BannerId != 0)
            {
                EditBanner(model, carouselImg);
                return RedirectToAction("BannerManagement", "Admin");
            }
            else
            {
                if (carouselImg != null)
                {
                    model.Image = saveImg(carouselImg, "Banner");
                }
                var newBanner = _db.Banners.Add(model);
                _db.SaveChanges();

                return RedirectToAction("BannerManagement", "Admin");
            }
        }
        #endregion Add Banner

        #region Delete Banner
        public IActionResult DeleteBanner(int id)
        {
            var banner = _db.Banners.FirstOrDefault(x => x.BannerId == id);
            banner.DeletedAt = DateTime.Now;
            _db.Banners.Update(banner);
            _db.SaveChanges();
            return RedirectToAction("BannerManagement", "Admin");
        }
        #endregion Delete Banner

        #region Edit Banner
        [HttpGet]
        public IActionResult EditBanner(int id)
        {
            var banner = _db.Banners.FirstOrDefault(b => b.BannerId == id);
            return PartialView("_AddBannerPartial", banner);
        }

        [HttpPost]
        public IActionResult EditBanner(Banner model, IFormFile? carouselImg)
        {
            if (carouselImg != null)
            {
                model.Image = saveImg(carouselImg, "Banner");
            }

            model.UpdatedAt = DateTime.Now;
            _db.Banners.Update(model);
            _db.SaveChanges();
            return RedirectToAction("BannerManagement", "Admin");
        }
        #endregion Edit Banner

        #region saveImg
        public string saveImg(IFormFile img, string folder)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;

            string fileName = Guid.NewGuid().ToString();
            var uploads = Path.Combine(wwwRootPath, @"Images\" + folder);
            var extension = Path.GetExtension(img.FileName);

            using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
            {
                img.CopyTo(fileStreams);
            }
            return @"~/Images/" + folder +  "/" + fileName + extension;

        }
        #endregion saveImg
    }
}
