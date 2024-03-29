using Avalonia.Controls;

namespace DynamicForms.Avalonia.Groups;

public abstract class DynamicFormGroupStyleControl : UserControl
{
    public abstract void AddBody(DynamicFormGroupTypeControl typeControl);
}