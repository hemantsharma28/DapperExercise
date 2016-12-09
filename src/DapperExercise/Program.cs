using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LogEntry;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using System.Diagnostics;

namespace DapperExercise
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int b = 0;
            //try
            //{
            //    int result = (7 / b);
            //}
            //catch (Exception ee)
            //{
            //    string errorMessage = LogEntry.ExceptionLog.CreateErrorMessage(ee);
            //    LogEntry.ExceptionLog.LogFileWrite(errorMessage);            
            //}
         

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
