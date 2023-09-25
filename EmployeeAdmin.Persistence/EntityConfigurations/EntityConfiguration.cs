using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EmployeeAdmin.Domain.Model;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EmployeeAdmin.Persistence.EntityConfigurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            // Set the table name for the entity
            builder.ToTable("EmployeePositions");


            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.Id).IsUnique();

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Registered_On)
                .HasColumnType("DateTime")
                .HasDefaultValueSql("getdate()")
                .ValueGeneratedOnAddOrUpdate()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            builder.HasMany(e => e.ChildPositions)
               .WithOne()
               .HasForeignKey(e => e.ParentId);



        }
    }
}