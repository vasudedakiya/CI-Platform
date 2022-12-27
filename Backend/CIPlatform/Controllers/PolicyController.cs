using CIPlatform.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatform.Controllers
{
    public class PolicyController : Controller
    {
        public CiPlatformContext _db = new CiPlatformContext();

        public IActionResult Policy()
        {
            var policys = _db.CmsPages.ToList();
            return View(policys);
        }
    }
}
