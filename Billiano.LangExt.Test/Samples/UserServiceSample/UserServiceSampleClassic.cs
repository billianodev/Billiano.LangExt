namespace Billiano.LangExt.Test.Samples.UserServiceSample;

public class UserServiceSampleClassic : ISample
{
    public void RunSample()
    {
        var userService = new UserServiceClassic();

        try
        {
            userService.CreateUser("user@domain.com", "Foo");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        try
        {
            userService.CreateUser("user@domain.com", "Bar");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        Console.WriteLine(userService.GetUser("user@domain.com"));

        try
        {
            userService.UpdateUser("user@domain.com", user => user with { Name = "Bar" });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        Console.WriteLine(userService.GetUser("user@domain.com"));
    }
}
