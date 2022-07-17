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
        public async Task TestCommand(InteractionContext ctx)
        {
            string cs = Program.config[2];
            using MySqlConnection con = new(cs);
            await con.OpenAsync();
            int pingdatabase = con.ConnectionTimeout;
            await con.CloseAsync();
            int StartTime = (DateTime.Now.Date - Process.GetCurrentProcess().StartTime).Days;
            int membros = 0;
            foreach (var server in ctx.Client.Guilds)
            {
                membros += server.Value.MemberCount;
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
                Title = ":cowboy: Informações",
                Color = DiscordColor.CornflowerBlue,
                Description =
                $"> Online há: `{StartTime} dias`\n" +
                $"> Em aproximadamente `{ctx.Client.Guilds.Count}` servidores.\n" +
                $"> Divertindo aproximadamente `{membros}` usuários.\n" +
                $"> Desenvolvido por {Formatter.MaskedUrl("Miguel Oliveira", new Uri("https://migueloliveira.xyz"), "pode chamar ele de gostoso")} " +
                $"e {Formatter.MaskedUrl("Paulo HS", new Uri("https://paulohpps.xyz"), "pergunta se o time dele tem mundial")}\n\n" +
                $"**SOFTWARE**\n" +
                $"> Versão do Dotnet: `{Environment.Version}`\n" +
                $"> Versão do DsharpPlus: `{Utils.Variables.VersionDSharpPlus}`\n" +
                $"> Uso de memória total: `{memory}mb`\n" +
                $"> Ping WebSocket: `{ctx.Client.Ping}ms`\n" +
                $"> Ping API: `{ctx.Client.Ping}ms`\n" +
                $"> Ping Database: `{pingdatabase}ms`"
            };
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                   new DiscordInteractionResponseBuilder().AddEmbed(embed));
            return;
        }
    }
}