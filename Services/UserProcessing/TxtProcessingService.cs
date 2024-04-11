//----------------------------------------
// Tarteeb School (c) All rights reserved
//----------------------------------------

using FileDB.Models.Users;
using FileDB.Services.Identities;
using FileDB.Services.TxtFileServices;

namespace FileDB.Services.UserProcessing
{
    internal class TxtProcessingService: IUserProcessing
    {
        private readonly IUserTxtService userService;
        private readonly IdentityWithTxtFileService identityService;

        public TxtProcessingService()
        {
            this.userService = new UserTxtService();
            this.identityService = IdentityWithTxtFileService.GetIdentityService();
        }

        public void CreateNewUser(string name)
        {
            User user = new User();
            user.Id = this.identityService.GetNewId();
            user.Name = name;
            this.userService.AddUser(user);
        }

        public void DisplayUsers()
        {
            this.userService.ShowUsers();
        }

        public void UpdateUser(int id, string name)
        {
            User user = new User()
            {
                Id = id,
                Name = name
            };
            this.userService.Update(user);
        }

        public void DeleteUser(int id)
        {
            this.userService.Delete(id);
        }
    }
}
