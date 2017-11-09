﻿using Core.Interfaces;
using Core.Interfaces.Repositories;
using Core.Interfaces.Repositories.System;

namespace Infrastructure.Repositories.System.Message
{
    public class MessageRepository<TSpecificEntity> : Repository<TSpecificEntity>, IMessageRepository<TSpecificEntity> where TSpecificEntity : class, IEntityRoot, new()
    {
        public MessageRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
