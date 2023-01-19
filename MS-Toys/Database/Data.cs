using MS_Toys.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace StoreAdministration
{
    internal class QuantityException : Exception
    {
        public QuantityException(string message) : base(message) { }
    }

    internal class ProductException : Exception
    {
        public ProductException(string message) : base(message) { }
    }

    internal class StoreDataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }

    internal class Data
    {
        public static void InsertProduct(StoreDataContext context, Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }

        public static void SellProducts(StoreDataContext context, string username, long productId, int quantity)
        {
            var product = context.Products.Find(productId);

            if (product == null)
            {
                throw new ProductException($"Product `{productId}` not found");
            }

            product.Quantity -= quantity;

            if (product.Quantity < 0)
            {
                throw new QuantityException($"Not enough: {product.Quantity + quantity}");
            }

            MakeTransaction(context, username, product.Name, quantity);
            context.SaveChanges();
        }

        public static void SignUserUp(StoreDataContext context, string username, string encryptedPassword, string firstName, string lastName)
        {
            User user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Username = username,
                EncryptedPassword = encryptedPassword  // FIXME this needs to be encrypted
            };

            context.Users.Add(user);
            context.SaveChanges();
        }

        public static User GetUser(StoreDataContext context, string username)
        {
            var users = (from user in context.Users
                        where user.Username == username
                        select user).ToArray();

            if(users.Length == 0)
            {
                return null;
            }

            return users[0];
        }
        
        public static List<User> GetAllUsernames(StoreDataContext context)
        {
            return context.Users.ToList();
        }

        private static void MakeTransaction(StoreDataContext context, string username, string name, int quantity)
        {
            var transaction = new Transaction
            {
                Username = username,
                Name = name,
                Quantity = quantity
            };

            context.Transactions.Add(transaction);
        }
    }
}
