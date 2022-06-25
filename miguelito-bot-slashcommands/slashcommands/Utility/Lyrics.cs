using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using miguelito_bot_slashcommands.Utils;
using Newtonsoft.Json;

namespace miguelito_bot_slashcommands.slashcommands.Utility
{
    internal class Lyrics : ApplicationCommandModule
    {
        [SlashCommand("Lyrics", "See a lyric of music")]
        public async Task LyricsSearch(InteractionContext ctx, [Option("Artist", "Who is Artist?")] string artist, [Option("Musica", "What is the music?")] string music)
        {
            string url = $"https://api.lyrics.ovh/v1/{artist}/{music}";
            string json = await new HttpClient().GetStringAsync(url);
            dynamic? data = JsonConvert.DeserializeObject(json);
            string? lyrics = data?.lyrics;
            DiscordEmbedBuilder embed = new()
            {
                Description = lyrics,
                Color = Variables.Cores()
            };
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(embed));
        }
    }
}