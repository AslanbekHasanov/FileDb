//----------------------------------------
// Tarteeb School (c) All rights reserved
//----------------------------------------

using FileDB.Brokers.Storages;
using FileDB.Models.Users;
using System.Text.Json;

namespace FileDB.Brokers.Storages
{
    internal class JSONStorageBroker : IStorageBroker
    {
        private const string FilePath = "../../../Assets/Jsons/UserDb.json";
        private bool isUpdateOrDelete;

        public JSONStorageBroker()
        {
            isUpdateOrDelete = false;
            EnsureFileExists();
        }

        public User AddUser(User user)
        {
            string userInfo = File.ReadAllText(FilePath);
            List<User> users = JsonSerializer.Deserialize<List<User>>(userInfo);
            User userExsist = users.FirstOrDefault(u => u.Id == user.Id);

            if (userExsist is null)
            {
                users.Add(user);
                string jsonConvertUserInfo = JsonSerializer.Serialize(users);
                File.WriteAllText(FilePath, $"{jsonConvertUserInfo}");
                return user;
            }

            return new User();
        }

        public bool DeleteUser(int id)
        {
            string userInfo = File.ReadAllText(FilePath);
            List<User> users = JsonSerializer.Deserialize<List<User>>(userInfo);
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

            if (isUpdateOrDelete is true)
            {
                string jsonConvertUsersInfo = JsonSerializer.Serialize(userDeletedNextInfo);
                File.WriteAllText(FilePath, $"{jsonConvertUsersInfo}");
                return isUpdateOrDelete;
            }

            return isUpdateOrDelete;
        }

        public List<User> ReadAllUsers()
        {
            string userInfo = File.ReadAllText(FilePath);
            List<User> users = JsonSerializer.Deserialize<List<User>>(userInfo);

            return users;
        }

        public User UpdateUser(User user)
        {
            string userInfo = File.ReadAllText(FilePath);
            List<User> users = JsonSerializer.Deserialize<List<User>>(userInfo);

            User userExsist = users.FirstOrDefault(u => u.Id == user.Id);

            if (userExsist is not null)
            {
                userExsist.Name = user.Name;
                string jsonConvertUsersInfo = JsonSerializer.Serialize(users);
                File.WriteAllText(FilePath, $"{jsonConvertUsersInfo}");

                return user;
            }

            return new User();
        }
        private void EnsureFileExists()
        {
            if (File.Exists(FilePath) is false)
            {
                File.WriteAllText(FilePath,"[]");
            }
        }
    }
}
