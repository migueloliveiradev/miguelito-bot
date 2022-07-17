using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using miguelito_bot_commands;
using MySql.Data.MySqlClient;
using Tweetinvi;

namespace miguelito_bot_events.events
{
    internal class Events
    {
        public static async Task ReadyEvent(DiscordClient sender, ReadyEventArgs e)
        {
            TwitterClient client = new(Program.config[9], Program.config[10], Program.config[11], Program.config[12]);
            await Twitter.Tweet(client);
        }

        public static async Task GuildMemberAddEvent(DiscordClient sender, GuildMemberAddEventArgs e)
        {
            string cs = Program.config[2];
            using MySqlConnection con = new(cs);
            await con.OpenAsync();
            string sql = $"SELECT CHAT_ENTRADA, MENSAGUEM_ENTRADA FROM GERAL WHERE ID_SERVIDOR='{e.Guild.Id}'";
            MySqlCommand cmd = new(sql, con);
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            if (rdr[0] != null)
            {
                try
                {
                    string desc = rdr[1].ToString();
                    desc = desc.Replace("{name}", e.Member.Username);
                    desc = desc.Replace("{id}", e.Member.Id.ToString());
                    desc = desc.Replace("{servidor_name}", e.Guild.Name);
                    desc = desc.Replace("{servidor_id}", e.Guild.Id.ToString());
                    desc = desc.Replace("{data}", DateTime.Now.ToString());
                    desc = desc.Replace("{member_count}", e.Guild.MemberCount.ToString());
                    desc = desc.Replace("\n", "\n");
                    desc = desc.Replace("{data_hora}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

                    DiscordChannel channel = e.Guild.GetChannel(ulong.Parse(rdr[0].ToString()));
                    DiscordEmbedBuilder embed = new()
                    {
                        Description = desc,
                        Color = DiscordColor.Blue,
                        ImageUrl = "https://media.discordapp.net/attachments/949836472985460766/979604657309626368/fundo_1.png"
                    };
                    embed.WithAuthor(e.Member.Username, null, e.Member.AvatarUrl);
                    embed.WithThumbnail(e.Member.AvatarUrl);
                    await channel.SendMessageAsync($"{e.Member.Mention} :smiley:", embed);
                }
                catch { }
            }
            con.Close();
        }

        public static async Task GuildMemberRemoveEvent(DiscordClient sender, GuildMemberRemoveEventArgs e)
        {
            string cs = Program.config[2];
            using MySqlConnection con = new(cs);
            await con.OpenAsync();
            string sql = $"SELECT CHAT_SAIDA, MENSAGUEM_SAIDA FROM GERAL WHERE ID_SERVIDOR='{e.Guild.Id}'";
            MySqlCommand cmd = new(sql, con);
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            if (rdr[0] != null)
            {
                try
                {
                    string desc = rdr[1].ToString();
                    desc = desc.Replace("{name}", e.Member.Username);
                    desc = desc.Replace("{id}", e.Member.Id.ToString());
                    desc = desc.Replace("{servidor_name}", e.Guild.Name);
                    desc = desc.Replace("{servidor_id}", e.Guild.Id.ToString());
                    desc = desc.Replace("{data}", DateTime.Now.ToString());
                    desc = desc.Replace("{member_count}", e.Guild.MemberCount.ToString());
                    desc = desc.Replace("\n", "\n");
                    desc = desc.Replace("{data_hora}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

                    DiscordChannel channel = e.Guild.GetChannel(ulong.Parse(rdr[0].ToString()));
                    DiscordEmbedBuilder embed = new()
                    {
                        Title = $"Adeus {e.Member.Username}...",
                        Description = desc,
                        Color = DiscordColor.Blue,
                    };
                    embed.WithAuthor(e.Member.Username, null, e.Member.AvatarUrl);
                    embed.WithThumbnail(e.Member.AvatarUrl);
                    await sender.SendMessageAsync(channel, $"{e.Member.Mention} :pensive:", embed);
                }
                catch { }
            }
        }

        public static async Task GuildCreateEvent(DiscordClient sender, GuildCreateEventArgs e)
        {
            string cs = Program.config[2];
            using var con = new MySqlConnection(cs);
            await con.OpenAsync();
            using var cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO GERAL(ID_SERVIDOR, DATA_ENTRADA) VALUES('{e.Guild.Id}', '{DateTime.Now.Date.ToString().Remove(10)}')";
            cmd.ExecuteNonQuery();
            con.Close();
            DiscordMember bot = await e.Guild.GetMemberAsync(Program.cliente.CurrentApplication.Id);
            DiscordEmbedBuilder embed = new()
            {
                Title = "Ajuda do Miguelito",
                Color = DiscordColor.CornflowerBlue,
                Description = $"O Miguelito é um pequeno bot para ajudar na diversão e moderação do seu servidor, com varios jogos, funções para moderação e muitas outras, descubra\n\n" +
                $":cowboy: veja todos os comandos {Formatter.MaskedUrl("aqui", new Uri("https://miguelito.miguelsoft.com.br/comandos/"), "tem comandos secretos que não são divulgados")}\n\n" +
                $":tools: Esta com duvidas ou algum problema, entre em contato com meu suporte {Formatter.MaskedUrl("aqui", new Uri("https://miguelito.miguelsoft.com.br/suporte/"), "não venha atrás de suporte se a culpa foi sua")}\n\n" +
                $":hotdog: Me ajude comprar o leite das crianças {Formatter.MaskedUrl("aqui", new Uri("https://miguelito.miguelsoft.com.br/donate/"), "as crianças morreram de fome semana passada")}\n\n" +
                $":nerd: Dashboard do Miguelito {Formatter.MaskedUrl("aqui", new Uri("https://bit.ly/3t3Ics5"), "dashboard")}\n\n"
            };
            embed.WithAuthor(bot.Username, "https://miguelito.miguelsoft.com.br", bot.AvatarUrl)
                .WithThumbnail(bot.AvatarUrl);
            await sender.SendMessageAsync(e.Guild.GetDefaultChannel(), embed);
            DiscordChannel channel = await sender.GetChannelAsync(997683926912991232);
            await channel.SendMessageAsync($"{e.Guild.Name} ({e.Guild.Id})");
        }

        public static async Task GuildDeleteEvent(DiscordClient sender, GuildDeleteEventArgs e)
        {
            string cs = Program.config[2];
            using var con = new MySqlConnection(cs);
            await con.OpenAsync();
            using var cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"DELETE FROM GERAL WHERE ID_SERVIDOR={e.Guild.Id}";
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static async Task ChannelCreateEvent(DiscordClient sender, ChannelCreateEventArgs e)
        {
            Random random = new();
            string[] texts =
            {
                ":face_with_spiral_eyes:",
                ":nerd:",
                ":hushed:",
                ":face_with_monocle:",
                ":cowboy:",
                ":ok_hand:",
                ":eyes:",
                ":wave:",
                ":smirk:",
                ":yawning_face:"
            };
            int i = random.Next(0, texts.Length);
            await sender.SendMessageAsync(e.Channel, texts[i]);
        }

        public static async Task MessageDeleteEvent(DiscordClient sender, MessageDeleteEventArgs e)
        {
            string cs = Program.config[2];
            using MySqlConnection con = new(cs);
            await con.OpenAsync();
            string sql = $"SELECT CHAT_LOG, MENSAGUEM_ENTRADA FROM GERAL WHERE ID_SERVIDOR='{e.Guild.Id}'";
            MySqlCommand cmd = new(sql, con);
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            if (rdr[0] != null)
            {
                try
                {
                    DiscordChannel channel = e.Guild.GetChannel(ulong.Parse(rdr[0].ToString()));
                    DiscordEmbedBuilder embed = new()
                    {
                        Title = "Mensagem deletada",
                        Description = $"Uma mensagem de {e.Message.Author.Mention} foi deletada no canal {e.Channel.Mention}\n\n" +
                        $"Mensagem: ```{e.Message.Content}```",
                        Color = DiscordColor.Red,
                    };
                    embed.WithAuthor(e.Message.Author.Username, null, e.Message.Author.AvatarUrl);
                    embed.WithThumbnail(e.Message.Author.AvatarUrl);
                    await channel.SendMessageAsync(embed);
                }
                catch { }
            }
            con.Close();
        }

        public static async Task MessageUpdateEvent(DiscordClient sender, MessageUpdateEventArgs e)
        {
            string cs = Program.config[2];
            using MySqlConnection con = new(cs);
            await con.OpenAsync();
            string sql = $"SELECT CHAT_LOG, MENSAGUEM_ENTRADA FROM GERAL WHERE ID_SERVIDOR='{e.Guild.Id}'";
            MySqlCommand cmd = new(sql, con);
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            if (rdr[0] != null)
            {
                try
                {
                    DiscordChannel channel = e.Guild.GetChannel(ulong.Parse(rdr[0].ToString()));
                    DiscordEmbedBuilder embed = new()
                    {
                        Title = "Mensagem deletada",
                        Description = $"Uma mensagem de {e.Message.Author.Mention} foi editada no canal {e.Channel.Mention}\n\n" +
                        $"Mensagem anterior: ```{e.MessageBefore.Content}```\n" +
                        $"Mensagem atual: ```{e.Message.Content}```",
                        Color = DiscordColor.Blue,
                    };
                    embed.WithAuthor(e.Message.Author.Username, null, e.Message.Author.AvatarUrl);
                    embed.WithThumbnail(e.Message.Author.AvatarUrl);
                    await channel.SendMessageAsync(embed);
                }
                catch { }
            }
            con.Close();
        }
    }
}