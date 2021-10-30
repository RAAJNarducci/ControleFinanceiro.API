using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreditCard.API.Infrastructure.EntityConfiguration
{
    public class CreditCardEntityTypeConfiguration
        : IEntityTypeConfiguration<Model.CreditCard>
    {
        public void Configure(EntityTypeBuilder<Model.CreditCard> builder)
        {
            builder.ToTable("CreditCard");

            builder.HasKey(cc => cc.Id);

            builder.Property(cc => cc.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(cc => cc.Active)
                .IsRequired();

            builder.Property(cc => cc.ExpirationDate)
                .IsRequired();

            builder.Property(cc => cc.Flag)
                .IsRequired();

            builder.Property(cc => cc.Number)
                .IsRequired();

            builder.Property(cc => cc.VerificationCode)
                .IsRequired();
        }
    }
}
