namespace dotmockator.core.definitions;

public class Definition
{
    public IEnumerable<DefinitionField> Fields { get; } = new List<DefinitionField>();

    private List<DefinitionField> GetFieldList()
    {
        return (List<DefinitionField>) Fields;
    }
    
    public void AddField(DefinitionField definitionField)
    {
        GetFieldList().Add(definitionField);
    }
}