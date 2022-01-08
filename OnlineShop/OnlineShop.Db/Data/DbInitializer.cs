using OnlineShop.Db.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Db.Data
{
    public class DbInitializer
    {
        public static void Initialize(DatabaseContext databaseContext)
        {
            databaseContext.Database.EnsureCreated();

            // Look for any productss.
            if (databaseContext.Products.Any())
            {
                return;   // DB has been seeded
            }

            var products = new List<Product>()
            {
                 new Product(){
                                Name = "Полевая мышь",
                                Cost = 400,
                                Description = "Любит чистоту",
                                ImagePath = "/images/полевка3.jpg",
                                Category = ProductCategory.Wild
                               },
                 new Product(){
                                Name = "Попугай 'Ара'",
                                Cost = 4000,
                                Description = "Знает три анекдота",
                                ImagePath = "/images/ара1.jpg",
                                Category = ProductCategory.Exotic
                              },
                 new Product(){
                                Name = "Рыбка Дори",
                                Cost = 900,
                                Description = "Не злопамятная",
                                ImagePath =  "/images/рыбка2.jpg",
                                Category = ProductCategory.Wild
                              },
                 new Product(){
                                 Name = "Джунгарский хомяк",
                                 Cost = 700,
                                 Description = "Очень вонючий",
                                 ImagePath = "/images/хомяк1.jpg",
                                 Category = ProductCategory.Domestic
                             },
                 new Product(){
                                Name = "Кошка 'Сфинкс'",
                                Cost = 500,
                                Description = "Очень злая",
                                ImagePath = "/images/сфинкс2.jpg",
                                Category = ProductCategory.Domestic
                              },
                 new Product(){
                               Name = "Мадагаскарский таракан",
                               Cost = 100,
                               Description = "Очень умный",
                               ImagePath ="/images/таракан1.jpg",
                               Category = ProductCategory.Exotic
                             },
                 new Product(){
                               Name = "Собака 'Корги'",
                               Cost = 1500,
                               Description = "Очень дружелюбный",
                               ImagePath = "/images/корги1.jpg",
                               Category = ProductCategory.Domestic
                             },
                 new Product(){
                                Name = "Ленивец",
                                Cost = 5000,
                                Description = "Самое ленивое создание",
                                ImagePath = "/images/ленивец1.jpg",
                                Category = ProductCategory.Exotic
                             },
                 new Product(){
                                Name = "Муравей",
                                Cost = 100,
                                Description = "Самый сильный",
                                ImagePath = "/images/муравей1.jpg",
                                Category = ProductCategory.Wild
                             },
                 new Product(){
                                Name = "Енот Маша",
                                Cost = 5000,
                                Description = "Очень любит мягкие игрушки",
                                ImagePath =  "/images/енот1.jpg",
                                Category = ProductCategory.Exotic
                             },
                 new Product(){
                               Name = "Белка 'Летяга'",
                               Cost = 900,
                               Description = "Скрасит ваши серые будни",
                               ImagePath =  "/images/белка1.jpg",
                               Category = ProductCategory.Wild
                              }
            };

            foreach (Product product in products)
            {
                databaseContext.Products.Add(product);
            }

            databaseContext.SaveChanges();
        }
    }
}
