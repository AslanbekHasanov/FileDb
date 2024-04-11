//----------------------------------------
// Tarteeb School (c) All rights reserved
//----------------------------------------

using FileDB.Brokers.Storages;
using FileDB.Models.Users;
using Newtonsoft.Json;

namespace FileDB.Brokers.Storages
{
    internal class JSONStorageBroker : IStorageBroker
    {
        private const string FilePath = "../../../Assets/UserDb.json";
        private bool isUpdateOrDelete;

        public JSONStorageBroker()
        {
            isUpdateOrDelete = false;
            EnsureFileExists();
        }

        public User AddUser(User user)
        {
            List<User> users = ReadAllUsers();
            users.Add(user);
            string jsonConvertUserInfo = JsonConvert.SerializeObject(users);
            File.WriteAllText(FilePath, $"[{jsonConvertUserInfo}]");

            return user;
        }

        public bool DeleteUser(int id)
        {
            string userInfo = File.ReadAllText(FilePath);
            List<User> users = JsonConvert.DeserializeObject<List<User>>(userInfo);
            List<User> userDeletedNextInfo = new List<User>();

            foreach (var user in users)
            {
                if (user.Id != id)
                {
                    userDeletedNextInfo.Add(user);
                }
                else
                {
                    isUpdateOrDelete = true;
                }
            }

            string jsonConvertUsersInfo = JsonConvert.SerializeObject(userDeletedNextInfo);
            File.WriteAllText(FilePath, $"[{jsonConvertUsersInfo}]");

            return isUpdateOrDelete;
        }

        public List<User> ReadAllUsers()
        {
            string userInfo = File.ReadAllText(FilePath);
            List<User> users = JsonConvert.DeserializeObject<List<User>>(userInfo);

            foreach (var user in users)
            {
                Console.WriteLine($"{user.Id}. {user.Name}");
            }

            return users;
        }

        public bool UpdateUser(User user)
        {
            string userInfo = File.ReadAllText(FilePath);
            List<User> users = JsonConvert.DeserializeObject<List<User>>(userInfo);

            foreach (var userItem in users)
            {
                if (userItem.Id == user.Id)
                {
                    isUpdateOrDelete = true;
                    userItem.Name = user.Name;
                }
            }

            string jsonConvertUsersInfo = JsonConvert.SerializeObject(users);
            File.WriteAllText(FilePath, $"[{jsonConvertUsersInfo}]");

            return isUpdateOrDelete;
        }
        private void EnsureFileExists()
        {
            if (File.Exists(FilePath) is false)
            {
                File.Create(FilePath).Close();
            }
        }
    }
}
