namespace Gainwell.Models.Dapper;

public class MemberModel : ModelBase {

    public TenantModel? Tenant { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public UserModel? User { get; set; }

    public bool? Active { get; set; }

}
