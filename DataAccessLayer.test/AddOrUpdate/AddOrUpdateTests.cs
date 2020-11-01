using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.EfStructures.Edntities;
using DataAccessLayer.EfStructures.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DataAccessLayer.test.AddOrUpdate
{
    public class AddOrUpdateTests:IDisposable
    {
        private readonly AdventureWorksContext _context;

        public AddOrUpdateTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        [Fact]
        public void Test1()
        {
            var product = TestHelpers.CreateProduct("15");
            _context.Product.AddOrUpdate(product, p => p.Name);
            var newEntry = _context.ChangeTracker.Entries<Product>()
                .FirstOrDefault(p => p.Entity.ProductId == product.ProductId);

            Assert.NotNull(newEntry);
            Assert.Equal(EntityState.Added,newEntry.State);

        }

        [Fact]
        public void Test2()
        {
            var product = TestHelpers.CreateProduct("15");
            product.Name = "BB Ball Bearing";
            _context.Product.AddOrUpdate(product, p => p.Name);
            var newEntry = _context.ChangeTracker.Entries<Product>()
                .FirstOrDefault(p => p.Entity.ProductId == product.ProductId);

            Assert.NotNull(newEntry);
            Assert.Equal(EntityState.Modified, newEntry.State);
        }

        [Fact]
        public void Test3()
        {
            var product = TestHelpers.CreateProduct("15");
            product.Name = "BB Ball Bearing";
            product.ProductNumber = "BE-2349";
            _context.Product.AddOrUpdate(product, p => p.Name,p=>p.ProductNumber);
            var newEntry = _context.ChangeTracker.Entries<Product>()
                .FirstOrDefault(p => p.Entity.ProductId == product.ProductId);

            Assert.NotNull(newEntry);
            Assert.Equal(EntityState.Modified, newEntry.State);
        }
    }
}
