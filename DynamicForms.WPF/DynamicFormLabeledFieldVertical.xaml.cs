using System.Windows;
using DynamicForms.Core;

namespace DynamicForms.WPF;

public partial class DynamicFormLabeledFieldVertical : DynamicFormLabeledField
{
    public DynamicFormLabeledFieldVertical(DynamicFormField formField) : base(formField)
    {
        InitializeComponent();
        if (formField.Attributes.FieldType == DynamicFormFieldType.CheckBox)
        {
            MainLabel.Visibility = Visibility.Collapsed;
        }
        else
        {
            MainLabel.Text = formField.Attributes.DisplayName;
        }

        StackPanel.Children.Add(BodyControl);
    }
}