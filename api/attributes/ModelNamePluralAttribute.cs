namespace Gainwell.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class ModelNamePluralAttribute(string pluralName) : Attribute {

    public string PluralName { get; } = pluralName;

}
