using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.EfStructures.Edntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Xunit;

namespace DataAccessLayer.test.Tracking
{
   public class TrackingTests:IDisposable
    {
        private readonly AdventureWorksContext _context;

        public TrackingTests()
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
            //var product = _context.Product.Find(1);
            //var product2 = _context.Product.Find(1);
            //product.Name = "Iman";
            //_context.SaveChanges();


            Product product = _context.Product.First();

            List<EntityEntry<Product>> entries =
                _context.ChangeTracker.Entries<Product>().ToList();
            Assert.Single(entries);

            var name = product.Name;
            product.Name = "foo";

            Assert.Equal(name,
                entries[0].OriginalValues[nameof(Product.Name)].ToString()
                );
            Assert.Equal(product.Name,
                entries[0].CurrentValues[nameof(Product.Name)].ToString()
            );

            Assert.Equal(EntityState.Modified,entries[0].State);
        }

        [Fact]
        public void test2()
        {
            Product product = _context.Product.AsNoTracking().First();
            List<EntityEntry<Product>> entries =
                _context.ChangeTracker.Entries<Product>().ToList();
            Assert.Empty(entries);
        }

        [Fact]
        public void Test3()
        {
            _context.ChangeTracker.QueryTrackingBehavior =
                QueryTrackingBehavior.NoTracking;
            Product product = _context.Product.First();
            List<EntityEntry<Product>> entries =
                _context.ChangeTracker.Entries<Product>().ToList();
            Assert.Empty(entries);

          //  _context.Entry().State = EntityState.Modified;

        }
    }
}
