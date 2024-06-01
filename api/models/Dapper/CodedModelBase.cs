namespace Gainwell.Models.Dapper;

public class CodedModelBase : ModelBase {

    public string? Code { get; set; }

    public string? Description { get; set; }

    public bool? Active { get; set; }

}