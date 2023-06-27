using AutoMapper;

using ScheduledMeets.Persistence.Model;

namespace ScheduledMeets.Persistence.Tests;

public class MappingValidations
{
    [Test]
    public void AssertMappingsAreValid()
    {
        MapperConfiguration configuration = new(setting => setting.AddProfiles());
        configuration.AssertConfigurationIsValid();
    }
}