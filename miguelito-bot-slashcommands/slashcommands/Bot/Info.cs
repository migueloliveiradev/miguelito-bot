using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace miguelito_bot_slashcommands.slashcommands.Bot
{
    internal class Info : ApplicationCommandModule
    {
        [SlashCommand("Info", "Bot ┇ Receive technical information about the bot")]
        public async Task BotInfo(InteractionContext ctx)
        {
            string cs = Program.config[2];
            using MySqlConnection con = new(cs);
            await con.OpenAsync();
            int pingdatabase = con.ConnectionTimeout;
            await con.CloseAsync();
            int membros = 0;
            foreach (DiscordGuild server in ctx.Client.Guilds.Values)
            {
                membros += server.MemberCount;
            }
            Process[] Processes = Process.GetProcesses();
            long memory = 0;
            foreach (Process process in Processes)
            {
                if (process.ProcessName.Contains("miguelito-bot"))
                {
                    memory += process.WorkingSet64 / (1024 * 1024);
                }
            }
            DiscordEmbedBuilder embed = new()
            {
                Color = DiscordColor.CornflowerBlue,
                Description =
                $"> Online há: {Formatter.Timestamp(Process.GetCurrentProcess().StartTime, TimestampFormat.ShortTime)}\n" +
                $"> Em aproximadamente `{ctx.Client.Guilds.Count}` servidores.\n" +
                $"> Usado por aproximadamente `{membros}` usuários.\n" +
                $"> Desenvolvido por {Formatter.MaskedUrl("Miguel Oliveira", new Uri("https://migueloliveira.xyz"), "pode chamar ele de gostoso")} " +
                $"e {Formatter.MaskedUrl("Paulo HS", new Uri("https://paulohpps.xyz"), "pergunta se o time dele tem mundial")}\n\n" +
                $"**SOFTWARE**\n" +
                $"> Versão do Dotnet: `{Environment.Version}`\n" +
                $"> Versão do DSharpPlus: `{Utils.Variables.VersionDSharpPlus}`\n" +
                $"> Uso de memória total: `{memory}mb`\n" +
                $"> Ping WebSocket: `{ctx.Client.Ping}ms`\n" +
                $"> Ping API: `{ctx.Client.Ping}ms`\n" +
                $"> Ping Database: `{pingdatabase}ms`\n"
            };
            embed.WithAuthor("Miguelito", "https://miguelito.miguelsoft.com.br", ctx.Client.CurrentUser.AvatarUrl);
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                   new DiscordInteractionResponseBuilder().AddEmbed(embed));
            return;
        }
    }
}