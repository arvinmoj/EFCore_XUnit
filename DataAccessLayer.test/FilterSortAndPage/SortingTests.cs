using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.EfStructures.Edntities;
using Xunit;

namespace DataAccessLayer.test.FilterSortAndPage
{
   public class SortingTests:IDisposable
    {
        private readonly AdventureWorksContext _context;

        public SortingTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        [Fact]
        public void ShouldSortData()
        {
            IOrderedQueryable<Product> query = _context.Product
                .Where(p => p.ListPrice != 0)
                .OrderBy(p => p.ListPrice)
                .ThenByDescending(p=>p.DaysToManufacture)
                .ThenBy(p=>p.Name);
            var productList = query.ToList();
            Assert.NotEmpty(productList);
        }
    }
}
