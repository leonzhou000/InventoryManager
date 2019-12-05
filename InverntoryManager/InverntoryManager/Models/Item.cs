using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace InverntoryManager.Models
{
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string ImageUrl { get; set; }
        [MaxLength(255)]
        public int Stock { get; set; }
        [MaxLength(255)]
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
