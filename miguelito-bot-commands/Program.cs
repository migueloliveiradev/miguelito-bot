using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;
using Microsoft.Extensions.Logging;
using miguelito_bot_commands.commands;
using miguelito_bot_commands.Utils;

namespace miguelito_bot_commands
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

        public static DiscordClient? Cliente { get; private set; }

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
                MinimumLogLevel = LogLevel.Error,
                Intents = DiscordIntents.All,
            };
            //I'm satoshi nakamoto
            Console.WriteLine("Loading...");
            Cliente = new DiscordClient(cfg);
            string[] prefix = new string[1];
            prefix[0] = "-";
            Cliente.UseInteractivity(new InteractivityConfiguration()
            {
                PollBehaviour = PollBehaviour.KeepEmojis,
                Timeout = TimeSpan.FromSeconds(20)
            });
            Console.WriteLine("Registering commands...");
            CommandsNextExtension cnt = Cliente.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = prefix,
                EnableDms = true,
                CaseSensitive = false,
                EnableDefaultHelp = false,
                EnableMentionPrefix = true,
                IgnoreExtraArguments = true,
            });
            Cliente.ClientErrored += Events.ClientErrored;
            Cliente.Ready += Events.OnReady;
            cnt.RegisterCommands<Main_commands>();
            cnt.RegisterCommands<commands_administration>();
            cnt.RegisterCommands<commands_matematica>();
            cnt.RegisterCommands<commands_configuration>();
            cnt.RegisterCommands<commands_twitter>();
            cnt.RegisterCommands<commands_api>();
            cnt.RegisterCommands<Commands_texto>();
            cnt.RegisterCommands<commands_games>();
            cnt.RegisterCommands<random_commands>();
            cnt.RegisterCommands<roll_commands>();
            cnt.RegisterCommands<Commands_Downloads>();
            cnt.RegisterCommands<Commands_Animals>();
            Console.WriteLine("Connecting...");
            await Cliente.ConnectAsync();
            Console.WriteLine("Running...");
            await Task.Delay(-1);
        }

        public static async Task Log(string nome)
        {
            DiscordGuild guild = await Program.Cliente.GetGuildAsync(880904935787601960);
            DiscordChannel channel = guild.GetChannel(Convert.ToUInt64(config[8]));
            await channel.SendMessageAsync($"Comando {nome} usado.");
            Console.WriteLine($"Comando {nome} usado. {DateTime.Now}");
        }
    }
}