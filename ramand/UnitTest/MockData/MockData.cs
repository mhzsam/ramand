using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.MockData
{
    public class MockData
    {
        public static List<User> GetUsers()
        {
            return new List<User>
        {
            new User { Id = 1,Username="mohammad",Password="123"},
            new User { Id = 1,Username="Ali",Password="567"}
        };

        }
        public static  User GetByUserName()
        {
            var res=  new User { Id = 1, Username = "mohammad", Password = "1234" };
            return res;
        }
        public static string GetNewToken(int id)
        {
            return "tokenGenerated";
        }
        public static bool ReturnTrue()
        {
            return true;
        }
    }
}
