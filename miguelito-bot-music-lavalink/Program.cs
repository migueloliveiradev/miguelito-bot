using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Lavalink;
using DSharpPlus.Net;
using Microsoft.Extensions.Logging;
using miguelito_bot_music_lavalink.Commands;

namespace miguelito_bot_music_lavalink
{
    public class Program
    {
        public static string[] config { get; private set; }

        public static DiscordClient cliente { get; private set; }


        public static async Task Main(string[] args) => new Program().RodarBot().GetAwaiter().GetResult();

        public async Task RodarBot()
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
            Console.WriteLine("Setting...");
            DiscordConfiguration cfg = new()
            {
                Token = config[0],
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                ReconnectIndefinitely = true,
                GatewayCompressionLevel = GatewayCompressionLevel.Stream,
                MinimumLogLevel = LogLevel.Debug,
                Intents = DiscordIntents.All,
            };
            //I'm satoshi nakamoto
            Console.WriteLine("Loading...");
            cliente = new DiscordClient(cfg);
            string[] prefix = new string[1];
            prefix[0] = "-";
            Console.WriteLine("Registering commands...");
            CommandsNextExtension cnt = cliente.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = prefix,
                EnableDms = false,
                CaseSensitive = false,
                EnableDefaultHelp = false,
                EnableMentionPrefix = true,
                IgnoreExtraArguments = true,
            });
            ConnectionEndpoint endpoint = new()
            {
                Hostname = "lavalink.oops.wtf",
                Port = 443,
                Secured = true
            };

            LavalinkConfiguration lavalinkConfig = new()
            {
                Password = "www.freelavalink.ga", // From your server configuration.
                RestEndpoint = endpoint,
                SocketAutoReconnect = true,
                SocketEndpoint = endpoint
            };

            cnt.RegisterCommands<Commands_Music>();

            Console.WriteLine("Connecting...");
            LavalinkExtension lavalink = cliente.UseLavalink();
            await cliente.ConnectAsync();

            await lavalink.ConnectAsync(lavalinkConfig);
            Console.WriteLine("Running...");
            await Task.Delay(-1);
        }
    }
}