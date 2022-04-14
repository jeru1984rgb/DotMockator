using DotMockator.Core.Definitions;
using DotMockator.Core.Generator;

namespace DotMockator.Transport.File.Writers;

public abstract class AbstractFileWriter<T>
{
    public string File { get; }
    public string Path { get; }

    public IEnumerable<T> Mocks { get; }
    
    public Definition Definition { get; }

    public AbstractFileWriter(string file, string path)
    {
        File = file;
        Path = path;
        Mocks = new List<T>();
        Definition = DefinitionExtractor.ExtractDefinition(typeof(T));
    }

    public async Task WriteToFile(int amount)
    {
        var mockObservable = MockatorGenerator.GenerateObservable<T>(amount, Definition);
        
        mockObservable.Subscribe(x => { ((List<T>) Mocks).Add(x); });
        await using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine(Path, File)))
        {
            await outputFile.WriteAsync(DeserializeMocks());
        }
    }

    public abstract string DeserializeMocks();
}