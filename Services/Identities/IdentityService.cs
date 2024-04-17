//----------------------------------------
// Tarteeb School (c) All rights reserved
//----------------------------------------

using FileDB.Brokers.Storages;
using FileDB.Models.Users;

namespace FileDB.Services.Identities
{
    internal class IdentityService: IIdentityService
    {
        private static IdentityService instance;
        private readonly IStorageBroker storagesBroker;

        private IdentityService(IStorageBroker storageBroker)
        {
            this.storagesBroker = storageBroker;
        }

        public static IdentityService GetIdentityService(IStorageBroker storagesBroker)
        {
            if (instance == null)
            {
                instance = new IdentityService(storagesBroker);
            }
            return instance;
        }

        public int GetNewId()
        {
            List<User> users = this.storagesBroker.ReadAllUsers();

            return users.Count is not 0
                ? IncrementListUsersId(users)
                : 1;
        }

        private static int IncrementListUsersId(List<User> users)
        {
            return users[users.Count - 1].Id + 1;

        }
    }
}
