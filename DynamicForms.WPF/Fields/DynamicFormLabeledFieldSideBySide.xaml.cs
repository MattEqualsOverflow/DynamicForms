using System.Windows.Controls;
using DynamicForms.Core;

namespace DynamicForms.WPF.Fields;

public partial class DynamicFormLabeledFieldSideBySide : DynamicFormLabeledField
{
    public DynamicFormLabeledFieldSideBySide(DynamicFormField formField) : base(formField)
    {
        InitializeComponent();
        
        MainLabel.Content = formField.Attributes.DisplayName;
        MainContent.Content = BodyControl;
    }
    
}