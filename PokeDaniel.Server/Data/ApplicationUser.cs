﻿using Microsoft.AspNetCore.Identity;

namespace PokeDaniel.Server.Data
{

    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RefreshToken { get; set; }
    }
}
