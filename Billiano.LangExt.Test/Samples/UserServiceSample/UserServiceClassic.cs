namespace Billiano.LangExt.Test.Samples.UserServiceSample;

public class UserServiceClassic
{
    private readonly List<User> _usersDatabase = [];

    public User? GetUser(string email)
    {
        return _usersDatabase.Find(user => user.Email == email);
    }

    public User UpdateUser(string email, Func<User, User> update)
    {
        var index = _usersDatabase.FindIndex(user => user.Email == email);
        if (index == -1)
        {
            throw new KeyNotFoundException(email);
        }

        var result = update(_usersDatabase[index]);
        _usersDatabase[index] = result;
        return result;
    }

    public User CreateUser(string email, string name)
    {
        if (_usersDatabase.Any(user => user.Email == email))
        {
            throw new ArgumentException("Email already exists", nameof(email));
        }

        var user = new User(email, name);
        _usersDatabase.Add(user);
        return user;
    }
}