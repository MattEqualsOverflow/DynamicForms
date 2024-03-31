using System.Reflection;
using DynamicForms.Core.FieldAttributes;

namespace DynamicForms.Core;

public class DynamicFormGroup : DynamicFormObject
{
    private string _parentGroupName;

    public DynamicFormGroup(object value, string parentGroupName)
    {
        var attribute = value.GetType().GetCustomAttributes(true).FirstOrDefault(x => x is DynamicFormAttribute) as DynamicFormAttribute 
                        ?? new DynamicFormAttribute(DynamicFormGroupStyle.Basic, DynamicFormGroupType.Vertical);
        Value = value;
        GroupName = attribute.Name;
        _parentGroupName = parentGroupName;
        Style = attribute.Style;
        Type = attribute.Type;
        
        AddFormObjects(value);
    }
    
    public DynamicFormGroup(object value, string groupName, string parentGroupName, DynamicFormGroupStyle style, DynamicFormGroupType type)
    {
        Value = value;
        GroupName = groupName;
        _parentGroupName = parentGroupName;
        Style = style;
        Type = type;
    }
    
    public string GroupName { get; init; }
    public DynamicFormGroupStyle Style { get; init; }
    public DynamicFormGroupType Type { get; init; }
    public override bool IsGroup => true;
    public override string ParentGroupName => _parentGroupName;

    private void AddFormObjects(object value)
    {
        var subGroupAttributes = value.GetType().GetCustomAttributes()
            .Where(x => x is DynamicFormGroupAttribute)
            .Cast<DynamicFormGroupAttribute>()
            .OrderBy(x => x.Order)
            .ToList();

        var subGroups = subGroupAttributes
            .Where(x => x.ParentGroup == null)
            .Select(x => new DynamicFormGroup(value, x.Name, GroupName, x.Style, x.Type))
            .ToList();

        var subGroupMap = subGroups.ToDictionary(x => x.GroupName, x => x);

        foreach (var childSubgroupAttribute in subGroupAttributes
                     .Where(x => x.ParentGroup != null))
        {
            var parentSubgroup = subGroups.FirstOrDefault(x => x.GroupName == childSubgroupAttribute.ParentGroup) ??
                                 subGroups.First();
            var childSubgroup = new DynamicFormGroup(value, childSubgroupAttribute.Name, parentSubgroup.GroupName,
                childSubgroupAttribute.Style, childSubgroupAttribute.Type);
            parentSubgroup.Objects.Add(childSubgroup);
            subGroupMap.Add(childSubgroupAttribute.Name, childSubgroup); 
        }

        var hasSubGroups = subGroups.Count != 0;
        Objects.AddRange(subGroups);
        
        foreach (var childObject in GetObjectsFrom(value))
        {
            if (hasSubGroups)
            {
                subGroupMap.TryGetValue(childObject.ParentGroupName, out var parentGroup);
                parentGroup ??= subGroups.First();
                parentGroup.Objects.Add(childObject);
            }
            else
            {
                Objects.Add(childObject);
            }
        }
    }
    
    private List<DynamicFormObject> GetObjectsFrom(object parentObject)
    {
        List<DynamicFormObject> toReturn = [];

        var properties = parentObject.GetType().GetProperties()
            .Select(x => (IsProperty: true,
                Property: x as object,
                Attributes: x.GetCustomAttributes(true).FirstOrDefault(a => a is DynamicFormObjectAttribute)))
            .Where(x => x.Attributes != null);
        
        var events = parentObject.GetType().GetEvents()
            .Select(x => (IsProperty: false,
                Property: x as object,
                Attributes: x.GetCustomAttributes(true).FirstOrDefault(a => a is DynamicFormObjectAttribute)))
            .Where(x => x.Attributes != null);

        var items = properties.Concat(events).OrderBy(x => ((DynamicFormObjectAttribute)x.Attributes!).Order);
        
        foreach (var item in items)
        {
            if (item.IsProperty)
            {
                var property = (PropertyInfo)item.Property;
                var propValue = property.GetValue(parentObject);
                if (item.Attributes is DynamicFormFieldAttribute fieldAttribute)
                {
                    toReturn.Add(new DynamicFormField(parentObject, propValue, property, fieldAttribute, fieldAttribute.GroupName));
                } 
                else if (item.Attributes is DynamicFormObjectAttribute subObjectAttribute)
                {
                    if (propValue == null)
                    {
                        throw new InvalidOperationException("DynamicFormGroups cannot be null");
                    }
                    
                    toReturn.Add(new DynamicFormGroup(propValue, subObjectAttribute.GroupName));
                }
            }
            else
            {
                var eventInfo = (EventInfo)item.Property;
                var attribute = (DynamicFormButtonAttribute)item.Attributes!;
                toReturn.Add(new DynamicFormField(parentObject, eventInfo.Name, null, attribute, attribute.GroupName));
            }
        }

        return toReturn;
    }

    public List<DynamicFormObject> Objects { get; set; } = [];
    public object? Value { get; init; }
    
}