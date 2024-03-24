namespace DynamicForms.Core;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
public class DynamicFormGroupAttribute : Attribute
{
    public DynamicFormGroupAttribute(string groupName = "")
    {
        GroupName = groupName;
    }
    
    public string GroupName { get; init; }
}