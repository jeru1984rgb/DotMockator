using dotmockator.core.definitions;

namespace dotmockator.core.generator;

public sealed class GeneratorRegister
{
    private static GeneratorRegister _instance = new();

    private Dictionary<FieldFunctionEnum, IGenerator> _generatorMap = new();

    static GeneratorRegister()
    {
    }

    private GeneratorRegister()
    {
        _generatorMap.TryAdd(FieldFunctionEnum.FirstName, NameFieldGenerator.FirstNameGenerator);
        _generatorMap.TryAdd(FieldFunctionEnum.LastName, NameFieldGenerator.LastNameGenerator);
    }

    public static GeneratorRegister Instance
    {
        get { return _instance; }
    }
    
    public IGenerator GetGenerator(FieldFunctionEnum fieldFunctionEnum)
    {
        if (_generatorMap.TryGetValue(fieldFunctionEnum,  out var generator))
        {
            return generator;
        }

        throw new ArgumentException($"No generator for {generator.FunctionEnum.ToString()} can be found");
    }
}