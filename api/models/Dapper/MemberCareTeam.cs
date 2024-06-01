namespace Gainwell.Models.Dapper;

public class MemberCareTeamModel : ModelBase {

    public required MemberModel Member { get; set; }

    public required List<PersonModel> People { get; set; }

}
