using Avalonia.Controls;
using AvaloniaControls.Controls;
using DynamicForms.Core;

namespace DynamicForms.Avalonia.Fields;

public partial class DynamicFormLabeledFieldSideBySide : DynamicFormLabeledField
{
    public DynamicFormLabeledFieldSideBySide(DynamicFormField formField) : base(formField)
    {
        InitializeComponent();

        var labeledControl = this.Find<LabeledControl>(nameof(MainControl))!;
        labeledControl.Text = formField.Attributes.DisplayName;
        labeledControl.Content = BodyControl;
    }
}