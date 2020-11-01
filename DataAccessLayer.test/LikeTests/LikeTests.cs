using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.EfStructures.Edntities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DataAccessLayer.test.LikeTests
{
   public class LikeTests:IDisposable
    {
        private readonly AdventureWorksContext _context;

        public LikeTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        [Fact]
        public void ShouldGetProductsUsingLike()
        {
            var products = _context.Product
                .Where(p => p.Name.Contains("Carnkarm"))
                .ToList();

            //var products = _context.Product
            //    .Where(p => p.Name.StartsWith("Carnkarm"))
            //    .ToList();

            Assert.Equal(3,products.Count);
        }

        [Fact]
        public void ShouldGetProductsUsingLike2()
        {
            var products = _context.Product
                .Where(p => EF.Functions.Like(p.Name, "%Carnkarm%"))
                .ToList();
            Assert.NotNull(products);
        }
    }
}
