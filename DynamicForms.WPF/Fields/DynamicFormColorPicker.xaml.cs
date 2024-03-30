﻿using System.Windows.Controls;
using System.Windows.Media;
using DynamicForms.Core;

namespace DynamicForms.WPF.Fields;

public partial class DynamicFormColorPicker : UserControl
{
    public DynamicFormColorPicker(byte[] bytes)
    {
        InitializeComponent();
        SetValue(bytes);
    }

    public byte[] Value { get; private set; } = null!;
    
    public event EventHandler? ValueChanged;

    public void SetValue(byte[] bytes)
    {
        ColorTextBox.Text = StringColorConverter.Convert(bytes);
        ColorRectangle.Background = new SolidColorBrush(Color.FromArgb(bytes[0], bytes[1], bytes[2], bytes[3]));
        Value = bytes;
    }
    
    private void ColorTextBox_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        var bytes = StringColorConverter.Convert(ColorTextBox.Text ?? "#00000000");
        ColorRectangle.Background = new SolidColorBrush(Color.FromArgb(bytes[0], bytes[1], bytes[2], bytes[3]));
        Value = bytes;
        ValueChanged?.Invoke(this, EventArgs.Empty);
    }
}