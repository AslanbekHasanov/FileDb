//----------------------------------------
// Tarteeb School (c) All rights reserved
//----------------------------------------

using FileDB.Brokers.Loggings;
using FileDB.Brokers.Storages;
using FileDB.Models.Users;

namespace FileDB.Services.TxtFileServices
{
    internal class UserTxtService : IUserTxtService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public UserTxtService()
        {
            this.storageBroker = new FileStorageBroker();
            this.loggingBroker = new LoggingBroker();
        }

        public User AddUser(User user)
        {
            return user is null
                ? CreateAndLogInvalidUser()
                : ValidateAndAddUser(user);
        }

        public bool Delete(int id)
        {
            return id is 0
                ? DeleteAndLogInvalidId()
                : ValidateAndDeleteUser(id);
        }

        public void ShowUsers()
        {
            List<User> users = this.storageBroker.ReadAllUsers();

            foreach (User user in users)
            {
                this.loggingBroker.LogInformation($"{user.Id}. {user.Name}");
            }

            this.loggingBroker.LogInformation($"=== End of users ===");
        }


        public bool Update(User user)
        {
            return user is null
                ? UpdateAndLogInvalidUser()
                : ValidateAndUpdateUser(user);
        }

        private bool ValidateAndUpdateUser(User user)
        {
            if (user.Id is 0
                || String.IsNullOrWhiteSpace(user.Name))
            {
                this.loggingBroker.LogError("User details missing.");
                return false;
            }
            else
            {
                this.loggingBroker.LogInformation("User updated.");
                return this.storageBroker.UpdateUser(user);
            }
        }

        private bool UpdateAndLogInvalidUser()
        {
            this.loggingBroker.LogError("User is invalid.");
            return false;
        }

        private User ValidateAndAddUser(User user)
        {
            if (user.Id is 0
                || String.IsNullOrWhiteSpace(user.Name))
            {
                this.loggingBroker.LogError("User details missing.");
                return new User();
            }
            else
            {
                this.loggingBroker.LogInformation("User create.");
                return this.storageBroker.AddUser(user);
            }
        }

        private User CreateAndLogInvalidUser()
        {
            this.loggingBroker.LogError("User is invalid.");
            return new User();
        }

        private bool ValidateAndDeleteUser(int id)
        {
            if (String.IsNullOrWhiteSpace(id.ToString()))
            {
                this.loggingBroker.LogError("User id details missing.");
                return false;
            }
            else
            {
                this.loggingBroker.LogInformation("User deleted.");
                return this.storageBroker.DeleteUser(id);
            }
        }

        private bool DeleteAndLogInvalidId()
        {
            this.loggingBroker.LogError("User is invalid.");
            return false;
        }
    }
}
