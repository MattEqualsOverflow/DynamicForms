using DynamicForms.Core;
using DynamicForms.WPF.Fields;

namespace DynamicForms.WPF.Groups;

public class DynamicFormGroupTypeControlSideBySide : DynamicFormGroupTypeControlVertical
{
    public override void AddField(DynamicFormField field)
    {
        AddControl(new DynamicFormLabeledFieldSideBySide(field));
    }
}