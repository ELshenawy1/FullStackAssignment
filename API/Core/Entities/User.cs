﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class User : IdentityUser
    {
        public DateTime LastLoginTime { get; set; }
        public List<RefreshToken>? RefreshTokens { get; set; } = new();
    }
}
