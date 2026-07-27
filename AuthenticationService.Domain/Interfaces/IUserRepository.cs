using AuthenticationService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationService.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);

        Task<User?> GetByEmailAsync(string email);

        Task<IEnumerable<User>> GetAllAsync();

        Task AddAsync(User user);

        Task UpdateAsync(User user);

        Task DeleteAsync(Guid id);
    }
}
