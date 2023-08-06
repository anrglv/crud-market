using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System;
using System.Collections.Generic;

namespace CRUDMarket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarketController : ControllerBase
    {
        private readonly MarketContext _context;

        public MarketController(MarketContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult AddProduct(SevenEleven product)
        {
            try
            {
                _context.Market.Add(product);
                _context.SaveChanges();

                return Ok("Товар успешно добавлен.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Произошла ошибка: {ex.Message}");
            }
        }

        [HttpGet("{name}")]
        public IActionResult GetProductInfo(string name)
        {
            try
            {
                var product = _context.Market.FirstOrDefault(p => p.Name == name);

                if (product != null)
                {
                    return Ok(product);
                }
                else
                {
                    return NotFound("Товар не найден.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Произошла ошибка: {ex.Message}");
            }
        }
        [HttpPut("{name}")]
        public IActionResult UpdateProductInfo(string name, SevenEleven updatedProduct)
        {
            try
            {
                var product = _context.Market.FirstOrDefault(p => p.Name == name);
                if (product != null)
                {
                    // Внесение изменений в объект сущности
                    product.Name = updatedProduct.Name;
                    product.Amount = updatedProduct.Amount;
                    product.Price = updatedProduct.Price;

                    _context.SaveChanges(); // Сохранение изменений в базе данных

                    return Ok("Товар успешно обновлен.");
                }
                else
                {
                    return NotFound("Товар не найден.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Произошла ошибка: {ex.Message}");
            }
        }
        [HttpDelete("{name}")]
        public IActionResult DeleteProduct(string name)
        {
            {
                try
                {
                    var product = _context.Market.FirstOrDefault(p => p.Name == name);
                    if (product != null)
                    {
                        _context.Market.Remove(product);
                        _context.SaveChanges();
                        return Ok("Товар успешно удален.");
                    }
                    else
                    {
                        return NotFound("Товар не найден.");
                    }

                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Произошла ошибка: {ex.Message}");
                }
            }

        }
    }
}    
