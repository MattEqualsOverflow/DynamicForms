using System.Reflection;
using DynamicForms.Core.FieldAttributes;

namespace DynamicForms.Core;

public class DynamicFormField(object parent, object? value, PropertyInfo property, DynamicFormFieldAttribute attribute, string groupName) : DynamicFormObject
{
    public override bool IsGroup => false;

    public object ParentObject => parent;
    
    public object? Value { get; set; } = value;
    public PropertyInfo Property => property;
    public DynamicFormFieldAttribute Attributes => attribute;
    public override string ParentGroupName => groupName;
}