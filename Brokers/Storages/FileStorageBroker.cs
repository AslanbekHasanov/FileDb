//----------------------------------------
// Tarteeb School (c) All rights reserved
//----------------------------------------

using FileDB.Models.Users;
using System.IO;
using System.Numerics;

namespace FileDB.Brokers.Storages
{
    internal class FileStorageBroker : IStorageBroker
    {
        private const string FilePath = "../../../Assets/UserDb.txt";
        private bool isUpdateOrDelete;

        public FileStorageBroker()
        {
            isUpdateOrDelete = false;
            EnsureFileExists();
        }

        public User AddUser(User user)
        {
            string userLine = $"{user.Id}*{user.Name}\n";

            File.AppendAllText(FilePath, userLine);
            return user;
        }

        public bool UpdateUser(User user)
        {
            List<User> users = ReadAllUsers();
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Id == user.Id)
                {
                    users[i].Name = user.Name;
                    isUpdateOrDelete = true;
                    return isUpdateOrDelete;
                }
            }
            File.WriteAllText(FilePath, string.Empty);
            foreach (User user1 in users)
            {
                AddUser(user1);
            }

            return isUpdateOrDelete;
        }
        public List<User> ReadAllUsers()
        {
            string[] userLines = File.ReadAllLines(FilePath);
            List<User> users = new List<User>();

            foreach (string userLine in userLines)
            {
                string[] userProperties = userLine.Split("*");
                User user = new User
                {
                    Id = Convert.ToInt32(userProperties[0]),
                    Name = userProperties[1],
                };
                users.Add(user);
            }

            return users;
        }

        public bool DeleteUser(int id)
        {
            List<User> users = ReadAllUsers();
            File.WriteAllText(FilePath, string.Empty);

            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Id != id)
                {
                    AddUser(users[i]);
                    isUpdateOrDelete = true;
                    return isUpdateOrDelete;
                }
            }
            return isUpdateOrDelete;
        }

        private void EnsureFileExists()
        {
            bool fileExists = File.Exists(FilePath);
            if (fileExists is false)
            {
                File.Create(FilePath).Close();
            }
        }

    }
}
