using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Lavalink;

namespace miguelito_bot_music_lavalink.Commands
{
    internal class Commands_Music : BaseCommandModule
    {
        public static Dictionary<ulong, Queue<LavalinkTrack>> track = new();
        [Command("play"), Aliases("p")]
        public async Task Play(CommandContext ctx, [RemainingText] string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                LavalinkExtension lava = ctx.Client.GetLavalink();
                LavalinkNodeConnection node = lava.ConnectedNodes.Values.First();
                LavalinkGuildConnection conn = node.GetGuildConnection(ctx.Guild);
                if (!lava.ConnectedNodes.Any())
                {
                    await ctx.RespondAsync("A conexão esta dormindo");
                    return;
                }
                if (conn != null)
                {
                    if (conn.Channel != ctx.Member.VoiceState.Channel)
                    {
                        await ctx.RespondAsync("Não estou conectado no mesmo canal q vc");
                        return;
                    }
                }
                else
                {
                    await node.ConnectAsync(ctx.Member.VoiceState.Channel);
                }
                Console.WriteLine(search);
                LavalinkLoadResult loadResult = await node.Rest.GetTracksAsync(search);
                if (loadResult.LoadResultType == LavalinkLoadResultType.LoadFailed
                    || loadResult.LoadResultType == LavalinkLoadResultType.NoMatches)
                {
                    await ctx.RespondAsync($"Track search failed for {search}.");
                    return;
                }
                var con = node.GetGuildConnection(ctx.Member.VoiceState.Guild);
                if (!track.ContainsKey(ctx.Guild.Id))
                {
                    track.Add(ctx.Guild.Id, null);
                }
                track[ctx.Guild.Id].Enqueue(loadResult.Tracks.First());
                await con.PlayAsync(track[ctx.Guild.Id].Dequeue());
                await ctx.RespondAsync($"Now playing !");
            }
            else
            {
                await ctx.RespondAsync("tu quer me fazer de trouxa, bota a porra da musica q tu quer");
            }
        }
        [Command("skip"), Aliases("s")]
        public async Task skip(CommandContext ctx, [RemainingText] string search)
        {
            LavalinkExtension lava = ctx.Client.GetLavalink();
            LavalinkNodeConnection node = lava.ConnectedNodes.Values.First();
            var con = node.GetGuildConnection(ctx.Member.VoiceState.Guild);
            await con.PlayAsync(track[ctx.Guild.Id].Dequeue());
        }
    }
}
