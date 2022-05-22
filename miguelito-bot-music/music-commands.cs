using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.VoiceNext;
using System.Diagnostics;
using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Converter;

namespace miguelito_bot_music
{
    internal class music_commands : BaseCommandModule
    {
        YoutubeClient youtube = new YoutubeClient();
        TimeSpan max = TimeSpan.FromMinutes(60);
        public static Dictionary<ulong, Queue<string>> track = new Dictionary<ulong, Queue<string>>();
        public static Dictionary<ulong, bool> loop = new Dictionary<ulong, bool>();
        public static Dictionary<ulong, TimeSpan> time = new Dictionary<ulong, TimeSpan>();
        public static Dictionary<ulong, string> directory = new Dictionary<ulong, string>();

        [Command("play"), Aliases("p")]
        public async Task Play(CommandContext ctx, [RemainingText] string musica = "")
        {
            await ctx.TriggerTypingAsync();
            if (!time.ContainsKey(ctx.Guild.Id))
            {
                time.Add(ctx.Guild.Id, TimeSpan.FromMinutes(0));
            }
            if (!directory.ContainsKey(ctx.Guild.Id))
            {
                directory.Add(ctx.Guild.Id, "");
            }
            if (musica != "")
            {
                #region vars
                DiscordMember bot = await ctx.Guild.GetMemberAsync(Program.cliente.CurrentUser.Id);
                VoiceNextConnection connection;
                VoiceTransmitSink transmit;
                VoiceNextExtension vnext;
                #endregion

                #region AdicionarAFila
                if (!track.ContainsKey(ctx.Guild.Id))
                {
                    track.Add(ctx.Guild.Id, new Queue<string>());
                }
                if (!musica.Contains("playlist?list"))
                {
                    await Adicionar(ctx, musica);
                }
                else
                {
                    await PesquisarPaylist(musica, ctx);
                }
                #endregion

                vnext = ctx.Client.GetVoiceNext();
                connection = vnext.GetConnection(ctx.Guild);
                if (!loop.ContainsKey(ctx.Guild.Id))
                {
                    loop[ctx.Guild.Id] = false;
                }
                if (bot.VoiceState?.Channel != ctx.Member?.VoiceState?.Channel)
                {
                    DiscordChannel channel = null;
                    channel ??= ctx.Member.VoiceState?.Channel;
                    await channel.ConnectAsync();
                    connection = vnext.GetConnection(ctx.Guild);
                    transmit = connection.GetTransmitSink();
                }
                if (!connection.IsPlaying)
                {
                    transmit = connection.GetTransmitSink();
                    for (int i = 0; i < track[ctx.Guild.Id].Count; i = 0)
                    {
                        var videoid = track[ctx.Guild.Id].Peek();
                        var duration = await youtube.Videos.GetAsync(videoid);
                        time[ctx.Guild.Id] -= duration.Duration.Value;
                        directory[ctx.Guild.Id] = $"{GetDirectory()}\\music\\{ctx.Guild.Id}-{ctx.User.Id}-{ctx.Message.Id}-{videoid}.mp3";
                        if (!loop[ctx.Guild.Id])
                        {
                            await youtube.Videos.DownloadAsync(videoid, directory[ctx.Guild.Id].ToString(), o => o
                                .SetContainer(YoutubeExplode.Videos.Streams.Container.Mp3)
                                .SetPreset(ConversionPreset.UltraFast)
                                .SetFFmpegPath(Path.Combine(GetDirectory() + "//ffmpeg//ffmpeg.exe")));
                        }
                        var pcm = ConvertAudioToPcm(directory[ctx.Guild.Id]);
                        await pcm.CopyToAsync(transmit);
                        if (!loop[ctx.Guild.Id])
                        {
                            videoid = track[ctx.Guild.Id].Dequeue();
                            File.Delete(directory[ctx.Guild.Id]);
                            directory[ctx.Guild.Id] = "";
                            await pcm.DisposeAsync();
                        }
                    }
                    connection.Disconnect();
                }
            }
            else
            {
                await ctx.RespondAsync("infelizmente eu não tenho poder o suficiente para advinhar qual musica você quer escutar :pensive:");
            }
        }

        [Command("stop"), Aliases("parar")]
        public async Task stop(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            VoiceNextExtension vnext = ctx.Client.GetVoiceNext();
            VoiceNextConnection connection = vnext.GetConnection(ctx.Guild);
            if (connection.IsPlaying)
            {
                connection.Disconnect();
                track[ctx.Guild.Id].Clear();
                time[ctx.Guild.Id] = TimeSpan.Zero;
                await ctx.RespondAsync("Musica parada e lista limpa com sucesso :stop_button:");
            }
            else
            {
                await ctx.RespondAsync("nenhuma musica esta tocando para usar esse comando");
            }
        }

        [Command("pause")]
        public async Task pause(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            VoiceNextExtension vnext = ctx.Client.GetVoiceNext();
            VoiceNextConnection connection = vnext.GetConnection(ctx.Guild);
            connection.Pause();
            await ctx.RespondAsync("Musica pausada :pause_button:");
        }

