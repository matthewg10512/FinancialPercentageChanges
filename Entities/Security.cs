using System;
using System.Collections.Generic;
using System.Text;

namespace FinancialPercentageChanges.Entities
{
    public class Security
    {

      public int  Id { get; set; }
      public string SecurityType { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public decimal? Dividend { get; set; }
        public DateTime? DividendDate { get; set; }
        public DateTime? EarningsDate { get; set; }
        public decimal CurrentPrice { get; set; }
        public int? IPOYear { get; set; }
        public string Sector { get; set; }
        public string Industry { get; set; }
        public decimal? YearLow { get; set; }
        public decimal? YearHigh { get; set; }
        public int? Volume { get; set; }
        public decimal? DayLow { get; set; }
        public decimal? DayHigh { get; set; }
        public DateTime? LastModified { get; set; }
        public bool preferred { get; set; }
        public decimal? PriorDayOpen { get; set; }
        public bool excludeHistorical { get; set; }
        public decimal? PercentageChange { get; set; }
}
}
