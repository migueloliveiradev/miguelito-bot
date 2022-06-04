using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using miguelito_bot_commands.Utils;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace miguelito_bot_commands.commands
{
    internal class main_commands : BaseCommandModule
    {
        [Command("avatar"), Aliases("av", "icon")]
        public async Task avatar(CommandContext ctx, DiscordUser user = null)
        {
            await ctx.TriggerTypingAsync();
            if (user == null)
            {
                user = ctx.User;
            }
            DiscordMessageBuilder builder = new();
            DiscordEmbedBuilder embed = new()
            {
                Color = Variables.Cores(),
                ImageUrl = user.GetAvatarUrl(ImageFormat.Png, 2048),
            };
            embed.WithAuthor(user.Username, user.GetAvatarUrl(ImageFormat.Png, 2048), user.AvatarUrl)
                 .WithFooter($"Solicitado por {ctx.User.Username}#{ctx.User.Discriminator}", ctx.User.AvatarUrl);
            DiscordLinkButtonComponent link = new(user.GetAvatarUrl(ImageFormat.Png, 2048), "Link");
            builder.AddEmbed(embed);
            builder.AddComponents(link);
            await ctx.Message.DeleteAsync();
            await ctx.Client.SendMessageAsync(ctx.Channel, builder);
            await Program.Log("avatar");
            return;
        }

        [Command("ajuda"), Aliases("aj", "help")]
        public async Task ajuda(CommandContext ctx)
        {
            DiscordMember bot = await ctx.Guild.GetMemberAsync(Program.cliente.CurrentApplication.Id);
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new()
            {
                Title = "Ajuda do Miguelito",
                Color = Variables.Cores(),
                Description = $"O Miguelito é um pequeno bot para ajudar na diversão e moderação do seu servidor, com varios jogos, funções para moderação e muitas outras, descubra\n\n" +
                $":cowboy: veja todos os comandos {Formatter.MaskedUrl("aqui", new Uri("https://miguelito.miguelsoft.com.br/comandos/"), "tem comandos secretos que não são divulgados")}\n\n" +
                $":tools: Esta com duvidas ou algum problema, entre em contato com meu suporte {Formatter.MaskedUrl("aqui", new Uri("https://miguelito.miguelsoft.com.br/suporte/"), "não venha atrás de suporte se a culpa foi sua")}\n\n" +
                $":hotdog: Me ajude comprar o leite das crianças {Formatter.MaskedUrl("aqui", new Uri("https://miguelito.miguelsoft.com.br/donate/"), "as crianças morreram de fome semana passada")}\n\n" +
                $":nerd: Dashboard do Miguelito {Formatter.MaskedUrl("aqui", new Uri("https://bit.ly/3t3Ics5"), "dashboard")}\n\n"
            };
            embed.WithAuthor(bot.Username, "https://miguelito.miguelsoft.com.br", bot.AvatarUrl)
                .WithThumbnail(bot.AvatarUrl);
            await ctx.RespondAsync($"Olá {ctx.Member.Mention}", embed);
            await Program.Log("ajuda");
            return;
        }

        [Command("hora"), Aliases("time")]
        public async Task hora(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync(DateTime.Now.Hour + ":" + DateTime.Now.Minute);
            await Program.Log("hora");
            return;
        }

        [Command("data"), Aliases("date")]
        public async Task data(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync(DateTime.Now.Date.ToString("dd/MM/yyyy"));
            await Program.Log("data");
            return;
        }

        [Command("bomdia"), Aliases("bom-dia")]
        public async Task bomdia(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            Random random = new();
            int i = random.Next(0, Variables.bom_dia.Count);
            DiscordEmbedBuilder embed = new()
            {
                Title = "Bom dia meu rei :cowboy:",
                Color = Variables.Cores(),
                ImageUrl = Variables.bom_dia[i],
            };
            await ctx.RespondAsync($"{ctx.User.Mention} desejo um otimo dia para você :3", embed);
            await Program.Log("bomdia");
            return;
        }

        [Command("userinfo")]
        public async Task userinfo(CommandContext ctx, DiscordUser user = null)
        {
            await ctx.TriggerTypingAsync();
            if (user == null)
            {
                user = ctx.User;
            }
            DiscordEmbedBuilder embed = new()
            {
                Color = Variables.Cores(),
                ImageUrl = user.GetAvatarUrl(ImageFormat.Png, 2048),
                Description = $":hash: Tag do Discord: **{user.Username}#{user.Discriminator}**\n\n" +
                    $":detective: ID do Discord: **{user.Id}**\n\n" +
                    $":hourglass_flowing_sand: Conta criada em: **{user.CreationTimestamp:dd/MM/yyyy}**",
            };
            embed.WithThumbnail(user.AvatarUrl);
            embed.WithAuthor(user.Username, null, user.AvatarUrl);
            await ctx.Message.DeleteAsync();
            await ctx.Channel.SendMessageAsync(ctx.Member.Mention, embed);
            await Program.Log("userinfo");
            return;
        }

        [Command("serverinfo")]
        public async Task serverinfo(CommandContext ctx, ulong server = 0)
        {
            await ctx.TriggerTypingAsync();
            DiscordGuild guild = ctx.Guild;
            if (server != 0)
            {
                try
                {
                    guild = await ctx.Client.GetGuildAsync(server);
                }
                catch
                {
                    await ctx.RespondAsync("Meu chapa, infelizmente eu não tô nesse server pra ver as informações dele ou ele não existe :pensive:");
                    return;
                }
            }
            DiscordEmbedBuilder embed = new()
            {
                Color = Variables.Cores(),
                Description = $":detective: ID do Servidor: **{guild.Id}**\n\n" +
                    $":crown: Dono: **{guild.Owner.Nickname}**({guild.Owner.Id})\n\n" +
                    $":hourglass_flowing_sand: Criado em : **{guild.CreationTimestamp:dd/MM/yyyy}**\n\n" +
                    $":speech_balloon: Canais: **{guild.Channels.Count}**\n\n" +
                    $":man_cartwheeling: Membros: **{guild.MemberCount}**\n\n" +
                    $":notebook_with_decorative_cover: Cargos: **{guild.Roles.Count}**"
            };
            embed.WithThumbnail(guild.IconUrl);
            embed.WithAuthor(guild.Name, null, guild.IconUrl);
            await ctx.RespondAsync(embed);
            await Program.Log("serverinfo");
        }

        [Command("servericon")]
        public async Task servericon(CommandContext ctx, ulong server = 0)
        {
            await ctx.TriggerTypingAsync();
            DiscordGuild guild = ctx.Guild;
            if (server != 0)
            {
                try
                {
                    guild = await ctx.Client.GetGuildAsync(server);
                }
                catch
                {
                    await ctx.RespondAsync("Meu chapa, infelizmente eu não tô nesse server pra ver o icon dele ou ele não existe :pensive:");
                    return;
                }
            }
            DiscordEmbedBuilder embed = new()
            {
                Color = Variables.Cores(),
                ImageUrl = guild.GetIconUrl(ImageFormat.Png, 2048),
            };
            embed.WithAuthor(guild.Name, null, guild.IconUrl);
            await ctx.RespondAsync(embed);
            await Program.Log("servericon");
        }

        [Command("ping")]
        public async Task ping(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            int ping_message = DateTimeOffset.UtcNow.Millisecond - ctx.Message.Timestamp.Millisecond; 
            DiscordMessage ping = await ctx.RespondAsync("pinga? aceito :eyes:");
            string cs = Program.config[2];
            MySqlConnection con = new(cs);
            await con.OpenAsync();
            int pingdatabase = con.ConnectionTimeout;
            con.Close();
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder
            {
                Color = Variables.Cores(),
            }
            .AddField("→ Gateway Latency ←", $"```cs\n      {ctx.Client.Ping}ms```", true)
            .AddField(" → Database Latency ←", $"```cs\n       {pingdatabase}ms```", true)
            .AddField("⠀", "⠀", true)
            .AddField("→ Message Latency ←", $"```cs\n      {ping_message}ms```", true)
            .AddField("→ Discord API Latency ←", $"```cs\n      {ctx.Client.Ping * 1.5}ms```", true)
            .AddField("⠀", "⠀", true)
            .WithFooter("Hoje às " + ctx.Message.Timestamp.Hour + ":" + ctx.Message.Timestamp.Minute);
            await ping.DeleteAsync();
            await ctx.Message.DeleteAsync();
            await ctx.Channel.SendMessageAsync(embed);
            await Program.Log("ping");
            return;
        }

        [Command("serverquantidade")]
        public async Task serverquantidade(CommandContext ctx)
        {
            if (ctx.User.Id == 944942359169363989 || ctx.User.Id == 336211359106727936)
            {
                await ctx.Message.DeleteAsync();
                await ctx.Client.SendMessageAsync(ctx.Channel, $"O bot Miguelito esta em {ctx.Client.Guilds.Count} servidores.");
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
        public async Task say(CommandContext ctx, DiscordChannel channel, [RemainingText] string text = "")
        {
            try
            {
                await ctx.Message.DeleteAsync();
                await channel.TriggerTypingAsync();
                await channel.SendMessageAsync(text);
            }
            catch
            {
                await ctx.TriggerTypingAsync();
                await ctx.Client.SendMessageAsync(ctx.Channel, $"{ctx.Member.Mention}, infelizmente por algum motivo eu não posso dizer isso :pensive:");
            }
            await Program.Log("say");
        }

        [Command("botinfo")]
        public async Task botinfo(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
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
            DiscordEmbedBuilder embed = new()
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
            return;
        }
    }
}
