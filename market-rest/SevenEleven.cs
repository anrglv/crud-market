using System;
using System.Collections.Generic;


namespace CRUDMarket
{
    public class SevenEleven
    {
        public string Name { get; set; } // помогает для чтения и записи 
        public int Amount { get; set; } // помогает для чтения и записи 
        public double Price { get; set; } // помогает для чтения и записи 

        public SevenEleven(string name, int amount, double price) // создание объекта 
        {
            Name = name;
            Amount = amount;
            Price = price;
        }
    }
}