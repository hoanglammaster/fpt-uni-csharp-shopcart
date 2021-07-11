using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopCart.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Decimal Price { get; set; }
        public int Quantity { get; set; }
        public string  ImageURL { get; set; }
        public int Order { get; set; }
        public Book()
        {
        }
        public Book(int id, string name, decimal price, int quantity, string imageURL)
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
            ImageURL = imageURL;
        }

    }
}