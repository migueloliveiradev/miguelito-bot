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
            public async Task Info(InteractionContext ctx, [Option("user", "user you want information")] DiscordUser user = null)
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
                };
                embed.AddField("**Informações do Usuario**", $"Tag {user.Username}#{user.Discriminator}\n" +
                    $"ID {user.Id}\n" +
                    $"Criado {user.CreationTimestamp:dd/MM/yyyy}", true);
                embed.AddField("**Status**", $"{Methods.TranslateStatus(user.Presence.Status.GetName())}\n", true);
                if (ctx.Guild.Members.ContainsKey(user.Id))
                {
                    embed.AddField("**Informações de membro**", $"Entrou {Formatter.Timestamp(ctx.Guild.Members[user.Id].JoinedAt, TimestampFormat.ShortTime)}\n");
                }
                embed.WithFooter($"Solicitado por {ctx.User.Username}#{ctx.User.Discriminator}", ctx.User.AvatarUrl);
                embed.WithThumbnail(user.AvatarUrl);
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(embed));
                return;
            }
        }
    }
}