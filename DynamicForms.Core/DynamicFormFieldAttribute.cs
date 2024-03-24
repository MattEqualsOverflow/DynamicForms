namespace DynamicForms.Core;

[AttributeUsage((AttributeTargets.Property))]
public class DynamicFormFieldAttribute : Attribute
{
    public DynamicFormFieldAttribute(string displayName, DynamicFormFieldType fieldType, string? hintText = null, string? visibleWhenProperty = null, string? editableWhenProperty = null, string? comboBoxOptionsProperty = null)
    {
        DisplayName = displayName;
        FieldType = fieldType;
        VisibleWhenProperty = visibleWhenProperty;
        EditableWhenProperty = editableWhenProperty;
        ComboBoxOptionsProperty = comboBoxOptionsProperty;
    }
    
    public string DisplayName { get; set; }
    
    public DynamicFormFieldType? FieldType { get; init; }
    
    public string? VisibleWhenProperty { get; set; }
    
    public string? EditableWhenProperty { get; set; }
    
    public string? ComboBoxOptionsProperty { get; set; }
}