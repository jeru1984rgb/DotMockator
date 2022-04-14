using dotmockator.core.definitions.field;

namespace dotmockator.core.generator.strategies;

public static class ResolverStrategy
{
    public static void HandleResolver<T>(T candidate, DefinitionField definitionField)
    {
        if (!definitionField.ResolverType.IsPresent && !definitionField.StaticFunc.IsPresent)
            return;

        object? resolvedValue = null;

        if (definitionField.ResolverType.IsPresent)
        {
            var resolver = (IDynamicFieldResolver) Activator.CreateInstance(definitionField.ResolverType.Value)!;
            resolvedValue = resolver.ResolveValue();
        }
        else if (definitionField.StaticFunc.IsPresent)
        {
            resolvedValue = definitionField.StaticFunc.Value.Invoke();
        }

        definitionField.PropertyInfo.Value.SetValue(candidate, resolvedValue);
    }
}