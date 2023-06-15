using API.Controllers;
using Application.Services;
using Domain.Entities;
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
using Xunit;

namespace UnitTest
{
    public  class ControllerTest
    {
        [Fact]
        public async Task GetAllUserShouldBeUserList()
        {
            var userRepository = new Mock<IUserRepository>();

            userRepository.Setup(s => s.GetAll()).ReturnsAsync(MockData.MockData.GetUsers());


            UserController userController = new UserController(userRepository.Object,null);
            var res = await userController.GetAllUser();

         
            Assert.IsType<List<User>>(res);
        }
    }
}
