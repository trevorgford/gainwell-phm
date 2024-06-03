using System.Data;
using Dapper;
using Gainwell.Attributes;
using Gainwell.Data;
using Gainwell.Models.Dapper;

namespace Gainwell.Repositories.Dapper;

[ModelName("memberCareTeam")]
public class MemberCareTeamRepository(DapperDbContext context) : RepositoryBase<MemberCareTeamModel>(context) { 

    public async Task<int> AddPersonToCareTeamAsync(int? memberId, string? firstName, string? lastName, string? title, int tenantId) {
        var parameters = new DynamicParameters();
        parameters.Add("@memberId", memberId);
        parameters.Add("@tenantId", tenantId);
        parameters.Add("@firstName", firstName);
        parameters.Add("@lastName", lastName);
        parameters.Add("@title", title);
        parameters.Add("@personId", dbType: DbType.Int32, direction: ParameterDirection.Output);

        using var db = _context.CreateConnection();
        var results = await db.ExecuteStoredProcedureAsync("memberCareTeam_person_add", parameters);
        return parameters.Get<int>("@personId");
    }

    public async Task<int> RemovePersonFromCareTeamAsync(int? memberId, int? personId) {
        var parameters = new DynamicParameters();
        parameters.Add("@memberId", memberId);
        parameters.Add("@personId", personId);

        using var db = _context.CreateConnection();
        return await db.ExecuteStoredProcedureAsync("memberCareTeam_person_remove", parameters);
    }

    public async Task<MemberCareTeamModel?> GetCareTeamAsync(int memberId) {
        var parameters = new DynamicParameters();
        parameters.Add("@memberId", memberId);

        using var db = _context.CreateConnection();
        var results = await db.QueryAsync<dynamic>("memberCareTeam_load", parameters, commandType: CommandType.StoredProcedure);

        MemberCareTeamModel? careTeam = null;

        foreach (var row in results) {
            careTeam ??= new MemberCareTeamModel {
                    Member = new MemberModel {
                        Id = row.Id,
                        FirstName = row.firstName,
                        LastName = row.lastName
                    },
                    People = []
                };

            if (row.personId != null) {
                careTeam.People.Add(new PersonModel {
                    Id = row.personId,
                    FirstName = row.personFirstName,
                    LastName = row.personLastName,
                    Title = row.title
                });
            }
        }

        return careTeam;        
    }

    public override Task<int> CreateAsync(MemberCareTeamModel entity, int userId, int tenantId) {
        throw new NotImplementedException();
    }

    public override Task<int> DeactivateAsync(DynamicParameters parameters) {
        throw new NotImplementedException();
    }

    public override Task<int> ActivateAsync(DynamicParameters parameters) {
        throw new NotImplementedException();
    }

}