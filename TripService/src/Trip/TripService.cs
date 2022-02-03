using System.Collections.Generic;
using TripService.Exception;

namespace TripService.Trip;

public class TripService
{
    public List<Trip> GetTripsByUser(User.User user, User.User loggedInUser)
    {
        if (loggedInUser is null)
        {
            throw new UserNotLoggedInException();
        }
        
        return user.IsFriendOf(loggedInUser) ? FindTripsByUser(user) : NoTrips();
    }

    private static List<Trip> NoTrips()
    {
        return new List<Trip>();
    }

    protected virtual List<Trip> FindTripsByUser(User.User user)
    {
        return TripDAO.FindTripsByUser(user);
    }
}

