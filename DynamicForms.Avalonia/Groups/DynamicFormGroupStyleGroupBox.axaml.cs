using Avalonia.Controls;
using AvaloniaControls.Controls;
using DynamicForms.Core;

namespace DynamicForms.Avalonia.Groups;

public partial class DynamicFormGroupStyleGroupBox : DynamicFormGroupStyleControl
{
    private CardControl _cardControl;
    
    public DynamicFormGroupStyleGroupBox(string groupName)
    {
        InitializeComponent();
        _cardControl = this.Find<CardControl>(nameof(MainPanel))!;
        _cardControl.HeaderText = groupName;
    }

    public override void AddBody(DynamicFormGroupTypeControl typeControl)
    {
        _cardControl.Content = typeControl;
    }
}