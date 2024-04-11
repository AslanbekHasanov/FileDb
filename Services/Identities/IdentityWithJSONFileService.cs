//----------------------------------------
// Tarteeb School (c) All rights reserved
//----------------------------------------

using FileDB.Brokers.Storages;
using FileDB.Models.Users;

namespace FileDB.Services.Identities
{
    internal class IdentityWithJSONFileService
    {
        private static IdentityWithJSONFileService instance;
        private readonly IStorageBroker storagesBroker;

        private IdentityWithJSONFileService()
        {
            this.storagesBroker = new JSONStorageBroker();
        }

        public static IdentityWithJSONFileService GetIdentityService()
        {
            if (instance == null)
            {
                instance = new IdentityWithJSONFileService();
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
