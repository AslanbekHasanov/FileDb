//----------------------------------------
// Tarteeb School (c) All rights reserved
//----------------------------------------

using FileDB.Models.Users;

namespace FileDB.Services.TxtFileServices
{
    internal interface IUserTxtService
    {
        User AddUser(User user);
        void ShowUsers();
        bool Update(User user);
        bool Delete(int id);
    }
}
