using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.VoiceNext;
using Microsoft.Extensions.Logging;


namespace miguelito_bot_music
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
            string[] prefix = new string[1];
            prefix[0] = "-";

            CommandsNextExtension cnt = cliente.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = prefix,
                EnableDms = true,
                CaseSensitive = false,
                EnableDefaultHelp = false,
                EnableMentionPrefix = true,
                IgnoreExtraArguments = true,
            });
            cliente.ClientErrored += events_music.Client_ClientErrored;
            cliente.VoiceStateUpdated += events_music.Client_VoiceStateUpdate;
            
            cliente.UseVoiceNext();
            cnt.RegisterCommands<music_commands>();
            events_music.VerificarBot();
            await cliente.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}