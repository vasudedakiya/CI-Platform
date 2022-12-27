using Microsoft.AspNetCore.Mvc.Rendering;

namespace CIPlatform.Models
{
    public class AddUserSkillModel
    {
        public int UserId { get; set; }

        public List<SelectListItem> skills { get; set; }
        
        public List<SelectListItem>? userOldSkill { get; set; }

        public List<SelectListItem>? userNewSkill { get; set; }

    }
}
