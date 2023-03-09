using AutoMapper;

using ScheduledMeets.Persistance.Model;

using System.Linq.Expressions;

namespace ScheduledMeets.Persistance.Tests;

public class UserMappingTests
{
    private IMapper _mapper = null!;

    [OneTimeSetUp]
    public void SetUp()
    {
        MapperConfiguration config = new(cfg => cfg
            .AddConfigurations()
            .AddProfile<UserProfile>());
        _mapper = config.CreateMapper();
    }

    [Test]
    public void ModelUserToUser()
    {
        User user = new()
        {
            Id = 1,
            Username = "TestUsername",
            FirstName = "TestFirstName",
            LastName = "TestLastName",
        };

        Core.User sut = _mapper.Map<Core.User>(user);


        sut.Should().BeEquivalentTo(user, options => options
            .Excluding(_ => _.Attendees)
            .Excluding(_ => _.FirstName)
            .Excluding(_ => _.LastName));
        sut.Name.Should().BeEquivalentTo(user, options => options
            .Including(_ => _.FirstName)
            .Including(_ => _.LastName));
    }

    [Test]
    public void UserToModelUser()
    {
        Core.User user = new("TestUsername")
        {
            Name = new("TestFirstName", "TestLastName")
        };
        typeof(Core.User).GetProperty(nameof(Core.User.Id))!.SetValue(user, 2);

        User sut = _mapper.Map<User>(user);


        sut.Should().BeEquivalentTo(user, options => options
            .Excluding(_ => _.Name));
        sut.Should().BeEquivalentTo(user.Name, options => options
            .Including(_ => _.FirstName)
            .Including(_ => _.LastName));
    }

    [Test]
    public void Quer()
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        Expression<Func<Core.User, bool>> pattern = user => user.Name.FirstName == "5";
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        Expression<Func<User, bool>> image = user => user.FirstName == "5";

        var sut = _mapper.Map<Expression<Func<User, bool>>>(pattern);


        sut.Should().BeEquivalentTo(image);
    }
}
