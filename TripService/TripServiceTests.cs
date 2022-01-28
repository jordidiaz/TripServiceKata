using System;
using FluentAssertions;
using TripService.Exception;
using Xunit;

namespace TripService;

public class TripServiceTests
{
    private const User.User Guest = null!;
    private const User.User NoUser = null!;
    private readonly User.User _registeredUser = new ();
    private static User.User? _loggedInUser;
    private TestableTripService _sut;

    public TripServiceTests()
    {
        _sut = new TestableTripService();
    }
    
    [Fact]
    public void Should_throw_an_exception_when_user_is_not_logged_in()
    {
        _loggedInUser = Guest;

        Action action = () => _sut.GetTripsByUser(NoUser);

        action.Should().Throw<UserNotLoggedInException>();
    }

    [Fact]
    public void Should_not_return_any_trips_if_users_are_not_friends()
    {
        _loggedInUser = _registeredUser;
        var friend = new User.User();
        friend.AddTrip(new Trip.Trip("Brazil"));

        var trips = _sut.GetTripsByUser(friend);

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