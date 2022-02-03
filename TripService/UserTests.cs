using FluentAssertions;
using Xunit;

namespace TripService;

public class UserTests
{
    private User.User Bob = new();
    private User.User Paul = new();
    
    [Fact]
    public void Should_inform_when_user_are_not_friends()
    {
        var user = new TripServiceTests.UserBuilder()
            .WithFriends(Bob)
            .Build();

        user.IsFriendOf(Paul).Should().BeFalse();
    }
    
    [Fact]
    public void Should_inform_when_user_are_friends()
    {
        var user = new TripServiceTests.UserBuilder()
            .WithFriends(Bob)
            .Build();

        user.IsFriendOf(Bob).Should().BeTrue();
    }
}