using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.Models
{
    public class DataProductsContextMock : DataProductsContext
    {
        public DataProductsContextMock()
        {
            Products.Add(new Products { Nombre = "Tenis", Description = "Running", PriceClient = 2.3m, IsEnabled = true });
            Products.Add(new Products { Nombre = "Tenis", Description = "Running", PriceClient = 2.3m, IsEnabled = true });
            Products.Add(new Products { Nombre = "Tenis", Description = "Running", PriceClient = 2.3m, IsEnabled = true });
            Products.Add(new Products { Nombre = "Tenis", Description = "Running", PriceClient = 2.3m, IsEnabled = true });
            Products.Add(new Products { Nombre = "Tenis", Description = "Running", PriceClient = 2.3m, IsEnabled = true });
            Products.Add(new Products { Nombre = "Tenis", Description = "Running", PriceClient = 2.3m, IsEnabled = true });
            Products.Add(new Products { Nombre = "Tenis", Description = "Running", PriceClient = 2.3m, IsEnabled = true });
            Products.Add(new Products { Nombre = "Tenis", Description = "Running", PriceClient = 2.3m, IsEnabled = true });
            Products.Add(new Products { Nombre = "Tenis", Description = "Running", PriceClient = 2.3m, IsEnabled = true });
            Products.Add(new Products { Nombre = "Tenis", Description = "Running", PriceClient = 2.3m, IsEnabled = true });

            ImagesProduct.Add(new ImagesProduct { IdImageProduct = 1, Decription = "description", IsEnabled = "1" });
            ImagesProduct.Add(new ImagesProduct { IdImageProduct = 2, Decription = "description", IsEnabled = "1" });
            ImagesProduct.Add(new ImagesProduct { IdImageProduct = 3, Decription = "description", IsEnabled = "1" });
            ImagesProduct.Add(new ImagesProduct { IdImageProduct = 4, Decription = "description", IsEnabled = "1" });
            ImagesProduct.Add(new ImagesProduct { IdImageProduct = 5, Decription = "description", IsEnabled = "1" });
            ImagesProduct.Add(new ImagesProduct { IdImageProduct = 6, Decription = "description", IsEnabled = "1" });
            ImagesProduct.Add(new ImagesProduct { IdImageProduct = 7, Decription = "description", IsEnabled = "1" });
            ImagesProduct.Add(new ImagesProduct { IdImageProduct = 8, Decription = "description", IsEnabled = "1" });
            ImagesProduct.Add(new ImagesProduct { IdImageProduct = 9, Decription = "description", IsEnabled = "1" });
            ImagesProduct.Add(new ImagesProduct { IdImageProduct = 10, Decription = "description", IsEnabled = "1" });

            SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase();
            }
        }
    }
}
