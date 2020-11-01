using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.EfStructures.Edntities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DataAccessLayer.test.RelatedData
{
    public class RelatedDataTests:IDisposable
    {
        private readonly AdventureWorksContext _context;

        public RelatedDataTests()
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
            List<Product> list = _context.Product
                .Where(p => p.ProductModelId == 7).ToList();

            Assert.Null(list[0].ProductModel);
        }

        [Fact]
        public void Test2()
        {
            List<Product> list = _context.Product
                .Where(p => p.ProductModelId == 7)
                .Include(p => p.ProductModel)
                .ThenInclude(p=>p.ProductModelIllustration)
                .ToList();
            Assert.NotNull(list[0].ProductModel);
        }

        [Fact]
        public void Test3()
        {
            var list = _context.Product
                .Where(p => p.ProductModelId == 7)
                .Include(p => p.ProductModel)
                .ThenInclude(p => p.ProductModelIllustration);

            var productList = list.Select(p => new
            {
                p.ProductId,
                p.Name,
                p.ListPrice
            }).ToList();

            Assert.NotNull(productList);

        }

    }
}
