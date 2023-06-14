using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Interface
{
    public interface IUserRepository
    {
        Task<User> GetByUsername(string username);
        Task<List<User>> GetAll();
        Task<bool> UpdateUserToken(int userId, string token);

    }
}
