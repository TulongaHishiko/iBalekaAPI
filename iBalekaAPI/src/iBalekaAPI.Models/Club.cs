﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iBalekaAPI.Models
{
    public class Club
    {
        public Club()
        {
        }
        [Key]
        public int ClubId { get; set; }
        [ConcurrencyCheck]
        public string DateCreated { get; set; }
        public bool Deleted { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        [ConcurrencyCheck]
        public string Name { get; set; }

        public string UserId { get; set; }

        public virtual ICollection<ClubMember> ClubMember { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
