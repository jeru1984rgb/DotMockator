namespace dotmockator.core.definitions;

[AttributeUsage(AttributeTargets.Property)]
public class MockatorFieldAttribute : System.Attribute
{
    public FieldFunctionEnum FieldFunction;

    public MockatorFieldAttribute(FieldFunctionEnum fieldFunction)
    {
        FieldFunction = fieldFunction;
    }
}