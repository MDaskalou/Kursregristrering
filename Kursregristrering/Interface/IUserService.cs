using System;
using System.Threading.Tasks;
using Kursregristrering.DTOs;
using Kursregristrering.Models;

namespace Kursregristrering.Interface
{
    public interface IUserService
    {
        Task<UserDTO> GetUserByIdAsync(Guid id);
        Task<UserDTO> CreateUSerAsync(UserDTO userDto);
        Task<UserDTO> UpdateUserAsync(Guid id, UserDTO userDto);
        Task<bool> DeleteUserAsync(Guid id);
    }
}
