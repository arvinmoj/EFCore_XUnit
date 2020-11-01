using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.EfStructures.Edntities;
using DataAccessLayer.EfStructures.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DataAccessLayer.test.RawQuery
{
    public class RawSqlWithLinqTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public RawSqlWithLinqTests()
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
            var schemaAndTableName = _context.GetSqlServerTableName<Product>();
            Assert.Equal("Production.Product",schemaAndTableName);
        }

        [Fact]
        public void Test2()
        {
            var schemaAndTableName = _context.GetSqlServerTableName<Product>();
            var sqlQuery = $"Select * From {schemaAndTableName}";
            List<Product> productList = _context.Product.FromSql(sqlQuery).ToList();
            Assert.Equal(406,productList.Count);
        }

        [Fact]
        public void Test3()
        {
            var schemaAndTableName = _context.GetSqlServerTableName<Product>();
            var sqlQuery = $"Select * From {schemaAndTableName}";
            List<Product> productList = _context.Product
                .IgnoreQueryFilters()
                .FromSql(sqlQuery).ToList();
            Assert.Equal(504, productList.Count);
        }

        [Fact]
        public void Test4()
        {
            var schemaAndTableName = _context.GetSqlServerTableName<Product>();
            var sqlQuery = $"Select * From {schemaAndTableName}";
            List<Product> productList = _context.Product
                .FromSql(sqlQuery)
                .Where(p => p.ProductModelId == 7)
                .Include(p => p.ProductModel).ThenInclude(p => p.ProductModelIllustration)
                .Include(p => p.ProductCostHistory)
                .ToList();
            Assert.NotNull(productList[0].ProductModel);
        }
    }
}
