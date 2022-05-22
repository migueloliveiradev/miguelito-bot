using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;
using Microsoft.Extensions.Logging;
using miguelito_bot_commands.commands;

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
        //Line 13  Api Weather API

        public static DiscordClient cliente { get; private set; }
        public static async Task Main(string[] args) => new Program().rodarBot().GetAwaiter().GetResult();

        public async Task rodarBot()
        {
            if (Environment.UserName == "Miguel Oliveira")
            {
                config = File.ReadAllLines($@"C:\Users\Miguel Oliveira\Documents\config.miguelito");
            }
            else
            {
                config = File.ReadAllLines($@"/home/ubuntu/github/config/config.miguelito");
            }
            DiscordConfiguration cfg = new DiscordConfiguration
            {
                Token = config[0],
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                ReconnectIndefinitely = true,
                GatewayCompressionLevel = GatewayCompressionLevel.Stream,
                MinimumLogLevel = LogLevel.Debug,
                Intents = DiscordIntents.All,
            };
            cliente = new DiscordClient(cfg);
            string[] prefix = new string[1];
            prefix[0] = "-";
            cliente.UseInteractivity(new InteractivityConfiguration()
            {
                PollBehaviour = PollBehaviour.KeepEmojis,
                Timeout = TimeSpan.FromSeconds(20)
            });
            CommandsNextExtension cnt = cliente.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = prefix,
                EnableDms = true,
                CaseSensitive = false,
                EnableDefaultHelp = false,
                EnableMentionPrefix = true,
                IgnoreExtraArguments = true,
            });
            cnt.CommandErrored += (cnext, e) =>
            {
                Console.WriteLine($"error: {e.Exception.Message}");
                return Task.CompletedTask;
            };
            cliente.ClientErrored += this.Client_ClientErrored;
            cnt.RegisterCommands<main_commands>();
            cnt.RegisterCommands<commands_administration>();
            cnt.RegisterCommands<commands_matematica>();
            cnt.RegisterCommands<commands_configuration>();
            cnt.RegisterCommands<commands_twitter>();
            cnt.RegisterCommands<commands_api>();
            cnt.RegisterCommands<commands_texto>();
            cnt.RegisterCommands<commands_games>();
            cnt.RegisterCommands<random_commands>();
            cnt.RegisterCommands<roll_commands>();
            await cliente.ConnectAsync();
            await Task.Delay(-1);
        }
        private async Task Client_ClientErrored(DiscordClient sender, ClientErrorEventArgs e)
        {
            DiscordMessageBuilder builder = new DiscordMessageBuilder();
            sender.Logger.LogError("o erro foi : ", e.Exception.Message);
            DiscordGuild guild = (DiscordGuild)await Program.cliente.GetGuildAsync(880904935787601960);
            DiscordChannel channel = guild.GetChannel(Convert.ToUInt64(config[8]));
            builder.WithContent($"Deu muito ruim \n {e.Exception.Message}");
            await builder.SendAsync(channel);
        }
        public static async Task log(string nome)
        {
            DiscordMessageBuilder builder = new DiscordMessageBuilder();
            DiscordGuild guild = (DiscordGuild)await Program.cliente.GetGuildAsync(880904935787601960);
            DiscordChannel channel = guild.GetChannel(Convert.ToUInt64(config[9]));
            builder.WithContent($"Comando {nome} usado.");
            await builder.SendAsync(channel);
        }
    }
}
