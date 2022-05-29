using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;
using Microsoft.Extensions.Logging;
using miguelito_bot_24h.events;


namespace miguelito_bot_24h
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

        public static DiscordClient cliente { get; private set; }
        public static async Task Main(string[] args) => new Program().rodarBot().GetAwaiter().GetResult();

        public async Task rodarBot()
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
            DiscordConfiguration cfg = new DiscordConfiguration
            {
                Token = config[0],
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                ReconnectIndefinitely = true,
                GatewayCompressionLevel = GatewayCompressionLevel.Stream,
                MinimumLogLevel = LogLevel.Critical,
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
                EnableDms = true,
                CaseSensitive = false,
                EnableDefaultHelp = false,
                EnableMentionPrefix = true,
                IgnoreExtraArguments = true,
            });
            cliente.ClientErrored += this.Client_ClientErrored;
            cnt.RegisterCommands<Main_Commands>();
            Events.remind();
            Console.WriteLine("Connecting...");
            await cliente.ConnectAsync();
            Console.WriteLine("Running...");
            await Task.Delay(-1);
        }
        private async Task Client_ClientErrored(DiscordClient sender, ClientErrorEventArgs e)
        {
            DiscordGuild guild = await sender.GetGuildAsync(880904935787601960);
            DiscordChannel channel = guild.GetChannel(Convert.ToUInt64(config[8]));
            await sender.SendMessageAsync(channel, $"{e.Exception.Message}");
        }
    }
}
