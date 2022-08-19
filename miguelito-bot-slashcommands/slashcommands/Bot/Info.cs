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
            Process[] Processes = Process.GetProcesses();
            DiscordEmbedBuilder embed = new()
            {
                Color = DiscordColor.CornflowerBlue,
                Description =
                $"> Online há: {Formatter.Timestamp(Process.GetCurrentProcess().StartTime, TimestampFormat.ShortTime)}\n" +
                $"> Desenvolvido por {Formatter.MaskedUrl("Miguel Oliveira", new Uri("https://migueloliveira.xyz"), "pode chamar ele de gostoso")} " +
                $"e {Formatter.MaskedUrl("Paulo HS", new Uri("https://paulohpps.xyz"), "pergunta se o time dele tem mundial")}\n\n"
            };
            
            embed.AddField("**Discord**", 
                $">  Em aproximadamente `{ctx.Client.Guilds.Count}` servidores.\n" +
                $">  Usado por aproximadamente `{ctx.Client.Guilds.Sum(p => p.Value.MemberCount)}` usuários.\n");
            embed.AddField("**Sistema**",
                $"> Versão do Dotnet: `{Environment.Version}`\n" +
                $"> Versão do Ubuntu: `{Environment.OSVersion}`\n" +
                $"> Versão do DSharpPlus: `{Utils.Variables.VersionDSharpPlus}`\n" +
                $"> Espaço disponivel em Disco: `{DriveInfo.GetDrives().Sum(p => p.AvailableFreeSpace / 1024 / 1024 / 1024)}gb`\n" +
                $"> Espaço total do Disco: `{DriveInfo.GetDrives().Sum(p => p.TotalSize / 1024 / 1024 / 1024)}gb`\n" +
                $"> Uso de memória total: `{Processes.Where(p => p.ProcessName.Contains("miguelito-bot")).Sum(p => p.WorkingSet64)}mb`\n", true);
            embed.AddField("**ping**",
                $"> Ping WebSocket: `{ctx.Client.Ping}ms`\n" +
                $"> Ping API: `{ctx.Client.Ping}ms`\n" +
                $"> Ping Database: `{pingdatabase}ms`\n");
            embed.AddField("**Software**",
                $"> Uso de memória total: `mb`\n" +
                $"> Uso de memória livre: `mb`\n" +
                $"> Uso de memória usada: `mb`\n");
            embed.WithAuthor("Miguelito", "https://miguelito.miguelsoft.com.br", ctx.Client.CurrentUser.AvatarUrl);
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                   new DiscordInteractionResponseBuilder().AddEmbed(embed));
            return;
        }
    }
}