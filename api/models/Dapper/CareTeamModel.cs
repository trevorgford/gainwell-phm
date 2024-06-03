namespace Gainwell.Models.Dapper;

public class CareTeamModel {
    
    public required MemberModel Member { get; set; }
    public List<PersonModel> CareTeamPeople { get; set; } = new List<PersonModel>();

}