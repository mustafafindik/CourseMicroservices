using Course.Services.Discount.Repositories.Abstract;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Discount.Repositories.Concrete
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<bool> Delete(int id)
        {
            var status = await _dbConnection.ExecuteAsync("delete from discount where id=@Id", new { Id = id });
            return status > 0;
        }

        public async Task<IEnumerable<Entities.Discount>> GetAll()
        {
            var discounts = await _dbConnection.QueryAsync<Entities.Discount>("Select * from discount");
            return discounts;
        }

        public async Task<Entities.Discount> GetByCodeAndUserId(string code, string userId)
        {
            var discounts = await _dbConnection.QueryAsync<Entities.Discount>("select * from discount where userid=@UserId and code=@Code", new { UserId = userId, Code = code });
            return discounts.FirstOrDefault();
        }

        public async Task<Entities.Discount> GetById(int id)
        {
            var discount = (await _dbConnection.QueryAsync<Entities.Discount>("select * from discount where id=@Id", new { Id = id })).SingleOrDefault();
            return discount;
        }

        public async Task<bool> Save(Entities.Discount discount)
        {
            var saveStatus = await _dbConnection.ExecuteAsync("INSERT INTO discount (userid,rate,code) VALUES(@UserId,@Rate,@Code)", discount);
            return saveStatus > 0;
        }

        public async Task<bool> Update(Entities.Discount discount)
        {
            var status = await _dbConnection.ExecuteAsync("update discount set userid=@UserId, code=@Code, rate=@Rate where id=@Id", new { Id = discount.Id, UserId = discount.UserId, Code = discount.Code, Rate = discount.Rate });
            return status > 0;
        }
    }
}
