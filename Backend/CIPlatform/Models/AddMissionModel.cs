using CIPlatform.DataModels;

namespace CIPlatform.Models
{
    public class AddMissionModel
    {
        public Mission mission { get; set; }

        public List<Skill> skills { get; set; }

        public List<Theme> themes { get; set; }

        public List<City> citys { get; set; }

        public List<State> states { get; set; }

        public List<Country> countrys { get; set; }

        public List<Skill> addSkill { get; set; }

        public GoalMission goal { get; set; }

        public MissionDocument missionDocument { get; set; }

        public MissionMedium missionMedia { get; set; }

        public MissionSkill missionSkill { get; set; }

    }
}
