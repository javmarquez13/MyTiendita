using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyTiendita.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTiendita.Persistence.Database.Configurations.Seeds
{
    internal class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product() { id = 1, Barcode=null, Description = "Ruffles Verdes",  Capture = null,  Created = new DateTime(2023, 11, 21), Updated = new DateTime(2023, 11, 21), IsActive = true },
                new Product() { id = 2, Barcode = null, Description = "Doritos Verdes", Capture = null, Created = new DateTime(2023, 11, 21), Updated = new DateTime(2023, 11, 21), IsActive = true },
                new Product() {id = 3, Barcode = null, Description = "Sabritas Verdes", Capture = null, Created = new DateTime(2023, 11, 21), Updated = new DateTime(2023, 11, 21), IsActive = true },
                new Product() {id = 4, Barcode = null, Description = "Chetos Verdes", Capture = null, Created = new DateTime(2023, 11, 21), Updated = new DateTime(2023, 11, 21), IsActive = true },
                new Product() {id = 5, Barcode = null, Description = "Rancheritos", Capture = null, Created = new DateTime(2023, 11, 21), Updated = new DateTime(2023, 11, 21), IsActive = true },
                new Product() {id = 6, Barcode = null, Description = "Chetos Flama", Capture = null, Created = new DateTime(2023, 11, 21), Updated = new DateTime(2023, 11, 21), IsActive = true }
            );         
        }
    }
}
