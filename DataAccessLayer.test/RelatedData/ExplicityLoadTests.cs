using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.EfStructures.Edntities;
using Xunit;

namespace DataAccessLayer.test.RelatedData
{
   public class ExplicityLoadTests:IDisposable
    {
        private readonly AdventureWorksContext _context;

        public ExplicityLoadTests()
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
            Product product = _context.Product
                .FirstOrDefault(p => p.ProductModelId == 7);
            Assert.Null(product.ProductModel);
            _context.Entry(product).Reference(p => p.ProductModel).Load();
            Assert.NotNull(product.ProductModel);
        }

        [Fact]
        public void Test2()
        {
            Product product = _context.Product
                .FirstOrDefault(p => p.ProductModelId == 7);
            Assert.Null(product.ProductModel);
            _context.Entry(product).Reference(p => p.ProductModel).Load();
            _context.Entry(product.ProductModel)
                .Collection(pm=>pm.ProductModelIllustration).Load();

            Assert.NotNull(product.ProductModel.ProductModelIllustration);

        }

        [Fact]
        public void Test3()
        {
            ProductModel pm = _context.ProductModel
                .Find(7);
            int count = _context.Entry(pm)
                .Collection(p => p.Product).Query().Count();
            Assert.Equal(8,count);
        }
    }
}
