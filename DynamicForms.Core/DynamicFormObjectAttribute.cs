namespace DynamicForms.Core;

public class DynamicFormObjectAttribute (string groupName = "",
    int order = 1000) : Attribute
{
    public string GroupName => groupName;
    public int Order => order;
}