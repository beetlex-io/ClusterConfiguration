using BeetleX.FastHttpApi;
using BeetleX.FastHttpApi.Admin;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FastHttpApi.ClusterConfiguration
{
    class Program
    {

        static void Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<HttpServerHosted>();
                });
            builder.Build().Run();
        }
    }

    public class HttpServerHosted : IHostedService
    {
        private HttpApiServer mApiServer;

        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            mApiServer = new HttpApiServer();
            mApiServer.Debug();
            mApiServer.Options.WebSocketMaxRPS = 100;
            mApiServer.Register(typeof(Program).Assembly);
            mApiServer.Register(typeof(_Admin).Assembly);
            mApiServer.Open();
            return Task.CompletedTask;
        }

        public virtual Task StopAsync(CancellationToken cancellationToken)
        {
            mApiServer.Dispose();
            return Task.CompletedTask;
        }
    }
}
