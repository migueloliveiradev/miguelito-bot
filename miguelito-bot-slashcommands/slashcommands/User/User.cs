using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miguelito_bot_slashcommands.slashcommands.User
{
    internal class User : ApplicationCommandModule
    {
        [SlashCommandGroup("user", "commands to the user")]
        public class GroupContainer : ApplicationCommandModule
        {
            [SlashCommand("avatar", "receive your avatar or the desired user")]
            public async Task avatar(InteractionContext ctx, [Option("user", "user you want icon")] DiscordUser user = null)
            {
                DiscordEmbedBuilder embed;
                if (user == null)
                {
                    user = ctx.User;
                    embed = new DiscordEmbedBuilder
                    {
                        Color = DiscordColor.CornflowerBlue,
                        ImageUrl = user.AvatarUrl,
                    };
                    embed.WithAuthor(user.Username, null, user.AvatarUrl).
                        WithFooter($"Solicitado por {ctx.User.Username}#{ctx.User.Discriminator}", ctx.User.AvatarUrl);
                }
                else
                {
                    embed = new DiscordEmbedBuilder
                    {
                        Color = DiscordColor.CornflowerBlue,
                        ImageUrl = user.AvatarUrl,
                    };
                    embed.WithAuthor(user.Username, null, user.AvatarUrl).
                        WithFooter($"Solicitado por {ctx.User.Username}#{ctx.User.Discriminator}", ctx.User.AvatarUrl);
                }
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(embed));
            }

            [SlashCommand("info", "receive your information or the desired user")]
            public async Task info(InteractionContext ctx, [Option("user", "user you want information")] DiscordUser user = null)
            {
                DiscordEmbedBuilder embed;
                if (user == null)
                {
                    user = ctx.User;
                    embed = new DiscordEmbedBuilder
                    {
                        Title = ":person_doing_cartwheel: " + user.Username,
                        Color = color.cores(),
                        ImageUrl = user.GetAvatarUrl(ImageFormat.Auto, 2048),
                        Description = ":hash: Tag do Discord: **" + user.Username + "#" + user.Discriminator + "**\n\n" +
                        ":detective: ID do Discord: **" + user.Id + "**\n\n" +
                        ":hourglass_flowing_sand: Conta criada em: **" + user.CreationTimestamp.ToString().Replace("+00:00", "") + "**",
                    };
                }
                else
                {
                    embed = new DiscordEmbedBuilder
                    {
                        Title = ":person_doing_cartwheel: " + user.Username,
                        Color = color.cores(),
                        ImageUrl = user.GetAvatarUrl(ImageFormat.Auto, 2048),
                        Description = ":hash: Tag do Discord: **" + user.Username + "#" + user.Discriminator + "**\n\n" +
                         ":detective: ID do Discord: **" + user.Id + "**\n\n" +
                         ":hourglass_flowing_sand: Conta criada em: **" + user.CreationTimestamp.ToString().Replace("+00:00", "") + "**"
                    };
                }
                embed.WithThumbnail(user.AvatarUrl);
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(embed));
            }
        }
    }
}
