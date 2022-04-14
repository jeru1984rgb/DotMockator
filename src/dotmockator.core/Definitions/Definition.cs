using DotMockator.Core.Definitions.Field;

namespace DotMockator.Core.Definitions;

public class Definition
{
    public Type DefinitionType { get; }

    public Definition(Type definitionType)
    {
        DefinitionType = definitionType;
    }

    public IEnumerable<DefinitionField> Fields { get; } = new List<DefinitionField>();

    private List<DefinitionField> GetFieldList()
    {
        return (List<DefinitionField>) Fields;
    }
    
    
    public void AddField(DefinitionField definitionField)
    {
        GetFieldList().Add(definitionField);
    }

    public DefinitionField? GetFieldByPropertyName(string propertyName)
    {
        return Fields.FirstOrDefault(df => df.PropertyName == propertyName);
    }
}