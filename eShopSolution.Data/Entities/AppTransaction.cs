using eShopSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Entities
{
    public class AppTransaction : BaseEntityModel
    {
        public DateTime TransactionDate { get; set; }
        public int ExternalTransactionId { get; set; }
        public int Amount { get; set; }
        public int Fee { get; set; }
        public string Result { get; set; }
        public string Message { get; set; }
        public TransactionStatus Status { get; set; }
        public string Providers { get; set; }
        public Guid UserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}