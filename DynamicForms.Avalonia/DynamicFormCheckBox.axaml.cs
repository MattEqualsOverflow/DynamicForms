using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DynamicForms.Core;

namespace DynamicForms.Avalonia;

public partial class DynamicFormCheckBox : UserControl
{
    public DynamicFormCheckBox(DynamicFormField formField)
    {
        InitializeComponent();
        
        var control = this.Find<CheckBox>(nameof(MainControl))!;
        control.Content = formField.Attributes.DisplayName;
        control.IsChecked = (bool?)formField.Value == true;
        
        control.IsCheckedChanged += (sender, args) =>
        {
            formField.Property.SetValue(formField.ParentObject, control.IsChecked);
        };
        
        if (formField.ParentObject is INotifyPropertyChanged notifyPropertyChanged)
        {
            notifyPropertyChanged.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != formField.Property.Name)
                {
                    return;
                }

                control.IsChecked = (bool?)formField.Value == true;
            };
        }
    }
}