using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DynamicForms.Core;

namespace DynamicForms.Avalonia;

public partial class DynamicFormGroupTypeControlVertical : DynamicFormGroupTypeControl
{
    private StackPanel _mainPanel;
    
    public DynamicFormGroupTypeControlVertical()
    {
        InitializeComponent();
        _mainPanel = this.Find<StackPanel>(nameof(MainPanel))!;
    }

    public override void AddField(DynamicFormField field)
    {
        AddControl(new Fields.DynamicFormLabeledFieldVertical(field));
    }

    public override void AddControl(Control control)
    {
        _mainPanel.Children.Add(control);
    }
}