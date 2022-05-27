using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using miguelito_bot_commands;
using MySql.Data.MySqlClient;

namespace miguelito_bot_events.events
{
    internal class events
    {
        public static async Task Client_entrada(DiscordClient sender, GuildMemberAddEventArgs e)
        {
            string cs = Program.config[2];
            using MySqlConnection con = new(cs);
            await con.OpenAsync();
            string sql = $"SELECT CHAT_ENTRADA, MENSAGUEM_ENTRADA FROM GERAL WHERE ID_SERVIDOR='{e.Guild.Id}'";
            MySqlCommand cmd = new (sql, con);
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

        public static async Task registro(DiscordClient sender, MessageReactionAddEventArgs e)
        {
            if (e.Guild.Id == 880904935787601960)
            {
                if (e.Channel.Id == 953066005440135199)
                {
                    var user = e.Guild.GetMemberAsync(e.User.Id).Result;
                    DiscordRole discordRole;
                    if (e.Message.Id == 953459808076439652)
                    {
                        if (e.Message.Reactions.Last().Emoji.Name.ToString() == "Csharp")
                        {
                            discordRole = e.Guild.GetRole(932308563995553822);
                            await user.GrantRoleAsync(discordRole);
                        }
                        else if (e.Message.Reactions.Last().Emoji.Name.ToString() == "c_")
                        {
                            discordRole = e.Guild.GetRole(932308553077772288);
                            await user.GrantRoleAsync(discordRole);
                        }
                        else if (e.Message.Reactions.Last().Emoji.Name.ToString() == "cpp")
                        {
                            discordRole = e.Guild.GetRole(914205871121252363);
                            await user.GrantRoleAsync(discordRole);
                        }
                        else if (e.Message.Reactions.Last().Emoji.Name.ToString() == "python")
                        {
                            discordRole = e.Guild.GetRole(934880069820743680);
                            await user.GrantRoleAsync(discordRole);
                        }
                        else if (e.Message.Reactions.Last().Emoji.Name.ToString() == "java")
                        {
                            discordRole = e.Guild.GetRole(934879359553142884);
                            await user.GrantRoleAsync(discordRole);
                        }
                        else if (e.Message.Reactions.Last().Emoji.Name.ToString() == "unity")
                        {
                            discordRole = e.Guild.GetRole(949481016165277728);
                            await user.GrantRoleAsync(discordRole);
                        }
                        else if (e.Message.Reactions.Last().Emoji.Name.ToString() == "unreal")
                        {
                            discordRole = e.Guild.GetRole(949480887702151258);
                            await user.GrantRoleAsync(discordRole);
                        }
                        else if (e.Message.Reactions.Last().Emoji.Name.ToString() == "js")
                        {
                            discordRole = e.Guild.GetRole(934880204986404894);
                            await user.GrantRoleAsync(discordRole);
                        }
                        else if (e.Message.Reactions.Last().Emoji.Name.ToString() == "HTML")
                        {
                            discordRole = e.Guild.GetRole(934879792279474227);
                            await user.GrantRoleAsync(discordRole);
                        }
                        else if (e.Message.Reactions.Last().Emoji.Name.ToString() == "CSS")
                        {
                            discordRole = e.Guild.GetRole(953476259172724827);
                            await user.GrantRoleAsync(discordRole);
                        }
                        else if (e.Message.Reactions.Last().Emoji.Name.ToString() == "ruby")
                        {
                            discordRole = e.Guild.GetRole(934880651130310666);
                            await user.GrantRoleAsync(discordRole);
                        }
                        else if (e.Message.Reactions.Last().Emoji.Name.ToString() == "typescript")
                        {
                            discordRole = e.Guild.GetRole(934880032323694592);
                            await user.GrantRoleAsync(discordRole);
                        }
                        else if (e.Message.Reactions.Last().Emoji.Name.ToString() == "assembly")
                        {
                            discordRole = e.Guild.GetRole(953477456516505622);
                            await user.GrantRoleAsync(discordRole);
                        }
                    }
                    else if (e.Message.Id == 953460553240707123)
                    {
                        if (e.Message.Reactions.Last().Emoji.Name.ToString() == "sim")
                        {
                            discordRole = e.Guild.GetRole(914200320882643006);
                            await user.GrantRoleAsync(discordRole);
                        }
                    }
                    else if (e.Message.Id == 953461258127015968)
                    {

                        if (e.Message.Reactions.Last().Emoji.Name.ToString() == "sim")
                        {
                            discordRole = e.Guild.GetRole(942138421638492241);
                            await user.GrantRoleAsync(discordRole);
                        }
                    }
                    else if (e.Message.Id == 953461718103777300)
                    {
                        if (e.Message.Reactions.Last().Emoji.Name.ToString() == "sim")
                        {
                            discordRole = e.Guild.GetRole(881299809124884561);
                            await user.GrantRoleAsync(discordRole);
                        }
                    }
                }
            }
        }
        public static async Task entrada_server(DiscordClient sender, GuildCreateEventArgs e)
        {
            string cs = Program.config[2];
            using var con = new MySqlConnection(cs);
            await con.OpenAsync();
            using var cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO GERAL(ID_SERVIDOR, DATA_ENTRADA) VALUES('{e.Guild.Id}', '{DateTime.Now.Date.ToString().Remove(10)}')";
            cmd.ExecuteNonQuery();
        }

        public static async Task saida_server(DiscordClient sender, GuildDeleteEventArgs e)
        {
            string cs = Program.config[2];
            using var con = new MySqlConnection(cs);
            await con.OpenAsync();
            using var cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"DELETE FROM GERAL WHERE ID_SERVIDOR={e.Guild.Id}";
            cmd.ExecuteNonQuery();
        }

        public static async Task Channel_Create(DiscordClient sender, ChannelCreateEventArgs e)
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
    }
}
