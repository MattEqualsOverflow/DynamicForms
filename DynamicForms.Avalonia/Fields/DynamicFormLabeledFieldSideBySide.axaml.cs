using Avalonia.Controls;
using Avalonia.Layout;
using AvaloniaControls.Controls;
using DynamicForms.Core;

namespace DynamicForms.Avalonia.Fields;

public partial class DynamicFormLabeledFieldSideBySide : DynamicFormLabeledField
{
    public DynamicFormLabeledFieldSideBySide(DynamicFormField formField) : base(formField)
    {
        InitializeComponent();
        
        var labeledControl = this.Find<LabeledControl>(nameof(MainControl))!;

        if (formField.Type is DynamicFormFieldType.Text or DynamicFormFieldType.CheckBox)
        {
            labeledControl.Text = "";
        }
        else
        {
            labeledControl.Text = formField.Attributes.DisplayName;
        }

        if (formField.Type is DynamicFormFieldType.Button)
        {
            labeledControl.Content = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Right,
                Children = { BodyControl }
            };
        }
        else
        {
            labeledControl.Content = BodyControl;
        }
        
    }
}