        [Command("resume")]
        public async Task resume(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            VoiceNextExtension vnext = ctx.Client.GetVoiceNext();
            VoiceNextConnection connection = vnext.GetConnection(ctx.Guild);
            await connection.ResumeAsync();
            await ctx.RespondAsync("Musica retornada :arrow_forward:");
        }

        [Command("loop")]
        public async Task loop_(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordMember bot = await ctx.Guild.GetMemberAsync(Program.cliente.CurrentUser.Id);
            if (bot.VoiceState?.Channel == ctx.Member?.VoiceState?.Channel)
            {
                if (loop[ctx.Guild.Id] == false)
                {
                    loop[ctx.Guild.Id] = true;
                    await ctx.RespondAsync("modo loop ativado, vou ficar doido de tanto tocar a mesma musica");
                }
                else
                {
                    loop[ctx.Guild.Id] = false;
                    await ctx.RespondAsync("modo loop desativado, ufa, não aguentava mais tocar a mesma musica tantas vezes");
                }
            }
            else
            {
                await ctx.RespondAsync("Uai meu rei, tu quer que eu coloque qual musica tocando em loop? :thinking:");
            }
        }

        [Command("shuffle"), Aliases("embaralhar")]
        public async Task shuffle(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordMember bot = await ctx.Guild.GetMemberAsync(Program.cliente.CurrentUser.Id);
            if (bot.VoiceState?.Channel == ctx.Member?.VoiceState?.Channel)
            {
                await ctx.RespondAsync("Fila de musicas embraralhada com sucesso :face_with_spiral_eyes:");
                Random rnd = new();
                var music = new List<string>();
                for (int i = 0; i <= track[ctx.Guild.Id].Count; i++)
                {
                    music[i]= track[ctx.Guild.Id].Dequeue(); 
                }
                for (int o = 0; o <= music.Count; o++)
                {
                    int a = rnd.Next(music.Count);
                    string temp = music[o];
                    music[o] = music[a];
                    music[a] = temp;
                }
                for (int i = 0; i <= music.Count; i++)
                {
                    track[ctx.Guild.Id].Enqueue(music[i]);
                }
            }
            else
            {
                await ctx.RespondAsync("Uai meu rei, tu quer embaralhar qual musica? :thinking:");
            }
        }

        [Command("volume"), Aliases("vol")]
        public async Task volume(CommandContext ctx, int volume)
        {
            await ctx.TriggerTypingAsync();
            VoiceNextExtension vnext = ctx.Client.GetVoiceNext();
            VoiceNextConnection connection = vnext.GetConnection(ctx.Guild);
            VoiceTransmitSink transmit = connection.GetTransmitSink();
            if (connection.IsPlaying)
            {
                if (volume > 100)
                {
                    await ctx.RespondAsync("O volume não pode ser maior que 100 :thinking:");
                }
                else
                {
                    transmit.VolumeModifier = volume;
                    await ctx.RespondAsync($"Volume alterado para {volume}% :loud_sound:");
                }
            }
            else
            {
                await ctx.RespondAsync("Nenhuma musica esta tocando para usar esse comando");
            }
        }

        [Command("queue"), Aliases("lista")]
        public async Task queue(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordMember bot = await ctx.Guild.GetMemberAsync(Program.cliente.CurrentUser.Id);
            if (bot.VoiceState?.Channel == ctx.Member?.VoiceState?.Channel)
            {
                if (track[ctx.Guild.Id].Count == 0)
                {
                    await ctx.RespondAsync("Nenhuma musica na fila :face_with_raised_eyebrow:");
                }
                else
                {
                    string music = "";
                    for (int i = 0; i <= 10; i++)
                    {
                        music += $"{i + 1} - {track[ctx.Guild.Id].ElementAt(i)}\n";
                    }
                    await ctx.RespondAsync(music);
                }
            }
            else
            {
                await ctx.RespondAsync("Uai meu rei, tu quer ver a fila de musicas? :thinking:");
            }
        }

