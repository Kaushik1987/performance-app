﻿using System.Data.Entity.ModelConfiguration;
using Core.Domain.Identity;

namespace Infrastructure.EntityConfigurations.SystemConfigurations.IdentityConfigurations
{
    public class UserLoginConfiguration : EntityTypeConfiguration<UserLogin>
    {
        public UserLoginConfiguration()
        {
            Map(c =>
            {
                c.ToTable("UserLogins");
                c.Properties(p => new
                {
                    p.LoginProvider,
                    p.ProviderKey,
                    p.UserId
                });
            }).HasKey(p => new
            {
                p.LoginProvider,
                p.ProviderKey,
                p.UserId
            });
        }

    }
}
