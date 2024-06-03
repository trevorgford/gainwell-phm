using System.Data;
using Dapper;
using Gainwell.Attributes;
using Gainwell.Data;
using Gainwell.Models.Dapper;

namespace Gainwell.Repositories.Dapper;

[ModelName("member")]
public class MemberRepository(DapperDbContext context) : RepositoryBase<MemberModel>(context) { 

    public override async Task<int> CreateAsync(MemberModel entity, int userId, int tenantId) {
        var parameters = new DynamicParameters();
        parameters.Add("@tenantId", tenantId);
        parameters.Add("@firstName", entity.FirstName);
        parameters.Add("@lastName", entity.LastName);
        parameters.Add("@dateOfBirth", entity.DateOfBirth, DbType.Date);
        return await CreateAsync(parameters);
    }

    public virtual async Task<IEnumerable<MemberModel>> Search(string query, int tenantId) {
        using var db = _context.CreateConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@searchText", query);
        parameters.Add("@tenantId", tenantId);
        return await db.QueryStoredProcedureAsync<MemberModel>("members_search", parameters);
    }

}
