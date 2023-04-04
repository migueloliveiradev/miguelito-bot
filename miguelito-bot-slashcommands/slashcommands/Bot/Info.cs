using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System.Diagnostics;
using System.Reflection;

namespace miguelito_bot_slashcommands.slashcommands.Bot
{
    internal class Info : ApplicationCommandModule
    {
        [SlashCommand("Info", "Bot ┇ Receive technical information about the bot")]
        public static async Task BotInfo(InteractionContext ctx)
        {
            string cs = Program.config[2];
            int pingdatabase = GetConnectionTimeout();

            DiscordEmbedBuilder embed = new()
            {
                Color = DiscordColor.CornflowerBlue,
                Description =
                $"> Online há: {Formatter.Timestamp(Process.GetCurrentProcess().StartTime, TimestampFormat.ShortTime)}\n" +
                $"> Desenvolvido por {Formatter.MaskedUrl("Miguel Oliveira", new Uri("https://migueloliveira.studio"), "pode chamar ele de gostoso")} " +
                $"e {Formatter.MaskedUrl("Paulo HS", new Uri("https://paulohpps.xyz"), "pergunta se o time dele tem mundial")}\n\n"
            };

            embed.AddField("**Discord**",
                $">  Em aproximadamente `{ctx.Client.Guilds.Count}` servidores.\n" +
                $">  Usado por aproximadamente `{ctx.Client.Guilds.Sum(p => p.Value.MemberCount)}` usuários.\n");
            embed.AddField("**Sistema**",
                $"> Versão do Dotnet: `{Environment.Version}`\n" +
                $"> Versão do Ubuntu: `{Environment.OSVersion}`\n" +
                $"> Versão do DSharpPlus: `{GetVersionDSharpPlus()}`\n" +
                $"> Espaço disponivel em Disco: `{GetSpaceDiskAvailableGb()}gb`\n" +
                $"> Espaço total do Disco: `{GetSpaceDiskUsageGb()}gb`\n" +
                $"> Uso de memória total: `{GetMemoryUsedMb()}mb`\n", true);
            embed.AddField("**ping**",
                $"> Ping WebSocket: `{ctx.Client.Ping}ms`\n" +
                $"> Ping API: `{ctx.Client.Ping}ms`\n" +
                $"> Ping Database: `{pingdatabase}ms`\n");
            await ctx.CreateResponseAsync(new DiscordInteractionResponseBuilder().AddEmbed(embed));
            return;
        }

        private static int GetConnectionTimeout()
        {
            return 0;
        }

        private static long GetMemoryUsedMb()
        {
            Process[] Processes = Process.GetProcesses();
            return Processes.Where(p => p.ProcessName.Contains("miguelito-bot")).Sum(p => p.WorkingSet64);
        }

        private static long GetSpaceDiskUsageGb()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            return drives.Sum(p => p.TotalSize / 1024 / 1024 / 1024);
        }

        private static long GetSpaceDiskAvailableGb()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            return drives.Sum(p => p.AvailableFreeSpace / 1024 / 1024 / 1024);
        }

        private static string GetVersionDSharpPlus()
        {
            Assembly assembly = typeof(DiscordClient).GetTypeInfo().Assembly;
            return assembly?.GetName()?.Version?.ToString()!;
        }
    }
}