﻿using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain.Contacts;

namespace Infrastructure.EntityConfigurations.BusinessConfigurations.ContactConfigurations
{
    public class ContactConfiguration : BaseEntityConfiguration<Contact>
    {
        public ContactConfiguration()
        {
            ToTable("Contact");

            Property(c => c.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("ContactId");

            Property(c => c.Name)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnName("ContactName");

            Property(c => c.FirstName)
                .HasMaxLength(255)
                .HasColumnName("ContactFirstName");

            Property(c => c.LastName)
                .HasMaxLength(255)
                .HasColumnName("ContactLastName");

            Property(c => c.Email)
                .HasMaxLength(255)
                .HasColumnName("ContactEmail");

            Property(c => c.PhoneNumber)
                .HasMaxLength(40)
                .HasColumnName("ContactPhoneNumber");

            HasRequired(c => c.Partner)
                .WithMany(p => p.Contacts);
        }
    }
}
