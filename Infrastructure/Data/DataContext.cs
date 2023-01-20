using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }
    //public  DbSet<Name_is_Class> Name_table (get; set;)
    public DbSet<Person> Persons { get; set; }

}
