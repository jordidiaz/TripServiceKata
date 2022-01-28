using System;
using FluentAssertions;
using TripService.Exception;
using Xunit;

namespace TripService;

public class TripServiceTests
{
    [Fact]
    public void Should_throw_an_exception_when_user_is_not_logged_in()
    {
        var tripService = new TestableTripService();

        Action action = () => tripService.GetTripsByUser(null!);

        action.Should().Throw<UserNotLoggedInException>();
    }
    
    private class TestableTripService : Trip.TripService
    {
        protected override User.User GetLoggedUser()
        {
            return null!;
        }
    }
}