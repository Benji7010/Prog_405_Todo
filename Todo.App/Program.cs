using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Todo.Common.Services;

namespace Todo.App
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddTransient<ITaskService, TaskService>();
            await builder.Build().RunAsync();
        }
    }
}
