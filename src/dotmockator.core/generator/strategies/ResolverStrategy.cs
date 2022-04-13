using dotmockator.core.definitions;

namespace dotmockator.core.generator.strategies;

public class ResolverStrategy
{
    public static void HandleResolver<T>(T candidate, DefinitionField definitionField)
    {
        if (!definitionField.IsResolver)
            return;

        object? resolvedValue = null;

        if (definitionField.ResolverType != null)
        {
            var resolver = (IDynamicFieldResolver) Activator.CreateInstance(definitionField.ResolverType)!;
            resolvedValue = resolver.ResolveValue();
        }
        else if (definitionField.StaticFunc != null)
        {
            resolvedValue = definitionField.StaticFunc.Invoke();
        }

        definitionField.PropertyInfo.SetValue(candidate, resolvedValue);
    }
}