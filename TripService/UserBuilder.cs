namespace TripService;

public partial class TripServiceTests
{
    public class UserBuilder
    {
        private Trip.Trip[] _trips = {};
        private User.User[] _friends = {};

        public UserBuilder User()
        {
            return new UserBuilder();
        }
        
        public UserBuilder WithTrips(params Trip.Trip[] trips)
        {
            _trips = trips;
            return this;
        }

        public UserBuilder WithFriends(params User.User[] friends)
        {
            _friends = friends;
            return this;
        }

        public User.User Build()
        {
            var user = new User.User();
            AddFriends(user);
            AddTrips(user);
            return user;
        }
        
        private void AddFriends(User.User user)
        {
            foreach (var friend in _friends)
            {
                user.AddFriend(friend);
            }
        }

        private void AddTrips(User.User user)
        {
            foreach (var trip in _trips)
            {
                user.AddTrip(trip);
            }
        }
    }
}