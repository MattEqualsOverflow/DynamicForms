using System.Windows;
using System.Windows.Controls;
using DynamicForms.Core;

namespace DynamicForms.WPF;

public partial class DynamicFormControl : UserControl
{
    private bool _loaded;
    
    public DynamicFormControl()
    {
        InitializeComponent();
    }
    
    public static readonly DependencyProperty DataProperty 
        = DependencyProperty.Register( 
            nameof(Data), 
            typeof( object ), 
            typeof( DynamicFormControl ), 
            new PropertyMetadata( false ) 
        );
    
    //public static readonly DependencyProperty OverWidthProperty =
    //    DependencyProperty.RegisterAttached("OverWidth", typeof(double), typeof(Extensions), new PropertyMetadata(default(double)));

    public object? Data
    {
        get => GetValue(DataProperty);
        set
        {
            SetValue(DataProperty, value);
            LoadDataObject();
        }
    }

    private void LoadDataObject()
    {
        if (Data == null)
        {
            return;
        }
        var dynamicForm = new DynamicForm(Data);

        var panel = ParentPanel;

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
        
        panel.Children.Add(verticalPanel);
    }

    private void DynamicFormControl_OnLoaded(object sender, RoutedEventArgs e)
    {
        if (_loaded)
        {
            return;
        }
        
        LoadDataObject();
        _loaded = true;
    }
}