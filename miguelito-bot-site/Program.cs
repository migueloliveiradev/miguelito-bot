using DSharpPlus;

namespace miguelito_bot_site
{
    public class Program
    {
        public static string[] Tokens = File.ReadAllLines(@"C:\Users\Miguel Oliveira\Documents\config-site.miguelito");

        public static DiscordClient Discord { get; private set; }

        public static async Task Main(string[] args) => new Program().Run().GetAwaiter().GetResult();

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
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                ReconnectIndefinitely = true,
                GatewayCompressionLevel = GatewayCompressionLevel.Stream,
                MinimumLogLevel = LogLevel.Debug,
                Intents = DiscordIntents.All,
            };
            Discord = new(cfg);
            await Discord.ConnectAsync();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Run();
            await Task.Delay(-1);
        }
    }
}