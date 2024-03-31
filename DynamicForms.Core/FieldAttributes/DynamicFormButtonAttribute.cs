namespace DynamicForms.Core.FieldAttributes;

[AttributeUsage((AttributeTargets.Event))]
public class DynamicFormButtonAttribute(
    string displayName,
    string? hintText = null,
    string? visibleWhenProperty = null,
    string? editableWhenProperty = null,
    string groupName = "",
    int order = 1000)
    : DynamicFormFieldAttribute(displayName, hintText, visibleWhenProperty, editableWhenProperty, groupName, order)
{
    public override DynamicFormFieldType FieldType => DynamicFormFieldType.Button;
}