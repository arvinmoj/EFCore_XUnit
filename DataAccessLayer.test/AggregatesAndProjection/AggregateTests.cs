using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.EfStructures.Edntities;
using Xunit;

namespace DataAccessLayer.test.AggregatesAndProjection
{
    public class AggregateTests:IDisposable
    {
        private readonly AdventureWorksContext _context;

        public AggregateTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        [Fact]
        public void Sum()
        {
            decimal _sum = _context.Product
                .Sum(p=>p.ListPrice);
            Assert.True(_sum>0);
        }

        [Fact]
        public void Count()
        {
            int count = _context.Product
                .Count(p => p.ListPrice!=0);
            Assert.True(count > 0);
        }

        [Fact]
        public void Average()
        {
            decimal avg = _context.Product
                .Where(p => p.ListPrice != 0)
                .Average(p => p.ListPrice);

            //decimal avg = _context.Product
            //    .Where(p => p.ListPrice != 0)
            //    .Average(p =>(decimal?) p.ListPrice)??0;
            Assert.Equal(635.319029M, avg);
        }

        [Fact]
        public void Max()
        {
            decimal max = _context.Product.Where(p => p.ListPrice != 0)
                .Max(p => p.ListPrice);
            Assert.True(max > 0);
        }

        [Fact]
        public void Min()
        {
            decimal min = _context.Product.Where(p => p.ListPrice != 0)
                .Min(p => p.ListPrice);
            Assert.True(min > 0);
        }
    }
}
