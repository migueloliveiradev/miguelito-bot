using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using miguelito_bot_slashcommands.Utils;

namespace miguelito_bot_slashcommands.slashcommands.User
{
    internal class User : ApplicationCommandModule
    {
        [SlashCommandGroup("user", "commands to the user")]
        public class GroupContainer : ApplicationCommandModule
        {
            [SlashCommand("avatar", "User ┇ Receive your avatar or the desired user")]
            public async Task Avatar(InteractionContext ctx, [Option("user", "User you want icon")] DiscordUser user = null)
            {
                if (user == null)
                {
                    user = ctx.User;
                }
                DiscordEmbedBuilder embed = new()
                {
                    Color = Variables.Cores(),
                    ImageUrl = user.AvatarUrl,
                };
                embed.WithAuthor(user.Username, null, user.AvatarUrl).
                        WithFooter($"Solicitado por {ctx.User.Username}#{ctx.User.Discriminator}", ctx.User.AvatarUrl);

                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(embed));
                return;
            }

            [SlashCommand("info", "User ┇ Receive your information or the desired user")]
            public async Task info(InteractionContext ctx, [Option("user", "user you want information")] DiscordUser user = null)
            {
                if (user == null)
                {
                    user = ctx.User;
                }
                DiscordEmbedBuilder embed = new DiscordEmbedBuilder
                {
                    Title = ":person_doing_cartwheel: " + user.Username,
                    Color = Variables.Cores(),
                    ImageUrl = user.GetAvatarUrl(ImageFormat.Auto, 2048),
                    Description = $":hash: Tag do Discord: **{user.Username}#{user.Discriminator}**\n\n" +
                     $":detective: ID do Discord: **{user.Id}**\n\n" +
                     $":hourglass_flowing_sand: Conta criada em: **{user.CreationTimestamp:dd/MM/yyyy HH:mm}**",
                };
                embed.WithThumbnail(user.AvatarUrl);
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(embed));
                return;
            }
        }
    }
}