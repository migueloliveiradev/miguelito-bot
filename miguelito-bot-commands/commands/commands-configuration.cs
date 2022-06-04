using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using MySql.Data.MySqlClient;

namespace miguelito_bot_commands.commands
{
    internal class commands_configuration : BaseCommandModule
    {
        [RequirePermissions(Permissions.ManageGuild)]
        [Command("ConfigureChatInput")]
        public async Task ConfigureChatInput(CommandContext ctx, DiscordChannel channel = null)
        {
            try
            {
                if (channel != null)
                {
                    string cs = Program.config[2];
                    using var con = new MySqlConnection(cs);
                    await con.OpenAsync();
                    using var cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = $"UPDATE GERAL SET CHAT_ENTRADA = '{channel.Id}' WHERE ID_SERVIDOR = '{ctx.Guild.Id}'";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = $"UPDATE GERAL SET MODELO_MESSAGEM_ENTRADA = '1' WHERE ID_SERVIDOR = '{ctx.Guild.Id}'";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    await ctx.TriggerTypingAsync();
                    await ctx.RespondAsync($"{ctx.User.Mention} Otimo, **Chat entrada** configurado com sucesso.");
                }
                else
                {
                    await ctx.TriggerTypingAsync();
                    await ctx.RespondAsync($"{ctx.User.Mention} Me perdoa mas eu ainda não tenho inteligência o suficiente para advinhar qual chat você quer configurar :pensive:");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                await ctx.TriggerTypingAsync();
                await ctx.RespondAsync($"{ctx.User.Mention} Meu chapa, algo deu errado, poderia entrar em contato com o suporte :pensive: \n\n https://miguelito.miguelsoft.com.br/suporte/");

            }
        }

        [RequirePermissions(Permissions.ManageGuild)]
        [Command("ConfigureTextChatInput")]
        public async Task ConfigureTextChatInput(CommandContext ctx, [RemainingText] string text = "")
        {
            await ctx.TriggerTypingAsync();
            try
            {
                if (text != "")
                {
                    if (text.Length < 500)
                    {
                        string cs = Program.config[2];
                        using var con = new MySqlConnection(cs);
                        await con.OpenAsync();
                        using var cmd = new MySqlCommand();
                        cmd.Connection = con;
                        cmd.CommandText = $"UPDATE GERAL SET MENSAGUEM_ENTRADA = '{text}' WHERE ID_SERVIDOR = '{ctx.Guild.Id}'";
                        cmd.ExecuteNonQuery();
                        con.Close();
                        await ctx.RespondAsync($"{ctx.User.Mention} Otimo, texto de boas vindas configurado com sucesso.");
                    }
                    else
                    {
                        await ctx.TriggerTypingAsync();
                        await ctx.RespondAsync($"{ctx.User.Mention} Me perdoa mas o texto que você está tentando configurar é muito grande, o limite é de 500 caracteres.");
                    }
                }
                else
                {

                    await ctx.RespondAsync($"{ctx.User.Mention} Me perdoa mas eu ainda não tenho inteligência o suficiente para advinhar qual mensagem você quer configurar :pensive:");
                }
            }
            catch
            {

                await ctx.RespondAsync($"{ctx.User.Mention} Meu chapa, algo deu errado, poderia entrar em contato com o suporte :pensive: \n\n https://miguelito.miguelsoft.com.br/suporte/");

            }
        }

        [RequirePermissions(Permissions.ManageGuild)]
        [Command("RemoveChatInput")]
        public async Task RemoveChatInput(CommandContext ctx)
        {
            try
            {
                string cs = Program.config[2];
                using var con = new MySqlConnection(cs);
                await con.OpenAsync();
                using var cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandText = $"UPDATE GERAL SET CHAT_ENTRADA = NULL WHERE ID_SERVIDOR = {ctx.Guild.Id}";
                cmd.ExecuteNonQuery();
                cmd.CommandText = $"UPDATE GERAL SET MODELO_MESSAGEM_ENTRADA = NULL WHERE ID_SERVIDOR = {ctx.Guild.Id}";
                cmd.ExecuteNonQuery();
                con.Close();
                await ctx.TriggerTypingAsync();
                await ctx.RespondAsync($"{ctx.User.Mention} **Chat entrada** removido com sucesso.");
            }
            catch
            {
                await ctx.TriggerTypingAsync();
                await ctx.RespondAsync($"{ctx.User.Mention} Meu chapa, algo deu errado, poderia entrar em contato com o suporte :pensive: \n\n https://miguelito.miguelsoft.com.br/suporte/");
            }
        }

        [RequirePermissions(Permissions.ManageGuild)]
        [Command("ConfigureChatLog")]
        public async Task ConfigureChatLog(CommandContext ctx, DiscordChannel channel = null)
        {
            try
            {
                if (channel != null)
                {
                    string cs = Program.config[2];
                    using var con = new MySqlConnection(cs);
                    await con.OpenAsync();
                    using var cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = $"UPDATE GERAL SET CHAT_LOG = {channel.Id} WHERE ID_SERVIDOR = {ctx.Guild.Id}";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    await ctx.TriggerTypingAsync();
                    await ctx.RespondAsync($"{ctx.User.Mention} Otimo, **Chat entrada** configurado com sucesso.");
                }
                else
                {
                    await ctx.TriggerTypingAsync();
                    await ctx.RespondAsync($"{ctx.User.Mention} Me perdoa mas eu ainda não tenho inteligência o suficiente para advinhar qual chat você quer configurar :pensive:");
                }
            }
            catch
            {
                await ctx.TriggerTypingAsync();
                await ctx.RespondAsync($"{ctx.User.Mention} Meu chapa, algo deu errado, poderia entrar em contato com o suporte :pensive: \n\n https://miguelito.miguelsoft.com.br/suporte/");
            }
        }

        [RequirePermissions(Permissions.ManageGuild)]
        [Command("RemoveChatLog")]
        public async Task RemoveChatLog(CommandContext ctx)
        {
            try
            {
                string cs = Program.config[2];
                using var con = new MySqlConnection(cs);
                await con.OpenAsync();
                using var cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandText = $"UPDATE GERAL SET CHAT_LOG = NULL WHERE ID_SERVIDOR = {ctx.Guild.Id}";
                cmd.ExecuteNonQuery();
                con.Close();
                await ctx.TriggerTypingAsync();
                await ctx.RespondAsync($"{ctx.User.Mention} **Chat entrada** removido com sucesso.");
            }
            catch
            {
                await ctx.TriggerTypingAsync();
                await ctx.RespondAsync($"{ctx.User.Mention} Meu chapa, algo deu errado, poderia entrar em contato com o suporte :pensive: \n\n https://miguelito.miguelsoft.com.br/suporte/");
            }
        }

        [RequirePermissions(Permissions.ManageGuild)]
        [Command("ConfigureChatGeneral")]
        public async Task ConfigureChatGeneral(CommandContext ctx, DiscordChannel channel = null)
        {
            try
            {
                if (channel != null)
                {
                    string cs = Program.config[2];
                    using var con = new MySqlConnection(cs);
                    await con.OpenAsync();
                    using var cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = $"UPDATE GERAL SET CHAT_PRINCIPAL = {channel.Id} WHERE ID_SERVIDOR = {ctx.Guild.Id}";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    await ctx.TriggerTypingAsync();
                    await ctx.RespondAsync($"{ctx.User.Mention} Otimo, **Chat entrada** configurado com sucesso.");
                }
                else
                {
                    await ctx.TriggerTypingAsync();
                    await ctx.RespondAsync($"{ctx.User.Mention} Me perdoa mas eu ainda não tenho inteligência o suficiente para advinhar qual chat você quer configurar :pensive:");
                }
            }
            catch
            {
                await ctx.TriggerTypingAsync();
                await ctx.RespondAsync($"{ctx.User.Mention} Meu chapa, algo deu errado, poderia entrar em contato com o suporte :pensive: \n\n https://miguelito.miguelsoft.com.br/suporte/");
            }
        }

        [RequirePermissions(Permissions.ManageGuild)]
        [Command("RemoveChatGeneral")]
        public async Task RemoveChatGeneral(CommandContext ctx)
        {
            try
            {
                string cs = Program.config[2];
                using var con = new MySqlConnection(cs);
                await con.OpenAsync();
                using var cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandText = $"UPDATE GERAL SET CHAT_PRINCIPAL = NULL WHERE ID_SERVIDOR = {ctx.Guild.Id}";
                cmd.ExecuteNonQuery();
                con.Close();
                await ctx.TriggerTypingAsync();
                await ctx.RespondAsync($"{ctx.User.Mention} **Chat entrada** removido com sucesso.");
            }
            catch
            {
                await ctx.TriggerTypingAsync();
                await ctx.RespondAsync($"{ctx.User.Mention} Meu chapa, algo deu errado, poderia entrar em contato com o suporte :pensive: \n\n https://miguelito.miguelsoft.com.br/suporte/");
            }
        }

        [RequirePermissions(Permissions.ManageGuild)]
        [Command("ConfigureChatExit")]
        public async Task ConfigureChatExit(CommandContext ctx, DiscordChannel channel = null)
        {
            try
            {
                if (channel != null)
                {
                    string cs = Program.config[2];
                    using var con = new MySqlConnection(cs);
                    await con.OpenAsync();
                    using var cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = $"UPDATE GERAL SET CHAT_SAIDA = {channel.Id} WHERE ID_SERVIDOR = {ctx.Guild.Id}";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    await ctx.TriggerTypingAsync();
                    await ctx.RespondAsync($"{ctx.User.Mention} Otimo, **Chat entrada** configurado com sucesso.");
                }
                else
                {
                    await ctx.TriggerTypingAsync();
                    await ctx.RespondAsync($"{ctx.User.Mention} Me perdoa mas eu ainda não tenho inteligência o suficiente para advinhar qual chat você quer configurar :pensive:");
                }
            }
            catch
            {
                await ctx.TriggerTypingAsync();
                await ctx.RespondAsync($"{ctx.User.Mention} Meu chapa, algo deu errado, poderia entrar em contato com o suporte :pensive: \n\n https://miguelito.miguelsoft.com.br/suporte/");
            }
        }

        [RequirePermissions(Permissions.ManageGuild)]
        [Command("RemoveChatExit")]
        public async Task RemoveChatExit(CommandContext ctx)
        {
            try
            {
                string cs = Program.config[2];
                using var con = new MySqlConnection(cs);
                await con.OpenAsync();
                using var cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandText = $"UPDATE GERAL SET CHAT_SAIDA = NULL WHERE ID_SERVIDOR = {ctx.Guild.Id}";
                cmd.ExecuteNonQuery();
                con.Close();
                await ctx.TriggerTypingAsync();
                await ctx.RespondAsync($"{ctx.User.Mention} **Chat entrada** removido com sucesso.");
            }
            catch
            {
                await ctx.TriggerTypingAsync();
                await ctx.RespondAsync($"{ctx.User.Mention} Meu chapa, algo deu errado, poderia entrar em contato com o suporte :pensive: \n\n https://miguelito.miguelsoft.com.br/suporte/");
            }
        }

        [RequirePermissions(Permissions.ManageGuild)]
        [Command("ConfigureModelInput")]
        public async Task ConfigureModelInput(CommandContext ctx, DiscordChannel channel = null)
        {
            try
            {
                if (channel != null)
                {
                    string cs = Program.config[2];
                    using var con = new MySqlConnection(cs);
                    await con.OpenAsync();
                    using var cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = $"UPDATE GERAL SET MODELO_MESSAGEM_ENTRADA = {channel.Id} WHERE ID_SERVIDOR = {ctx.Guild.Id}";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    await ctx.TriggerTypingAsync();
                    await ctx.RespondAsync($"{ctx.User.Mention} Otimo, **Chat entrada** configurado com sucesso.");
                }
                else
                {
                    await ctx.TriggerTypingAsync();
                    await ctx.RespondAsync($"{ctx.User.Mention} Me perdoa mas eu ainda não tenho inteligência o suficiente para advinhar qual chat você quer configurar :pensive:");
                }
            }
            catch
            {
                await ctx.TriggerTypingAsync();
                await ctx.RespondAsync($"{ctx.User.Mention} Meu chapa, algo deu errado, poderia entrar em contato com o suporte :pensive: \n\n https://miguelito.miguelsoft.com.br/suporte/");
            }
        }

        [RequirePermissions(Permissions.ManageGuild)]
        [Command("BotGuildConfiguration")]
        public async Task BotGuildConfiguration(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            string cs = Program.config[2];
            using MySqlConnection con = new(cs);
            await con.OpenAsync();
            string sql = $"SELECT CHAT_ENTRADA, CHAT_SAIDA, CHAT_LOG, CHAT_PRINCIPAL, MENSAGUEM_ENTRADA FROM GERAL WHERE ID_SERVIDOR='{ctx.Guild.Id}'";
            MySqlCommand cmd = new(sql, con);
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            string mensagem = rdr[4].ToString();
            DiscordEmbedBuilder embed = new()
            {
                Title = "Configurações do servidor",
                Description = $"",
                Color = DiscordColor.CornflowerBlue
            };
            if (rdr[0].ToString() != "")
            {
                DiscordChannel entrada = ctx.Guild.GetChannel(Convert.ToUInt64(rdr[0]));
                embed.Description += $"**Channel entrada** {entrada.Mention}";
            }
            else
            {
                embed.Description += $"\n\n**Channel entrada** Não configurado";
            }
            if (rdr[1].ToString() != "")
            {
                DiscordChannel saida = ctx.Guild.GetChannel(Convert.ToUInt64(rdr[1]));
                embed.Description += $"\n\n**Channel saida** {saida.Mention}";
            }
            else
            {
                embed.Description += $"\n\n**Channel saida** Não configurado";
            }
            if (rdr[2].ToString() != "")
            {
                DiscordChannel log = ctx.Guild.GetChannel(Convert.ToUInt64(rdr[2]));
                embed.Description += $"\n\n**Channel log** {log.Mention}";
            }
            else
            {
                embed.Description += $"\n\n**Channel log** Não configurado";
            }
            if (rdr[3].ToString() != "")
            {
                DiscordChannel principal = ctx.Guild.GetChannel(Convert.ToUInt64(rdr[3]));
                embed.Description += $"\n\n**Channel principal** {principal.Mention}";
            }
            else
            {
                embed.Description += $"\n\n**Channel principal** Não configurado";
            }
            embed.Description += $"\n\n**Mensagem entrada** ```{mensagem}```";
            await ctx.RespondAsync(embed);
        }
    }
}
