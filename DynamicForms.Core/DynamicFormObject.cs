namespace DynamicForms.Core;

public abstract class DynamicFormObject
{
    public abstract bool IsGroup { get; }

    public abstract string ParentGroupName { get; }
}