using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using timeChecker.Models;

namespace timeChecker.Services
{
    internal class UserService
    {
        /// <summary>
        /// List of the users
        /// </summary>
        List<User> UserList = new List<User>();
        /// <summary>
        /// Used to gather a list of all users
        /// </summary>
        /// <returns>A list of user objects</returns>
        public async Task<List<User>> GetUsers()
        {
            UserList = await App.DatabasePublic.GetUsersAsync();
            return UserList;
        }


    }
}
