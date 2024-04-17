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

        public User UpdateUser(User user)
        {
            List<User> users = this.ReadAllUsers();

            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Id == user.Id)
                {
                    users[i] = user;
                    isUpdateOrDelete = true;
                }
            }

            if (isUpdateOrDelete is true)
            {
                File.WriteAllText(FilePath, string.Empty);

                foreach (var userInfo in users)
                {
                    this.AddUser(userInfo);
                }

                return user;
            }

            return new User();
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
            string[] users = File.ReadAllLines(FilePath);
            File.WriteAllText(FilePath, string.Empty);

            for (int itaration = 0; itaration < users.Length; itaration++)
            {
                string userLine = users[itaration];
                string[] userProperties = userLine.Split("*");

                if (userProperties[0].Contains(id.ToString()) is true)
                {
                    isUpdateOrDelete = true;
                }
                else
                {
                    File.AppendAllText(FilePath, userLine);
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
