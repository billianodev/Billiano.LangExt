using Billiano.LangExt.Functional.OptionResult;

namespace UserServiceSample;

public class UserService
{
    private readonly List<User> _usersDatabase = [];

    public Option<User> GetUser(string email)
    {
        return _usersDatabase.Find(user => user.Email == email);
    }

    public Result<User> CreateUser(string email, string name)
    {
        if (_usersDatabase.Any(user => user.Email == email))
        {
            return Result.Fail<User>(new ArgumentException($"Email {email} already exists"));
        }

        var user = new User(email, name);
        _usersDatabase.Add(user);
        return user;
    }

    public Result<User> UpdateUser(User user, Func<User, User> update)
    {
        var index = _usersDatabase.FindIndex(u => u == user);
        if (index == -1)
        {
            return Result.Fail<User>(new KeyNotFoundException(user.Email));
        }

        var result = update(user);
        _usersDatabase[index] = result;
        return result;
    }

    public Result DeleteUser(User user)
    {
        if (_usersDatabase.Remove(user))
        {
            return Result.Ok();
        }

        return Result.Fail(new KeyNotFoundException(user.Email));
    }
}

public record User(string Email, string Name);