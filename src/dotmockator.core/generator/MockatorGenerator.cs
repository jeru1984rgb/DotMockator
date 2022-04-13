using System.Reactive.Disposables;
using System.Reactive.Linq;
using Bogus.DataSets;
using dotmockator.core.definitions;

namespace dotmockator.core.generator;

public static class MockatorGenerator
{
    public static IObservable<T> GenerateObservable<T>(int amount)
    {
        return Observable.Create<T>(observer =>
        {
            for (int i = 0; i < amount; i++)
            {
                observer.OnNext(GenerateSingle<T>());
            }

            return Disposable.Empty;
        });
    }

    public static IObservable<T> GenerateObservable<T>(int amount, Definition definition)
    {
        return Observable.Create<T>(observer =>
        {
            for (int i = 0; i < amount; i++)
            {
                observer.OnNext(GenerateSingle<T>(definition));
            }

            return Disposable.Empty;
        });
    }

    public static object GenerateSingle(Type mockType)
    {
        var definition = GetDefinition(mockType);
        var candidate =  Convert.ChangeType(CreateCandidate(definition, mockType), mockType);


        return GenerateInternal(candidate!, definition);
    }

    public static T GenerateSingle<T>()
    {
        var definition = GetDefinition<T>();
        var candidate = CreateCandidate(definition, typeof(T));

        return (T) GenerateInternal(candidate!, definition);
    }

    public static T GenerateSingle<T>(Definition definition)
    {
        var candidate = CreateCandidate(definition, definition.DefinitionType);

        return (T) GenerateInternal(candidate!, definition);
    }

    private static object? CreateCandidate(Definition definition, Type candidateType)
    {
        var candidate = Activator.CreateInstance(candidateType);
        return candidate;
    }

    private static object GenerateInternal(object candidate, Definition definition)
    {
        foreach (var definitionField in definition.Fields)
        {
            HandleEmbedded(candidate, definitionField);
            HandleGroup(candidate, definitionField);
            HandleField(candidate, definitionField);
            HandleResolver(candidate, definitionField);
        }

        return candidate;
    }

    private static void HandleEmbedded<T>(T candidate, DefinitionField definitionField)
    {
        if (!definitionField.IsEmbedded && !definitionField.IsDefinitionReuse)
            return;

        
        if (definitionField.IsDefinitionReuse)
        {
            definitionField.PropertyInfo.SetValue(candidate, GenerateSingle<T>(definitionField.ReuseDefinition));
        }
        else if (definitionField.EmbeddedType.IsInterface)
        {
            definitionField.PropertyInfo.SetValue(candidate, GenerateSingle(definitionField.ImplementationType));
        }
        else
        {
            definitionField.PropertyInfo.SetValue(candidate, GenerateSingle(definitionField.EmbeddedType));
        }
    }

    private static void HandleGroup<T>(T candidate, DefinitionField definitionField)
    {
        if (!definitionField.IsGroup)
            return;

        var result = Activator.CreateInstance(definitionField.PropertyInfo.PropertyType);
        var rnd = new Random();
        int amountFields = rnd.Next(definitionField.Min, definitionField.Max);
        for (int i = 0; i <= amountFields; i++)
        {
            result.GetType().GetMethod("Add")
                .Invoke(result,
                    new[]
                    {
                        GenerateSingle(definitionField.GroupType.IsInterface
                            ? definitionField.ImplementationType
                            : definitionField.GroupType)
                    });
        }

        definitionField.PropertyInfo.SetValue(candidate, result);
    }

    private static void HandleField<T>(T candidate, DefinitionField definitionField)
    {
        if (!definitionField.IsGenerator)
            return;

        object? generator = Activator.CreateInstance(definitionField.GeneratorType);

        if (generator is IGenerator)
        {
            definitionField.PropertyInfo.SetValue(candidate, ((IGenerator) generator).Generate(definitionField));
        }
    }

    private static Definition GetDefinition(Type definitionType)
    {
        return DefinitionExtractor.ExtractDefinition(definitionType);
    }

    private static Definition GetDefinition<T>()
    {
        return DefinitionExtractor.ExtractDefinition(typeof(T));
    }

    private static void HandleResolver<T>(T candidate, DefinitionField definitionField)
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