using System;
using System.Collections.Generic;


namespace CRUDMarket
{
    public class SevenEleven
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }

        public SevenEleven(string name, int amount, double price)
        {
            Name = name;
            Amount = amount;
            Price = price;
        }
    }
}