        [Command("clear"), Aliases("limpar")]
        public async Task clear(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordMember bot = await ctx.Guild.GetMemberAsync(Program.cliente.CurrentUser.Id);
            if (bot.VoiceState?.Channel == ctx.Member?.VoiceState?.Channel)
            {
                if (track[ctx.Guild.Id].Count == 0)
                {
                    await ctx.RespondAsync("Nenhuma musica na fila :face_with_raised_eyebrow:");
                }
                else
                {
                    track[ctx.Guild.Id].Clear();
                    await ctx.RespondAsync("Fila de musicas limpa com sucesso :stop_button:");
                }
            }
            else
            {
                await ctx.RespondAsync("Uai meu rei, tu quer limpar a fila de musicas? :thinking:");
            }
        }
        private Stream ConvertAudioToPcm(string filePath)
        {
            var ffmpeg = Process.Start(new ProcessStartInfo
            {
                FileName = GetDirectory() + "//ffmpeg//ffmpeg.exe",
                Arguments = $@"-i ""{filePath}"" -ac 2 -f s16le -ar 48000 pipe:1",
                RedirectStandardOutput = true,
                UseShellExecute = false
            });
            return ffmpeg.StandardOutput.BaseStream;
        }
        static string GetDirectory()
        {
            return Directory.GetCurrentDirectory();
        }
        public async Task Adicionar(CommandContext ctx, string musica)
        {
            string resultado;
            if (musica.Contains("list"))
            {
                resultado = musica.Substring(0, musica.IndexOf('&'));
                await foreach (var result in youtube.Search.GetVideosAsync(resultado))
                {
                    if (result.Duration < max)
                    {
                        track[ctx.Guild.Id].Enqueue(result.Id);
                        time[ctx.Guild.Id] += result.Duration.Value;
                        DiscordEmbedBuilder embed = new DiscordEmbedBuilder
                        {
                            Title = "Musica adicionada a Lista",
                            Color = DiscordColor.DarkGray,
                            Description = Formatter.MaskedUrl(result.Title, new Uri(result.Url), result.Title) + "\n\n" +
                            $"Tempo estimado: {result.Duration}\n" +
                            $"Tempo total estimado {time[ctx.Guild.Id]}" +
                            $"Quantidade de musicas na lista {track[ctx.Guild.Id].Count}"
                        }
                        .WithThumbnail(result.Thumbnails.ToString())
                        .WithAuthor(result.Title, result.Url, null);
                        await ctx.RespondAsync(embed);
                    }
                    else
                    {
                        await ctx.RespondAsync("infelizmente ainda sou um pobre bot que suporta no maximo musicas de **uma hora**, poderia colocar outra? :pensive::fist:");
                    }
                    break;
                }
            }
            else
            {
                await foreach (var result in youtube.Search.GetVideosAsync(musica))
                {
                    if (result.Duration < max)
                    {
                        track[ctx.Guild.Id].Enqueue(result.Id);
                        time[ctx.Guild.Id] += result.Duration.Value;
                        DiscordEmbedBuilder embed = new DiscordEmbedBuilder
                        {
                            Title = "Musica adicionada a Lista",
                            Color = DiscordColor.DarkGray,
                            Description = Formatter.MaskedUrl(result.Title, new Uri(result.Url), result.Title) + "\n\n" +
                            $"Tempo estimado: {result.Duration}\n" +
                            $"Tempo total estimado {time[ctx.Guild.Id]}\n" +
                            $"Quantidade de musicas na lista {track[ctx.Guild.Id].Count}"
                        }
                        .WithThumbnail($"https://img.youtube.com/vi/{result.Id}/default.jpg")
                        .WithAuthor(result.Title, result.Url, null);
                        await ctx.RespondAsync(embed);
                    }
                    else
                    {
                        await ctx.RespondAsync("infelizmente ainda sou um pobre bot que suporta no maximo musicas de **uma hora**, poderia colocar outra? :pensive::fist:");
                    }
                    break;
                }
            }
        }

        public async Task PesquisarPaylist(string musica, CommandContext ctx)
        {
            TimeSpan tempototal = TimeSpan.Zero;
            int quantidade = 0;
            var playlist = await youtube.Playlists.GetAsync(musica);
            var videosSubset = await youtube.Playlists
            .GetVideosAsync(playlist.Id)
            .CollectAsync(100);
            foreach (var video in videosSubset)
            {
                if (video.Duration.Value < max)
                {
                    tempototal += video.Duration.Value;
                    time[ctx.Guild.Id] += video.Duration.Value;
                    track[ctx.Guild.Id].Enqueue(video.Id);
                }
                else
                {
                    quantidade++;
                }
            }
            if (quantidade != videosSubset.Count)
            {
                var embed = new DiscordEmbedBuilder
                {
                    Title = "Musica adicionada a Lista",
                    Color = DiscordColor.DarkGray,
                    Description = Formatter.MaskedUrl(playlist.Title, new Uri(playlist.Url), playlist.Title) + "\n\n" +
                            $"Tempo estimado: {tempototal}\n" +
                            $"Tempo total estimado {time[ctx.Guild.Id]}\n" +
                            $"Quantidade de musicas na lista {track[ctx.Guild.Id].Count}"
                }
                .WithThumbnail($"https://img.youtube.com/vi/{videosSubset[0].Id}/default.jpg")
                .WithAuthor(playlist.Title, playlist.Url, $"https://img.youtube.com/vi/{videosSubset[0].Id}/default.jpg"); ;
                await ctx.RespondAsync(embed);
            }
            if (quantidade != 0)
            {
                await ctx.Client.SendMessageAsync(ctx.Channel, $"infelizmente ainda sou um pobre bot que suporta e suporto no maximo musicas de **uma hora**, " +
                    $"por isso {quantidade} videos não conseguiram entrar na lista :pensive::fist:");
            }
        }
    }
}
