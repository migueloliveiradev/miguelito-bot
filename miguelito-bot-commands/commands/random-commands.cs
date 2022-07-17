using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using miguelito_bot_commands.Utils;

namespace miguelito_bot_commands.commands
{
    internal class random_commands : BaseCommandModule
    {
        /*[Command("perfil")]
        public async Task perfil(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            HttpClient http = new();
            Stream img2 = await http.GetStreamAsync(ctx.User.AvatarUrl);
            Image webImage = Image.FromStream(img2);
            Bitmap img = new(1000, 1000);
            Graphics g = Graphics.FromImage(img);
            Graphics g2 = Graphics.FromImage(webImage);
            g2.DrawRectangle(new Pen(Brushes.Black), new Rectangle(10, 10, 100, 100));
            g2.Save();
            g.FillRectangle(Brushes.Black, 0, 0, 1000, 1000);
            g.DrawString(ctx.User.Username, new Font("Arial", 30), Brushes.White, new PointF(500, 50));
            g.DrawImage(webImage, new Point(50, 50));
            g.Save();
            img.Save($"C://{ctx.User.Id}", ImageFormat.Png);
            FileStream perfil = File.OpenRead($"C://{ctx.User.Id}");
            DiscordMessageBuilder message = new()
            {
                Content = "aaaaa"
            };
            message.WithFile("perfil.png", perfil);
            await ctx.RespondAsync(message);
        }*/

        [Command("bolo"), Aliases("mulango")]
        public async Task bolo(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new()
            {
                Color = DiscordColor.Red,
                ImageUrl = "https://c.tenor.com/t4zUkk1R-UIAAAAd/pica-pau.gif"
            };
            await ctx.RespondAsync(ctx.User.Mention, embed);
            await Program.Log("bolo");
        }

        [Command("portugues"), Aliases("pt")]
        public async Task portugues(CommandContext ctx, DiscordUser user = null)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed;
            if (user == null)
            {
                user = ctx.User;
                embed = new DiscordEmbedBuilder
                {
                    Color = Variables.Cores(),
                    ImageUrl = "https://media.discordapp.net/attachments/949836472985460766/950064401732493372/fala_portugues.png",
                    Description = ctx.User.Mention,
                };
            }
            else
            {
                embed = new DiscordEmbedBuilder
                {
                    Color = Variables.Cores(),
                    ImageUrl = "https://media.discordapp.net/attachments/949836472985460766/950064401732493372/fala_portugues.png?width=587&height=559",
                    Description = " ei " + user.Mention,
                };
            }
            await ctx.RespondAsync(ctx.User.Mention, embed);
            await Program.Log("pt");
        }

        [Command("viniccius13"), Aliases("13", "vini")]
        public async Task viniccius(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Red,
                ImageUrl = "https://cdn.discordapp.com/attachments/949838790774636644/949838931581612062/vinicius13.gif"
            };
            await ctx.RespondAsync(embed);
            await Program.Log("viniccius13");
        }

        [Command("migueloliveira")]
        public async Task migueloliveira(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Red,
                ImageUrl = "https://pbs.twimg.com/profile_images/1520827797761105920/vJvgL4Tw_400x400.jpg",
            };
            embed.WithAuthor("Miguel Oliveira", "https://migueloliveira.xyz", "https://pbs.twimg.com/profile_images/1486866093604016131/jzbj65Ku_400x400.jpg");
            await ctx.RespondAsync($"{ctx.User.Mention} Falou o nome do patrão?", embed);
            await Program.Log("miguel oliveira");
        }

        [Command("aiai")]
        public async Task aiai(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder
            {
                Title = "aiai",
                Color = Variables.Cores(),
                ImageUrl = "https://cdn.discordapp.com/attachments/949838790774636644/949839400790028308/essa-gente-inventa.gif"
            };
            await ctx.RespondAsync(embed);
            await Program.Log("aiai");
        }

        [Command("diálogo"), Aliases("dialogo")]
        public async Task diálogo(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder
            {
                Title = "dialogo",
                Color = Variables.Cores(),
                ImageUrl = "https://cdn.discordapp.com/attachments/949838790774636644/949839927972102184/dialogo.gif",
                Description = ctx.User.Mention + "vamo resolver isso no dialogo?"
            };
            await ctx.RespondAsync(embed);
            await Program.Log("dialogo");
        }

        #region comandos diferenciados

        [Command("sexo"), Aliases("hentai", "porno", "buceta", "punheta")]
        public async Task sexo(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Red,
                ImageUrl = "https://media.discordapp.net/attachments/949836472985460766/951596734331633734/vai_orar_imunda.jpg"
            };
            await ctx.RespondAsync(":new_moon_with_face:", embed);
            await Program.Log(":new_moon_with_face:");
        }

        #endregion comandos diferenciados

        [Command("boanoite"), Aliases("boa-noite")]
        public async Task boanoite(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync("Boa noite");
            await Program.Log("boa noite");
        }
    }
}