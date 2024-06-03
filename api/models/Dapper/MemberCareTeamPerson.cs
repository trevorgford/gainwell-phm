namespace Gainwell.Models.Dapper;

public class MemberCareTeamPersonModel {

    // public MemberModel Member { get; set; }
    // public PersonModel Person { get; set; }

    public int? MemberId { get; set; }
    public int? PersonId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Title { get; set; }

}