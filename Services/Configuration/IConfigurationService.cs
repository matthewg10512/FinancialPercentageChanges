﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinancialPercentageChanges.Services.Configuration
{
   public interface IConfigurationService
    {
        IConfiguration GetConfiguration();
    }
}
