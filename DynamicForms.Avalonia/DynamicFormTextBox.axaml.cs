using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DynamicForms.Core;

namespace DynamicForms.Avalonia;

public partial class DynamicFormTextBox : UserControl
{
    public DynamicFormTextBox(DynamicFormField formField)
    {
        InitializeComponent();
        
        var textBox = this.Find<TextBox>(nameof(MainTextBox))!;
        textBox.Text = formField.Value as string ?? "";
        
        textBox.TextChanged += (sender, args) =>
        {
            formField.Property.SetValue(formField.ParentObject, textBox.Text);
        };
        
        if (formField.ParentObject is INotifyPropertyChanged notifyPropertyChanged)
        {
            notifyPropertyChanged.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != formField.Property.Name)
                {
                    return;
                }

                textBox.Text = formField.Property.GetValue(formField.ParentObject) as string ?? "";
            };
        }
    }
}