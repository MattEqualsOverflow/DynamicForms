using System.Reflection;

namespace DynamicForms.Core;

public class DynamicFormField : DynamicFormObject
{
    public override bool IsGroup => false;

    public DynamicFormField(object parent, object? value, PropertyInfo property, DynamicFormFieldAttribute attribute)
    {
        ParentObject = parent;
        Value = value;
        Property = property;
        Attributes = attribute;
    }
    
    public object ParentObject { get; set; }
    public object? Value { get; set; }
    public PropertyInfo Property { get; set; }
    public DynamicFormFieldAttribute Attributes { get; set; }
}