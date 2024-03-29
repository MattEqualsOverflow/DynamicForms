namespace DynamicForms.Core.FieldAttributes;

[AttributeUsage((AttributeTargets.Property))]
public class DynamicFormComboBoxAttribute(
    string displayName,
    string? hintText = null,
    string? visibleWhenProperty = null,
    string? editableWhenProperty = null,
    string? comboBoxOptionsProperty = null,
    string groupName = "",
    int order = 1000)
    : DynamicFormFieldAttribute(displayName, hintText, visibleWhenProperty, editableWhenProperty, groupName, order)
{
    public override DynamicFormFieldType FieldType => DynamicFormFieldType.ComboBox;
    
    public string? ComboBoxOptionsProperty { get; } = comboBoxOptionsProperty;
}