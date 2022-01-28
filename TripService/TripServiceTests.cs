using System;
using FluentAssertions;
using TripService.Exception;
using Xunit;

namespace TripService;

public class TripServiceTests
{
    private const User.User Guest = null!;
    private const User.User NoUser = null!;
    private static User.User? _loggedInUser;
    
    [Fact]
    public void Should_throw_an_exception_when_user_is_not_logged_in()
    {
        var tripService = new TestableTripService();

        _loggedInUser = Guest;

        Action action = () => tripService.GetTripsByUser(NoUser);

        action.Should().Throw<UserNotLoggedInException>();
    }
    
    private class TestableTripService : Trip.TripService
    {
        protected override User.User? GetLoggedUser()
        {
            return _loggedInUser;
        }
    }
}