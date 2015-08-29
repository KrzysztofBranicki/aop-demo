using System;
using Common;
using Common.Logging;
using AopDemo.Application.User;
using AopDemo.DeliveryMechanism.Console.CompositionRoot;

namespace AopDemo.DeliveryMechanism.Console
{
    class Program
    {
        static void Main()
        {
            var container = new Bootstrapper().CreateAndConfigureContainer();
            var userService = container.Resolve<IUserService>();

            var request = new ChangePasswordRequest { UserId = 1, NewPassword = "secret!" }; //VALID REQUEST
            //var request = new ChangePasswordRequest { UserId = 1 }; //INVALID - EMPTY PASSWORD
            //var request = new ChangePasswordRequest { UserId = 666, NewPassword = "secret!" }; //INVALID - WRONG USER ID
            //var request = new ChangePasswordRequest { UserId = 1, NewPassword = "abc" }; //INVALID - TOO SHORT PASSWORD
            
            var result = userService.ChangePassword(request);
            DisplayResult(result);

            System.Console.ReadKey();
        }

        private static void DisplayResult(Response result)
        {
            ColorConsole.WriteInColor(result.ToString(), result.Succeeded ? ConsoleColor.Green : ConsoleColor.Red);
        }
    }
}
