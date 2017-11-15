﻿using System;
using System.ComponentModel;

namespace Service.Dtos.Asset
{
    public class AssetDto
    {
        public int Id { get; set; }

        [DisplayName("Asset Name")]
        public string Name { get; set; }

        [DisplayName("Asset ISIN")]
        public string Isin { get; set; }

        [DisplayName("Issue Date")]
        public DateTime IssueDate { get; set; }

        [DisplayName("Maturity Date")]
        public DateTime MaturityDate { get; set; }

        [DisplayName("Currency")]
        public int CurrencyId { get; set; }

        [DisplayName("Face Value")]
        public decimal FaceValue { get; set; }
    }
}