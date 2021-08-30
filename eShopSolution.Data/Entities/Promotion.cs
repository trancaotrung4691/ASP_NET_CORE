using eShopSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Entities
{
    public class Promotion : BaseEntityModel
    {
        public int? DiscountAmount { get; set; }
        public int ProductIds { get; set; }
        public int ProductCategoryIds { get; set; }
        public bool ApplyForAll { get; set; }
        public decimal? DiscountPercent { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public Status Status { set; get; }
        public string Name { set; get; }
    }
}