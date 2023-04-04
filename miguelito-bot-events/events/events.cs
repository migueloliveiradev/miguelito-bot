using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using miguelito_bot_commands;
using Tweetinvi;

namespace miguelito_bot_events.events
{
    internal class Events
    {
        public static async Task ReadyEvent(DiscordClient sender, ReadyEventArgs e)
        {
            TwitterClient client = new(Program.config[9], Program.config[10], Program.config[11], Program.config[12]);
            await Twitter.Tweet(client);
        }

        public static async Task GuildMemberAddEvent(DiscordClient sender, GuildMemberAddEventArgs e)
        {
            
        }

        public static async Task GuildMemberRemoveEvent(DiscordClient sender, GuildMemberRemoveEventArgs e)
        {
           
        }

        public static async Task GuildCreateEvent(DiscordClient sender, GuildCreateEventArgs e)
        {
        }

        public static async Task GuildDeleteEvent(DiscordClient sender, GuildDeleteEventArgs e)
        {
           
        }

        public static async Task ChannelCreateEvent(DiscordClient sender, ChannelCreateEventArgs e)
        {
            Random random = new();
            string[] texts =
            {
                ":face_with_spiral_eyes:",
                ":nerd:",
                ":hushed:",
                ":face_with_monocle:",
                ":cowboy:",
                ":ok_hand:",
                ":eyes:",
                ":wave:",
                ":smirk:",
                ":yawning_face:"
            };
            int i = random.Next(0, texts.Length);
            await sender.SendMessageAsync(e.Channel, texts[i]);
        }

        public static async Task MessageDeleteEvent(DiscordClient sender, MessageDeleteEventArgs e)
        {
           
        }

        public static async Task MessageUpdateEvent(DiscordClient sender, MessageUpdateEventArgs e)
        {
            
        }
    }
}