using System;
using System.Collections.Generic;
using System.Text;

namespace FinancialPercentageChanges.Entities
{
    public class AutoSecurityTrade
    {
        public int Id{ get; set; }
        public int? SecurityId { get; set; }
        public DateTime? PurchaseDate{ get; set; }
        public DateTime? SellDate { get; set; }
        public decimal? PurchasePrice { get; set; }
        public decimal? SellPrice { get; set; }
        public int? SharesBought { get; set; }
        public int? PercentageLevel { get; set; }
    }
}
