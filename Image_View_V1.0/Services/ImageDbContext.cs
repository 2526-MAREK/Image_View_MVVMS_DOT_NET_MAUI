/**
* @file ImageDbContext.cs
* @brief Contains the ImageDbContext class, which represents the database context.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Image_View_V1._0.Services
{
    public class ImageDbContext : DbContext
    {
        public DbSet<ImageToProcess> Images { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=DataBaseForImageProcessed.db;");
        }
    }
}
