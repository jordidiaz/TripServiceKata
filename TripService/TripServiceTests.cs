using System;
using System.Collections.Generic;
using FluentAssertions;
using TripService.Exception;
using Xunit;

namespace TripService;

public class TripServiceTests
{
    private const string Brazil = "Brazil";
    private const string Zarautz = "Zarautz";

    private const User.User Guest = null!;
    private const User.User NoUser = null!;
    private readonly User.User _registeredUser = new ();
    private static User.User? _loggedInUser;
    
    private readonly TestableTripService _sut;

    public TripServiceTests()
    {
        _sut = new TestableTripService();
        
        _loggedInUser = _registeredUser;
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
        var friend = new User.User();
        friend.AddTrip(new Trip.Trip(Brazil));

        var trips = _sut.GetTripsByUser(friend);

        trips.Count.Should().Be(0);
    }
    
    [Fact]
    public void Should_return_trips_if_users_are_friends()
    {
        var friend = new User.User();
        friend.AddFriend(_loggedInUser!);
        friend.AddTrip(new Trip.Trip(Brazil));
        friend.AddTrip(new Trip.Trip(Zarautz));

        var trips = _sut.GetTripsByUser(friend);

        trips.Count.Should().Be(2);
    }
    
    private class TestableTripService : Trip.TripService
    {
        protected override User.User? GetLoggedUser()
        {
            return _loggedInUser;
        }

        protected override List<Trip.Trip> FindTripsByUser(User.User user)
        {
            return user.Trips();
        }
    }
}