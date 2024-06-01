namespace Gainwell.Models.Dapper;

public class PersonModel : ModelBase {

    public TenantModel? Tenant { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Title { get; set; }

    public UserModel? User { get; set; }

    public bool? Active { get; set; }

}