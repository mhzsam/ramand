using Dapper;
using Domain.Entities;
using Infrastructure.Dapper;
using Infrastructure.Repository.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private readonly DapperContext _context;
        private IDbConnection dbConnection = null;
        public UserRepository(DapperContext context)
        {
            _context = context;
            dbConnection = context.CreateConnection();
        }

        public void Dispose()
        {
            dbConnection.Close();
        }

        public async Task<List<User>> GetAll()
        {

            var query = "SELECT * FROM Users";
            //var client = _context.CreateConnection();
            var users = await dbConnection.QueryAsync<User>(query);

            return users.ToList();
        }

        public async Task<User> GetByUsername(string username)
        {
            //var client = _context.CreateConnection();
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@Username", username);

            var user=await dbConnection.QueryFirstOrDefaultAsync<User>("dbo.SP_FindUserByUsername", queryParameters, commandType: CommandType.StoredProcedure);

            return user;
        }
    }
}
