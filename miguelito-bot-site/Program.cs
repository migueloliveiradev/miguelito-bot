using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace miguelito_bot_site
{
    public class Program
    {
        public static string[] Tokens = File.ReadAllLines(@"C:\Users\Miguel Oliveira\Documents\config-site.miguelito");

        public static DiscordClient Discord { get; private set; }

        public static async Task Main() => new Program().Run().GetAwaiter().GetResult();

        public async Task Run()
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder();
            builder.Services.AddControllersWithViews();
            WebApplication app = builder.Build();
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/404";
                    await next();
                }
            });
            DiscordConfiguration cfg = new()
            {
                Token = Tokens[2],
                ReconnectIndefinitely = true,
                MinimumLogLevel = LogLevel.Error,
                Intents = DiscordIntents.All,
            };
            Discord = new(cfg);
            Discord.ClientErrored += ClientErrored;
            static async Task ClientErrored(DiscordClient sender, ClientErrorEventArgs e)
            {
                DiscordChannel channel = await sender.GetChannelAsync(1000826689045147758);
                await channel.SendMessageAsync(e.Exception.Message);
                Console.WriteLine(e.Exception.Message);
            }
            await Discord.ConnectAsync();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            await app.RunAsync();
            await Task.Delay(-1);
        }
    }
}