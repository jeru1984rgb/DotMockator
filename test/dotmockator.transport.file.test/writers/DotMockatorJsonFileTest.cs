using System.Collections.Generic;
using System.Threading.Tasks;
using dotmockator.core.test;
using dotmockator.core.test.testdata;
using dotmockator.core.test.testdata.complex;
using dotmockator.transport.file.writers;
using dotmockator.transport.file.writers.json;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace dotmockator.transport.file.test.writers;

public class DotMockatorJsonFileTest
{
    [Fact]
    public async Task WriteJson_ToFile()
    {
        DotMockatorJsonFile<ComplexMockDefinitionWithAttribute> jsonFile =
            new DotMockatorJsonFile<ComplexMockDefinitionWithAttribute>(".", "test.json");
        await jsonFile.WriteToFile(1);


        string assertCandidateJson = System.IO.File.ReadAllText("./test.json");
        assertCandidateJson.Should().NotBeEmpty();

        var assertCandidate =
            JsonConvert.DeserializeObject<IEnumerable<ComplexMockDefinitionWithAttribute>>(assertCandidateJson,
                jsonFile.JsonSerializerSettings);
        assertCandidate.Should().BeEquivalentTo(jsonFile.Mocks);
    }
}