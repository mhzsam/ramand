

using Application.DTO;
using Infrastructure.Repository.Interface;
using Infrustracture.Utility;
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
        private readonly EncryptionUtility _encryptionUtility;

        public AuthenticateService(IUserRepository userRepository, EncryptionUtility encryptionUtility)
        {
            _userRepository = userRepository;
           _encryptionUtility = encryptionUtility;
        }

        public async Task<string> Login(UserLogin user)
        {
            var findUser = await _userRepository.GetByUsername(user.UserName);
            if (findUser == null)
                return null;
            //از هش کردن و سایر روش ها صرف نظر شده صرفا برای سریع تر انجام شدن
            if (findUser.Password == user.Password)
            {
                var token= _encryptionUtility.GetNewToken(findUser.Id);
                var res=await _userRepository.UpdateUserToken(findUser.Id, token);
                if(res)
                return token;
                return null;
            }
            return null;
        }
    }
}
