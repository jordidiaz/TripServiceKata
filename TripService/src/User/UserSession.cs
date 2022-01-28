using TripService.Exception;

namespace TripService.User;
public class UserSession
{
    private static readonly UserSession Session = new();

    public static UserSession GetInstance()
    {
        return Session;
    }

    public bool IsUserLoggedIn(User user)
    {
        throw new DependentClassCallDuringUnitTestException(
            "UserSession.IsUserLoggedIn() should not be called in an unit test");
    }

    public User GetLoggedUser()
    {
        throw new DependentClassCallDuringUnitTestException(
            "UserSession.GetLoggedUser() should not be called in an unit test");
    }
}
