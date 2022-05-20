using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace miguelito_bot_commands.commands
{
    internal class commands_games : BaseCommandModule
    {
        [Command("jogo-da-velha"), Aliases("jogoveia", "jogodaveia", "jogodavelha")]
        public async Task jogo_da_velha(CommandContext ctx, DiscordUser user = null)
        {
            await ctx.TriggerTypingAsync();
            string[][] jogo = null;
            List<DiscordButtonComponent> ButtonsDisponiveis = null;
            Random rnd = new();
            DiscordButtonComponent BT1 = new (ButtonStyle.Secondary, "BT1", " ");
            DiscordButtonComponent BT2 = new (ButtonStyle.Secondary, "BT2", " ");
            DiscordButtonComponent BT3 = new (ButtonStyle.Secondary, "BT3", " ");
            DiscordButtonComponent BT4 = new (ButtonStyle.Secondary, "BT4", " ");
            DiscordButtonComponent BT5 = new (ButtonStyle.Secondary, "BT5", " ");
            DiscordButtonComponent BT6 = new (ButtonStyle.Secondary, "BT6", " ");
            DiscordButtonComponent BT7 = new (ButtonStyle.Secondary, "BT7", " ");
            DiscordButtonComponent BT8 = new (ButtonStyle.Secondary, "BT8", " ");
            DiscordButtonComponent BT9 = new (ButtonStyle.Secondary, "BT9", " ");
            DiscordMessageBuilder builder = new ();
            builder
                .AddComponents(BT1, BT2, BT3)
                .AddComponents(BT4, BT5, BT6)
                .AddComponents(BT7, BT8, BT9);
            
            if(user == null)
            {
                await ctx.RespondAsync($"Opa {ctx.Member.Mention}, vamo jogar um jogo da velha?\n\n" +
                $"Você é o **X**\nEu sou o **O**");
                builder.WithContent($"Vc começa :smiley:");
                await ctx.Client.SendMessageAsync(ctx.Channel,builder);

                Program.cliente.ComponentInteractionCreated += async (s, e) =>
                {
                    if(e.User == ctx.User)
                    {
                        await e.Interaction.CreateResponseAsync(InteractionResponseType.DeferredMessageUpdate);
                        if (e.Id == BT1.CustomId)
                        {
                            BT1 = new(ButtonStyle.Secondary, "BT1", "X", true);
                            jogo[0][0] = "X";
                        }
                        else if (e.Id == BT2.CustomId)
                        {
                            BT2 = new(ButtonStyle.Secondary, "BT2", "X", true);
                            jogo[0][1] = "X";
                        }
                        else if (e.Id == BT3.CustomId)
                        {
                            BT3 = new(ButtonStyle.Secondary, "BT3", "X", true);
                            jogo[0][2] = "X";
                        }
                        else if (e.Id == BT4.CustomId)
                        {
                            BT4 = new(ButtonStyle.Secondary, "BT4", "X", true);
                            jogo[1][0] = "X";
                        }
                        else if (e.Id == BT5.CustomId)
                        {
                            BT5 = new(ButtonStyle.Secondary, "BT5", "X", true);
                            jogo[1][1] = "X";
                        }
                        else if (e.Id == BT6.CustomId)
                        {
                            BT6 = new(ButtonStyle.Secondary, "BT6", "X", true);
                            jogo[1][2] = "X";
                        }
                        else if (e.Id == BT7.CustomId)
                        {
                            BT7 = new(ButtonStyle.Secondary, "BT7", "X", true);
                            jogo[2][0] = "X";
                        }
                        else if (e.Id == BT8.CustomId)
                        {
                            BT8 = new(ButtonStyle.Secondary, "BT8", "X", true);
                            jogo[2][1] = "X";
                        }
                        else if (e.Id == BT9.CustomId)
                        {
                            BT9 = new(ButtonStyle.Secondary, "BT9", "X", true);
                            jogo[2][2] = "X";
                        }
                        builder.WithContent("Boa jogada, minha vez");
                        await ctx.Client.SendMessageAsync(ctx.Channel, builder);

                        if (!BT1.Disabled)
                        {
                            ButtonsDisponiveis.Add(BT1);
                        }
                        if (!BT2.Disabled)
                        {
                            ButtonsDisponiveis.Add(BT2);
                        }
                        if (!BT3.Disabled)
                        {
                            ButtonsDisponiveis.Add(BT3);
                        }
                        if (!BT4.Disabled)
                        {
                            ButtonsDisponiveis.Add(BT4);
                        }
                        if (!BT5.Disabled)
                        {
                            ButtonsDisponiveis.Add(BT5);
                        }
                        if (!BT6.Disabled)
                        {
                            ButtonsDisponiveis.Add(BT6);
                        }
                        if (!BT7.Disabled)
                        {
                            ButtonsDisponiveis.Add(BT7);
                        }
                        if (!BT8.Disabled)
                        {
                            ButtonsDisponiveis.Add(BT8);
                        }
                        if (!BT9.Disabled)
                        {
                            ButtonsDisponiveis.Add(BT9);
                        }
                        DiscordButtonComponent JogadaBot = ButtonsDisponiveis[rnd.Next(0, ButtonsDisponiveis.Count)];
                        JogadaBot = new(ButtonStyle.Secondary, JogadaBot.CustomId, "O", true);
                        builder.WithContent($"Joguei, sua vez {ctx.Member.Mention}");
                        await ctx.Client.SendMessageAsync(ctx.Channel, builder);
                        ButtonsDisponiveis.Clear();
                    }
                    
                };
            }
            else
            {
                builder.WithContent($"Opa {ctx.Member.Mention} vai um jogo com o {user.Mention}, por favor confirmem para continuar")
                .AddComponents(new DiscordComponent[]
                    {
                        new DiscordButtonComponent(ButtonStyle.Success, "BT1", "Iniciar"),
                        new DiscordButtonComponent(ButtonStyle.Danger, "BT2", "Cancelar"),
                    });
                Program.cliente.ComponentInteractionCreated += async (s, e) =>
                {
                    await e.Interaction.CreateResponseAsync(InteractionResponseType.DeferredMessageUpdate);
                    if(e.User == ctx.User || e.User == user)
                    {
                        if (e.Id.Contains("BT1"))
                        {
                            await ctx.Client.SendMessageAsync(ctx.Channel, $"{e.User.Mention} Confirmou");
                        }   
                        else if (e.Id.Contains("BT2"))
                        {
                            await ctx.Client.SendMessageAsync(ctx.Channel, $"{e.User.Mention} Confirmou");
                        }
                    }
                };
                await ctx.RespondAsync(builder);
            }
           
            
        }
    }
}
