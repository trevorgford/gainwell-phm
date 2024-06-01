using System.Data;
using Dapper;
using Gainwell.Attributes;
using Gainwell.Data;
using Gainwell.Models.Dapper;

namespace Gainwell.Repositories.Dapper;

[ModelName("memberCareTeam")]
public class MemberCareTeamRepository(DapperDbContext context) : RepositoryBase<MemberCareTeamModel>(context) { 

    public async Task<int> AddPersonToCareTeamAsync(int memberId, int personId) {
        var parameters = new DynamicParameters();
        parameters.Add("@memberId", memberId);
        parameters.Add("@personId", personId);

        using var db = _context.CreateConnection();
        return await db.ExecuteStoredProcedureAsync("memberCareTeam_person_add", parameters);
    }

    public async Task<int> RemovePersonFromCareTeamAsync(int memberId, int personId) {
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

        var careTeam = new MemberCareTeamModel() { Member = new MemberModel(), People = []};

        await db.QueryAsync<MemberModel, PersonModel, MemberCareTeamModel>(
            "memberCareTeam_load",
            (member, person) => {
                careTeam.Member = member;
                careTeam.People.Add(person);
                return careTeam;
            },
            splitOn: "personId",
            param: parameters,
            commandType: CommandType.StoredProcedure
        );

        return careTeam;
    }

    public override Task<int> CreateAsync(MemberCareTeamModel entity, int userId) {
        throw new NotImplementedException();
    }

    public override Task<int> DeactivateAsync(DynamicParameters parameters) {
        throw new NotImplementedException();
    }

    public override Task<int> ActivateAsync(DynamicParameters parameters) {
        throw new NotImplementedException();
    }

}