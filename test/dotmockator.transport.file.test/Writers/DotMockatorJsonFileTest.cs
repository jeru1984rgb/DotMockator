using System.Collections.Generic;
using System.Threading.Tasks;
using DotMockator.Core.Test.TestData.Complex;
using DotMockator.Transport.File.Writers.Json;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace DotMockator.Transport.File.Test.Writers;

public class DotMockatorJsonFileTest
{
    [Fact]
    public async Task WriteJson_ToFile()
    {
        var jsonFile =
            new DotMockatorJsonFile<ComplexMockDefinitionWithAttribute>(".", "test.json");
        await jsonFile.WriteToFile(1);


        var assertCandidateJson = await System.IO.File.ReadAllTextAsync("./test.json");
        assertCandidateJson.Should().NotBeEmpty();

        var assertCandidate =
            JsonConvert.DeserializeObject<IEnumerable<ComplexMockDefinitionWithAttribute>>(assertCandidateJson,
                jsonFile.JsonSerializerSettings);
        assertCandidate.Should().BeEquivalentTo(jsonFile.Mocks);
    }
}