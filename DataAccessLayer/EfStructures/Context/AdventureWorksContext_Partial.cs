using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EfStructures.Edntities
{
    public partial class AdventureWorksContext
    {
        [DbFunction(FunctionName = "ufnGetStock",Schema = "dbo")]
        public static int GetStock(int productId)
        {
            throw new NotImplementedException();
        }


        internal void AddModelCreatingConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasQueryFilter(p => p.SellEndDate == null);

            //modelBuilder.HasDbFunction(this.GetType().GetMethod("GetStock"),
            //    options =>
            //    {
            //        options.HasSchema("dbo");
            //        options.HasName("ufnGetStock");
            //    }
            //);
        }
    }
}
