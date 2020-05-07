using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Chess.ApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(logging => 
                {
                    logging.ClearProviders();
                    logging.AddFile(@"Logs/Chess-ApiGateway-{Date}.txt");
                    logging.AddEventLog(cfg =>
                    {
                        cfg.LogName = "Chess.ApiGatewayLogs";
                        cfg.SourceName = "Chess.ApiGateway";
                    });
                    logging.AddConsole();
                });
    }
}
