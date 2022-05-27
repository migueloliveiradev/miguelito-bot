using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace miguelito_bot_commands.commands
{
    internal class commands_administration : BaseCommandModule
    {
        [Command("ban")]
        public async Task ban(CommandContext ctx, DiscordMember user = null)
        {
            await ctx.TriggerTypingAsync();
            if (user == ctx.User)
            {
                DiscordButtonComponent ButtonBan = new DiscordButtonComponent(ButtonStyle.Success, "ButtonBan", "Confirmar");
                DiscordButtonComponent ButtonBanCancel = new DiscordButtonComponent(ButtonStyle.Danger, "ButtonBanCancel", "Cancelar");
                DiscordMessageBuilder builder = new DiscordMessageBuilder();

                builder.WithContent($"Opa {ctx.Member.Mention} pelo visto você não vai muito com a propia cara, mas por favor confirme seu auto banimento.")
                       .AddComponents(ButtonBan, ButtonBanCancel);
                DiscordMessage messagem = await ctx.RespondAsync(builder);
                ctx.Client.ComponentInteractionCreated += async (s, e) =>
                {
                    if (e.User == ctx.User && e.Message.Id == messagem.Id)
                    {
                        await e.Interaction.CreateResponseAsync(InteractionResponseType.DeferredMessageUpdate);
                        if (e.Id == "ButtonBan")
                        {
                            await user.RemoveAsync();
                            await messagem.DeleteAsync();
                            await ctx.Client.SendMessageAsync(ctx.Channel, "O trouxa se baniu do servidor :joy::joy::joy::joy:");
                            return;
                        }
                        else if (e.Id == "ButtonBanCancel")
                        {
                            await messagem.DeleteAsync();
                            await ctx.Message.DeleteAsync();
                            await ctx.Client.SendMessageAsync(ctx.Channel, $"Ala, o {ctx.Member.Mention} queria se banir mas peidou :joy:");
                            return;
                        }
                    }
                };

            }
            else if (ctx.Member.Permissions.HasPermission(Permissions.BanMembers) && user != null)
            {
                if (!user.IsOwner)
                {
                    DiscordButtonComponent ButtonBan = new DiscordButtonComponent(ButtonStyle.Success, "ButtonBan", "Confirmar");
                    DiscordButtonComponent ButtonBanCancel = new DiscordButtonComponent(ButtonStyle.Danger, "ButtonBanCancel", "Cancelar");
                    DiscordMessageBuilder builder = new DiscordMessageBuilder();
                    if (user != ctx.User)
                    {
                        builder.WithContent($"Opa {ctx.Member.Mention} por favor confirme o banimento.")
                               .AddComponents(ButtonBan, ButtonBanCancel);
                        DiscordMessage messagem = await ctx.RespondAsync(builder);
                        ctx.Client.ComponentInteractionCreated += async (s, e) =>
                        {
                            if (e.User == ctx.User && e.Message.Id == messagem.Id)
                            {
                                await e.Interaction.CreateResponseAsync(InteractionResponseType.DeferredMessageUpdate);
                                if (e.Id == "ButtonBan")
                                {
                                    await user.BanAsync();
                                    await messagem.DeleteAsync();
                                    await ctx.Client.SendMessageAsync(ctx.Channel, $"{user.DisplayName}({user.Id}) foi banido por {ctx.Member.Mention}");
                                    return;
                                }
                                else if (e.Id == "ButtonBanCancel")
                                {
                                    await messagem.DeleteAsync();
                                    await ctx.Message.DeleteAsync();
                                    return;
                                }
                            }
                        };
                    }
                    else if (user == ctx.User)
                    {
                        builder.WithContent($"{ctx.Member.Mention}, pelo visto vc " +
                        $"não vai muito com a propia cara, mas enfim por gentileza " +
                        $"confirme ou cancele seu auto banimento.")
                               .AddComponents(ButtonBan, ButtonBanCancel);
                        DiscordMessage messagem = await ctx.RespondAsync(builder);
                        ctx.Client.ComponentInteractionCreated += async (s, e) =>
                        {
                            if (e.User == ctx.User)
                            {
                                await e.Interaction.CreateResponseAsync(InteractionResponseType.DeferredMessageUpdate);
                                if (e.Id == "ButtonBan")
                                {
                                    await user.BanAsync();
                                    await messagem.DeleteAsync();
                                    await ctx.Client.SendMessageAsync(ctx.Channel, $"{user.DisplayName}({user.Id}) foi banido por {ctx.Member.Mention}");
                                    return;
                                }
                                else if (e.Id == "ButtonBanCancel")
                                {
                                    await messagem.DeleteAsync();
                                    await ctx.Message.DeleteAsync();
                                    return;
                                }
                            }
                        };
                    }
                }
                else
                {
                    await ctx.RespondAsync($"Opa {ctx.User.Mention}, se liga meu rei, tu não pode banir o dono :rolling_eyes:");
                }
            }
            else if (ctx.Member.Permissions.HasPermission(Permissions.BanMembers) && user == null)
            {
                await ctx.RespondAsync("Da proxima vez que você quiser que eu adivinhe quem você quer banir eu vou banir é tu :rage:");
            }
            else if (!ctx.Member.Permissions.HasPermission(Permissions.BanMembers) && user.IsOwner)
            {
                await ctx.RespondAsync($"Putz {ctx.User.Mention} não sei o que é pior, você querer banir o dono " +
                    $"ou você não ter permissão pra banir alguem :joy:");
            }
            else if (!ctx.Member.Permissions.HasPermission(Permissions.BanMembers) && user != null)
            {
                await ctx.RespondAsync($"Putz {ctx.User.Mention} quem tu acha que é pra banir alguem :joy:");
            }
            else if (!ctx.Member.Permissions.HasPermission(Permissions.BanMembers) && user == null)
            {
                await ctx.RespondAsync($"Putz {ctx.User.Mention} não sei o que é pior, você querer que eu " +
                    $"advinhe quem é pra banir ou você não ter permissão pra banir alguem :joy:");
            }
        }

        [RequirePermissions(Permissions.BanMembers)]
        [Command("unban")]
        public async Task unban(CommandContext ctx, ulong id = 0)
        {
            if (id != 0)
            {
                var user = await ctx.Client.GetUserAsync(id);
                foreach (var result in ctx.Guild.GetBansAsync().Result)
                {
                    if (user == result.User)
                    {
                        await user.UnbanAsync(ctx.Guild);
                        await ctx.RespondAsync($"Eita, o {user.Username}({user.Id}) foi desbanido do servidor.");
                        break;
                    }
                }
            }
            else
            {
                await ctx.RespondAsync("Por gentileza coloque o ID do usuario que deseja desbanir");
            }
        }

        [RequirePermissions(Permissions.KickMembers)]
        [Command("kick")]
        public async Task kick(CommandContext ctx, DiscordMember user = null)
        {
            DiscordButtonComponent ButtonKick = new DiscordButtonComponent(ButtonStyle.Success, "ButtonKick", "Confirmar");
            DiscordButtonComponent ButtonKickCancel = new DiscordButtonComponent(ButtonStyle.Danger, "ButtonKickCancel", "Cancelar");
            DiscordMessageBuilder builder = new DiscordMessageBuilder();
            if (user != ctx.User && user != null && !user.IsOwner)
            {
                builder.WithContent($"Opa {ctx.Member.Mention} por favor confirme o kick.")
                       .AddComponents(ButtonKick, ButtonKickCancel);
                DiscordMessage messagem = await ctx.RespondAsync(builder);
                ctx.Client.ComponentInteractionCreated += async (s, e) =>
                {
                    if (e.User == ctx.User)
                    {
                        await e.Interaction.CreateResponseAsync(InteractionResponseType.DeferredMessageUpdate);
                        if (e.Id.Contains("ButtonKick"))
                        {
                            await user.RemoveAsync();
                            await messagem.DeleteAsync();
                            await ctx.Client.SendMessageAsync(ctx.Channel, $"{user.DisplayName}({user.Id}) foi expulso por {ctx.Member.Mention}");
                            return;
                        }
                        else if (e.Id.Contains("ButtonKickCancel"))
                        {
                            await messagem.DeleteAsync();
                            await ctx.Message.DeleteAsync();
                            return;
                        }
                    }
                };
            }
            else if (user == null)
            {
                await ctx.RespondAsync("Na proxima vez que vc quiser usar o comando de kick e não defiir o usuario quem vai legar kick é você :rage:");
            }
            else if (user.IsOwner)
            {
                await ctx.RespondAsync("Ala o cara querendo kitar o dono do servidor :joy:");
            }
        }

        [RequirePermissions(Permissions.ManageMessages)]
        [Command("clear"), Aliases("limpar")]
        public async Task clear(CommandContext ctx, int quantity = 0)
        {
            if (quantity <= 500)
            {
                var mensagens = await ctx.Channel.GetMessagesAsync(quantity);
                await ctx.Channel.DeleteMessagesAsync(mensagens);
                await ctx.Client.SendMessageAsync(ctx.Channel, $"O chat teve {quantity} mensagens apagadas por {ctx.User.Mention} :broom:");
            }
            else if (quantity == 0)
            {
                var mensagens = await ctx.Channel.GetMessagesAsync(100);
                await ctx.Channel.DeleteMessagesAsync(mensagens);
                await ctx.Client.SendMessageAsync(ctx.Channel, $"O chat teve 100 mensagens apagadas por {ctx.User.Mention} :broom:");
            }
            else if (quantity > 500)
            {
                await ctx.RespondAsync("Infelizmente ainda sou um pequeno bot que suporta apagar o maximo de 500 mensagens");
            }
        }
        
    }
}
