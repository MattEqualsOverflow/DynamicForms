namespace DynamicForms.Core.FieldAttributes;

[AttributeUsage((AttributeTargets.Property))]
public class DynamicFormSliderAttribute(
    string displayName,
    double minimumValue,
    double maximumValue,
    int decimalPlaces = 1,
    double incrementAmount = -1,
    bool isPercent = false,
    string? hintText = null,
    string? visibleWhenProperty = null,
    string? editableWhenProperty = null,
    string groupName = "",
    int order = 1000)
    : DynamicFormFieldAttribute(displayName, hintText, visibleWhenProperty, editableWhenProperty, groupName, order)
{
    public override DynamicFormFieldType FieldType => DynamicFormFieldType.Slider;

    public double MinimumValue { get; } = minimumValue;
    public double MaximumValue { get; } = maximumValue;
    public double IncrementAmount { get; } = incrementAmount;
    public int DecimalPlaces { get; } = decimalPlaces;
    public bool IsPercent { get; } = isPercent;

}