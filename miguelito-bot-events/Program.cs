using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Microsoft.Extensions.Logging;
using miguelito_bot_events.events;

namespace miguelito_bot_commands
{
    public class Program
    {
        public static string[] config { get; private set; }

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
            cliente.ClientErrored += this.Client_ClientErrored;
            cliente.GuildMemberAdded += events.Client_entrada;
            cliente.GuildCreated += events.entrada_server;
            cliente.ChannelCreated += events.Channel_Create;
            cliente.GuildDeleted += events.saida_server;
            cliente.MessageReactionAdded += events.registro;
            
            await cliente.ConnectAsync();
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