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
}
