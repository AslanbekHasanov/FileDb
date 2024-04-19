//----------------------------------------
// Tarteeb School (c) All rights reserved
//----------------------------------------

using FileDB.Brokers.Loggings;
using FileDB.Brokers.Storages;
using FileDB.Models.Users;
using FileDB.Services.Files;
using FileDB.Services.Identities;
using FileDB.Services.UserProcessing;
using FileDB.Services.UserService;

internal class Program
{
    private static void Main(string[] args)
    {
        SelectDbMenu();
        Console.Write("Enter command: ");
        int command = Convert.ToInt32(Console.ReadLine());
        if (command == 1)
        {
            IStorageBroker jsonBroker = new JSONStorageBroker();
            PrintMenuForUser(SelectJsonOrTxtFile(jsonBroker));
        }
        else
        {
            IStorageBroker txtBroker = new FileStorageBroker();
            PrintMenuForUser(SelectJsonOrTxtFile(txtBroker));
        }   
    }
    static void PrintMenuForUser(UserProcessingService userProcessing)
    {
        User user = new User();
        string userChoice;

        do
        {
            PrintMenu();
            Console.Write("Enter your choice:");
            userChoice = Console.ReadLine();
            switch (userChoice)
            {
                case "1":
                    Console.Clear();
                    Console.Write("Enter you name:");
                    user.Name = Console.ReadLine();
                    userProcessing.CreateUser(user);
                    break;

                case "2":
                    {
                        Console.Clear();
                        userProcessing.GetAllUser();
                    }
                    break;

                case "3":
                    {
                        Console.Clear();
                        Console.Write("Enter id:");
                        string removeUserId = Console.ReadLine();
                        int removeUserIdToInt32 = Convert.ToInt32(removeUserId);
                        userProcessing.RemoveUser(removeUserIdToInt32);
                    }
                    break;

                case "4":
                    {
                        Console.Clear();
                        Console.Write("Enter id:");
                        user.Id = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter name:");
                        user.Name = Console.ReadLine();
                        userProcessing.ModifyUser(user);
                    }
                    break;
                case "5":
                    {
                        Console.Clear();
                        string filePath = "../../../Assets";

                        DirectoryInfo directory = new DirectoryInfo(filePath);

                        IFileService fileSizeService = new FileService();
                        var fileSize = fileSizeService.GetFileSizeInProject(directory);

                    }
                    break;

                case "0": break;

                default:
                    Console.WriteLine("You entered wrong input, Try again");
                    break;
            }
        }
        while (userChoice != "0");
        Console.Clear();
        Console.WriteLine("The app has been finished");
    }
    public static void PrintMenu()
    {
        Console.WriteLine("1.Create User");
        Console.WriteLine("2.Display User");
        Console.WriteLine("3.Delete User by id");
        Console.WriteLine("4.Update User by id");
        Console.WriteLine("5.File size");
        Console.WriteLine("0.Exit");
    }
    static void SelectDbMenu()
    {
        Console.WriteLine("Welcome to, my project!");
        Console.WriteLine("1.JSON");
        Console.WriteLine("2.Txt");
    }
    static UserProcessingService SelectJsonOrTxtFile(IStorageBroker broker) 
    {
        IStorageBroker storageBroker = broker;
        ILoggingBroker loggingBroker = new LoggingBroker();
        IUserService userService = new UserService(storageBroker);
        IdentityService identitiyService = IdentityService.GetIdentityService(storageBroker);
        UserProcessingService userProcessingService = new UserProcessingService(userService, identitiyService);
        
        return userProcessingService;
    }
}
