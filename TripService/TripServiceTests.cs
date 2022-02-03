using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using TripService.Exception;
using TripService.Trip;
using Xunit;

namespace TripService;

public partial class TripServiceTests
{
    private const string Brazil = "Brazil";
    private const string Zarautz = "Zarautz";

    private const User.User Guest = null!;
    private const User.User NoUser = null!;
    private readonly User.User _registeredUser = new ();
    
    private readonly Trip.TripService _tripService;

    private readonly Mock<TripDAO> _tripDaoMock;

    public TripServiceTests()
    {
        _tripDaoMock = new Mock<TripDAO>();
        _tripService = new Trip.TripService(_tripDaoMock.Object);
    }
    
    [Fact]
    public void Should_throw_an_exception_when_user_is_not_logged_in()
    {
        Action action = () => _tripService.GetFriendTrips(NoUser, Guest);

        action.Should().Throw<UserNotLoggedInException>();
    }

    [Fact]
    public void Should_not_return_any_trips_if_users_are_not_friends()
    {
        var friend = new UserBuilder()
            .WithTrips(new Trip.Trip(Brazil))
            .Build();

        var trips = _tripService.GetFriendTrips(friend, _registeredUser);

        trips.Count.Should().Be(0);
    }
    
    [Fact]
    public void Should_return_trips_if_users_are_friends()
    {
        var trips = new List<Trip.Trip>
        {
            new (Brazil),
            new (Zarautz)
        };
        
        var friend = new UserBuilder()
            .WithFriends(_registeredUser)
            .WithTrips(trips.ToArray())
            .Build();

        _tripDaoMock.Setup(x => x.TripsBy(friend)).Returns(friend.Trips);
            
        var friendTrips = _tripService.GetFriendTrips(friend, _registeredUser);

        friendTrips.Count.Should().Be(2);
    }
}