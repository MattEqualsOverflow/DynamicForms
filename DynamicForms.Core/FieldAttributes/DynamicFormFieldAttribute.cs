namespace DynamicForms.Core.FieldAttributes;

[AttributeUsage((AttributeTargets.Property))]
public abstract class DynamicFormFieldAttribute(
    string displayName,
    string? hintText = null,
    string? visibleWhenProperty = null,
    string? editableWhenProperty = null,
    string groupName = "",
    int order = 1000)
    : DynamicFormObjectAttribute(groupName, order)
{
    public abstract DynamicFormFieldType FieldType { get; }
    
    public string DisplayName { get; } = displayName;
    public string? HintText { get; } = hintText;
    public string? VisibleWhenProperty { get; } = visibleWhenProperty;
    public string? EditableWhenProperty { get; } = editableWhenProperty;
}