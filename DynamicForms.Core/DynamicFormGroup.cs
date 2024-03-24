using System.Reflection;

namespace DynamicForms.Core;

public class DynamicFormGroup : DynamicFormObject
{
    public override bool IsGroup => true;

    public DynamicFormGroup(object value, DynamicFormGroupAttribute attributes, PropertyInfo? propertyInfo)
    {
        Value = value;
        Attributes = attributes;
        Property = propertyInfo;
        
        foreach (var prop in value.GetType().GetProperties())
        {
            var propValue = prop.GetValue(value);
            var customAttributes = prop.GetCustomAttributes(true);
            foreach (var customAttribute in customAttributes)
            {
                if (customAttribute is DynamicFormFieldAttribute fieldAttribute)
                {
                    Objects.Add(new DynamicFormField(value, propValue, prop, fieldAttribute));
                    break;
                }
                else if (customAttribute is DynamicFormGroupAttribute groupAttribute)
                {
                    if (propValue == null)
                    {
                        throw new InvalidOperationException("DynamicFormGroups cannot be null");
                    }
                    
                    Objects.Add(new DynamicFormGroup(propValue, groupAttribute, prop));
                    break;
                }
            }
        }
    }

    public List<DynamicFormObject> Objects { get; set; } = [];
    public object Value { get; init; }
    public PropertyInfo? Property { get; init; }
    public DynamicFormGroupAttribute Attributes { get; init; }
    
}