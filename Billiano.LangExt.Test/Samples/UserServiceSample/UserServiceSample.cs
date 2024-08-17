using Billiano.LangExt.Functional;

namespace Billiano.LangExt.Test.Samples.UserServiceSample;

public class UserServiceSample : ISample
{
    public void RunSample()
    {
        var userService = new UserService();

        userService.CreateUser("user@domain.com", "Foo")
            .Catch(Console.WriteLine);
        userService.CreateUser("user@domain.com", "Bar")
            .Catch(Console.WriteLine);

        userService.GetUser("user@domain.com")
            .Then(Console.WriteLine);
        userService.UpdateUser("user@domain.com", user => user with { Name = "Bar" })
            .Catch(Console.WriteLine);
        userService.GetUser("user@domain.com")
            .Then(Console.WriteLine);
    }
}
