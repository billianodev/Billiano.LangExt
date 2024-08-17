using Billiano.LangExt.Functional;

namespace Billiano.LangExt.Test.Samples.UserServiceSample;

public class UserService
{
    private readonly List<User> _usersDatabase = [];

    public Option<User> GetUser(string email)
    {
        return _usersDatabase.Find(user => user.Email == email);
    }

    public Result<User> UpdateUser(string email, Func<User, User> update)
    {
        var index = _usersDatabase.FindIndex(user => user.Email == email);
        if (index == -1)
        {
            return Result.Fail<User>(new KeyNotFoundException(email));
        }

        var result = update(_usersDatabase[index]);
        _usersDatabase[index] = result;
        return result;
    }

    public Result<User> CreateUser(string email, string name)
    {
        if (_usersDatabase.Any(user => user.Email == email))
        {
            return Result.Fail<User>(new ArgumentException("Email already exists", nameof(email)));
        }

        var user = new User(email, name);
        _usersDatabase.Add(user);
        return user;
    }
}