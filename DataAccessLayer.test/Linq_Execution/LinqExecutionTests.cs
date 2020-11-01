using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.EfStructures.Edntities;
using Xunit;

namespace DataAccessLayer.test.Linq_Execution
{
    public class LinqExecutionTests:IDisposable
    {
        private readonly AdventureWorksContext _context;

        public LinqExecutionTests()
        {
            _context=new AdventureWorksContext();
        }

        [Fact]
        public void LinqNeverExecutesWithoutInteratingValues()
        {
            var query = _context.Product;
            var Name = "Food";
        }

        [Fact]
        public void LinqExecutionOnToList()
        {
            string filter = "sa";
            IQueryable<Product> query = _context.Product;

            if (filter != "")
            {
                query = query.Where(p => p.Name.Contains(filter));
            }

            var resut = query.ToList();
            Assert.True(resut.Count>1);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
