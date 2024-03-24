using Avalonia.Controls;
using DynamicForms.Core;

namespace DynamicForms.Avalonia;

public partial class DynamicFormLabeledFieldVertical : DynamicFormLabeledField
{
    public DynamicFormLabeledFieldVertical(DynamicFormField formField) : base(formField)
    {
        InitializeComponent();
        if (formField.Attributes.FieldType == DynamicFormFieldType.CheckBox)
        {
            this.Find<TextBlock>(nameof(MainLabel))!.IsVisible = false;
        }
        else
        {
            this.Find<TextBlock>(nameof(MainLabel))!.Text = formField.Attributes.DisplayName;
        }
        this.Find<StackPanel>(nameof(StackPanel))!.Children.Add(BodyControl);
    }
}