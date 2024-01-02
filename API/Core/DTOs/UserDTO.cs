using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class UserDTO
    {
        public string Email { get; set; }
        public string Name { get; set; }    
        public string Token { get; set; }
        public DateTime TokenExpiration { get; set; }
        //[JsonIgnore]
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
