using FinancialPercentageChanges.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecurityProcessTasks.Services.Repository
{
    public interface ISecurityRepository
    {
        SecurityPercentageStatistics GetTasks(string taskName);
        bool Save();

        void UpdateTasks(SecurityPercentageStatistics task);
        List<AutoSecurityTrade> GetRecommendedSecurityTrades(string securityTradeType);

        public void UpdateSecurityTradeHistory(AutoSecurityTrade securityTradeHistory);
        bool SecurityTradesExists(AutoSecurityTrade securityTradeHistory);
        void AddSecurityTradeHistory(AutoSecurityTrade securityTradeHistory);
    }
}
