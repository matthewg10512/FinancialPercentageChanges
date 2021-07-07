using SecurityProcessTasks.DbContexts;
using FinancialPercentageChanges.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
namespace SecurityProcessTasks.Services.Repository
{
    //FinancialPercentageChanges
    public class SecurityRepository : ISecurityRepository
    {

        public SecurityContext _context { get; }

        public SecurityRepository(SecurityContext context)
        {
            _context = context;
        }


        public SecurityPercentageStatistics GetTasks(string taskName)
        {
            return _context.SecurityPercentageStatistics.FirstOrDefault();// x => x.TaskName == taskName);
        }

        public bool Save()
        {

            return (_context.SaveChanges() >= 0);
        }

        public void UpdateTasks(SecurityPercentageStatistics task)
        {
            _context.SecurityPercentageStatistics.Update(task);
            Save();
        }

        public List<AutoSecurityTrade> GetRecommendedSecurityTrades(string securityTradeType)
        {
            DateTime currentDay = DateTime.Now;
            DateTime priorDay = currentDay.AddDays(-1);
            decimal perLoss = (decimal).4;


            switch (securityTradeType)
            {

                case "averagedrop":
                default:
                    return _context.Securities
                .Join(_context.SecurityPercentageStatistics, x => x.Id, y => y.SecurityId, (security, tradeHistory) => new { security, tradeHistory })
                .Where(x => x.security.Volume > 100000 && x.security.LastModified > priorDay && x.security.preferred == true &&
                ((x.security.CurrentPrice - x.security.PriorDayOpen) / x.security.PriorDayOpen) * 100 < x.tradeHistory.AverageDrop - perLoss)
                .Select(x =>
                new AutoSecurityTrade
                {
                    SecurityId = x.security.Id,
                    PurchaseDate = currentDay,
                    PurchasePrice = x.security.CurrentPrice,
                    PercentageLevel = 1,
                    SharesBought = 1
                }).ToList();
                case "percent15":
                    perLoss = (decimal).2;
                    return _context.Securities
                .Join(_context.SecurityPercentageStatistics, x => x.Id, y => y.SecurityId, (security, tradeHistory) => new { security, tradeHistory })
                .Where(x => x.security.Volume > 100000 && x.security.LastModified > priorDay && x.security.preferred == true &&
                ((x.security.CurrentPrice - x.security.PriorDayOpen) / x.security.PriorDayOpen) * 100 < x.tradeHistory.Percent15 - perLoss)
                .Select(x =>
                new AutoSecurityTrade
                {
                    SecurityId = x.security.Id,
                    PurchaseDate = currentDay,
                    PurchasePrice = x.security.CurrentPrice,
                    PercentageLevel = 2,
                    SharesBought = 2
                }).ToList();
                case "percent10":
                    perLoss = (decimal).1;
                    return _context.Securities
                .Join(_context.SecurityPercentageStatistics, x => x.Id, y => y.SecurityId, (security, tradeHistory) => new { security, tradeHistory })
                .Where(x => x.security.Volume > 100000 && x.security.LastModified > priorDay && x.security.preferred == true &&
                ((x.security.CurrentPrice - x.security.PriorDayOpen) / x.security.PriorDayOpen) * 100 < x.tradeHistory.Percent10 - perLoss)
                .Select(x =>
                new AutoSecurityTrade
                {
                    SecurityId = x.security.Id,
                    PurchaseDate = currentDay,
                    PurchasePrice = x.security.CurrentPrice,
                    PercentageLevel = 3,
                    SharesBought = 3
                }).ToList();
                case "percent5":
                    perLoss = (decimal)0;
                    return _context.Securities
                .Join(_context.SecurityPercentageStatistics, x => x.Id, y => y.SecurityId, (security, tradeHistory) => new { security, tradeHistory })
                .Where(x => x.security.Volume > 100000 && x.security.LastModified > priorDay && x.security.preferred == true &&
                ((x.security.CurrentPrice - x.security.PriorDayOpen) / x.security.PriorDayOpen) * 100 < x.tradeHistory.Percent5 - perLoss)
                .Select(x =>
                new AutoSecurityTrade
                {
                    SecurityId = x.security.Id,
                    PurchaseDate = currentDay,
                    PurchasePrice = x.security.CurrentPrice,
                    PercentageLevel = 4,
                    SharesBought = 4
                }).ToList();
                case "checkSellPoint":
                    decimal percentRaise = (decimal)1.01;

                    return _context.Securities
                        .Join(_context.AutoSecurityTrades, 
                        x => x.Id, y => y.SecurityId, (security, tradeHistory) => new { security, tradeHistory })
                        .Where(x=> x.security.CurrentPrice > x.tradeHistory.PurchasePrice * percentRaise && x.tradeHistory.SellDate == null)
                        .Select(x =>
                new AutoSecurityTrade
                {   Id =x.tradeHistory.Id,
                    SecurityId = x.security.Id,
                    PurchaseDate = x.tradeHistory.PurchaseDate,
                    PurchasePrice = x.tradeHistory.PurchasePrice,
                    PercentageLevel = x.tradeHistory.PercentageLevel,
                    SharesBought = x.tradeHistory.SharesBought,
                    SellDate = currentDay,
                    SellPrice = x.security.CurrentPrice
                }).ToList();


            }


           
        }

        public bool SecurityTradesExists(AutoSecurityTrade securityTradeHistory)
        {
            DateTime? purchaseDate = securityTradeHistory.PurchaseDate;
                if (purchaseDate.HasValue)
            {


                purchaseDate = new DateTime(purchaseDate.Value.Year, purchaseDate.Value.Month, purchaseDate.Value.Day, 0, 0, 0);

               var securityTrade = _context.AutoSecurityTrades.Where(x => x.SecurityId == securityTradeHistory.SecurityId
                && x.PurchaseDate > purchaseDate && x.PercentageLevel == securityTradeHistory.PercentageLevel).FirstOrDefault();

                if (securityTrade == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
                
            }
            else
            {
                return true;
            }

        }

        public void UpdateSecurityTradeHistory(AutoSecurityTrade securityTradeHistory)
        {

            var entry = _context.AutoSecurityTrades.First(e => e.Id == securityTradeHistory.Id);
            _context.Entry(entry).CurrentValues.SetValues(securityTradeHistory);
            Save();
        }

        public void AddSecurityTradeHistory(AutoSecurityTrade securityTradeHistory)
        {

            _context.AutoSecurityTrades.Add(securityTradeHistory);
            Save();
        }
    }
}
