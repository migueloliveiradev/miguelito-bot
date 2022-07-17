using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Microsoft.Extensions.Logging;
using miguelito_bot_events.events;

namespace miguelito_bot_commands
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
            cliente.ClientErrored += this.Client_ClientErrored;
            cliente.GuildMemberAdded += Events.GuildMemberAddEvent;
            cliente.GuildCreated += Events.GuildCreateEvent;
            cliente.ChannelCreated += Events.ChannelCreateEvent;
            cliente.GuildDeleted += Events.GuildDeleteEvent;
            cliente.MessageDeleted += Events.MessageDeleteEvent;
            cliente.MessageUpdated += Events.MessageUpdateEvent;
            cliente.Ready += Events.ReadyEvent;
            Console.WriteLine("Connecting...");
            await cliente.ConnectAsync();
            Console.WriteLine("Running...");
            await Task.Delay(-1);
        }

        private async Task Client_ClientErrored(DiscordClient sender, ClientErrorEventArgs e)
        {
            DiscordMessageBuilder builder = new DiscordMessageBuilder();
            sender.Logger.LogError("o erro foi : ", e.Exception.Message);
            DiscordGuild guild = (DiscordGuild)await sender.GetGuildAsync(880904935787601960);
            DiscordChannel channel = guild.GetChannel(Convert.ToUInt64(config[8]));
            builder.WithContent($"Deu muito ruim \n {e.Exception.Message}");
            await builder.SendAsync(channel);
        }
    }
}