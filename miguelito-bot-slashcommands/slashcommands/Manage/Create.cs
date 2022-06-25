using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace miguelito_bot_slashcommands.slashcommands.Manage
{
    internal class Create : ApplicationCommandModule
    {
        //[SlashCommandPermissions(Permissions.ManageChannels)]
        [SlashCommandGroup("Create", "Manage server.")]
        public class SubGroupContainer : ApplicationCommandModule
        {
            [SlashCommandGroup("Channel", "Create a channel.")]
            public class SubGroup : ApplicationCommandModule
            {
                [SlashCommand("Text", "Manage server ┇ Create a text channel.")]
                public async Task CreateChannelText(InteractionContext ctx,
                    [Option("name", "Name of channel create.")] string name,
                    [Option("category", "Select the category that this channel will be.")] DiscordChannel channel = null,
                    [Option("Topic", "Channel topic.")] string topic = null,
                    [Choice("yes", "true")]
                    [Choice("no", "false")]
                    [Option("is_nsfw", "If this channel is nsfw or not.")] string nsfw = null,
                    [Choice("5s", 5)]
                    [Choice("10s", 10)]
                    [Choice("15s", 15)]
                    [Choice("30s", 30)]
                    [Choice("1m", 60)]
                    [Choice("2m", 120)]
                    [Choice("5m", 300)]
                    [Choice("10m", 600)]
                    [Choice("15m", 900)]
                    [Choice("30m", 1800)]
                    [Choice("1h", 3600)]
                    [Choice("2h", 7200)]
                    [Choice("6h", 21600)]
                    [Option("Slow", "Slow mode timeout for users.")] long slow = 0,
                    [Option("position", "Sorting position of the channel.")] long position = 0,
                    [Option("reason", "SReason for audit logs.")] string reason = null)
                {
                    try
                    {
                        await ctx.Guild.CreateTextChannelAsync(name, channel, topic, null, Convert.ToBoolean(nsfw),
                              Convert.ToInt32(slow), Convert.ToInt32(position), reason);
                        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                            new DiscordInteractionResponseBuilder().WithContent($"Canal {name} criado com sucesso."));
                    }
                    catch
                    {
                        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                            new DiscordInteractionResponseBuilder().WithContent("Ocorreu um erro ao tentar criar esse canal.").AsEphemeral(true));
                    }

                }

                [SlashCommand("Voice", "Manage server ┇ Create a voice channel")]
                public async Task CreateChannelVoice(InteractionContext ctx,
                    [Option("name", "Name of channel create.")] string name,
                    [Option("category", "Select the category that this channel will be.")] DiscordChannel channel = null,
                    [Option("bitrate", "Bitrate of the channel.")] long bitrate = 0,
                    [Option("user_limite", "Maximum number of users in the channel.")] long user_limite = 0,
                    [Option("position", "Sorting position of the channel.")] long position = 0,
                    [Option("reason", "SReason for audit logs.")] string reason = null)
                {
                    try
                    {
                        await ctx.Guild.CreateVoiceChannelAsync(name, null, Convert.ToInt32(bitrate), Convert.ToInt32(user_limite),
                              null, null, Convert.ToInt32(position), reason);
                        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                            new DiscordInteractionResponseBuilder().WithContent($"Canal {name} criado com sucesso."));
                    }
                    catch
                    {
                        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                           new DiscordInteractionResponseBuilder().WithContent("Ocorreu um erro ao tentar criar esse canal.").AsEphemeral(true));
                    }

                }
            }

            [SlashCommand("Category", "Create a Category.")]
            public async Task CreateCategory(InteractionContext ctx,
                    [Option("name", "Name of Category create.")] string name)
            {
                try
                {
                    await ctx.Guild.CreateChannelCategoryAsync(name);
                    await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                        new DiscordInteractionResponseBuilder().WithContent($"Categoria {name} criada com sucesso."));
                }
                catch
                {
                    await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                        new DiscordInteractionResponseBuilder().WithContent("Ocorreu um erro ao tentar criar essa Categoria.").AsEphemeral(true));
                }

            }

            [SlashCommand("Sticker", "Create a Sticker")]
            public async Task CreateSticker(InteractionContext ctx,
                [Option("name", "Name of sticker create.")] string name,
                [Option("description", "description of sticker create.")] string description,
                [Option("tags", "Name of sticker create.")] string tags,
                [Option("image", "image of sticker create.")] DiscordAttachment img,
                    [Choice("PNG", 0)]
                    [Choice("APNG", 1)]
                    [Choice("LOTTIE", 2)]
                    [Option("Sticker_format", "Format in which the figurine will be created.")] long formatNum = 0)
            {
                try
                {
                    StickerFormat format = StickerFormat.PNG;
                    if (formatNum == 1)
                    {
                        format = StickerFormat.APNG;
                    }
                    else if (formatNum == 2)
                    {
                        format = StickerFormat.LOTTIE;
                    }
                    Stream Sticker = await new HttpClient().GetStreamAsync(img.Url);
                    await ctx.Guild.CreateStickerAsync(name, description, tags, Sticker, format);
                    await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                        new DiscordInteractionResponseBuilder().WithContent($"Cargo {name} criado com sucesso."));
                }
                catch
                {
                    await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                        new DiscordInteractionResponseBuilder().WithContent("Ocorreu um erro ao tentar criar esse canal.").AsEphemeral(true));
                }

            }
            [SlashCommand("Emoji", "Create a Emoji")]
            public async Task CreateEmoji(InteractionContext ctx,
                [Option("name", "Name of emoji create")] string name,
                [Option("image", "image of emoji create")] DiscordAttachment img)
            {
                try
                {
                    Stream emoji = await new HttpClient().GetStreamAsync(img.Url);
                    await ctx.Guild.CreateEmojiAsync(name, emoji);
                    await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                        new DiscordInteractionResponseBuilder().WithContent($"Emoji :{name}: criado com sucesso."));
                }
                catch
                {
                    await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                        new DiscordInteractionResponseBuilder().WithContent("Ocorreu um erro ao tentar criar esse emoji.").AsEphemeral(true));
                }

            }
        }
    }
}