using System;
using System.Collections.Generic;

namespace CIPlatform.DataModels
{
    public partial class Country
    {
        public Country()
        {
            Missions = new HashSet<Mission>();
            States = new HashSet<State>();
            Users = new HashSet<User>();
        }

        public long CountryId { get; set; }
        public string Name { get; set; } = null!;
        public string? Iso { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Mission> Missions { get; set; }
        public virtual ICollection<State> States { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
