﻿using Billiano.LangExt.Functional.OptionResult;
using UserServiceSample;

var userService = new UserService();

// Request 1 : Get or create user
userService.GetUser("user@domain.com")
    .Or(() => userService.CreateUser("user@domain.com", "Foo")
        .IfFailed(Console.WriteLine)
        .ToOption())
    .IfSome(user => Console.WriteLine("Found user {0}", user));

// Request 2 : Create user
userService.CreateUser("user@domain.com", "Bar")
    .IfSuccess(user => Console.WriteLine("Successfully created user {0}", user))
    .IfFailed(Console.WriteLine);

// Request 3 : Rename user
userService.GetUser("user@domain.com")
    .Then(user => userService.UpdateUser(user, user => user with { Name = "Bar" })
        .IfSuccess(newUser => Console.WriteLine("Updated user {0} to {1}", user, newUser))
        .IfFailed(Console.WriteLine));

// Request 4 : Delete user
userService.GetUser("user@domain.com")
    .Then(user => userService.DeleteUser(user)
        .IfSuccess(() => Console.WriteLine("User {0} is successfully deleted!", user))
        .IfFailed(Console.WriteLine));