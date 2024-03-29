using System.Globalization;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace DynamicForms.Avalonia.Fields;

public partial class DynamicFormSliderControl : UserControl
{
    private bool _isPercent;
    
    public DynamicFormSliderControl(double currentValue, double maximum, double minimum, double incrementAmount, int decimalPlaces, bool isPercent)
    {
        _isPercent = isPercent;
        
        InitializeComponent();
        
        var slider = this.Find<Slider>(nameof(ValueSlider))!;
        slider.Maximum = maximum;
        slider.Minimum = minimum;
        slider.TickFrequency = incrementAmount;
        slider.Value = currentValue;

        var textBox = this.Find<TextBlock>(nameof(ValueTextBox))!;
        textBox.Text = currentValue.ToString(CultureInfo.CurrentCulture) + (isPercent ? "%" : "");

        slider.ValueChanged += (sender, args) =>
        {
            if (decimalPlaces == 0)
            {
                var value = Convert.ToInt32(args.NewValue);
                textBox.Text = value.ToString(CultureInfo.CurrentCulture) + (isPercent ? "%" : "");
                ValueChanged?.Invoke(sender, args);
            }
            else
            {
                var value = Math.Round(args.NewValue, decimalPlaces);
                textBox.Text = value.ToString(CultureInfo.CurrentCulture) + (isPercent ? "%" : "");
                ValueChanged?.Invoke(sender, args);
            }
        };
    }

    public void SetValue(double value)
    {
        this.Find<Slider>(nameof(ValueSlider))!.Value = value;
        this.Find<TextBlock>(nameof(ValueTextBox))!.Text =
            value.ToString(CultureInfo.CurrentCulture) + (_isPercent ? "%" : "");
    }

    public event EventHandler<RangeBaseValueChangedEventArgs>? ValueChanged;
}