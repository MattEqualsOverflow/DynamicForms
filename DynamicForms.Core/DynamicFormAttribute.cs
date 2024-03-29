namespace DynamicForms.Core;

[AttributeUsage(AttributeTargets.Class)]
public class DynamicFormAttribute(DynamicFormGroupStyle style, DynamicFormGroupType type, string name = "") : Attribute
{
    public DynamicFormGroupStyle Style => style;
    public DynamicFormGroupType Type => type;
    public string Name => name;
}