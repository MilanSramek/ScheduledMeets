using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping.EF;

using Microsoft.EntityFrameworkCore;

using ScheduledMeets.Persistence.Model;
using ScheduledMeets.TestTools.AsyncQueryables;
using ScheduledMeets.TestTools.Extensions;
using ScheduledMeets.View;

using System.Linq.Expressions;
using System.Threading;

namespace ScheduledMeets.Persistence.Tests;

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
    public void ExpressionMappingTest()
    {
        Expression<Func<Core.User, bool>> pattern = user => user.Name!.FirstName == "5";
        Expression<Func<User, bool>> image = user => user.FirstName == "5";

        var sut = _mapper.Map<Expression<Func<User, bool>>>(pattern);


        sut.Should().BeEquivalentTo(image);
    }

    [Test]
    public async Task QueryableMappingTest()
    {
        IQueryable<User> users = new User[]
        {
            new()
            {
                Id = 1
            },
            new()
            {
                Id = 2,
                Username = "TestUsername",
                FirstName = "TestFirstName",
                LastName = "TestLastName",
            },
        }.AsAsyncQueryable();


        IQueryable<UserView> sut = users.UseAsAsyncDataSource(_mapper).For<UserView>();
        var ttt = await sut
          .ToDictionaryAsync(_ => _.Id);

        List<UserView> result = await sut
            .Where(_ => _.Id == 2)
            .ToListAsync();

        result.Should().BeEquivalentTo(new[]
        {
            new
            {
                Id = 2,
                Username = "TestUsername",
                FirstName = "TestFirstName",
                LastName = "TestLastName",
            }
        });
    }
}
