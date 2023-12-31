﻿namespace MyApplication.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public string? Color { get; set; }
        public bool IsPublish { get; set; }
        public string Expire { get; set; }
        public DateTime? Publish { get; set; }
    }
}
