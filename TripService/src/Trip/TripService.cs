using System.Collections.Generic;
using TripService.Exception;

namespace TripService.Trip;

public class TripService
{
    private readonly TripDAO _tripDao;

    public TripService(TripDAO tripDao)
    {
        _tripDao = tripDao;
    }

    public List<Trip> GetFriendTrips(User.User friend, User.User loggedInUser)
    {
        IsUserLoggedOrThrow(loggedInUser);

        return friend.IsFriendOf(loggedInUser) ? FindTripsByUser(friend) : NoTrips();
    }

    private static void IsUserLoggedOrThrow(User.User loggedInUser)
    {
        if (loggedInUser is null)
        {
            throw new UserNotLoggedInException();
        }
    }

    private static List<Trip> NoTrips()
    {
        return new List<Trip>();
    }

    protected virtual List<Trip> FindTripsByUser(User.User user)
    {
        return _tripDao.TripsBy(user);
    }
}

