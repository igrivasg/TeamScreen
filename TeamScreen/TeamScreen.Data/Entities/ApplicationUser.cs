﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TeamScreen.Data.Entities
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public byte[] UserPhoto { get; set; }
    }
}
