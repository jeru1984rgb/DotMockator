namespace dotmockator.core.definitions.field;

public class OptionalDefinitionFieldParam<T>
{
    private T? OptionalValue { get; }

    public Boolean IsPresent => OptionalValue != null;

    public T Value => OptionalValue!;
    
    public OptionalDefinitionFieldParam(T? optionalValue)
    {
        OptionalValue = optionalValue;
    }
}