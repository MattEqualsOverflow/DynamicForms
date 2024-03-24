using System.ComponentModel;
using System.Runtime.CompilerServices;
using DynamicForms.Core;

namespace DynamicForms.Example.Shared;

[DynamicFormGroup]
public class ExampleSettings : INotifyPropertyChanged
{
    private string _option1 = "asdf1";

    [DynamicFormField("Option 1", DynamicFormFieldType.TextBox)]
    public string Option1
    {
        get => _option1;
        set => SetField(ref _option1, value);
    }

    [DynamicFormField("Option 2", DynamicFormFieldType.Text, VisibleWhenProperty = nameof(TestCheckbox))]
    public string Option2 { get; set; } = "asdf2";
    
    private bool _testCheckbox = true;

    [DynamicFormField("Test Checkbox", DynamicFormFieldType.CheckBox, EditableWhenProperty = nameof(IsEditable))]
    public bool TestCheckbox
    {
        get => _testCheckbox;
        set => SetField(ref _testCheckbox, value);
    }

    private string _comboBoxItem = "Item 1";

    [DynamicFormField("Test ComboBox", DynamicFormFieldType.ComboBox, ComboBoxOptionsProperty = nameof(ComboBoxOptions))]
    public string ComboBoxItem
    {
        get => _comboBoxItem;
        set => SetField(ref _comboBoxItem, value);
    }

    private ExampleEnum _exampleEnum;

    [DynamicFormField("Test Enum ComboBox", DynamicFormFieldType.Enum)]
    public ExampleEnum ExampleEnum
    {
        get => _exampleEnum;
        set => SetField(ref _exampleEnum, value);
    }

    public string[] ComboBoxOptions => ["Item 1", "Item 2", "Item 3"];
    
    public bool IsEditable { get; set; } = true;
    
    public bool IsVisible { get; set; } = true;
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}