using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DynamicForms.Core;

namespace DynamicForms.Avalonia;

public partial class DynamicFormTextBlock : UserControl
{
    public DynamicFormTextBlock(DynamicFormField formField)
    {
        InitializeComponent();
        
        var control = this.Find<TextBlock>(nameof(MainControl))!;
        control.Text = formField.Value as string ?? "";
        
        if (formField.ParentObject is INotifyPropertyChanged notifyPropertyChanged)
        {
            notifyPropertyChanged.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != formField.Property.Name)
                {
                    return;
                }

                control.Text = formField.Property.GetValue(formField.ParentObject) as string ?? "";
            };
        }
    }
}