using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.VoiceNext;
using Microsoft.Extensions.Logging;


namespace miguelito_bot_music
{
    internal class events_music
    {
        public static async Task Client_ClientErrored(DiscordClient sender, ClientErrorEventArgs e)
        {
            Console.WriteLine(e.Exception.Message);
        }
        public static async Task Client_VoiceStateUpdate(DiscordClient sender, VoiceStateUpdateEventArgs e)
        {
            DiscordUser bot = Program.cliente.CurrentUser;
            try
            {
                if(e.User == bot &&
                    e.Before.Channel != e.After.Channel)
                {
                    VoiceNextExtension vnext = Program.cliente.GetVoiceNext();
                    VoiceNextConnection connection = vnext.GetConnection(e.Guild);
                    connection.Disconnect();
                    music_commands.track[e.Guild.Id].Clear();
                    music_commands.time[e.Guild.Id] = TimeSpan.Zero;
                    music_commands.loop[e.Guild.Id] = false;
                    File.Delete(music_commands.directory[e.Guild.Id]);
                }

                else if (e.Before.Channel.Users.Contains(bot) && !e.After.Channel.Users.Contains(bot))
                {
                    try
                    {
                        if (e.After.Channel.Users?.Contains(Program.cliente.CurrentUser) ?? true) { }
                    }
                    catch
                    {
                        Console.WriteLine("merda");
                        VoiceNextExtension vnext = Program.cliente.GetVoiceNext();
                        VoiceNextConnection connection = vnext.GetConnection(e.Guild);
                        connection.Disconnect();
                        music_commands.track[e.Guild.Id].Clear();
                        music_commands.time[e.Guild.Id] = TimeSpan.Zero;
                        music_commands.loop[e.Guild.Id] = false;
                        File.Delete(music_commands.directory[e.Guild.Id]);
                    }
                }
            }
            catch { }
        }

        public static async Task VerificarBot()
        {
            while (true)
            {
                foreach (var guild in Program.cliente.Guilds)
                {
                    foreach (var channel in guild.Value.Channels)
                    {
                        if (channel.Value.Users.Count == 1)
                        {
                            if (channel.Value.Users.Contains(Program.cliente.CurrentUser))
                            {
                                VoiceNextExtension vnext = Program.cliente.GetVoiceNext();
                                VoiceNextConnection connection = vnext.GetConnection(guild.Value);
                                VoiceTransmitSink transmit = connection.GetTransmitSink();
                                transmit.Dispose();
                                connection.Disconnect();
                                music_commands.track[guild.Value.Id].Clear();
                                music_commands.time[guild.Value.Id] = TimeSpan.Zero;
                                music_commands.loop[guild.Value.Id] = false;
                                File.Delete(music_commands.directory[guild.Value.Id]);
                            }
                        }
                    }
                }
                await Task.Delay(300000);
            }
        }
    }
}
