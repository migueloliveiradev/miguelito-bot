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
            string[,] jogo = new string[3, 3] { { " ", " ", " " }, { " ", " ", " " }, { " ", " ", " " } };
            List<DiscordButtonComponent> ButtonsDisponiveis = new List<DiscordButtonComponent>();
            List<DiscordButtonComponent> Buttons = new List<DiscordButtonComponent>();
            Random rnd = new();
            Buttons.Add(new(ButtonStyle.Secondary, "BT1", " "));
            Buttons.Add(new(ButtonStyle.Secondary, "BT2", " "));
            Buttons.Add(new(ButtonStyle.Secondary, "BT3", " "));
            Buttons.Add(new(ButtonStyle.Secondary, "BT4", " "));
            Buttons.Add(new(ButtonStyle.Secondary, "BT5", " "));
            Buttons.Add(new(ButtonStyle.Secondary, "BT6", " "));
            Buttons.Add(new(ButtonStyle.Secondary, "BT7", " "));
            Buttons.Add(new(ButtonStyle.Secondary, "BT8", " "));
            Buttons.Add(new(ButtonStyle.Secondary, "BT9", " "));
            DiscordMessageBuilder builder = new();
            builder
                .AddComponents(Buttons[0], Buttons[1], Buttons[2])
                .AddComponents(Buttons[3], Buttons[4], Buttons[5])
                .AddComponents(Buttons[6], Buttons[7], Buttons[8]);
            bool ganho = false;
            if (user == null)
            {
                await ctx.RespondAsync($"Opa {ctx.Member.Mention}, vamo jogar um jogo da velha?\n\n" +
                $"Você é o **X**\nEu sou o **O**");
                builder.WithContent($"Vc começa :smiley:");
                DiscordMessage message = await ctx.Client.SendMessageAsync(ctx.Channel, builder);

                Program.cliente.ComponentInteractionCreated += async (s, e) =>
                {
                    if (e.User == ctx.User && e.Message.Id == message.Id)
                    {
                        if (ganho == true)
                        {
                            for (int i = 0; i < Buttons.Count; i++)
                            {
                                Buttons[i] = new(Buttons[i].Style, Buttons[i].CustomId, Buttons[i].Label, true);
                            }
                            return;
                        }
                        await e.Interaction.CreateResponseAsync(InteractionResponseType.DeferredMessageUpdate);
                        if (e.Id == Buttons[0].CustomId)
                        {
                            Buttons[0] = new(ButtonStyle.Danger, "BT1", "X", true);
                            jogo[0, 0] = "X";
                        }
                        else if (e.Id == Buttons[1].CustomId)
                        {
                            Buttons[1] = new(ButtonStyle.Danger, "BT2", "X", true);
                            jogo[0, 1] = "X";
                        }
                        else if (e.Id == Buttons[2].CustomId)
                        {
                            Buttons[2] = new(ButtonStyle.Danger, "BT3", "X", true);
                            jogo[0, 2] = "X";
                        }
                        else if (e.Id == Buttons[3].CustomId)
                        {
                            Buttons[3] = new(ButtonStyle.Danger, "BT4", "X", true);
                            jogo[1, 0] = "X";
                        }
                        else if (e.Id == Buttons[4].CustomId)
                        {
                            Buttons[4] = new(ButtonStyle.Danger, "BT5", "X", true);
                            jogo[1, 1] = "X";
                        }
                        else if (e.Id == Buttons[5].CustomId)
                        {
                            Buttons[5] = new(ButtonStyle.Danger, "BT6", "X", true);
                            jogo[1, 2] = "X";
                        }
                        else if (e.Id == Buttons[6].CustomId)
                        {
                            Buttons[6] = new(ButtonStyle.Danger, "BT7", "X", true);
                            jogo[2, 0] = "X";
                        }
                        else if (e.Id == Buttons[7].CustomId)
                        {
                            Buttons[7] = new(ButtonStyle.Danger, "BT8", "X", true);
                            jogo[2, 1] = "X";
                        }
                        else if (e.Id == Buttons[8].CustomId)
                        {
                            Buttons[8] = new(ButtonStyle.Danger, "BT9", "X", true);
                            jogo[2, 2] = "X";
                        }

                        builder = new DiscordMessageBuilder();
                        builder.WithContent("Boa jogada, minha vez");
                        builder
                               .AddComponents(Buttons[0], Buttons[1], Buttons[2])
                               .AddComponents(Buttons[3], Buttons[4], Buttons[5])
                               .AddComponents(Buttons[6], Buttons[7], Buttons[8]);
                        await message.ModifyAsync(builder);

                        if (!Buttons[0].Disabled)
                        {
                            ButtonsDisponiveis.Add(Buttons[0]);
                        }
                        if (!Buttons[1].Disabled)
                        {
                            ButtonsDisponiveis.Add(Buttons[1]);
                        }
                        if (!Buttons[2].Disabled)
                        {
                            ButtonsDisponiveis.Add(Buttons[2]);
                        }
                        if (!Buttons[3].Disabled)
                        {
                            ButtonsDisponiveis.Add(Buttons[3]);
                        }
                        if (!Buttons[4].Disabled)
                        {
                            ButtonsDisponiveis.Add(Buttons[4]);
                        }
                        if (!Buttons[5].Disabled)
                        {
                            ButtonsDisponiveis.Add(Buttons[5]);
                        }
                        if (!Buttons[6].Disabled)
                        {
                            ButtonsDisponiveis.Add(Buttons[6]);
                        }
                        if (!Buttons[7].Disabled)
                        {
                            ButtonsDisponiveis.Add(Buttons[7]);
                        }
                        if (!Buttons[8].Disabled)
                        {
                            ButtonsDisponiveis.Add(Buttons[8]);
                        }
                        if (
                        jogo[0, 0] == "X" && jogo[0, 1] == "X" && jogo[0, 2] == "X"
                        || jogo[0, 0] == "X" && jogo[1, 0] == "X" && jogo[2, 0] == "X"
                        || jogo[0, 0] == "X" && jogo[1, 1] == "X" && jogo[2, 2] == "X"
                        || jogo[1, 0] == "X" && jogo[1, 1] == "X" && jogo[1, 2] == "X"
                        || jogo[0, 1] == "X" && jogo[1, 1] == "X" && jogo[2, 1] == "X"
                        || jogo[2, 0] == "X" && jogo[2, 1] == "X" && jogo[2, 2] == "X"
                        || jogo[2, 0] == "X" && jogo[1, 1] == "X" && jogo[0, 2] == "X"
                        || jogo[0, 2] == "X" && jogo[1, 2] == "X" && jogo[2, 2] == "X")
                        {
                            ganho = true;
                            await ctx.Client.SendMessageAsync(ctx.Channel, $"Opa {ctx.Member.Mention}, você ganhou!");
                            for (int i = 0; i < Buttons.Count; i++)
                            {
                                Buttons[i] = new(Buttons[i].Style, Buttons[i].CustomId, Buttons[i].Label, true);
                            }
                            ButtonsDisponiveis.Clear();
                            builder
                                  .AddComponents(Buttons[0], Buttons[1], Buttons[2])
                                  .AddComponents(Buttons[3], Buttons[4], Buttons[5])
                                  .AddComponents(Buttons[6], Buttons[7], Buttons[8]);
                            await message.ModifyAsync(builder);
                            return;
                        }
                        if (!ganho)
                        {
                            DiscordButtonComponent JogadaBot = ButtonsDisponiveis[rnd.Next(0, ButtonsDisponiveis.Count)];
                            for (int i = 0; i < Buttons.Count; i++)
                            {
                                if (Buttons[i].CustomId == JogadaBot.CustomId)
                                {
                                    Buttons[i] = new(ButtonStyle.Success, Buttons[i].CustomId, "O", true);
                                    if (Buttons[i].CustomId == "BT1")
                                    {
                                        jogo[0, 0] = "O";
                                    }
                                    else if (Buttons[i].CustomId == "BT2")
                                    {
                                        jogo[0, 1] = "O";
                                    }
                                    else if (Buttons[i].CustomId == "BT3")
                                    {
                                        jogo[0, 2] = "O";
                                    }
                                    else if (Buttons[i].CustomId == "BT4")
                                    {
                                        jogo[1, 0] = "O";
                                    }
                                    else if (Buttons[i].CustomId == "BT5")
                                    {
                                        jogo[1, 1] = "O";
                                    }
                                    else if (Buttons[i].CustomId == "BT6")
                                    {
                                        jogo[1, 2] = "O";
                                    }
                                    else if (Buttons[i].CustomId == "BT7")
                                    {
                                        jogo[2, 0] = "O";
                                    }
                                    else if (Buttons[i].CustomId == "BT8")
                                    {
                                        jogo[2, 1] = "O";
                                    }
                                    else if (Buttons[i].CustomId == "BT9")
                                    {
                                        jogo[2, 2] = "O";
                                    }
                                }
                            }
                            builder = new DiscordMessageBuilder();
                            builder.WithContent($"Joguei, sua vez {ctx.Member.Mention}");
                            builder
                                   .AddComponents(Buttons[0], Buttons[1], Buttons[2])
                                   .AddComponents(Buttons[3], Buttons[4], Buttons[5])
                                   .AddComponents(Buttons[6], Buttons[7], Buttons[8])
                                   .WithContent(message.Content);
                            await message.ModifyAsync(builder);

                            if (
                               jogo[0, 0] == "O" && jogo[0, 1] == "O" && jogo[0, 2] == "O"
                               || jogo[0, 0] == "O" && jogo[1, 0] == "O" && jogo[2, 0] == "O"
                               || jogo[0, 0] == "O" && jogo[1, 1] == "O" && jogo[2, 2] == "O"
                               || jogo[1, 0] == "O" && jogo[1, 1] == "O" && jogo[1, 2] == "O"
                               || jogo[0, 1] == "O" && jogo[1, 1] == "O" && jogo[2, 1] == "O"
                               || jogo[2, 0] == "O" && jogo[2, 1] == "O" && jogo[2, 2] == "O"
                               || jogo[2, 0] == "O" && jogo[1, 1] == "O" && jogo[0, 2] == "O"
                               || jogo[0, 2] == "O" && jogo[1, 2] == "O" && jogo[2, 2] == "O")
                            {
                                ganho = true;
                                await ctx.Client.SendMessageAsync(ctx.Channel, $"Opa Eu Ganhei");
                                for (int i = 0; i < Buttons.Count; i++)
                                {
                                    Buttons[i] = new(Buttons[i].Style, Buttons[i].CustomId, Buttons[i].Label, true);
                                }
                                ButtonsDisponiveis.Clear();
                                builder
                                      .AddComponents(Buttons[0], Buttons[1], Buttons[2])
                                      .AddComponents(Buttons[3], Buttons[4], Buttons[5])
                                      .AddComponents(Buttons[6], Buttons[7], Buttons[8])
                                      .WithContent(message.Content);
                                await message.ModifyAsync(builder);
                                return;
                            }
                            else if (ButtonsDisponiveis.Count == 0)
                            {
                                ganho = true;
                                await ctx.Client.SendMessageAsync(ctx.Channel, $"Opa {ctx.Member.Mention}, você empatou!");
                                for (int i = 0; i < Buttons.Count; i++)
                                {
                                    Buttons[i] = new(Buttons[i].Style, Buttons[i].CustomId, Buttons[i].Label, true);
                                }
                                ButtonsDisponiveis.Clear();
                                builder
                                      .AddComponents(Buttons[0], Buttons[1], Buttons[2])
                                      .AddComponents(Buttons[3], Buttons[4], Buttons[5])
                                      .AddComponents(Buttons[6], Buttons[7], Buttons[8])
                                      .WithContent(message.Content);
                                await message.ModifyAsync(builder);
                            }
                        }
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
                    if (e.User == ctx.User || e.User == user)
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
