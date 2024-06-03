using Dapper;
using Gainwell.Attributes;
using Gainwell.Data;
using Gainwell.Models.Dapper;

namespace Gainwell.Repositories.Dapper;

[ModelName("person")]
[ModelNamePlural("people")]
public class PersonRepository(DapperDbContext context) : RepositoryBase<PersonModel>(context) { 

    public override async Task<int> CreateAsync(PersonModel entity, int userId, int tenantId) {
        var parameters = new DynamicParameters();
        parameters.Add("@tenantId", tenantId);
        parameters.Add("@firstName", entity.FirstName);
        parameters.Add("@lastName", entity.LastName);
        parameters.Add("@title", entity.Title);
        return await CreateAsync(parameters);
    }

}
