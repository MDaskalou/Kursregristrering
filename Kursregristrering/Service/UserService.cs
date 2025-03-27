using AutoMapper;
using Kursregristrering.DBContext;
using Kursregristrering.DTOs;
using Kursregristrering.Interface;
using Kursregristrering.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Kursregristrering.Service
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDBContext _context;

        public UserService(IMapper mapper, ApplicationDBContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<UserDTO> GetUserByIdAsync(Guid id)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
            return _mapper.Map<UserDTO>(userEntity);
        }

        public async Task<UserDTO> CreateUSerAsync(UserDTO userDto)
        {
            // Här mappas DTO till entitet
            var userEntity = _mapper.Map<User>(userDto);

            // Lägg till entiteten i databasen
            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();

            // Mappa tillbaka till DTO (nu med genererat Id, etc.)
            return _mapper.Map<UserDTO>(userEntity);
        }

        public async Task<UserDTO> UpdateUserAsync(Guid id, UserDTO userDto)
        {
            var userEntity = await _context.Users.FindAsync(id);
            if (userEntity == null)
            {
                throw new Exception("Användaren hittades inte.");
            }

            _mapper.Map(userDto, userEntity);
            _context.Users.Update(userEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDTO>(userEntity);
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var userEntity = await _context.Users.FindAsync(id);
            if (userEntity == null)
            {
                return false;
            }

            _context.Users.Remove(userEntity);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
