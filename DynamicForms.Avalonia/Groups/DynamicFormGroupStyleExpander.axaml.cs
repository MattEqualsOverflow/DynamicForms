using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaControls.Controls;
using DynamicForms.Core;

namespace DynamicForms.Avalonia.Groups;

public partial class DynamicFormGroupStyleExpander : DynamicFormGroupStyleControl
{
    private ExpanderControl _expander;
    
    public DynamicFormGroupStyleExpander(string groupName)
    {
        InitializeComponent();
        _expander = this.Find<ExpanderControl>(nameof(MainPanel))!;
        _expander.HeaderText = groupName;
    }

    public override void AddBody(DynamicFormGroupTypeControl typeControl)
    {
        _expander.Content = typeControl;
    }
}