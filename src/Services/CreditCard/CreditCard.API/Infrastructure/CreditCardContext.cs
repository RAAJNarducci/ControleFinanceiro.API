using CreditCard.API.Infrastructure.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CreditCard.API.Infrastructure
{
    public class CreditCardContext : DbContext
    {
        public CreditCardContext(DbContextOptions<CreditCardContext> options) : base(options)
        {
        }

        public DbSet<Model.CreditCard> CreditCards { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CreditCardEntityTypeConfiguration());
        }
    }

    public class CatalogContextDesignFactory : IDesignTimeDbContextFactory<CreditCardContext>
    {
        public CreditCardContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CreditCardContext>()
                //.UseSqlServer("Server=sqldata;Database=ControleFinanceiro.CreditCard;User Id=sa;Password=Pass@word;");
                .UseSqlServer("Server=tcp:127.0.0.1,5433;Database=ControleFinanceiro.CreditCard;User Id=sa;Password=Pass@word;");

            return new CreditCardContext(optionsBuilder.Options);
        }
    }
}
