using Core.DTOs;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ITokenService
    {
        Task<UserDTO> CreateTokenAsync(User user);
        Task<UserDTO> RefreshTokenAsync(string token);//refreshtoken
    }
}
