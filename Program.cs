//----------------------------------------
// Tarteeb School (c) All rights reserved
//----------------------------------------

using FileDB.Services.UserProcessing;

internal class Program
{
    private static void Main(string[] args)
    {
        IUserProcessing txtProcessing = new TxtProcessingService();
        IUserProcessing jsonProcessing = new JsonProcessingService();

        SelectDb();
        Console.Write("Enter command: ");
        int command = Convert.ToInt32(Console.ReadLine());
        if (command == 1)
        {
            PrintMenuForUser(jsonProcessing);
        }
        else
        {
            PrintMenuForUser(txtProcessing);
        }
    }
    static void PrintMenuForUser(IUserProcessing userProcessing)
    {
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
                    string userName = Console.ReadLine();
                    userProcessing.CreateNewUser(userName);
                    break;

                case "2":
                    {
                        Console.Clear();
                        userProcessing.DisplayUsers();
                    }
                    break;

                case "3":
                    {
                        Console.Clear();
                        Console.WriteLine("Enter an id which you want to delete");
                        Console.Write("Enter id:");
                        string deleteWithIdStr = Console.ReadLine();
                        int deleteWithId = Convert.ToInt32(deleteWithIdStr);
                        userProcessing.DeleteUser(deleteWithId);
                    }
                    break;

                case "4":
                    {
                        Console.Clear();
                        Console.WriteLine("Enter an id which you want  to edit");
                        Console.Write("Enter an id:");
                        string idStr = Console.ReadLine();
                        int id = Convert.ToInt32(idStr);
                        Console.Write("Enter name:");
                        string name = Console.ReadLine();
                        userProcessing.UpdateUser(id, name);
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
        Console.WriteLine("0.Exit");
    }
    static void SelectDb()
    {
        Console.WriteLine("Welcome to, my project!");
        Console.WriteLine("1.JSON");
        Console.WriteLine("2.Txt");
    }
}
