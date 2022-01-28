using System;
using FluentAssertions;
using TripService.Exception;
using Xunit;

namespace TripService;

public class TripServiceTests
{
    private const User.User Guest = null!;
    private const User.User NoUser = null!;
    private User.User RegisteredUser = new ();
    private static User.User? _loggedInUser;
    
    [Fact]
    public void Should_throw_an_exception_when_user_is_not_logged_in()
    {
        var tripService = new TestableTripService();

        _loggedInUser = Guest;

        Action action = () => tripService.GetTripsByUser(NoUser);

        action.Should().Throw<UserNotLoggedInException>();
    }

    [Fact]
    public void Should_not_return_any_trips_if_users_are_not_friends()
    {
        var tripService = new TestableTripService();

        _loggedInUser = RegisteredUser;
        var friend = new User.User();
        friend.AddTrip(new Trip.Trip("Brazil"));

        var trips = tripService.GetTripsByUser(friend);

        trips.Count.Should().Be(0);

    }
    
    private class TestableTripService : Trip.TripService
    {
        protected override User.User? GetLoggedUser()
        {
            return _loggedInUser;
        }
    }
}