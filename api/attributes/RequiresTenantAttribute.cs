namespace Gainwell.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class RequiresTenantAttribute(bool requiresTenant) : Attribute {

    public bool RequiresTenant { get; } = requiresTenant;

}
