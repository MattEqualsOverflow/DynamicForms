using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using DynamicForms.Core;

namespace DynamicForms.WPF;

public class DynamicFormLabeledField : UserControl
{
    protected FrameworkElement BodyControl { get; private set; }
    
    public DynamicFormLabeledField(DynamicFormField formField)
    {
        switch (formField.Attributes.FieldType)
        {
            case DynamicFormFieldType.TextBox:
                BodyControl = GetTextBox(formField);
                break;
            case DynamicFormFieldType.Text:
                BodyControl = GetTextBlock(formField);
                break;
            case DynamicFormFieldType.CheckBox:
                BodyControl = GetCheckBox(formField);
                break;
            case DynamicFormFieldType.ComboBox:
                BodyControl = GetComboBox(formField);
                break;
            case DynamicFormFieldType.Enum:
                BodyControl = GetEnumComboBox(formField);
                break;
        }
        
        if (BodyControl != null && !string.IsNullOrEmpty(formField.Attributes.VisibleWhenProperty))
        {
            var property = formField.ParentObject.GetType().GetProperty(formField.Attributes.VisibleWhenProperty);
            Visibility = (bool?)property?.GetValue(formField.ParentObject) != false ? Visibility.Visible : Visibility.Collapsed;

            if (formField.ParentObject is INotifyPropertyChanged notifyParent)
            {
                notifyParent.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName != formField.Attributes.VisibleWhenProperty)
                    {
                        return;
                    }
                    Visibility = (bool?)property?.GetValue(formField.ParentObject) != false ? Visibility.Visible : Visibility.Collapsed;
                };
            }
        }
        
        if (BodyControl != null && !string.IsNullOrEmpty(formField.Attributes.EditableWhenProperty))
        {
            var property = formField.ParentObject.GetType().GetProperty(formField.Attributes.EditableWhenProperty);
            BodyControl.IsEnabled = (bool?)property?.GetValue(formField.ParentObject) != false;
            
            if (formField.ParentObject is INotifyPropertyChanged notifyParent)
            {
                notifyParent.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName != formField.Attributes.EditableWhenProperty)
                    {
                        return;
                    }
                    BodyControl.IsEnabled = (bool?)property?.GetValue(formField.ParentObject) != false;
                };
            }
        }
    }
    
    private TextBox GetTextBox(DynamicFormField formField)
    {
        var control = new TextBox() { Text = formField.Value as string ?? "" };
        
        control.TextChanged += (sender, args) =>
        {
            formField.Property.SetValue(formField.ParentObject, control.Text);
        };
        
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

        return control;
    }
    
    private TextBlock GetTextBlock(DynamicFormField formField)
    {
        var control = new TextBlock() { Text = formField.Value as string };
        
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

        return control;
    }
    
    private CheckBox GetCheckBox(DynamicFormField formField)
    {
        var control = new CheckBox() { IsChecked = formField.Value as bool?, Content = formField.Attributes.DisplayName};
        
        control.Checked += (sender, args) =>
        {
            formField.Property.SetValue(formField.ParentObject, control.IsChecked);
        };
        
        control.Unchecked += (sender, args) =>
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

                control.IsChecked = formField.Property.GetValue(formField.ParentObject) as bool?;
            };
        }

        return control;
    }
    
    private ComboBox GetComboBox(DynamicFormField formField)
    {

        var optionsProperty = formField.ParentObject.GetType().GetProperties()
            .FirstOrDefault(x => x.Name == formField.Attributes.ComboBoxOptionsProperty);

        if (optionsProperty == null)
        {
            throw new InvalidOperationException(
                "ComboBox specified without a ComboBoxOptionsProperty being designated");
        }

        var options = optionsProperty.GetValue(formField.ParentObject) as ICollection<string>;

        if (options == null)
        {
            throw new InvalidOperationException("ComboBoxOptionsProperty must be a collection of strings");
        }
            
        var control = new ComboBox() { ItemsSource = options, SelectedItem = formField.Value };
        
        control.SelectionChanged += (sender, args) =>
        {
            formField.Property.SetValue(formField.ParentObject, control.SelectedItem);
        };
        
        if (formField.ParentObject is INotifyPropertyChanged notifyPropertyChanged)
        {
            notifyPropertyChanged.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != formField.Property.Name)
                {
                    return;
                }

                control.SelectedItem = formField.Property.GetValue(formField.ParentObject) as string;
            };
        }

        return control;
    }
    
    private ComboBox GetEnumComboBox(DynamicFormField formField)
    {
        if (formField.Value == null)
        {
            throw new InvalidOperationException();
        }

        Dictionary<string, Enum> descriptionToEnums = new();
        var selectedItem = "";
        
        foreach (var enumValue in Enum.GetValues(formField.Value.GetType()).Cast<Enum>())
        {
            descriptionToEnums.Add(enumValue.GetDescription(), enumValue);

            if (Equals(enumValue, formField.Value))
            {
                selectedItem = enumValue.GetDescription();
            }
        }
        
        var control = new ComboBox() { ItemsSource = descriptionToEnums.Keys, SelectedItem = string.IsNullOrEmpty(selectedItem) ? descriptionToEnums.Keys.First() : selectedItem };
        
        control.SelectionChanged += (sender, args) =>
        {
            
            formField.Property.SetValue(formField.ParentObject, descriptionToEnums[(string)control.SelectedItem]);
        };
        
        if (formField.ParentObject is INotifyPropertyChanged notifyPropertyChanged)
        {
            notifyPropertyChanged.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != formField.Property.Name)
                {
                    return;
                }

                control.SelectedItem = ((Enum)formField.Property.GetValue(formField.ParentObject)!).GetDescription();
            };
        }

        return control;
    }
}