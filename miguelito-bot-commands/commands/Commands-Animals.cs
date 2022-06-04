using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using miguelito_bot_commands.Utils;
using Newtonsoft.Json;
using System.Net;

namespace miguelito_bot_commands.commands
{
    internal class Commands_Animals : BaseCommandModule
    {
        [Command("gato"), Aliases("cats", "fofo")]
        public async Task gato(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            string url = "https://api.thecatapi.com/v1/images/search";
            string json = new WebClient().DownloadString(url);
            dynamic data = JsonConvert.DeserializeObject(json);
            string cats = data[0].url;
            Console.WriteLine(cats);
            DiscordEmbedBuilder embed = new()
            {
                Color = Variables.Cores(),
                ImageUrl = cats,
            };
            await ctx.RespondAsync($"Opa {ctx.Member.Mention} espero q goste desse gato fofo q escolhi :3", embed);
            await Program.Log("gato");
            return;
        }

        [Command("cachorros"), Aliases("dog", "Cão")]
        public async Task dogs(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            string url = "https://dog.ceo/api/breeds/image/random";
            string json = new WebClient().DownloadString(url);
            dynamic data = JsonConvert.DeserializeObject(json);
            string dogs = data.message;
            DiscordEmbedBuilder embed = new()
            {
                Color = Variables.Cores(),
                ImageUrl = dogs,
            };
            await ctx.RespondAsync($"Opa {ctx.Member.Mention} espero q goste desse cachorro fofo q escolhi :3", embed);
            await Program.Log("gato");
            return;
        }

        [Command("raposa"), Aliases("fox", "firefox")]
        public async Task fox(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            string url = "https://randomfox.ca/floof/";
            string json = new WebClient().DownloadString(url);
            dynamic data = JsonConvert.DeserializeObject(json);
            string fox = data.image;
            DiscordEmbedBuilder embed = new()
            {
                Color = Variables.Cores(),
                ImageUrl = fox,
            };
            await ctx.RespondAsync($"Opa {ctx.Member.Mention} espero q goste dessa raposa fofa q escolhi :3", embed);
            await Program.Log("gato");
            return;
        }

        [Command("pato"), Aliases("duck")]
        public async Task duck(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            string url = "https://random-d.uk/api/random?format=json";
            string json = new WebClient().DownloadString(url);
            dynamic data = JsonConvert.DeserializeObject(json);
            string img = data.url;
            DiscordEmbedBuilder embed = new()
            {
                Color = Variables.Cores(),
                ImageUrl = img,
            };
            await ctx.RespondAsync($"Opa {ctx.Member.Mention} espero q goste desse pato fofo q escolhi :3", embed);
            await Program.Log("gato");
            return;
        }
    }
}
