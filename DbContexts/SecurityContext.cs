using Microsoft.EntityFrameworkCore;
using FinancialPercentageChanges.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityProcessTasks.DbContexts
{
    public class SecurityContext : DbContext
    {


        public SecurityContext(DbContextOptions<SecurityContext> options)
           : base(options)
        {

        }


        public DbSet<SecurityPercentageStatistics> SecurityPercentageStatistics { get; set; }
        public DbSet<Security> Securities { get; set; }
        public DbSet<AutoSecurityTrade> AutoSecurityTrades { get; set; }

    }
}
