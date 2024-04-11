//----------------------------------------
// Tarteeb School (c) All rights reserved
//----------------------------------------

using FileDB.Brokers.Storages;
using FileDB.Models.Users;

namespace FileDB.Services.Identities
{
    internal class IdentityWithTxtFileService
    {
        private static IdentityWithTxtFileService instance;
        private readonly IStorageBroker storagesBroker;

        private IdentityWithTxtFileService()
        {
            this.storagesBroker = new FileStorageBroker();
        }

        public static IdentityWithTxtFileService GetIdentityService()
        {
            if (instance == null)
            {
                instance = new IdentityWithTxtFileService();
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
