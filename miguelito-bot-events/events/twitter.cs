using DSharpPlus.Entities;
using miguelito_bot_commands;
using Tweetinvi;
using Tweetinvi.Streaming;

namespace miguelito_bot_events.events
{
    public class Twitter
    {
        public static async Task Tweet(TwitterClient client)
        {
            DiscordChannel channel = Program.cliente.GetChannelAsync(984271750437175306).Result;
            IFilteredStream stream = client.Streams.CreateFilteredStream();
            stream.AddFollow(1189872424134811656);
            stream.MatchingTweetReceived += (sender, eventReceived) =>
            {
                channel.SendMessageAsync(eventReceived.Tweet.Url);
            };
            await stream.StartMatchingAnyConditionAsync();
        }
    }
}