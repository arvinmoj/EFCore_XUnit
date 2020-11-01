using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.EfStructures.Edntities;
using Xunit;

namespace DataAccessLayer.test.ClientServerEval
{
    public class ClientServerEvalTests:IDisposable
    {
        private readonly AdventureWorksContext _context;

        public ClientServerEvalTests()
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
            Assert.Throws<InvalidOperationException>(() => _context.Product.Last());
        }

        [Fact]
        public void Test2()
        {
            var product = _context.Product.OrderByDescending(p => p.ProductId).First();
        }
    }
}
