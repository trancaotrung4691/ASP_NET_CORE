﻿using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Entities
{
    public class ProductTranslation : BaseEntityModel
    {
        public string Name { get; set; }
        public string Details { get; set; }
        public string SeoDescription { get; set; }
        public string SeoTitle { get; set; }
        public string SeoAlias { get; set; }
        public string Description { get; set; }
        public int ProductId { get; set; }
        public string LanguageId { get; set; }
        public Product Product { get; set; }
        public Language Language { get; set; }
    }
}