using System.Data;
using Dapper;
using Gainwell.Attributes;
using Gainwell.Data;
using Gainwell.Models.Dapper;

namespace Gainwell.Repositories.Dapper;

[ModelName("member")]
public class MemberRepository(DapperDbContext context) : RepositoryBase<MemberModel>(context) { 

    public override async Task<int> CreateAsync(MemberModel entity, int userId) {
        var parameters = new DynamicParameters();
        parameters.Add("@tenantId", entity.Tenant?.Id);
        parameters.Add("@firstName", entity.FirstName);
        parameters.Add("@lastName", entity.LastName);
        parameters.Add("@dateOfBirth", entity.DateOfBirth, DbType.Date);
        return await CreateAsync(parameters);
    }

}
