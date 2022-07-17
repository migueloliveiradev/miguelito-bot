using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using miguelito_bot_slashcommands.Utils;
using Newtonsoft.Json;
using SpotifyAPI.Web;
using System.Net;
using Tweetinvi;
using YoutubeExplode;

namespace miguelito_bot_slashcommands.slashcommands.Search
{
    internal class Search : ApplicationCommandModule
    {
        [SlashCommandGroup("search", "Search related commands")]
        public class GroupContainer : ApplicationCommandModule
        {
            //Cliente Youtube
            private YoutubeClient Youtube = new();

            //Client Twitter
            private TwitterClient Twitter = new(Program.config[9], Program.config[10], Program.config[11], Program.config[12]);

            [SlashCommand("google", "Search ┇ search for a result on google")]
            public async Task SearchGoogle(InteractionContext ctx,
                [Option("query", "Type what you want to search")] string search,
                [Choice("Search", 0)]
                [Choice("Image", 1)]
                [Option("result_type", "Optionally choose a specific search")] long type = 0)
            {
                await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);
                if (type == 0)
                {
                    string cx = "ed5b1310ccee9e87b";
                    string url = $"https://www.googleapis.com/customsearch/v1?key={Program.config[13]}&cx={cx}&q={search}";
                    var request = WebRequest.Create(url);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream data = response.GetResponseStream();
                    StreamReader reader = new(data);
                    string rs = reader.ReadToEnd();
                    dynamic? json = JsonConvert.DeserializeObject(rs);
                    string? title = json?.items[0]?.title;
                    string? link = json?.items[0].link;
                    string? snippet = json?.items[0].snippet;
                    string? image = json?.items[0].pagemap.cse_image[0].src;
                    string description = $"**Título:** {title}\n" +
                                         $"**Link:** {link}\n" +
                                         $"**Descrição:** {snippet}\n";

                    DiscordEmbedBuilder embed = new()
                    {
                        Title = "Resultado da pesquisa",
                        Color = DiscordColor.CornflowerBlue,
                        Description = description,
                        ImageUrl = image
                    };
                    await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
                }
                else if (type == 1)
                {
                    string cx = "ed5b1310ccee9e87b";
                    string url = $"https://www.googleapis.com/customsearch/v1?key={Program.config[13]}&cx={cx}&q={search}&searchType=image";
                    var request = WebRequest.Create(url);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream data = response.GetResponseStream();
                    StreamReader reader = new(data);
                    string rs = reader.ReadToEnd();
                    dynamic? json = JsonConvert.DeserializeObject(rs);
                    string? title = json?.items[0]?.title;
                    string? img = json?.items[0].link;
                    string? snippet = json?.items[0].snippet;
                    string? link = json?.items[0].image.contextLink;
                    string description = $"**Título:** {title}\n" +
                                         $"**Link** {link}";

                    DiscordEmbedBuilder embed = new()
                    {
                        Title = "Resultado da pesquisa",
                        Color = DiscordColor.CornflowerBlue,
                        Description = description,
                        ImageUrl = img
                    };

                    await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
                }
                await Methods.CommandsUsed("Search Google", ctx.Guild.Id, ctx.User.Id);
                return;
            }

            [SlashCommand("youtube", "Search ┇ search for something on youtube")]
            public async Task SearchYoutube(InteractionContext ctx,
                [Option("query", "Type what you want to search")] string search,
                [Choice("Video", 1)]
                [Choice("Playlist", 2)]
                [Choice("Channel", 3)]
                [Option("result_type", "Optionally choose a specific search")] long type = 0)
            {
                try
                {
                    await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);
                    if (type == 1)
                    {
                        await foreach (var video in Youtube.Search.GetVideosAsync(search))
                        {
                            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent(video.Url));
                            break;
                        }
                    }
                    else if (type == 2)
                    {
                        await foreach (var playlists in Youtube.Search.GetPlaylistsAsync(search))
                        {
                            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent(playlists.Url));
                            break;
                        }
                    }
                    else if (type == 3)
                    {
                        await foreach (var channels in Youtube.Search.GetChannelsAsync(search))
                        {
                            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent(channels.Url));
                            break;
                        }
                    }
                    else if (type == 0)
                    {
                        await foreach (var result in Youtube.Search.GetResultsAsync(search))
                        {
                            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent(result.Url));
                            break;
                        }
                    }
                    await Methods.CommandsUsed("Search Youtube", ctx.Guild.Id, ctx.User.Id);
                    return;
                }
                catch
                {
                    await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Ocorreu um erro ao tentar pesquisar."));
                }
            }

            [SlashCommand("Spotify", "Search ┇ search for a result on spotify")]
            public async Task SearchSpotify(InteractionContext ctx,
                [Option("query", "Type what you want to search")] string search,
                [Choice("Tracks", 1)]
                [Choice("Albums", 2)]
                [Choice("Shows", 3)]
                [Choice("Episodes", 4)]
                [Choice("Playlists", 5)]
                [Choice("Artists", 6)]
                [Option("result_type", "Optionally choose a specific search")] long type = 1)
            {
                try
                {
                    await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);
                    SpotifyClientConfig config = SpotifyClientConfig.CreateDefault();
                    ClientCredentialsRequest request = new(Program.config[15], Program.config[16]);
                    ClientCredentialsTokenResponse response = await new OAuthClient(config).RequestToken(request);
                    SpotifyClient spotify = new(config.WithToken(response.AccessToken));
                    SearchRequest Request = new(SearchRequest.Types.All, search);
                    SearchResponse resultado = await spotify.Search.Item(Request);
                    if (type == 1)
                    {
                        await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent(resultado?.Tracks?.Items?[0].ExternalUrls.First().Value));
                    }
                    else if (type == 2)
                    {
                        await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent(resultado?.Albums?.Items?[0].ExternalUrls.First().Value));
                    }
                    else if (type == 3)
                    {
                        await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent(resultado?.Shows?.Items?[0].ExternalUrls.First().Value));
                    }
                    else if (type == 4)
                    {
                        await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent(resultado?.Episodes?.Items?[0].ExternalUrls.First().Value));
                    }
                    else if (type == 5)
                    {
                        await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent(resultado?.Playlists?.Items?[0].ExternalUrls.First().Value));
                    }
                    else if (type == 6)
                    {
                        await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent(resultado?.Artists?.Items?[0].ExternalUrls.First().Value));
                    }
                    await Methods.CommandsUsed("Search Spotify", ctx.Guild.Id, ctx.User.Id);
                    return;
                }
                catch
                {
                    await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Ocorreu um erro ao tentar buscar."));
                }
            }

            [SlashCommand("twitter", "Search ┇ search for a result on twitter")]
            public async Task SearchTwitter(InteractionContext ctx,
                [Option("query", "Type what you want to search")] string search,
                [Choice("User", 1)]
                [Choice("Tweet", 2)]
                [Option("result_type", "Optionally choose a specific search")] long type = 2)
            {
                await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);
                if (type == 2)
                {
                    var tweet = await Twitter.Search.SearchTweetsAsync(search);
                    await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent(tweet[0].Url));
                }
                else if (type == 1)
                {
                    try
                    {
                        var user = await Twitter.UsersV2.GetUserByNameAsync(search);
                        await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent($"https://twitter.com/{user.User.Username}"));
                    }
                    catch
                    {
                        await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Não consegui achar o usuario pedido."));
                        return;
                    }
                }
                await Methods.CommandsUsed("Search Youtube", ctx.Guild.Id, ctx.User.Id);
                return;
            }
        }
    }
}