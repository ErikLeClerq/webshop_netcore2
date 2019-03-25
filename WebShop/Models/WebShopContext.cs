
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class WebShopContext
    {
        public string ConnectionString { get; set; }

        public WebShopContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public List<Product>GetAllProducts()
        {
            List<Product> products = new List<Product>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from products", conn);


                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Title = reader["Title"].ToString(),
                            Description = reader["Description"].ToString(),
                            ImagePath = reader["ImagePath"].ToString(),
                            Price = Convert.ToDecimal(reader["Price"]),
                            StockAmount = Convert.ToInt16(reader["StockAmount"]),
                        });
                    }
                }
            }
            return products;
        }

        public Product GetProductFromId(string id)
        {
            Product product = new Product();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from products where id = " + id, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        product = (new Product()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Title = reader["Title"].ToString(),
                            Description = reader["Description"].ToString(),
                            ImagePath = reader["ImagePath"].ToString(),
                            Price = Convert.ToDecimal(reader["Price"]),
                            StockAmount = Convert.ToInt16(reader["StockAmount"]),
                        });
                    }
                }
            }
            return product;
        }
    }
}
