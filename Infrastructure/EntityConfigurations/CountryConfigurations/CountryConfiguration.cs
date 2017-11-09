﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Core.Domain.BaseData.Countries;

namespace Infrastructure.EntityConfigurations.CountryConfigurations
{
    public class CountryConfiguration : EntityTypeConfiguration<Country>
    {
        public CountryConfiguration()
        {
            Property(c => c.IsDeleted).HasColumnName("IsDeleted");

            HasKey(c => c.Id);

            ToTable("Country");

            Property(c => c.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("CountryId");

            Property(c => c.Name)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnName("CountryName");

            Property(c => c.Code)
                .HasMaxLength(2)
                .HasColumnName("CountryCode");

            HasRequired(b => b.Currency)
                .WithMany(c => c.Countries)
                .WillCascadeOnDelete(false);

            Property(c => c.IsEnabled)
                .HasColumnName("CountryEnabled");
        }
    }
}
