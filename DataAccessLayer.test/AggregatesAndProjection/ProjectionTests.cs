using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.EfStructures.Edntities;
using DataAccessLayer.EfStructures.ViewModels;
using Xunit;

namespace DataAccessLayer.test.AggregatesAndProjection
{
   public class ProjectionTests:IDisposable
    {
        private readonly AdventureWorksContext _context;

        public ProjectionTests()
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
            var newObjectList = _context.Product
                .Where(p => p.MakeFlag ?? true)
                .Select(p => new
                {
                    p.ProductId,
                    p.Name,
                    p.ListPrice
                })
                .OrderBy(p => p.Name)
                .ToList();
            Assert.NotNull(newObjectList);
        }

        [Fact]
        public void Test2()
        {
            List<ProductViewModel> result = _context.Product
                .Where(p => p.MakeFlag ?? true)
                .Select(p => new ProductViewModel()
                {
                    Name = p.Name,
                    ProductId = p.ProductId,
                    Price = p.ListPrice
                }).OrderBy(p => p.Name)
                .ToList();

            Assert.NotNull(result);
        }
    }
}
