using System;
using System.Collections.Generic;
using System.Text;

namespace InverntoryManager.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Stock { get; set; }
        public string Owner { get; set; }

        public Item() { }
        public Item(string name, string imageUrl, int stock)
        {
            this.Name = name;
            this.ImageUrl = imageUrl;
            this.Stock = stock;
        }
    }
}
