using DSharpPlus;
using Microsoft.Extensions.Logging;

namespace miguelito_bot_log
{
    internal class Program
    {
        public static string[] config { get; private set; }

        public static DiscordClient cliente { get; private set; }

        public static async Task Main(string[] args) => new Program().RodarBot().GetAwaiter().GetResult();

        public async Task RodarBot()
        {
            if (Environment.UserName == "Miguel Oliveira")
            {
                config = File.ReadAllLines(@"C:\Users\Miguel Oliveira\Documents\config.miguelito");
            }
            else if (Environment.UserName == "Paulo")
            {
                config = File.ReadAllLines(@"C:\Users\Paulo\Documents\config.miguelito");
            }
            else
            {
                config = File.ReadAllLines(@"/home/ubuntu/github/config/config.miguelito");
            }
            Console.WriteLine("Setting...");
            DiscordConfiguration cfg = new()
            {
                Token = config[0],
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                ReconnectIndefinitely = true,
                GatewayCompressionLevel = GatewayCompressionLevel.Stream,
                MinimumLogLevel = LogLevel.Error,
                Intents = DiscordIntents.All,
            };
            Console.WriteLine("Loading...");
            cliente = new DiscordClient(cfg);
            cliente.ClientErrored += Events.Events.ClientErrorEvent;
            Console.WriteLine("Connecting...");
            await cliente.ConnectAsync();
            Console.WriteLine("Running...");
            await Task.Delay(-1);
        }
    }
}