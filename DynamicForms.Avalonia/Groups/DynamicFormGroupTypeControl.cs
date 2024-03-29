using Avalonia.Controls;
using DynamicForms.Core;

namespace DynamicForms.Avalonia;

public abstract class DynamicFormGroupTypeControl : UserControl
{
    public abstract void AddField(DynamicFormField field);

    public abstract void AddControl(Control control);
}