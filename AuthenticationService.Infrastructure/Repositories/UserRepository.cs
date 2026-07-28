using AuthenticationService.Domain.Entities;
using AuthenticationService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using AuthenticationService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Get user by Id
        /// </summary>
        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Get user by Email
        /// </summary>
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        /// <summary>
        /// Get all users
        /// </summary>
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Add new user
        /// </summary>
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Update existing user
        /// </summary>
        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete user by Id
        /// </summary>
        public async Task DeleteAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
