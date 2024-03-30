using System.Windows.Controls;

namespace DynamicForms.WPF.Groups;

public abstract class DynamicFormGroupStyleControl : UserControl
{
    public abstract void AddBody(DynamicFormGroupTypeControl typeControl);
}