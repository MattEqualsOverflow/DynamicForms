using DynamicForms.Core.FieldAttributes;

namespace DynamicForms.Core;

[AttributeUsage((AttributeTargets.Property))]
public class DynamicFormCheckBoxAttribute(
    string displayName,
    string? hintText = null,
    string? visibleWhenProperty = null,
    string? editableWhenProperty = null,
    string groupName = "",
    int order = 1000)
    : DynamicFormFieldAttribute(displayName, hintText, visibleWhenProperty, editableWhenProperty, groupName, order)
{
    public override DynamicFormFieldType FieldType => DynamicFormFieldType.CheckBox;
}