namespace hmw;
using System;
using System.ComponentModel.DataAnnotations;

public class Order
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; }

    [Range(0, 10000)]
    public int Id { get; set; }

    [Range(0.01, 10000)]
    public double Price { get; set; }

    public Order(string name, double price, int id)
    {
        Name = name;
        Price = price;
        Id = id;
    }
}