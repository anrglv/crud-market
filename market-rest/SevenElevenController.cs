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
        private readonly string connString =
            "Host=localhost;Username=postgres;Port=5432;Password=postgres;Database=seveneleven";

        [HttpPost]
        public IActionResult AddProduct(SevenEleven product)
        {
            try
            {
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(
                           "INSERT INTO market (name, amount, price) VALUES (@name, @amount, @price)", conn))
                {
                    cmd.Parameters.AddWithValue("name", product.Name);
                    cmd.Parameters.AddWithValue("amount", product.Amount);
                    cmd.Parameters.AddWithValue("price", product.Price);
                    cmd.ExecuteNonQuery();
                }

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
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                using (var cmd = new NpgsqlCommand("SELECT * FROM market WHERE name = @name", conn))
                {
                    cmd.Parameters.AddWithValue("name", name);

                    using var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string productName = reader.GetString(1);
                        int amount = reader.GetInt32(2);
                        double price = reader.GetDouble(3);

                        var product = new SevenEleven(productName, amount, price);
                        return Ok(product);
                    }
                    else
                    {
                        return NotFound("Товар не найден.");
                    }
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
                using var conn = new NpgsqlConnection(connString);
                conn.Open();
                
                using (var cmdCheck = new NpgsqlCommand("SELECT COUNT(*) FROM market WHERE name = @name", conn))
                {
                    cmdCheck.Parameters.AddWithValue("name", name);
                    int count = Convert.ToInt32(cmdCheck.ExecuteScalar());

                    if (count == 0)
                    {
                        return NotFound("Товар не найден.");
                    }
                }
                
                using (var cmdUpdate =
                       new NpgsqlCommand(
                           "UPDATE market SET name = @newName, amount = @amount, price = @price WHERE name = @oldName",
                           conn))
                {
                    cmdUpdate.Parameters.AddWithValue("oldName", name);
                    cmdUpdate.Parameters.AddWithValue("newName", updatedProduct.Name);
                    cmdUpdate.Parameters.AddWithValue("amount", updatedProduct.Amount);
                    cmdUpdate.Parameters.AddWithValue("price", updatedProduct.Price);

                    int rowsAffected = cmdUpdate.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return Ok("Информация о товаре успешно обновлена.");
                    }
                    else
                    {
                        return StatusCode(500, "Ошибка при обновлении информации о товаре.");
                    }
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
            try
            {
                using var conn = new NpgsqlConnection(connString);
                conn.Open();
                
                using (var cmdCheck = new NpgsqlCommand("SELECT COUNT(*) FROM market WHERE name = @name", conn))
                {
                    cmdCheck.Parameters.AddWithValue("name", name);
                    int count = Convert.ToInt32(cmdCheck.ExecuteScalar());

                    if (count == 0)
                    {
                        return NotFound("Товар не найден.");
                    }
                }
                
                using (var cmdDelete =
                       new NpgsqlCommand(
                           "DELETE FROM market WHERE name = @name", conn ))
                {
                    cmdDelete.Parameters.AddWithValue("name", name);

                    int rowsAffected = cmdDelete.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return Ok("Товар успешно удален.");
                    }
                    else
                    {
                        return StatusCode(500, "Ошибка при удалении товара.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Произошла ошибка: {ex.Message}");
            }
        }
    }
}
