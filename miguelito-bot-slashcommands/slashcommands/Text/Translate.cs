﻿using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miguelito_bot_slashcommands.slashcommands.Text
{
    internal class Translate : ApplicationCommandModule
    {
        [SlashCommand("translate", "Translate ┇Translate a text")]
        public async Task translate(InteractionContext ctx,
        [Option("text", "text to be translated")] string text,
        [Option("lang", "result language")] string lang = "pt")
        {
            await ctx.CreateResponseAsync(new DiscordInteractionResponseBuilder().WithContent(Utils.Methods.Translator(text, lang)));
        }
    }
}
