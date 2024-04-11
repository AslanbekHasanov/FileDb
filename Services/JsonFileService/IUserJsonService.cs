//----------------------------------------
// Tarteeb School (c) All rights reserved
//----------------------------------------

using FileDB.Models.Users;

namespace FileDB.Services.JsonFileService
{
    internal interface IUserJsonService
    {
        User AddUser(User user);
        void ShowUsers();
        bool Update(User user);
        bool Delete(int id);
    }
}
