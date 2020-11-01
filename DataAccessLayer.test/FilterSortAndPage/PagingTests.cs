using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.EfStructures.Edntities;
using Xunit;

namespace DataAccessLayer.test.FilterSortAndPage
{
    public class PagingTests:IDisposable
    {
        private readonly AdventureWorksContext _context;

        public PagingTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        [Fact]
        public void ShouldSkipFirst25Records()
        {
            //406
            var productList = _context.Product
                .Where(p => p.SellEndDate == null)
                .Skip(25)
                .ToList();
            Assert.NotNull(productList);
        }

        [Fact]
        public void ShouldtakeFirst25Records()
        {
            var productList = _context.Product
                .Where(p => p.SellEndDate == null)
                .OrderBy(p=>p.Name)
                .Take(25)
                .ToList();
            Assert.NotNull(productList);
        }

        [Fact]
        public void Paging()
        {
            int pageId = 2;
            int take = 25;
            int skip = (pageId-1) * take;

            var query = _context.Product.OrderByDescending(p => p.Name)
                .Skip(skip).Take(take).ToList();
            Assert.NotNull(query);
        }
    }
}
