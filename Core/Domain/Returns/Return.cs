﻿using System;
using Core.Interfaces;

namespace Core.Domain.Returns
{
    public class Return : BaseEntity, IEntityRoot
    {
        public DateTime CalculatedTime { get; set; }
    }
}