using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using DynamicForms.Core;

namespace DynamicForms.Avalonia;

public partial class DynamicFormControl : UserControl
{
    private bool _loaded;
    
    public DynamicFormControl()
    {
        InitializeComponent();
        var data = Data;
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

        var verticalPanel = new StackPanel()
        {
            Orientation = Orientation.Vertical
        };

        foreach (var field in dynamicForm.ParentGroup.Objects.Cast<DynamicFormField>())
        {
            //verticalPanel.Children.Add(new Label() { Content = field.Attributes.DisplayName} );
            //verticalPanel.Children.Add(new TextBox() { Text = field.Attributes.DisplayName} );
            verticalPanel.Children.Add(new DynamicFormLabeledFieldVertical(field));
        }
        
        dockPanel.Children.Add(verticalPanel);
    }
}