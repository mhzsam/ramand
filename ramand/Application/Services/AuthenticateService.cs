

using Application.DTO;
using Infrastructure.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IUserRepository _userRepository;

        public AuthenticateService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> Login(UserLogin user)
        {
            var findUser = await _userRepository.GetByUsername(user.UserName);
            //از sha256 و سایر روش ها صرف نظز شده 
            if (findUser.Password == user.Password)
            {
                return "token";
            }
            return "";
        }
    }
}
