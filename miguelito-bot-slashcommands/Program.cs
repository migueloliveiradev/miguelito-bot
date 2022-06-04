using DSharpPlus;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace miguelito_bot_slashcommands
{
    public class Program
    {
        public static string[] config { get; private set; }

        public static DiscordShardedClient cliente { get; private set; }

        public static ServiceProvider ServiceProvider { get; private set; }

        public static void Main() => MainAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        public static async Task MainAsync()
        {
            if (Environment.UserName == "Miguel Oliveira")
            {
                config = File.ReadAllLines($@"C:\Users\Miguel Oliveira\Documents\config.miguelito");
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
            Console.WriteLine("Registrando comandos...");

            foreach (SlashCommandsExtension slashCommandShardExtension in (await cliente.UseSlashCommandsAsync(slashCommandsConfiguration)).Values)
            {

                // slashCommandShardExtension.RegisterCommands(typeof(aaaaaa), 961782898153881650);

            }
            Console.WriteLine("comandos registrados.");
            await Task.Delay(-1);
        }
    }
}