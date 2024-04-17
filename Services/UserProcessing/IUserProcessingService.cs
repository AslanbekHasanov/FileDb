//----------------------------------------
// Tarteeb School (c) All rights reserved
//----------------------------------------

using FileDB.Models.Users;

namespace FileDB.Services.UserProcessing
{
    internal interface IUserProcessingService
    {
        User CreatUser(User user);
        List<User> GetAllUser();
        User ModifyUser(User user);
        bool RemoveUser(int id);
    }
}
