using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.EfStructures.Edntities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DataAccessLayer.test.BasicSave
{
   public class BasicSaveTests
    {
        private readonly AdventureWorksContext _context;

        public BasicSaveTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        [Fact]
        public void test1()
        {
            var product = TestHelpers.CreateProduct("1");
           // _context.Add(product);
            Assert.Equal(EntityState.Detached,_context.Entry(product).State);
            Assert.Empty(_context.ChangeTracker.Entries());
        }

        [Fact]
        public void test2()
        {
            var productUnChanged = _context.Product.First();
            //productUnChanged.Name = "Iman";
            Assert.Equal(EntityState.Unchanged,_context.Entry(productUnChanged).State);
            Assert.Single(_context.ChangeTracker.Entries());
        }

        [Fact]
        public void test3()
        {
            var product1 = TestHelpers.CreateProduct("1");
            _context.Add(product1);
            Assert.Equal(EntityState.Added, _context.Entry(product1).State);
            Assert.Single(_context.ChangeTracker.Entries());

        }

        [Fact]
        public void test4()
        {
            var ProductToRemove = _context.Product
                .OrderBy(p => p.ProductId).First();
            _context.Remove(ProductToRemove);
            Assert.Equal(EntityState.Deleted, _context.Entry(ProductToRemove).State);

            Product productToRemove2 = TestHelpers.CreateProduct("1");
            productToRemove2.ProductId = 3;
            _context.Remove(productToRemove2);
            Assert.Equal(EntityState.Deleted, _context.Entry(productToRemove2).State);

        }

        [Fact]
        public void test5()
        {
            var product = _context.Product
                .OrderBy(p => p.ProductId).First();
            product.Name += "test";
            _context.Update(product);
            Assert.Equal(EntityState.Modified, _context.Entry(product).State);
            
        }

        [Fact]
        public void test6()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                var product = _context.Product
                    .OrderBy(p => p.ProductId).First();
                product.Name += "test";
                Assert.Equal(EntityState.Modified, _context.Entry(product).State);
                _context.SaveChanges();
                Assert.Equal(EntityState.Unchanged, _context.Entry(product).State);
                transaction.Rollback();
            }
        }

        [Fact]
        public void test7()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                var product = _context.Product
                    .OrderBy(p => p.ProductId).First();
                product.Name += "test";
                Assert.Equal(EntityState.Modified, _context.Entry(product).State);
                _context.SaveChanges(false);
                _context.ChangeTracker.AcceptAllChanges();
                Assert.Equal(EntityState.Unchanged, _context.Entry(product).State);
                transaction.Rollback();
            }
        }
    }
}
