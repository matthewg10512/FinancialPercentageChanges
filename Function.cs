using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using FinancialPercentageChanges.Entities;
using Microsoft.Extensions.DependencyInjection;
using SecurityProcessTasks;
using SecurityProcessTasks.Services.Repository;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace FinancialPercentageChanges
{


    public class Jobs
    {
        public string securityTradeType { get; set; }
    }


    public class Function
    {

        static string details = "";
        public ISecurityRepository _securityRepository { get; }


        public IServiceProvider services;
        public Function()
        {
            
            var resolver = new DependencyResolver(ConfigureServices);

            _securityRepository = resolver.ServiceProvider.GetService<ISecurityRepository>();
        }

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public List<AutoSecurityTrade> FunctionHandler(Jobs input, ILambdaContext context)
        {

           var securityTrades = _securityRepository.GetRecommendedSecurityTrades(input.securityTradeType);
            for (int i = 0; i < securityTrades.Count; i++){

                if(securityTrades[i].Id > 0)
                {
                    _securityRepository.UpdateSecurityTradeHistory(securityTrades[i]);
                    
                }
                else if (_securityRepository.SecurityTradesExists(securityTrades[i]))
                {
                    securityTrades.RemoveAt(i);
                    i--;
                }
                else
                {
                    _securityRepository.AddSecurityTradeHistory(securityTrades[i]);
                }

            }
            return securityTrades;

            //return input?.ToUpper();
        }



        // Register services with DI system
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ISecurityRepository, SecurityRepository>();
        }
    }
}
