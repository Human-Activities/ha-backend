﻿using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.DataEntities
{
    public class Activity
    {
        public int Id { get; set; }

        public Guid ActivityGuid { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public bool IsTemplate { get; set; } = false;

        public bool IsPrivate { get; set; } = true;


        public virtual User User { get; set; }

        public virtual Category Category { get; set; }
    }
}
