using Microsoft.EntityFrameworkCore;
using FinancialPercentageChanges.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityProcessTasks.DbContexts
{
    interface ISecurityContext
    {
        DbSet<SecurityPercentageStatistics> SecurityTasks { get; set; }

        DbSet<Security> Securities { get; set; }
        DbSet<AutoSecurityTrade> AutoSecurityTrades { get; set; }

    }
}
