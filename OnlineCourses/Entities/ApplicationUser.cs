﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineCourses.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(250)]
        public string FirstName { get; set; }

        [StringLength(250)]
        public string LastName { get; set; }

        [StringLength(250)]
        public string Address1 { get; set; }

        [StringLength(250)]
        public string Address2 { get; set; }
        [StringLength(50)]
        public string PostCode { get; set; }

        [ForeignKey("UserId")]
        public virtual ICollection<UserCategory> UserCategory { get; set; }
    }
}
