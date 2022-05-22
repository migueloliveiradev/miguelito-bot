using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using miguelito_bot_commands.commands;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace miguelito_bot_commands
{
    internal class main_commands : BaseCommandModule
    {
        [Command("avatar"), Aliases("av", "icon")]
        public async Task avatar(CommandContext ctx, DiscordUser user = null)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed;
            if (user == null)
            {
                user = ctx.User;
                embed = new DiscordEmbedBuilder
                {
                    Description = $"{Formatter.MaskedUrl("Baixar", new Uri(user.AvatarUrl + "?size=2048"), "Baixar")} imagem",
                    Color = cores(),
                    ImageUrl = user.GetAvatarUrl(ImageFormat.Auto, 2048),
                };
                embed.WithAuthor(user.Username, null, user.AvatarUrl).
                    WithFooter($"Solicitado por {ctx.User.Username}#{ctx.User.Discriminator}", ctx.User.AvatarUrl);
            }
            else
            {
                embed = new DiscordEmbedBuilder
                {
                    Description = $"{Formatter.MaskedUrl("Baixar", new Uri(user.AvatarUrl + "?size=2048"), "Baixar")} imagem",
                    Color = cores(),
                    ImageUrl = user.GetAvatarUrl(ImageFormat.Auto, 2048),
                };
                embed.WithAuthor(user.Username, null, user.AvatarUrl).
                    WithFooter($"Solicitado por {ctx.User.Username}#{ctx.User.Discriminator}", ctx.User.AvatarUrl);
            }
            await ctx.RespondAsync(embed);
            await Program.log("avatar");
        }

        [Command("ajuda"), Aliases("aj", "help")]
        public async Task ajuda(CommandContext ctx)
        {
            DiscordMember bot = await ctx.Guild.GetMemberAsync(Program.cliente.CurrentApplication.Id);
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new()
            {
                Title = "Ajuda do Miguelito",
                Color = cores(),
                Description = $"O Miguelito é um pequeno bot para ajudar na diversão e moderação do seu servidor\n\n" +
                $":cowboy: veja todos os comandos {Formatter.MaskedUrl("aqui", new Uri("https://miguelito.miguelsoft.com.br/comandos/"), "tem comandos secretos que não são divulgados")}\n\n" +
                $":tools: Esta com duvidas ou algum problema, entre em contato com meu suporte {Formatter.MaskedUrl("aqui", new Uri("https://miguelito.miguelsoft.com.br/suporte/"), "não venha atrás de suporte se a culpa foi sua")}\n\n" +
                $":hotdog: Me ajude comprar o leite das crianças {Formatter.MaskedUrl("aqui", new Uri("https://miguelito.miguelsoft.com.br/donate/"), "as crianças morreram de fome semana passada")}\n\n",
            };
            embed.WithAuthor(bot.Username, "https://miguelito.miguelsoft.com.br", bot.AvatarUrl)
                .WithThumbnail(bot.AvatarUrl);
            await ctx.RespondAsync($"Olá {ctx.Member.Mention}", embed);
            await Program.log("ajuda");
        }

        [Command("gato"), Aliases("fofo")]
        public async Task gato(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            List<string> gatos = commands_texto.ReadLines(() => Assembly.GetExecutingAssembly()
           .GetManifestResourceStream("miguelito_bot_commands.text.gatos.miguelito"), Encoding.UTF8)
               .ToList();
            Random random = new Random();
            int i = random.Next(0, gatos.Count);
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder
            {
                Color = cores(),
                ImageUrl = gatos[i],
            };
            await ctx.RespondAsync($"{ctx.User.Mention} espero q goste desse gato fofo q escolhi :3", embed);
            await Program.log("gato");
        }

        public static DiscordColor cores()
        {
            DiscordColor[] cores = {
                DiscordColor.Aquamarine,
                DiscordColor.Azure,
                DiscordColor.Black,
                DiscordColor.Blue,
                DiscordColor.Blurple,
                DiscordColor.Brown,
                DiscordColor.Chartreuse,
                DiscordColor.CornflowerBlue,
                DiscordColor.Cyan,
                DiscordColor.DarkBlue,
                DiscordColor.DarkButNotBlack,
                DiscordColor.DarkGray,
                DiscordColor.DarkGreen,
                DiscordColor.DarkRed,
                DiscordColor.Gold,
                DiscordColor.Goldenrod,
                DiscordColor.Gray,
                DiscordColor.Grayple,
                DiscordColor.Green,
                DiscordColor.HotPink,
                DiscordColor.IndianRed,
                DiscordColor.LightGray,
                DiscordColor.Lilac,
                DiscordColor.Magenta,
                DiscordColor.MidnightBlue,
                DiscordColor.None,
                DiscordColor.NotQuiteBlack,
                DiscordColor.Orange,
                DiscordColor.PhthaloBlue,
                DiscordColor.PhthaloGreen,
                DiscordColor.Purple,
                DiscordColor.Red,
                DiscordColor.Rose,
                DiscordColor.SapGreen,
                DiscordColor.Sienna,
                DiscordColor.SpringGreen,
                DiscordColor.Teal,
                DiscordColor.Turquoise,
                DiscordColor.VeryDarkGray,
                DiscordColor.Violet,
                DiscordColor.Wheat,
                DiscordColor.White,
                DiscordColor.Yellow};
            Random rnd = new Random();
            return cores[rnd.Next(0, cores.Length)];
        }

        [Command("hora"), Aliases("time")]
        public async Task hora(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync(DateTime.Now.ToShortTimeString());
            await Program.log("hora");
        }

        [Command("data"), Aliases("date")]
        public async Task data(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync(DateTime.Now.Date.ToString().Remove(10));
            await Program.log("data");
        }

        [Command("bomdia"), Aliases("bom-dia")]
        public async Task bomdia(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();

            List<string> bomdia = commands_texto.ReadLines(() => Assembly.GetExecutingAssembly()
           .GetManifestResourceStream("miguelito_bot_commands.text.bomdia.miguelito"), Encoding.UTF8)
               .ToList();
            Random random = new Random();
            int i = random.Next(0, bomdia.Count);
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder
            {
                Title = "Bom dia meu rei :cowboy:",
                Color = cores(),
                ImageUrl = bomdia[i],
            };
            await ctx.RespondAsync(ctx.User.Mention + " desejo um otimo dia para você :3" + embed);
            await Program.log("bomdia");
        }

        [Command("userinfo")]
        public async Task userinfo(CommandContext ctx, DiscordUser user = null)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed;
            if (user == null)
            {
                user = ctx.User;
                embed = new DiscordEmbedBuilder
                {
                    Title = ":person_doing_cartwheel: " + user.Username,
                    Color = cores(),
                    ImageUrl = user.GetAvatarUrl(ImageFormat.Auto, 2048),
                    Description = ":hash: Tag do Discord: **" + user.Username + "#" + user.Discriminator + "**\n\n" +
                    ":detective: ID do Discord: **" + user.Id + "**\n\n" +
                    ":hourglass_flowing_sand: Conta criada em: **" + user.CreationTimestamp.ToString().Replace("+00:00", "") + "**",
                };
            }
            else
            {
                embed = new DiscordEmbedBuilder
                {
                    Title = ":person_doing_cartwheel: " + user.Username,
                    Color = cores(),
                    ImageUrl = user.GetAvatarUrl(ImageFormat.Auto, 2048),
                    Description = ":hash: Tag do Discord: **" + user.Username + "#" + user.Discriminator + "**\n\n" +
                     ":detective: ID do Discord: **" + user.Id + "**\n\n" +
                     ":hourglass_flowing_sand: Conta criada em: **" + user.CreationTimestamp.ToString().Replace("+00:00", "") + "**"
                };
            }
            embed.WithThumbnail(user.AvatarUrl);
            await ctx.RespondAsync(embed);
            await Program.log("userinfo");
        }

        [Command("serverinfo")]
        public async Task serverinfo(CommandContext ctx, ulong server = 0)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
            if (server == 0)
            {
                server = ctx.Guild.Id;
                ulong OwnerId = ctx.Guild.OwnerId;
                string Owner = ctx.Guild.Owner.ToString();
                DiscordMember nome = await ctx.Guild.GetMemberAsync(OwnerId);
                string nome2 = nome.Username;
                Owner = Owner.Replace("Member " + OwnerId + ";", "").Replace($"({nome2})", "");
                embed = new DiscordEmbedBuilder
                {
                    Title = ":eyes: " + ctx.Guild.Name,
                    Color = cores(),
                    Description = $":detective: ID do Servidor: **{server}**\n\n" +
                    $":crown: Dono: **{Owner}**({ctx.Guild.OwnerId})\n\n" +
                    $":hourglass_flowing_sand: Criado em : **{ctx.Guild.CreationTimestamp.Date.ToString().Replace("00:00:00", "")}**\n\n" +
                    $":speech_balloon: Canais: **{ctx.Guild.Channels.Count}**\n\n" +
                    $":man_cartwheeling: Membros: **{ctx.Guild.MemberCount}**\n\n" +
                    $":notebook_with_decorative_cover: Cargos: **{ctx.Guild.Roles.Count}**"
                }.WithThumbnail(ctx.Guild.IconUrl);
            }
            else
            {
                try
                {
                    DiscordGuild guild2 = (DiscordGuild)await Program.cliente.GetGuildAsync(server);
                    server = guild2.Id;
                    ulong OwnerId = guild2.OwnerId;
                    string Owner = guild2.Owner.ToString();
                    DiscordMember nome = await guild2.GetMemberAsync(OwnerId);
                    string nome2 = nome.Username;
                    Owner = Owner.Replace("Member " + OwnerId + ";", "").Replace($"({nome2})", "");
                    embed = new DiscordEmbedBuilder
                    {
                        Title = ":eyes: " + guild2.Name,
                        Color = cores(),
                        Description = $":detective: ID do Servidor: **{server}**\n\n" +
                        $":crown: Dono: **{Owner}**({guild2.OwnerId})\n\n" +
                        $":hourglass_flowing_sand: Criado em : **{guild2.CreationTimestamp.Date.ToString().Replace("00:00:00", "")}**\n\n" +
                        $":speech_balloon: Canais: **{guild2.Channels.Count}**\n\n" +
                        $":man_cartwheeling: Membros: **{guild2.MemberCount}**\n\n" +
                        $":notebook_with_decorative_cover: Cargos: **{guild2.Roles.Count}**"
                    }.WithThumbnail(guild2.IconUrl);
                }
                catch
                {
                    await ctx.RespondAsync("Meu chapa, infelizmente eu não tô nesse server pra ver as informações dele :pensive:");
                }
            }
            await ctx.RespondAsync(embed);
            await Program.log("serverinfo");
        }

        [Command("servericon")]
        public async Task servericon(CommandContext ctx, ulong server = 0)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed;
            if (server == 0)
            {
                embed = new DiscordEmbedBuilder
                {
                    Description = $"{Formatter.MaskedUrl("Baixar", new Uri(ctx.Guild.IconUrl + "?size=2048"), "Baixar")} imagem",
                    Color = cores(),
                    ImageUrl = ctx.Guild.IconUrl + "?size=2048",
                };
                embed.WithAuthor(ctx.Guild.Name, null, ctx.Guild.IconUrl)
                     .WithFooter($"Solicitado por {ctx.User.Username}#{ctx.User.Discriminator}", ctx.User.AvatarUrl);
                await ctx.RespondAsync(embed);
            }
            else
            {
                try
                {
                    DiscordGuild guild = await ctx.Client.GetGuildAsync(server);
                    embed = new DiscordEmbedBuilder
                    {
                        Description = $"{Formatter.MaskedUrl("Baixar", new Uri(guild.IconUrl + "?size=2048"), "Baixar")} imagem",
                        Color = cores(),
                        ImageUrl = guild.IconUrl + "?size=2048",
                    };
                    embed.WithAuthor(guild.Name, null, guild.IconUrl)
                         .WithFooter($"Solicitado por {ctx.User.Username}#{ctx.User.Discriminator}", ctx.User.AvatarUrl);
                    await ctx.RespondAsync(embed);
                }
                catch
                {
                    await ctx.RespondAsync("Meu chapa, infelizmente eu não tô nesse server pra pegar o icon dele :pensive:");
                }
            }
            await Program.log("servericon");
        }

        [Command("ping")]
        public async Task ping(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordMessage ping = await ctx.RespondAsync("pinga? aceito :eyes:");
            string cs = Program.config[2];
            using MySqlConnection con = new MySqlConnection(cs);
            await con.OpenAsync();
            int pingdatabase = con.ConnectionTimeout;
            con.Close();
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder
            {
                Color = cores(),
            }
            .AddField("→ Gateway Latency ←", $"```cs\n      {Program.cliente.Ping}ms```", true)
            .AddField(" → Database Latency ←", $"```cs\n       {pingdatabase}ms```", true)
            .AddField("⠀", "⠀", true)
            .AddField("→ Message Latency ←", $"```cs\n      {DateTime.Now.Millisecond - ctx.Message.Timestamp.Millisecond}ms```", true)
            .AddField("→ Discord API Latency ←", $"```cs\n      {(int)(Program.cliente.Ping * 1.5)}ms```", true)
            .AddField("⠀", "⠀", true)
            .WithFooter("Hoje às " + ctx.Message.Timestamp.Hour + ":" + ctx.Message.Timestamp.Minute);
            await ping.DeleteAsync();
            await ctx.Client.SendMessageAsync(ctx.Channel, embed);
            await Program.log("ping");
        }

        [Command("serverquantidade")]
        public async Task serverquantidade(CommandContext ctx)
        {
            if (ctx.User.Id == 944942359169363989 || ctx.User.Id == 336211359106727936)
            {
                await ctx.Message.DeleteAsync();
                await ctx.Client.SendMessageAsync(ctx.Channel, $"O bot Miguelito esta em {Program.cliente.Guilds.Count} servidores.");
            }
        }

        [Command("serverlist")]
        public async Task serverlist(CommandContext ctx)
        {
            if (ctx.User.Id == 944942359169363989 || ctx.User.Id == 336211359106727936)
            {
                var servidor = "O Miguelito Bot esta nos seguintes servidores: \n";
                foreach (var server in Program.cliente.Guilds)
                {
                    servidor = $"{servidor} \n\n{server.Value.Name} ({server.Value.Id}) {server.Value.MemberCount} membros";
                }
                await ctx.Message.DeleteAsync();
                await ctx.Client.SendMessageAsync(ctx.Channel, servidor);
            }

        }
        [Command("memberquantidade")]
        public async Task memberquantidade(CommandContext ctx)
        {
            if (ctx.User.Id == 944942359169363989 || ctx.User.Id == 336211359106727936)
            {
                int membros = 0;
                foreach (var server in Program.cliente.Guilds)
                {
                    membros += server.Value.MemberCount;
                }
                await ctx.Message.DeleteAsync();
                await ctx.Client.SendMessageAsync(ctx.Channel, $"O Miguelito bot é usado por {membros} membros");
            }
        }

        

        [RequirePermissions(Permissions.ManageMessages)]
        [Command("say"), Aliases("dizer", "falar")]
        public async Task say(CommandContext ctx, DiscordChannel channel = null, [RemainingText] string text = "")
        {
            if (channel == null)
            {
                await ctx.TriggerTypingAsync();
                await ctx.RespondAsync(text);
            }
            else
            {
                await channel.TriggerTypingAsync();
                await channel.SendMessageAsync(text);
            }
            await Program.log("say");
        }

        [Command("botinfo")]
        public async Task botinfo(CommandContext ctx)
        {
            string cs = Program.config[2];
            using MySqlConnection con = new MySqlConnection(cs);
            await con.OpenAsync();
            int pingdatabase = con.ConnectionTimeout;
            con.Close();
            DateTime date_1 = Process.GetCurrentProcess().StartTime;
            DateTime date_2 = DateTime.Now.Date;
            int membros = 0;
            foreach (var server in Program.cliente.Guilds)
            {
                membros += server.Value.MemberCount;
            }
            long memoria = 0;
            try
            {
                memoria += Process.GetProcessesByName("miguelito-bot-commands").First().PrivateMemorySize64 / (1024 * 1024);
                memoria += Process.GetProcessesByName("miguelito-bot-events").First().PrivateMemorySize64 / (1024 * 1024);
                memoria += Process.GetProcessesByName("miguelito-bot-music").First().PrivateMemorySize64 / (1024 * 1024);
                memoria += Process.GetProcessesByName("miguelito-bot-slashcommands").First().PrivateMemorySize64 / (1024 * 1024);
            }
            catch { }
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder
            {
                Title = ":cowboy: Informações",
                Color = DiscordColor.CornflowerBlue,
                Description =
                $"> Online há: `{(date_2 - date_1).Days} dias`\n" +
                $"> Em aproximadamente `{Program.cliente.Guilds.Count()}` servidores.\n" +
                $"> Divertindo aproximadamente `{membros}` usuários.\n" +
                $"> Desenvolvido por {Formatter.MaskedUrl("Miguel Oliveira", new Uri("https://migueloliveira.xyz"), "pode chamar ele de gostoso")} " +
                $"e {Formatter.MaskedUrl("Paulo HS", new Uri("https://paulohpps.xyz"), "pergunta se o time dele tem mundial")}\n\n" +
                $"**SOFTWARE**\n" +
                $"> Versão do Dotnet: `{Environment.Version}`\n" +
                $"> Uso de memória: `{memoria}mb`\n" +
                $"> Ping WebSocket: `{Program.cliente.Ping}ms`\n" +
                $"> Ping API: `{ctx.Client.Ping}ms`\n" +
                $"> Ping Database: `{pingdatabase}ms`"
            };
            await ctx.RespondAsync(embed);
        }
    }
}
