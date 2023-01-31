using ArchitectureExample.Services.Users.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchitectureExample.Services.Users.Core.DAL.Configurations;

internal sealed class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> modelBuilder)
    {
        modelBuilder
            .Property(q => q.Id)
            .ValueGeneratedNever();
        modelBuilder
            .Property(q => q.Email)
            .IsRequired();
        modelBuilder
            .Property(q => q.Password)
            .IsRequired();
        modelBuilder
            .Property(q => q.Salt)
            .IsRequired();
    }
}