namespace DynamicForms.Core;

public class DynamicForm
{
    public DynamicForm(object parentObject)
    {
        DynamicFormGroupAttribute dynamicFormGroupAttribute = new DynamicFormGroupAttribute("");
        
        var customAttributes = parentObject.GetType().GetCustomAttributes(true);
        foreach (var customAttribute in customAttributes)
        {
            if (customAttribute is DynamicFormGroupAttribute fieldAttribute)
            {
                dynamicFormGroupAttribute = fieldAttribute;
                break;
            }
        }

        ParentGroup = new DynamicFormGroup(parentObject, dynamicFormGroupAttribute, null);
    }
    
    public DynamicFormGroup ParentGroup { get; init; }
}