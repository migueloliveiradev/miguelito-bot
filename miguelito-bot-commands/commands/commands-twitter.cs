using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Tweetinvi;

namespace miguelito_bot_commands.commands
{
    internal class commands_twitter : BaseCommandModule
    {
        private TwitterClient client = new(Program.config[9], Program.config[10], Program.config[11], Program.config[12]);

        [Command("TwitterInfo")]
        public async Task twitterInfo(CommandContext ctx, string user)
        {
            try
            {
                var usuario = await client.Users.GetUserAsync(user);
                var embed = new DiscordEmbedBuilder
                {
                    Color = DiscordColor.CornflowerBlue,
                    Description =
                    $"**{usuario.Description}**\n\n" +
                    $"**Criado em: {usuario.CreatedAt.DateTime} {(usuario.CreatedAt.DateTime.Date - DateTime.Now).Days.ToString().Remove(0, 1)} dias**\n\n" +
                    $"**Curtidas**\n{usuario.FavoritesCount} \n" +
                    $"**Seguidores**\n{usuario.FollowersCount} \n" +
                    $"**Seguindo**\n{usuario.FriendsCount} \n" +
                    $"**Tweets**\n{usuario.StatusesCount} \n",
                    ImageUrl = usuario.ProfileBannerURL,
                }.WithAuthor(usuario.Name, "https://twitter.com/" + usuario.ScreenName,
                usuario.ProfileImageUrl400x400).WithThumbnail(usuario.ProfileImageUrlFullSize);
                await ctx.RespondAsync(embed);
            }
            catch
            {
                await ctx.RespondAsync("não consegui encontrar o usuario digitado :pensive:");
            }
        }

        [Command("TwitterIcon")]
        public async Task TwitterIcon(CommandContext ctx, string user)
        {
            try
            {
                var usuario = await client.Users.GetUserAsync(user);
                var embed = new DiscordEmbedBuilder
                {
                    Color = DiscordColor.CornflowerBlue,
                    Description =
                    $"{Formatter.MaskedUrl("Baixar", new Uri(usuario.ProfileImageUrlFullSize), null)}",
                    ImageUrl = usuario.ProfileImageUrlFullSize,
                };
                embed.WithAuthor(usuario.Name, "https://twitter.com/" + usuario.ScreenName, usuario.ProfileImageUrl);
                await ctx.RespondAsync(embed);
            }
            catch
            {
                await ctx.RespondAsync("não consegui encontrar o usuario digitado :pensive:");
            }
        }
    }
}