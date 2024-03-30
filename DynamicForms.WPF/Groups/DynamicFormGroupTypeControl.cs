using System.Windows.Controls;
using DynamicForms.Core;

namespace DynamicForms.WPF.Groups;

public abstract class DynamicFormGroupTypeControl : UserControl
{
    public abstract void AddField(DynamicFormField field);

    public abstract void AddControl(Control control);
}