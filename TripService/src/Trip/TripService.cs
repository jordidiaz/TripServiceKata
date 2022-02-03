using System.Collections.Generic;
using TripService.Exception;
using TripService.User;

namespace TripService.Trip;

public class TripService
{
    public List<Trip> GetTripsByUser(User.User user)
    {
        if (GetLoggedUser() is null)
        {
            throw new UserNotLoggedInException();
        }
        
        return user.IsFriendOf(GetLoggedUser()) ? FindTripsByUser(user) : NoTrips();
    }

    private static List<Trip> NoTrips()
    {
        return new List<Trip>();
    }

    protected virtual List<Trip> FindTripsByUser(User.User user)
    {
        return TripDAO.FindTripsByUser(user);
    }

    protected virtual User.User? GetLoggedUser()
    {
        return UserSession.GetInstance().GetLoggedUser();
    }
}

