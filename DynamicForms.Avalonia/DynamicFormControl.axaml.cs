using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using AvaloniaControls.Controls;
using DynamicForms.Avalonia.Groups;
using DynamicForms.Core;

namespace DynamicForms.Avalonia;

public partial class DynamicFormControl : UserControl
{
    private bool _loaded;
    
    public DynamicFormControl()
    {
        InitializeComponent();
    }
    
    public static readonly StyledProperty<object?> DataProperty = AvaloniaProperty.Register<DynamicFormControl, object?>(
        nameof(Data));

    public object? Data
    {
        get => GetValue(DataProperty);
        set
        {
            SetValue(DataProperty, value);
            LoadDataObject();
        }
    }

    private void Control_OnLoaded(object? sender, RoutedEventArgs e)
    {
        if (_loaded)
        {
            return;
        }
        
        LoadDataObject();
        _loaded = true;
    }

    private void LoadDataObject()
    {
        if (Data == null)
        {
            return;
        }
        var dynamicForm = new DynamicForm(Data);

        var dockPanel = this.Find<DockPanel>(nameof(ParentPanel))!;

        var mainGroupControl = CreateFormGroup(dynamicForm.ParentGroup.GroupName, dynamicForm.ParentGroup.Style,
            dynamicForm.ParentGroup.Type,
            dynamicForm.ParentGroup.Objects);

        dockPanel.Children.Add(mainGroupControl);
    }

    private Control CreateFormGroup(string groupName, DynamicFormGroupStyle style, DynamicFormGroupType type, List<DynamicFormObject> groupObjects)
    {
        DynamicFormGroupStyleControl groupStyleControl = style switch
        {
            DynamicFormGroupStyle.Basic => new DynamicFormGroupStyleBasic(),
            DynamicFormGroupStyle.GroupBox => new DynamicFormGroupStyleGroupBox(groupName),
            DynamicFormGroupStyle.Expander => new DynamicFormGroupStyleExpander(groupName),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
            
        DynamicFormGroupTypeControl groupTypeControl = type switch
        {
            DynamicFormGroupType.Vertical => new DynamicFormGroupTypeControlVertical(),
            DynamicFormGroupType.TwoColumns => new DynamicFormGroupTypeControlTwoColumn(),
            DynamicFormGroupType.SideBySide => new DynamicFormGroupTypeControlSideBySide(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

        foreach (var formObject in groupObjects)
        {
            if (formObject is DynamicFormField field)
            {
                groupTypeControl.AddField(field);
            }
            else if (formObject is DynamicFormGroup group)
            {
                var subGroupControl = CreateFormGroup(group.GroupName, group.Style, group.Type, group.Objects);
                groupTypeControl.AddControl(subGroupControl);
            }
            else
            {
                throw new InvalidOperationException($"Unknown object type {formObject.GetType().Name}");
            }
        }
        
        groupStyleControl.AddBody(groupTypeControl);

        return groupStyleControl;
    }
}