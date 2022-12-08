using CIPlatform.DataModels;

namespace CIPlatform.Models
{
    public class MissionDetailModel
    {
        public MissionCardModel missionCard { get; set; }

        public List<MissionMedium> imgs { get; set; }

        public string? skills { get; set; }

        public string? availability { get; set; }

        public List<VolunteerModel>? volunteers { get; set; }

        public List<MissionDocument>? docs { get; set; }

        public List<MissionCardModel>? relatedMission { get; set; }

        public List<CommentModel>? comments { get; set; }

    }
}
