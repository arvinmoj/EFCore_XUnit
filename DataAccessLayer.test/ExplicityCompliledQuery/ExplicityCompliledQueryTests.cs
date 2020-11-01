using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.EfStructures.Edntities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DataAccessLayer.test.ExplicityCompliledQuery
{
    public class ExplicityCompliledQueryTests
    {
        private readonly AdventureWorksContext _context;

        public ExplicityCompliledQueryTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public static Func<AdventureWorksContext, decimal, bool, IEnumerable<Product>>
            GetProductBylistPriceAndMakeFlag =
                EF.CompileQuery((AdventureWorksContext context, decimal listPrice,
                    bool makeFlag) => 
                context.Product.Where(p=>p.ListPrice >=listPrice && (p.MakeFlag??false) == makeFlag)
                );


        [Fact]
        public void Test()
        {
            var productList = GetProductBylistPriceAndMakeFlag(_context, 0M, true).ToList();
        }
    }
}
