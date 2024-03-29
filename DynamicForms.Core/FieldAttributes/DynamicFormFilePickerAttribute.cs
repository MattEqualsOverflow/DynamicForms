namespace DynamicForms.Core.FieldAttributes;

[AttributeUsage((AttributeTargets.Property))]
public class DynamicFormFilePickerAttribute(
    string displayName,
    FilePickerType filePickerType,
    string filter = "All Files:*",
    string? checkSum = null,
    string? checkSumError = null,
    string? hintText = null,
    string? visibleWhenProperty = null,
    string? editableWhenProperty = null,
    string groupName = "",
    int order = 1000)
    : DynamicFormFieldAttribute(displayName, hintText, visibleWhenProperty, editableWhenProperty, groupName, order)
{
    public override DynamicFormFieldType FieldType => DynamicFormFieldType.FilePicker;
    
    public FilePickerType FilePickerType { get; } = filePickerType;

    public string Filter { get; } = filter;

    public string? CheckSum { get; } = checkSum;

    public string? CheckSumError { get; } = checkSumError;
}