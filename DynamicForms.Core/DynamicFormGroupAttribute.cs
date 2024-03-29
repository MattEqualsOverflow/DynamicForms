namespace DynamicForms.Core;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class DynamicFormGroupAttribute(DynamicFormGroupStyle style, DynamicFormGroupType type, string name = "", string? parentGroup = null, int order = 1000) : Attribute
{
    public DynamicFormGroupStyle Style => style;
    public DynamicFormGroupType Type => type;
    public string Name => name;
    public string? ParentGroup => parentGroup;
    public int Order => order;
}