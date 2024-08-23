### Before

```
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

var user1 = userService.GetUser("user@domain.com");
if (user1 is not null)
{
    Console.WriteLine(user1);
}

try
{
    userService.UpdateUser("user@domain.com", user => user with { Name = "Bar" });
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}

var user2 = userService.GetUser("user@domain.com");
if (user2 is not null)
{
    Console.WriteLine(user2);
}
```

### After

```csharp
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
```