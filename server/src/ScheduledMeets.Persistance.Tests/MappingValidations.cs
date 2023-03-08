using AutoMapper;

using ScheduledMeets.Persistance.Model;

namespace ScheduledMeets.Persistance.Tests;

public class MappingValidations
{
    [Test]
    public void AssertMappingsAreValid()
    {
        MapperConfiguration configuration = new(MappingConfiguration.AddProfiles);
        configuration.AssertConfigurationIsValid();
    }
}