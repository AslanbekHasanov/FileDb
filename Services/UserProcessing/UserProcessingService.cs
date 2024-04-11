//----------------------------------------
// Tarteeb School (c) All rights reserved
//----------------------------------------

using FileDB.Models.Users;
using FileDB.Services.Identities;
using FileDB.Services.UserService;

namespace FileDB.Services.UserProcessing
{
    internal class UserProcessingService
    {
        private readonly IUserService userService;
        private readonly IdentityService identityService;

        public UserProcessingService(IUserService userService,
            IdentityService identityService)
        {
            this.userService = userService;
            this.identityService = identityService;
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
