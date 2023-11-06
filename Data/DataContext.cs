using Microsoft.EntityFrameworkCore;
using Whitees.Models;

namespace Whitees.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Shirt> Shirts { get; set; }

    }

}




