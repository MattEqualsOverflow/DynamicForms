namespace DynamicForms.Core.FieldAttributes;

[AttributeUsage((AttributeTargets.Property))]
public class DynamicFormNumericUpDownAttribute(
    string displayName,
    double increment = 1,
    double minValue = int.MinValue,
    double maxValue = int.MaxValue,
    string? hintText = null,
    string? visibleWhenProperty = null,
    string? editableWhenProperty = null,
    string groupName = "",
    int order = 1000)
    : DynamicFormFieldAttribute(displayName, hintText, visibleWhenProperty, editableWhenProperty, groupName, order)
{
    public override DynamicFormFieldType FieldType => DynamicFormFieldType.NumericUpDown;

    public double Increment { get; } = increment;

    public double MinValue { get; } = minValue;

    public double MaxValue { get; } = maxValue;
}