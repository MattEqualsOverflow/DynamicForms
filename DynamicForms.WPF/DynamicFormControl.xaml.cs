﻿using System.Windows;
using System.Windows.Controls;
using DynamicForms.Core;
using DynamicForms.WPF.Groups;

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
    
    public object? Data
    {
        get => GetValue(DataProperty);
        set
        {
            SetValue(DataProperty, value);
            LoadDataObject();
        }
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
    
    private void LoadDataObject()
    {
        if (Data == null)
        {
            return;
        }
        var dynamicForm = new DynamicForm(Data);

        var mainGroupControl = CreateFormGroup(dynamicForm.ParentGroup.GroupName, dynamicForm.ParentGroup.Style,
            dynamicForm.ParentGroup.Type,
            dynamicForm.ParentGroup.Objects);

        ParentPanel.Children.Add(mainGroupControl);
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

        bool isFirst = true;

        foreach (var formObject in groupObjects)
        {
            if (formObject is DynamicFormField field)
            {
                groupTypeControl.AddField(field);
            }
            else if (formObject is DynamicFormGroup group)
            {
                var subGroupControl = CreateFormGroup(group.GroupName, group.Style, group.Type, group.Objects);
                if (!isFirst)
                {
                    subGroupControl.Margin = new Thickness(0, 5, 0, 0);
                }
                groupTypeControl.AddControl(subGroupControl);
            }
            else
            {
                throw new InvalidOperationException($"Unknown object type {formObject.GetType().Name}");
            }

            isFirst = false;
        }
        
        groupStyleControl.AddBody(groupTypeControl);

        return groupStyleControl;
    }
}