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

        public User CreateUser(User user)
        {
            user.Id = this.identityService.GetNewId();
            return this.userService.AddUser(user);
        }

        public List<User> GetAllUser()=>
            this.userService.ReadAllUsers();

        public User ModifyUser(User user)=>
            this.userService.UpdateUser(user);

        public bool RemoveUser(int id)=>
            this.userService.DeleteUser(id);
    }
}
