using System.ComponentModel.DataAnnotations; // Добавим пространство имен для атрибута [Key]

namespace CRUDMarket
{
    public class SevenEleven
    {
        [Key] // Укажем, что это свойство является первичным ключом
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }

        // Пустой конструктор для Entity Framework Core
        public SevenEleven() { }

        public SevenEleven(string name, int amount, double price)
        {
            Name = name;
            Amount = amount;
            Price = price;
        }
    }
}