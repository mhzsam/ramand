using Application.Services;
using Infrastructure.Repository.Interface;

using Infrustracture.Models;
using Infrustracture.Utility;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class ServiceTest
    {
        [Fact]
        public async Task LoginShouldBeString()
        {
            var userRepository = new Mock<IUserRepository>();
          
            userRepository.Setup(s => s.UpdateUserToken(1, "tokenGenerated")).ReturnsAsync(MockData.MockData.ReturnTrue());
            userRepository.Setup(s => s.GetByUsername("mohammad")).ReturnsAsync(MockData.MockData.GetByUserName());
            Configs configs = new Configs()
            {
                TokenKey = "67E6FF39A35B8,9F95A2B59D33E,8F2318242E1E6",
                TokenTimeOut = 60,
                RefreshTokenTimeOut = 15
            };
            IOptions<Configs> options = Options.Create<Configs>(configs);
            
            var encryptionUtility = new Mock<EncryptionUtility>(options);     
           
            encryptionUtility.Setup(f => f.GetNewToken(1)).Returns(MockData.MockData.GetNewToken(1));  

            AuthenticateService service = new AuthenticateService(userRepository.Object, encryptionUtility.Object);
            var res =await service.Login(new Application.DTO.UserLogin() { UserName = "mohammad", Password = "1234" });

            Assert.Equal("tokenGenerated", res);
        }
    }
}
