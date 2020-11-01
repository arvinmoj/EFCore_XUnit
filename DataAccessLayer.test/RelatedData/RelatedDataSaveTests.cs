using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.EfStructures.Edntities;
using Xunit;

namespace DataAccessLayer.test.RelatedData
{
    public class RelatedDataSaveTests:IDisposable
    {
        private readonly AdventureWorksContext _context;

        public RelatedDataSaveTests()
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
            using (var trans=_context.Database.BeginTransaction())
            {
                var catCount = _context.ProductCategory.Count();
                var subCatCount = _context.ProductSubcategory.Count();

                var productCategory = new ProductCategory()
                {
                    Name = "New Category",
                    ProductSubcategory = new List<ProductSubcategory>()
                    {
                        new ProductSubcategory()
                        {
                            Name = "New Sub Category 1"
                        },
                        new ProductSubcategory()
                        {
                            Name = "New Sub Category 2"
                        },
                        new ProductSubcategory()
                        {
                            Name = "New Sub Category 3"
                        },
                    }
                };
                _context.ProductCategory.Add(productCategory);
                _context.SaveChanges();
                Assert.Equal(catCount+1,_context.ProductCategory.Count());
                Assert.Equal(subCatCount+3,_context.ProductSubcategory.Count());

                _context.ProductCategory.Remove(productCategory);
                _context.ProductSubcategory.RemoveRange(productCategory.ProductSubcategory);
                _context.SaveChanges();

                trans.Rollback();

            }
        }
    }
}
