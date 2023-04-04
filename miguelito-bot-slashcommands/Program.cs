using DSharpPlus;
using DSharpPlus.SlashCommands;

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

        public static DiscordClient cliente { get; private set; }

        public static void Main() => MainAsync().GetAwaiter().GetResult();

        public static async Task MainAsync()
        {
            DotNetEnv.Env.Load();
            DiscordConfiguration discordConfiguration = new()
            {
                Intents = DiscordIntents.All,
                Token = config[0],
            };

            cliente = new(discordConfiguration);

            cliente.Ready += Utils.Events.OnReady;
            await cliente.ConnectAsync();
            SlashCommandsExtension slash = cliente.UseSlashCommands();
            slash.SlashCommandErrored += Utils.Events.SlashCommandError;
            slash.SlashCommandExecuted += Utils.Events.SlashCommandExecuted;

            Console.WriteLine("Registrando comandos...");
            slash.RegisterCommands<slashcommands.Bot.Info>(880904935787601960);
            /*slash.RegisterCommands<slashcommands.Search.Pokemon>();
            slash.RegisterCommands<slashcommands.Messages.Say>();
            slash.RegisterCommands<slashcommands.Search.Search>();
            slash.RegisterCommands<slashcommands.Technology.Phone>();
            //slash.RegisterCommands<slashcommands.Manage.Create>();
            slash.RegisterCommands<slashcommands.Embed.Embed>();
            slash.RegisterCommands<slashcommands.Math_.Basics>();
            slash.RegisterCommands<slashcommands.Math_.Bhaskara>();
            slash.RegisterCommands<slashcommands.Math_.Potentiation>();
            slash.RegisterCommands<slashcommands.Math_.Root>();
            slash.RegisterCommands<slashcommands.Moderation.Ban>();
            slash.RegisterCommands<slashcommands.Moderation.Unban>();
            slash.RegisterCommands<slashcommands.Moderation.Kick>();
            slash.RegisterCommands<slashcommands.Ramdom_Img.Animals.Cats>();
            slash.RegisterCommands<slashcommands.Ramdom_Img.Animals.Dogs>();
            slash.RegisterCommands<slashcommands.Ramdom_Img.Animals.Duck>();
            slash.RegisterCommands<slashcommands.Ramdom_Img.Animals.Fox>();
            slash.RegisterCommands<slashcommands.Ramdom_Img.Objects.Coffee>();
            slash.RegisterCommands<slashcommands.Reactions.Dance>();
            slash.RegisterCommands<slashcommands.Reactions.Happy>();
            slash.RegisterCommands<slashcommands.Reactions.Hug>();
            slash.RegisterCommands<slashcommands.Reactions.Kiss>();
            slash.RegisterCommands<slashcommands.Reactions.Poke>();
            slash.RegisterCommands<slashcommands.Reactions.Wink>();
            slash.RegisterCommands<slashcommands.User.User>();
            slash.RegisterCommands<slashcommands.Server.Server>();*/
            Console.WriteLine("Comandos registrados.");
            await Task.Delay(-1);
        }
    }
}