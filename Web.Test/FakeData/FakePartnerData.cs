﻿using System.Collections.Generic;
using Core.Domain.Partners;

namespace Web.Tests.FakeData
{
    public static class FakePartnerData
    {
        public static IList<Partner> GetList()
        {
            return new List<Partner>
            {
                new Partner
                {
                    Id = 1,
                    IsDeleted = false,
                    Name = "TESTPARTNER1",
                    Number = "PARTNERNUMBER1"
                },
                new Partner
                {
                    Id = 2,
                    IsDeleted = false,
                    Name = "TESTPARTNER1",
                    Number = "PARTNERNUMBER1"
                }
            };
        }
    }
}