//----------------------------------------
// Tarteeb School (c) All rights reserved
//----------------------------------------

namespace FileDB.Services.UserProcessing
{
    internal interface IUserProcessing
    {
        void CreateNewUser(string name);
        void DisplayUsers();
        void UpdateUser(int id, string name);
        void DeleteUser(int id);
    }
}
