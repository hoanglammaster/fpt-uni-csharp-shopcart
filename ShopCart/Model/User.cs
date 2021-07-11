using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopCart.Model
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public User()
        {
        }
        public User(int id, string userName, string password, string address, string phone, string email)
        {
            Id = id;
            UserName = userName;
            Password = password;
            Address = address;
            Phone = phone;
            Email = email;
        }
    }
}