using dotmockator.core.definitions;
using dotmockator.core.generator;

namespace dotmockator.transport.file.writers;

public abstract class AbstractFileWriter<T>
{
    public string File { get; }
    public string Path { get; }

    public IEnumerable<T> Mocks { get; }
    
    public Definition Definition { get; private set; }

    public AbstractFileWriter(string file, string path)
    {
        File = file;
        Path = path;
        Mocks = new List<T>();
    }

    public async Task WriteToFile(int amount)
    {
        Definition = DefinitionExtractor.ExtractDefinition(typeof(T));
        var mockObservable = MockatorGenerator.GenerateObservable<T>(amount, Definition);
        
        mockObservable.Subscribe(x => { ((List<T>) Mocks).Add(x); });
        await using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine(Path, File)))
        {
            await outputFile.WriteAsync(DeserializeMocks());
        }
    }

    public abstract string DeserializeMocks();
}