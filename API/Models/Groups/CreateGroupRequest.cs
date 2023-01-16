﻿using DAL.DataEntities;

namespace API.Models.Groups
{
    public class CreateGroupRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Guid> UserGuids { get; set; } // move it to add user to group endpoint
    }
}
