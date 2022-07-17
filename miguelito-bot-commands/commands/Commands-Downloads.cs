using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Diagnostics;

namespace miguelito_bot_commands.commands
{
    internal class Commands_Downloads : BaseCommandModule
    {
        [Command("download"), Aliases("videodownload", "dl")]
        public async Task Download(CommandContext ctx, string url = "")
        {
            if (url != "")
            {
                DiscordMessageBuilder builder = new();
                builder.WithContent("Buscando... Por favor aguarde.");
                DiscordMessage menssage = await ctx.RespondAsync(builder);
                ProcessStartInfo processInfo = new(@"C:\Users\Miguel Oliveira\Downloads\youtube-dl.exe", $"--get-url {url}")
                {
                    CreateNoWindow = false,
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true
                };
                var process = Process.Start(processInfo);
                string link = null;
                process.OutputDataReceived += async (object sender, DataReceivedEventArgs e) =>
                {
                    await ctx.RespondAsync(e.Data.ToString());
                    link = e.Data;
                };
                process.BeginOutputReadLine();
                process.WaitForExit();
                process.Close();

                DiscordEmbedBuilder embed = new()
                {
                    Title = "Download",
                    Description = $"Video encontrado com sucesso {Formatter.MaskedUrl("Baixar", new Uri(link), null)}",
                    Color = DiscordColor.Green
                };
                builder.WithContent("");
                builder.AddEmbed(embed);
                await menssage.ModifyAsync(builder);
            }
            else
            {
                await ctx.RespondAsync("Por favor insira o link do video/audio desejado");
                return;
            }
        }
    }
}