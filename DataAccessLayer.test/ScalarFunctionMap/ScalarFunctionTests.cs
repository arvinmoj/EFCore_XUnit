using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.EfStructures.Edntities;
using Xunit;

namespace DataAccessLayer.test.ScalarFunctionMap
{
   public class ScalarFunctionTests:IDisposable
    {
        private readonly AdventureWorksContext _context;

        public ScalarFunctionTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        [Fact]
        public void Test()
        {
            var productList = _context.Product
                .Where(p => AdventureWorksContext.GetStock(p.ProductId) > 4)
                .ToList();

            Assert.Equal(191,productList.Count);
        }
    }
}
