using System.Data;
using Dapper;
using Gainwell.Attributes;
using Gainwell.Data;
using Gainwell.Models.Dapper;

namespace Gainwell.Repositories.Dapper;

[ModelName("user")]
public class UserRepository(DapperDbContext context) : RepositoryBase<UserModel>(context) { 

    public override async Task<UserModel?> GetByIdAsync(int id) {
        if(string.IsNullOrEmpty(GetByIdStoredProcedure)) throw new InvalidOperationException("GetByIdStoredProcedure is not set");
        using var db = _context.CreateConnection();
        var results = await db.QueryAsync<dynamic>(GetByIdStoredProcedure, new { id }, commandType: CommandType.StoredProcedure);

        UserModel? user = null;

        foreach (var row in results) {
            user ??= new UserModel {
                    Id = row.id,
                    UserName = row.userName,
                    FirstName = row.firstName,
                    LastName = row.lastName,
                    Tenants = []
                };

            if (row.tenantId != null) {
                user.Tenants.Add(new TenantModel {
                    Id = row.tenantId,
                    Code = row.code,
                    Description = row.description
                });
            }
        }

        return user;
    }

    public override async Task<int> CreateAsync(UserModel entity, int userId) {
        var parameters = new DynamicParameters();
        parameters.Add("@username", entity.UserName);
        parameters.Add("@firstName", entity.FirstName);
        parameters.Add("@lastName", entity.LastName);
        parameters.Add("@email", entity.Email);
        parameters.Add("@passwordHash", entity.PasswordHash);
        parameters.Add("@passwordSalt", entity.PasswordSalt);
        return await CreateAsync(parameters);
    }

}
