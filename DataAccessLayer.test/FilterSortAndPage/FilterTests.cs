using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.EfStructures.Edntities;
using Xunit;

namespace DataAccessLayer.test.FilterSortAndPage
{
    public class FilterTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public FilterTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        [Fact]
        public void ShouldFindWithPrimaryKey()
        {
            Product product = _context.Product.Find(3);
            Assert.Equal("BB Ball Bearing",product.Name,ignoreCase:true);

            Product product2 = _context.Product.Find(3);
            Assert.Equal("BB Ball Bearing", product2.Name, ignoreCase: true);
        }

        [Fact]
        public void ShouldReturnNullIfPrimaryKeyIsNotFound()
        {
            Product product = _context.Product.Find(-1);
            Assert.Null(product);
        }

        [Fact]
        public void FilteringResultWithFindComplexKey()
        {
            ProductVendor pv = _context.ProductVendor.Find(2, 1688);

            Assert.Equal(5,pv.MaxOrderQty);
            Assert.Equal(3,pv.OnOrderQty);
        }

        [Fact]
        public void FilterWithSimpleWhereClause()
        {
            List<Product> productList = _context.Product
                .Where(p => (p.MakeFlag ?? true) && p.SellEndDate == null).ToList();
            Assert.Equal(163,productList.Count);
        }

        [Fact]
        public void FilterWithMultipleStatenebtWhereClauses()
        {
            //List<Product> productList = _context.Product
            //    .Where(p => (p.MakeFlag ?? true) && p.SellEndDate == null || p.ListPrice < 100M).ToList();

            List<Product> productList = _context.Product
                .Where(p => (p.MakeFlag ?? true) && (p.SellEndDate == null || p.ListPrice < 100M)).ToList();


            Assert.NotNull(productList);
        }

        [Fact]
        public void FilterWithBildingWhereClauses()
        {
            //var query = _context.Product
            //    .Where(p => p.SellEndDate == null).ToList();
            //query = query.Where(p => (p.MakeFlag ?? true)).ToList();
            //Assert.Equal(163,query.Count);

            IQueryable<Product> query = _context.Product
                .Where(p => p.SellEndDate == null);
            query = query.Where(p => (p.MakeFlag ?? true));
            Assert.Equal(163, query.ToList().Count);
        }

        [Fact]
        public void ShouldBeCarefulWithOrClauses()
        {
            var query2 = _context.Product
                .Where(p => (p.MakeFlag ?? true) || p.ListPrice != 0 && p.ListPrice < 100.00M && p.SellEndDate == null);
            Assert.NotNull(query2.ToList());

            var query = _context.Product
                .Where(p => (p.MakeFlag ?? true) || p.ListPrice != 0);
            query = query.Where(p => p.ListPrice < 100.00M && p.SellEndDate == null);
            //query = query.Where();

            var res = query.ToList();

        }

        [Fact]
        public void FilterWithListOfIds()
        {
            List<int> list=new List<int>(){1,3,5};

            var query = _context.Product.Where(p => list.Contains(p.ProductId)).ToList();
            Assert.Equal(2,query.Count);
        }

        [Fact]
        public void ShouldGetFirstRecord()
        {
            // Product product = _context.Product.FirstOrDefault(p => p.MakeFlag ?? true);
            Product product = _context.Product.Where(p => p.MakeFlag ?? true).FirstOrDefault();
            Assert.Equal("BB Ball Bearing",product.Name,ignoreCase:true);
        }

        [Fact]
        public void ShouldThrowWhenFirstFails()
        {
            Assert.Throws<InvalidOperationException>(
                ()=> _context.Product.First(p=>p.ProductId==-1)
            );
        }

        [Fact]
        public void ShouldGetTheLastRecord()
        {
            Product product = _context.Product.LastOrDefault(p => p.MakeFlag ?? true);
            Assert.Equal("Road-750 Black, 52",product.Name);
        }

        [Fact]
        public void ShouldReturnNullWhenRecordNotFound()
        {
            var product = _context.Product.FirstOrDefault(p => p.ProductId == -1);
            Assert.Null(product);
        }

        [Fact]
        public void ShouldReturnJustOneRecordWithSingle()
        {
            Product product = _context.Product.SingleOrDefault(p => p.MakeFlag ?? true);
            Assert.Equal("BB Ball Bearing",product.Name);
        }

        [Fact]
        public void ShouldFailIfMorethanOneRecordWithSingle()
        {
            Assert.Throws<InvalidOperationException>(() => 
            _context.Product.SingleOrDefault(p=>p.MakeFlag??true)
            );
        }
    }
}
