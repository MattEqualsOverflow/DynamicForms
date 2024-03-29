using DynamicForms.Core;

namespace DynamicForms.Avalonia;

public class DynamicFormGroupTypeControlSideBySide : DynamicFormGroupTypeControlVertical
{
    public override void AddField(DynamicFormField field)
    {
        AddControl(new Fields.DynamicFormLabeledFieldSideBySide(field));
    }
}