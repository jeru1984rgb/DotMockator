using System.Reactive.Disposables;
using System.Reactive.Linq;
using dotmockator.core.definitions;
using dotmockator.core.generator.strategies;

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
            EmbeddedStrategy.HandleEmbedded(candidate, definitionField);
            GroupStrategy.HandleGroup(candidate, definitionField);
            FieldStrategy.HandleField(candidate, definitionField);
            ResolverStrategy.HandleResolver(candidate, definitionField);
        }

        return candidate;
    }

    private static Definition GetDefinition(Type definitionType)
    {
        return DefinitionExtractor.ExtractDefinition(definitionType);
    }

    private static Definition GetDefinition<T>()
    {
        return DefinitionExtractor.ExtractDefinition(typeof(T));
    }
}