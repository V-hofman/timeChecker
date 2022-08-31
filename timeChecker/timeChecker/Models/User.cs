using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace timeChecker.Models
{
    public class User
    {
        /// <summary>
        /// Auto incremental primary key of the user
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        /// <summary>
        /// Username of the account
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Password of the account
        /// </summary>
        public string Password { get; set; }    
    }
}
