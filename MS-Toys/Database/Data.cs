﻿using MS_Toys.Models;
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

        public static List<Product> SearchProduct(string name)
        {
            using (var context = new StoreDataContext())
            {
                var query = (
                    from product in context.Products
                    where product.Name.ToLower().Contains(name.ToLower())
                    select product
                );

                return query.ToList();
            }
        }

        public static void AddProductQuantity(long id, int quantity)
        {
            using (var context = new StoreDataContext())
            {
                var productSearched = (
                    from product in context.Products
                    where product.Id == id
                    select product
                ).ToArray()[0];

                productSearched.Quantity += quantity;

                context.SaveChanges();
            }
        }

        public static string GetUserPassword(string username)
        {
            using (var context = new StoreDataContext())
            {
                var userSearched = (
                    from user in context.Users
                    where user.Username == username
                    select user
                ).ToArray()[0];

                return userSearched.EncryptedPassword;  // FIXME decrypted
            }
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

        public static void DeleteUser(string username)
        {
            using (var context = new StoreDataContext())
            {
                var userSearched = (
                    from user in context.Users
                    where user.Username == username
                    select user
                ).ToArray()[0];

                context.Users.Remove(userSearched);
                context.SaveChanges();
            }
        }

        public static List<Transaction> GetTransactions()
        {
            using (var context = new StoreDataContext())
            {
                var query = (
                    from transaction in context.Transactions
                    select transaction
                );

                return query.ToList();
            }
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
