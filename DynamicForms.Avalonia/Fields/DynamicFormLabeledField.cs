using System.ComponentModel;
using Avalonia.Controls;
using AvaloniaControls;
using AvaloniaControls.Controls;
using DynamicForms.Core;
using DynamicForms.Core.FieldAttributes;

namespace DynamicForms.Avalonia.Fields;

public class DynamicFormLabeledField : UserControl
{
    protected Control BodyControl;

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
            case DynamicFormFieldType.Slider:
                BodyControl = GetSlider(formField);
                break;
            case DynamicFormFieldType.ColorPicker:
                BodyControl = GetColorPicker(formField);
                break;
            case DynamicFormFieldType.FilePicker:
                BodyControl = GetFilePicker(formField);
                break;
            case DynamicFormFieldType.NumericUpDown:
                BodyControl = GetNumericUpDown(formField);
                break;
            default:
                throw new InvalidOperationException("Unknown DynamicFormFieldType");
        }

        if (!string.IsNullOrEmpty(formField.Attributes.VisibleWhenProperty))
        {
            var property = formField.ParentObject.GetType().GetProperty(formField.Attributes.VisibleWhenProperty);
            IsVisible = (bool?)property?.GetValue(formField.ParentObject) != false;

            if (formField.ParentObject is INotifyPropertyChanged notifyParent)
            {
                notifyParent.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName != formField.Attributes.VisibleWhenProperty)
                    {
                        return;
                    }
                    IsVisible = (bool?)property?.GetValue(formField.ParentObject) != false;
                };
            }
        }
        
        if (!string.IsNullOrEmpty(formField.Attributes.EditableWhenProperty))
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
        var control = new TextBox() { Text = formField.Value?.ToString() ?? "" };

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

                control.IsChecked = formField.Property.GetValue(formField.ParentObject) as bool?;
            };
        }

        return control;
    }
    
    private Control GetComboBox(DynamicFormField formField)
    {
        if (formField.Value is Enum)
        {
            var control = new EnumComboBox() { EnumType = formField.Value?.GetType(), Value = formField.Value };
        
            control.ValueChanged += (sender, args) =>
            {
                formField.Property.SetValue(formField.ParentObject, control.Value);
            };
        
            if (formField.ParentObject is INotifyPropertyChanged notifyPropertyChanged)
            {
                notifyPropertyChanged.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName != formField.Property.Name)
                    {
                        return;
                    }

                    control.Value = formField.Property.GetValue(formField.ParentObject);
                };
            }
        
            return control;
        }
        else if (formField.Attributes is DynamicFormComboBoxAttribute attributes)
        {
            var optionsProperty = formField.ParentObject.GetType().GetProperties()
                .FirstOrDefault(x => x.Name == attributes.ComboBoxOptionsProperty);

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
        else
        {
            throw new InvalidOperationException("Invalid DynamicFormField object for ComboBox");
        }
    }

    private Control GetSlider(DynamicFormField formField)
    {
        if (formField.Attributes is not DynamicFormSliderAttribute attributes)
        {
            throw new InvalidOperationException("Invalid DynamicFormField object for Slider");
        }
        
        var decimalPlaces = attributes.DecimalPlaces;
        if (formField.Value?.GetType() == typeof(int) || formField.Value?.GetType() == typeof(int?) || formField.Value?.GetType() == typeof(short) || formField.Value?.GetType() == typeof(short?))
        {
            decimalPlaces = 0;
        }
        
        var incrementAmount = attributes.IncrementAmount;
        if (Math.Abs(incrementAmount + 1) < .001)
        {
            incrementAmount = Math.Pow(0.1, decimalPlaces);
        }
        
        var control = new DynamicFormSliderControl(formField.Value as double? ?? attributes.MinimumValue, attributes.MaximumValue, attributes.MinimumValue, incrementAmount, decimalPlaces, attributes.IsPercent);
        
        control.ValueChanged += (sender, args) =>
        {
            if (decimalPlaces == 0)
            {
                formField.Property.SetValue(formField.ParentObject, Convert.ToInt32(args.NewValue));
            }
            else
            {
                formField.Property.SetValue(formField.ParentObject, Convert.ToDouble(args.NewValue));
            }
        };
        
        if (formField.ParentObject is INotifyPropertyChanged notifyPropertyChanged)
        {
            notifyPropertyChanged.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != formField.Property.Name)
                {
                    return;
                }

                control.SetValue(Convert.ToDouble(formField.Property.GetValue(formField.ParentObject)));
            };
        }

        return control;
    }
    
    private Control GetColorPicker(DynamicFormField formField)
    {
        if (formField.Value is not byte[] bytes)
        {
            throw new InvalidOperationException("Invalid value type for ColorPicker");
        }
        
        var control = new DynamicFormColorPicker(bytes);
        
        control.ValueChanged += (sender, args) =>
        {
            formField.Property.SetValue(formField.ParentObject, control.Value);
        };

        if (formField.ParentObject is INotifyPropertyChanged notifyPropertyChanged)
        {
            notifyPropertyChanged.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != formField.Property.Name)
                {
                    return;
                }

                control.SetValue(formField.Property.GetValue(formField.ParentObject) as byte[] ?? [0, 0, 0, 0]);
            };
        }

        return control;
    }
    
    private Control GetFilePicker(DynamicFormField formField)
    {
        if (formField.Attributes is not DynamicFormFilePickerAttribute attributes)
        {
            throw new InvalidOperationException("Invalid value type for ColorPicker");
        }

        var control = new FileControl()
        {
            FileInputType = attributes.FilePickerType switch
            {
                FilePickerType.OpenFile => FileInputControlType.OpenFile,
                FilePickerType.SaveFile => FileInputControlType.SaveFile,
                _ => FileInputControlType.Folder
            },
            Filter = attributes.Filter,
            FileValidationHash = attributes.CheckSum,
            FileValidationHashError = attributes.CheckSumError
        };
        
        control.OnUpdated += (sender, args) =>
        {
            formField.Property.SetValue(formField.ParentObject, control.FilePath);
        };

        if (formField.ParentObject is INotifyPropertyChanged notifyPropertyChanged)
        {
            notifyPropertyChanged.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != formField.Property.Name)
                {
                    return;
                }

                control.FilePath = formField.Property.GetValue(formField.ParentObject) as string ?? "";
            };
        }

        return control;
    }
    
    private Control GetNumericUpDown(DynamicFormField formField)
    {
        if (formField.Attributes is not DynamicFormNumericUpDownAttribute attributes)
        {
            throw new InvalidOperationException("Invalid attribute type for NumericUpDown control");
        }
        
        var control = new NumericUpDown()
        {
            Value = Convert.ToDecimal(formField.Value),
            Increment = Convert.ToDecimal(attributes.Increment),
            Minimum = Convert.ToDecimal(attributes.MinValue),
            Maximum = Convert.ToDecimal(attributes.MaxValue)
        };

        control.ValueChanged += (sender, args) =>
        {
            if (formField.Value is int)
            {
                formField.Property.SetValue(formField.ParentObject, Convert.ToInt16(control.Value));
            }
            else if (formField.Value is double)
            {
                formField.Property.SetValue(formField.ParentObject, Convert.ToDouble(control.Value));
            }
            else
            {
                formField.Property.SetValue(formField.ParentObject, control.Value);
            }
        };
        
        if (formField.ParentObject is INotifyPropertyChanged notifyPropertyChanged)
        {
            notifyPropertyChanged.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != formField.Property.Name)
                {
                    return;
                }

                control.Value = Convert.ToDecimal(formField.Property.GetValue(formField.ParentObject));
            };
        }

        return control;
    }
}