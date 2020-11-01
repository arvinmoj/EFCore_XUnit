using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.EfStructures.Edntities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DataAccessLayer.test.GlobalQueryFilters
{
    public class GlobalQueryFilterTests:IDisposable
    {
        private readonly AdventureWorksContext _context;

        public GlobalQueryFilterTests()
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
            var query = _context.Product.ToList();
            Assert.NotNull(query);
        }

        [Fact]
        public void Test2()
        {
            var query = _context.Product.IgnoreQueryFilters().ToList();
            Assert.NotNull(query);
        }
    }
}
