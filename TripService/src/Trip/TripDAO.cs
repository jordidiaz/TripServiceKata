using System.Collections.Generic;
using TripService.Exception;

namespace TripService.Trip;

public class TripDAO
{
    public static List<Trip> FindTripsByUser(User.User user)
    {
        throw new DependentClassCallDuringUnitTestException(
                    "TripDAO should not be invoked on an unit test.");
    }

    public virtual List<Trip> TripsBy(User.User user)
    {
        return FindTripsByUser(user);
    }
}

