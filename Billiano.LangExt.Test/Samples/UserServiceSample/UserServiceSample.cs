using Billiano.LangExt.Functional;

namespace Billiano.LangExt.Test.Samples.UserServiceSample;

public class UserServiceSample : ISample
{
    public void RunSample()
    {
        var userService = new UserService();

        userService.GetUser("user@domain.com")
            .Or(() => userService.CreateUser("user@domain.com", "Foo")
                .IfFailed(Console.WriteLine)
                .ToOption())
            .IfSome(Console.WriteLine);

        userService.CreateUser("user@domain.com", "Bar")
            .IfFailed(Console.WriteLine);

        userService.UpdateUser("user@domain.com", user => user with { Name = "Bar" })
            .IfFailed(Console.WriteLine)
            .IfSuccess(Console.WriteLine);
    }
}
