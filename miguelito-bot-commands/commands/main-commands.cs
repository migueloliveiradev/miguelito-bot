using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using miguelito_bot_commands.Utils;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace miguelito_bot_commands.commands
{
    internal class Main_commands : BaseCommandModule
    {
        [Command("avatar"), Aliases("av", "icon")]
        public async Task avatar(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new()
            {
                Title = ":arrow_up: Updates",
                Description = $"Opa {ctx.User.Username} sabia que agora o comando `-avatar` agora é em slash comandos? " +
                $"use `/user avatar` para testa-lo\n\n" +
                $"Todos os comandos que não são em slash irão desaparecer **31 de Agosto de 2022**, estão comece a se acostumar com eles\n\n" +
                $"Caso os comandos não estejam aparecendo {Formatter.MaskedUrl("autorize minhas permisões", new Uri("https://discord.com/api/oauth2/authorize?client_id=949488330620432386&permissions=8&scope=bot"), "beba agua")}\n\n" +
                $"Em caso de algum problema entre no meu {Formatter.MaskedUrl("servidor de suporte", new Uri("https://discord.gg/FZpH3SZahH"), "se o problema for culpa sua vai catar coquinho")}"
            };
            await ctx.RespondAsync(embed);
            return;
        }

        [Command("ajuda"), Aliases("aj", "help")]
        public async Task ajuda(CommandContext ctx)
        {
            DiscordMember bot = await ctx.Guild.GetMemberAsync(Program.Cliente.CurrentApplication.Id);
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

        [Command("userinfo")]
        public async Task userinfo(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new()
            {
                Title = ":arrow_up: Updates",
                Description = $"Opa {ctx.User.Username} sabia que agora o comando `-userinfo` agora é em slash comandos? " +
                $"use `/user info` para testa-lo\n\n" +
                $"Todos os comandos que não são em slash irão desaparecer **31 de Agosto de 2022**, estão comece a se acostumar com eles\n\n" +
                $"Caso os comandos não estejam aparecendo {Formatter.MaskedUrl("autorize minhas permisões", new Uri("https://discord.com/api/oauth2/authorize?client_id=949488330620432386&permissions=8&scope=bot"), "beba agua")}\n\n" +
                $"Em caso de algum problema entre no meu {Formatter.MaskedUrl("servidor de suporte", new Uri("https://discord.gg/FZpH3SZahH"), "se o problema for culpa sua vai catar coquinho")}"
            };
            await ctx.RespondAsync(embed);
            return;
        }

        [Command("serverinfo")]
        public async Task serverinfo(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new()
            {
                Title = ":arrow_up: Updates",
                Description = $"Opa {ctx.User.Username} sabia que agora o comando `-serverinfo` agora é em slash comandos? " +
                $"use `/server info` para testa-lo\n\n" +
                $"Todos os comandos que não são em slash irão desaparecer **31 de Agosto de 2022**, estão comece a se acostumar com eles\n\n" +
                $"Caso os comandos não estejam aparecendo {Formatter.MaskedUrl("autorize minhas permisões", new Uri("https://discord.com/api/oauth2/authorize?client_id=949488330620432386&permissions=8&scope=bot"), "beba agua")}\n\n" +
                $"Em caso de algum problema entre no meu {Formatter.MaskedUrl("servidor de suporte", new Uri("https://discord.gg/FZpH3SZahH"), "se o problema for culpa sua vai catar coquinho")}"
            };
            await ctx.RespondAsync(embed);
            return;
        }

        [Command("servericon")]
        public async Task servericon(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new()
            {
                Title = ":arrow_up: Updates",
                Description = $"Opa {ctx.User.Username} sabia que agora o comando `-servericon` agora é em slash comandos? " +
                $"use `/server icon` para testa-lo\n\n" +
                $"Todos os comandos que não são em slash irão desaparecer **31 de Agosto de 2022**, estão comece a se acostumar com eles\n\n" +
                $"Caso os comandos não estejam aparecendo {Formatter.MaskedUrl("autorize minhas permisões", new Uri("https://discord.com/api/oauth2/authorize?client_id=949488330620432386&permissions=8&scope=bot"), "beba agua")}\n\n" +
                $"Em caso de algum problema entre no meu {Formatter.MaskedUrl("servidor de suporte", new Uri("https://discord.gg/FZpH3SZahH"), "se o problema for culpa sua vai catar coquinho")}"
            };
            await ctx.RespondAsync(embed);
            return;
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
                foreach (var server in Program.Cliente.Guilds)
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
                foreach (var server in Program.Cliente.Guilds)
                {
                    membros += server.Value.MemberCount;
                }
                await ctx.Message.DeleteAsync();
                await ctx.Client.SendMessageAsync(ctx.Channel, $"O Miguelito bot é usado por {membros} membros");
            }
        }

        [Command("say"), Aliases("dizer", "falar")]
        public async Task say(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new()
            {
                Title = ":arrow_up: Updates",
                Description = $"Opa {ctx.User.Username} sabia que agora o comando `-say` agora é em slash comandos? " +
                $"use `/say` para testa-lo\n\n" +
                $"Todos os comandos que não são em slash irão desaparecer **31 de Agosto de 2022**, estão comece a se acostumar com eles\n\n" +
                $"Caso os comandos não estejam aparecendo {Formatter.MaskedUrl("autorize minhas permisões", new Uri("https://discord.com/api/oauth2/authorize?client_id=949488330620432386&permissions=8&scope=bot"), "beba agua")}\n\n" +
                $"Em caso de algum problema entre no meu {Formatter.MaskedUrl("servidor de suporte", new Uri("https://discord.gg/FZpH3SZahH"), "se o problema for culpa sua vai catar coquinho")}"
            };
            await ctx.RespondAsync(embed);
            return;
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
            foreach (var server in Program.Cliente.Guilds)
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
                $"> Em aproximadamente `{Program.Cliente.Guilds.Count()}` servidores.\n" +
                $"> Divertindo aproximadamente `{membros}` usuários.\n" +
                $"> Desenvolvido por {Formatter.MaskedUrl("Miguel Oliveira", new Uri("https://migueloliveira.xyz"), "pode chamar ele de gostoso")} " +
                $"e {Formatter.MaskedUrl("Paulo HS", new Uri("https://paulohpps.xyz"), "pergunta se o time dele tem mundial")}\n\n" +
                $"**SOFTWARE**\n" +
                $"> Versão do Dotnet: `{Environment.Version}`\n" +
                $"> Uso de memória: `{memoria}mb`\n" +
                $"> Ping WebSocket: `{Program.Cliente.Ping}ms`\n" +
                $"> Ping API: `{ctx.Client.Ping}ms`\n" +
                $"> Ping Database: `{pingdatabase}ms`"
            };
            await ctx.RespondAsync(embed);
            return;
        }
    }
}