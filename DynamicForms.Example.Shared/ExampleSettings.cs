using System.ComponentModel;
using System.Runtime.CompilerServices;
using DynamicForms.Core;
using DynamicForms.Core.FieldAttributes;

namespace DynamicForms.Example.Shared;

[DynamicFormGroup(DynamicFormGroupStyle.GroupBox, DynamicFormGroupType.SideBySide, "Parent Group", order: 0)]
[DynamicFormGroup(DynamicFormGroupStyle.Basic, DynamicFormGroupType.Vertical, "Group 1")]
[DynamicFormGroup(DynamicFormGroupStyle.Expander, DynamicFormGroupType.Vertical, "Group 2", order: 1)]
[DynamicFormGroup(DynamicFormGroupStyle.GroupBox, DynamicFormGroupType.Vertical, "Group 2 Subgroup", "Group 2")]
public class ExampleSettings : INotifyPropertyChanged
{
    private string _option1 = "asdf1";

    [DynamicFormTextBox("Option 1")]
    public string Option1
    {
        get => _option1;
        set => SetField(ref _option1, value);
    }

    [DynamicFormText("Option 2", visibleWhenProperty: nameof(TestCheckbox))]
    public string Option2 { get; set; } = "asdf2";
    
    private bool _testCheckbox = true;

    [DynamicFormCheckBox("Test Checkbox", editableWhenProperty: nameof(IsEditable))]
    public bool TestCheckbox
    {
        get => _testCheckbox;
        set => SetField(ref _testCheckbox, value);
    }

    private string _comboBoxItem = "Item 1";

    [DynamicFormComboBox("Test ComboBox", comboBoxOptionsProperty: nameof(ComboBoxOptions))]
    public string ComboBoxItem
    {
        get => _comboBoxItem;
        set => SetField(ref _comboBoxItem, value);
    }

    private ExampleEnum _exampleEnum;

    [DynamicFormComboBox("Test Enum ComboBox")]
    public ExampleEnum ExampleEnum
    {
        get => _exampleEnum;
        set => SetField(ref _exampleEnum, value);
    }

    public string[] ComboBoxOptions => ["Item 1", "Item 2", "Item 3"];

    [DynamicFormSlider("Test Int Slider", 1, 10)]
    public int TestSlider1 { get; set; } = 1;
    
    [DynamicFormSlider("Test Double Slider", 0, 100, decimalPlaces: 2, incrementAmount: 0.25, isPercent: true)]
    public double TestSlider2 { get; set; } = 1;

    [DynamicFormColorPicker("Test Color Picker")]
    public byte[] ColorPicker { get; set; } = [0xFF, 0x21, 0x21, 0x21];
    
    [DynamicFormFilePicker("Test File Picker", FilePickerType.OpenFile, "SNES ROMs:*.sfc,*.smc)", "21f3e98df4780ee1c667b84e57d88675", "Bad SM File!")]
    public string TestFilePicker { get; set; } = "";
    
    [DynamicFormFilePicker("Test Folder Picker", FilePickerType.Folder)]
    public string TestFolderPicker { get; set; } = "";
    
    [DynamicFormNumeric("Int NumericUpDown", groupName: "Group 2 Subgroup")]
    public int IntNumericUpDown { get; set; }
    
    [DynamicFormNumeric("Double NumericUpDown", increment: 0.1, groupName: "Group 2 Subgroup")]
    public double DoubleNumericUpDown { get; set; }
    
    [DynamicFormNumeric("Decimal NumericUpDown", increment: 0.25, groupName: "Group 2 Subgroup")]
    public decimal DecimalNumericUpDown { get; set; }

    [DynamicFormObject("Group 1")]
    public ExampleSubObject SubObject { get; set; } = new();
    
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