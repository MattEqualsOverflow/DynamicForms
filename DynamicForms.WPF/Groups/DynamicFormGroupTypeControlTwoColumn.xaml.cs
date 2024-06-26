﻿using System.Windows;
using System.Windows.Controls;
using DynamicForms.Core;

namespace DynamicForms.WPF.Groups;

public partial class DynamicFormGroupTypeControlTwoColumn : DynamicFormGroupTypeControl
{
    public DynamicFormGroupTypeControlTwoColumn()
    {
        InitializeComponent();
    }
    
    public override void AddField(DynamicFormField field)
    {
        AddControl(new DynamicFormLabeledFieldVertical(field));
    }

    public override void AddControl(Control control)
    {
        MainPanel.Children.Add(control);
        var count = MainPanel.Children.Count - 1;
        Grid.SetColumn(control, count % 2);
        Grid.SetRow(control, count / 2);

        if (MainPanel.RowDefinitions.Count < count / 2 + 1)
        {
            MainPanel.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
        }
    }
}