using DynamicForms.Core;

namespace DynamicForms.Example.Shared;

[DynamicFormGroup(DynamicFormGroupStyle.GroupBox, DynamicFormGroupType.TwoColumns, "Group 1 Group Box")]
public class ExampleSubObject
{
    [DynamicFormCheckBox("Checkbox Box Three", order: 3)]
    public bool TestCheckbox1 { get; set; }
    
    [DynamicFormCheckBox("Checkbox Box One", order: 1)]
    public bool TestCheckbox2 { get; set; }
    
    [DynamicFormCheckBox("Checkbox Box Two", order: 2)]
    public bool TestCheckbox3 { get; set; }
}