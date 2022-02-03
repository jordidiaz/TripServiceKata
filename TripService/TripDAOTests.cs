using System;
using FluentAssertions;
using TripService.Exception;
using TripService.Trip;
using Xunit;

namespace TripService;

public class TripDAOTests
{
    [Fact]
    public void Should_throw_exception_when_retrieving_user_trips()
    {
        Action action = () => new TripDAO().TripsBy(new User.User());
        action.Should().Throw<DependentClassCallDuringUnitTestException>();
    }
}