using DSharpPlus;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace miguelito_bot_slashcommands
{
    public class Program
    {
        public static string[] config { get; private set; }
        //Line 0   Token Miguelito
        //Line 1   Token Bot Test (privated)
        //Line 2   Conection Database
        //Line 3   Token Api Virus Total
        //Line 4   Token Api Google translate
        //Line 5   Token Api Cutt.ly
        //Line 6   Token Api HG-finance
        //Line 7   ID Channel Discord Erro
        //Line 8   ID Channel Log
        //Line 9   Api Twitter CONSUMER_KEY
        //Line 10  Api Twitter CONSUMER_SECRET
        //Line 11  Api Twitter ACCESS_TOKEN
        //Line 12  Api Twitter ACCESS_TOKEN_SECRET
        //Line 13  Api Custom Search key
        //Line 14  APi Pexels
        //Line 15  APi Spotify
        //Line 16  APi Spotify Secret

        public static DiscordShardedClient cliente { get; private set; }

        public static ServiceProvider ServiceProvider { get; private set; }

        public static void Main() => MainAsync().GetAwaiter().GetResult();

        public static async Task MainAsync()
        {
            if (Environment.UserName == "Miguel Oliveira")
            {
                config = File.ReadAllLines($@"C:\Users\Miguel Oliveira\Documents\config.miguelito");
            }
            else if (Environment.UserName == "Paulo")
            {
                config = File.ReadAllLines($@"C:\Users\Paulo\Documents\config.miguelito");
            }
            else
            {
                config = File.ReadAllLines($@"/home/ubuntu/github/config/config.miguelito");
            }
            ServiceCollection services = new();
            ServiceProvider = services.BuildServiceProvider();
            DiscordConfiguration discordConfiguration = new()
            {
                Intents = DiscordIntents.All,
                Token = config[0],
                LoggerFactory = ServiceProvider.GetService<ILoggerFactory>()
            };

            SlashCommandsConfiguration slashCommandsConfiguration = new()
            {
                Services = ServiceProvider
            };

            cliente = new(discordConfiguration);
            await cliente.StartAsync();
            cliente.Ready += Utilities.Events.OnReady;
            var slash = await cliente.UseSlashCommandsAsync();
            Console.WriteLine("Registrando comandos...");

            slash.RegisterCommands<slashcommands.Messages.Say>(880904935787601960);
            slash.RegisterCommands<slashcommands.Search.Search>(880904935787601960);
            slash.RegisterCommands<slashcommands.Technology.Phone>(880904935787601960);
            //slash.RegisterCommands<slashcommands.Manage.Create>(880904935787601960);
            slash.RegisterCommands<slashcommands.Embed.Embed>(880904935787601960);
            //slash.RegisterCommands<slashcommands.Math_.Basics>(880904935787601960);
            //slash.RegisterCommands<slashcommands.Math_.Bhaskara>(880904935787601960);
            //slash.RegisterCommands<slashcommands.Math_.Potentiation>(880904935787601960);
            //slash.RegisterCommands<slashcommands.Math_.Root>(880904935787601960);
            slash.RegisterCommands<slashcommands.Moderation.Ban>(880904935787601960);
            slash.RegisterCommands<slashcommands.Moderation.Unban>(880904935787601960);
            slash.RegisterCommands<slashcommands.Moderation.Kick>(880904935787601960);
            slash.RegisterCommands<slashcommands.Ramdom_Img.Animals.Cats>(880904935787601960);
            slash.RegisterCommands<slashcommands.Ramdom_Img.Animals.Dogs>(880904935787601960);
            slash.RegisterCommands<slashcommands.Ramdom_Img.Animals.Duck>(880904935787601960);
            slash.RegisterCommands<slashcommands.Ramdom_Img.Animals.Fox>(880904935787601960);
            slash.RegisterCommands<slashcommands.Ramdom_Img.Objects.Coffee>(880904935787601960);
            slash.RegisterCommands<slashcommands.Reactions.Dance>(880904935787601960);
            slash.RegisterCommands<slashcommands.Reactions.Happy>(880904935787601960);
            slash.RegisterCommands<slashcommands.Reactions.Hug>(880904935787601960);
            slash.RegisterCommands<slashcommands.Reactions.Kiss>(880904935787601960);
            slash.RegisterCommands<slashcommands.Reactions.Poke>(880904935787601960);
            slash.RegisterCommands<slashcommands.Reactions.Wink>(880904935787601960);
            slash.RegisterCommands<slashcommands.User.User>(880904935787601960);
            slash.RegisterCommands<slashcommands.Server.Server>(880904935787601960);
            Console.WriteLine("Comandos registrados.");
            await Task.Delay(-1);
        }
    }
}