using Cutt_ly;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Google.Apis.Services;
using Google.Apis.Translate.v2;
using Google.Cloud.Translation.V2;
using HG_Finance;
using MediaWikiApi.Wiki;
using MediaWikiApi.Wiki.Response.OpenSearch;
using MediaWikiApi.Wiki.Response.Query.Extracts;
using Newtonsoft.Json;
using System.Globalization;
using System.Net;
using VirusTotalNet;
using VirusTotalNet.Objects;
using VirusTotalNet.ResponseCodes;
using VirusTotalNet.Results;

namespace miguelito_bot_commands.commands
{
    internal class commands_api : BaseCommandModule
    {
        #region clients
        VirusTotal virusTotal = new(Program.config[3]);
        WebClient client;
        Cutt cutt = new Cutt();
        HG_Client clientHG = new()
        {
            Key = Program.config[6],
        };
        #endregion

        [Command("dolar"), Aliases("dólar")]
        public async Task Dolar(CommandContext ctx)
        {
            clientHG.Currencies("USD");
            if (clientHG.CurrencyResponse.Buy != 0)
            {
                if (clientHG.CurrencyResponse.Variation < 0)
                {
                    await ctx.RespondAsync($"Eita, o dólar sumiu, esta exatamente R${decimal.Round(clientHG.CurrencyResponse.Buy, 2)}");
                }
                else
                {
                    await ctx.RespondAsync($"Eita, o dólar caiu, esta exatamente R${decimal.Round(clientHG.CurrencyResponse.Buy, 2)}");
                }
            }
            await Program.log("dolar");
        }

        [Command("euro")]
        public async Task Euro(CommandContext ctx)
        {
            clientHG.Currencies("EUR");
            if (clientHG.CurrencyResponse.Buy != 0)
            {
                if (clientHG.CurrencyResponse.Variation < 0)
                {
                    await ctx.RespondAsync($"Eita, o dólar sumiu, esta exatamente R${decimal.Round(clientHG.CurrencyResponse.Buy, 2)}");
                }
                else
                {
                    await ctx.RespondAsync($"Eita, o dólar caiu, esta exatamente R${decimal.Round(clientHG.CurrencyResponse.Buy, 2)}");
                }
            }
            await Program.log("euro");
        }

        [Command("cotação"), Aliases("cotacao")]
        public async Task cotacao(CommandContext ctx, string moeda)
        {
            clientHG.Currencies(moeda);
            if (clientHG.CurrencyResponse.Buy != 0)
            {
                if (clientHG.CurrencyResponse.Variation < 0)
                {
                    await ctx.RespondAsync($"Eita, o dólar sumiu, esta exatamente R${decimal.Round(clientHG.CurrencyResponse.Buy, 2)}");
                }
                else
                {
                    await ctx.RespondAsync($"Eita, o dólar caiu, esta exatamente R${decimal.Round(clientHG.CurrencyResponse.Buy, 2)}");
                }
            }
            await Program.log("cotacao");
        }

        [Command("real")]
        public async Task Real(CommandContext ctx)
        {
            await ctx.RespondAsync("Atualmente o real esta estavel, valendo exatamente 1 real");
            await Program.log("real");
        }

        [Command("encurtar"), Aliases("encurtardor", "encurta")]
        public async Task Encurtar(CommandContext ctx, string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                try
                {
                    cutt.key = Program.config[5];
                    cutt.ShortLinkGenerator(url);
                    await ctx.RespondAsync(cutt.Value.ShortLink);
                }
                catch
                {
                    await ctx.RespondAsync("não foi possivel encurtar o link pedido :pensive");
                }
            }
            else
            {
                await ctx.RespondAsync("Poderia por gentileza colocar o link desejado, ainda não tenho poder de adivinhar '-'");
            }
            await Program.log("encurtar");
        }

        [Command("virus")]
        public async Task Virus(CommandContext ctx, string url = "")
        {
            try
            {
                if (url.Contains("https://cdn.discordapp.com/attachments/") || ctx.Message.Attachments.Count > 0)
                {
                    byte[] file = null;
                    if (ctx.Message.Attachments.Count > 0)
                    {
                        file = client.DownloadData(ctx.Message.Attachments.First().Url);
                    }
                    else if (url.Contains("https://cdn.discordapp.com/attachments/"))
                    {
                        file = client.DownloadData(url);
                    }
                    int detectado = 0;
                    int notdetectado = 0;
                    string BitDefender = "passou :white_check_mark:";
                    string Malwarebytes = "passou :white_check_mark:";
                    string Avast = "passou :white_check_mark:";
                    string McAfee = "passou :white_check_mark:";
                    string avira = "passou :white_check_mark:";
                    string Microsoft = "passou :white_check_mark:";
                    string Baidu = "passou :white_check_mark:";
                    FileReport fileReport = await virusTotal.GetFileReportAsync(file);
                    if (fileReport.ResponseCode == FileReportResponseCode.Present)
                    {
                        foreach (KeyValuePair<string, ScanEngine> scan in fileReport.Scans)
                        {

                            if (scan.Value.Detected)
                            {
                                detectado++;
                            }
                            else
                            {
                                notdetectado++;
                            }
                            if (scan.Key == "BitDefender" && scan.Value.Detected)
                            {
                                BitDefender = "não passou :warning:";
                            }
                            else if (scan.Key == "Malwarebytes" && scan.Value.Detected)
                            {
                                Malwarebytes = "não passou :warning:";
                            }
                            else if (scan.Key == "Avast" && scan.Value.Detected)
                            {
                                Avast = "não passou :warning:";
                            }
                            else if (scan.Key == "McAfee-GW-Edition" && scan.Value.Detected)
                            {
                                McAfee = "não passou :warning:";
                            }
                            else if (scan.Key == "Microsoft" && scan.Value.Detected)
                            {
                                Microsoft = "não passou :warning:";
                            }
                            else if (scan.Key == "Baidu" && scan.Value.Detected)
                            {
                                Baidu = "não passou :warning:";
                            }
                            else if (scan.Key == "Avira" && scan.Value.Detected)
                            {
                                Baidu = "não passou :warning:";
                            }
                        }
                        DiscordEmbedBuilder embed = new DiscordEmbedBuilder
                        {
                            Title = ":warning: **Verificação de virus concluida**",
                            Color = DiscordColor.CornflowerBlue,
                            Description =
                            $"**`{detectado}` sinalizaram este arquivo como malicioso**\n" +
                             $"**`{notdetectado}` não sinalizaram este arquivo como malicioso**\n\n" +
                            $"**PRINCIPAIS ANTIVIRUS**\n" +
                             $"BitDefender: {BitDefender}\n" +
                            $"Malwarebytes: {Malwarebytes}\n" +
                            $"Avast: {Avast}\n" +
                            $"McAfee: {McAfee}\n" +
                            $"Microsoft: {Microsoft}\n" +
                            $"Avira: {avira}\n" +
                            $"Baidu: {Baidu}\n"
                        };
                        await ctx.RespondAsync(embed);
                    }

                }
                else if (url.Contains("http://") || url.Contains("https://"))
                {
                    int detectado = 0;
                    int notdetectado = 0;
                    string BitDefender = "passou :white_check_mark:";
                    string google = "passou :white_check_mark:";
                    string Avira = "passou :white_check_mark:";
                    string Baidu = "passou :white_check_mark:";
                    UrlReport urlReport = await virusTotal.GetUrlReportAsync(url);
                    if (urlReport.ResponseCode == UrlReportResponseCode.Present)
                    {
                        Console.WriteLine("uai5");
                        foreach (KeyValuePair<string, UrlScanEngine> scan in urlReport.Scans)
                        {
                            if (scan.Value.Detected)
                            {
                                detectado++;
                            }
                            else
                            {
                                notdetectado++;
                            }
                            if (scan.Key == "BitDefender" && scan.Value.Detected)
                            {
                                BitDefender = "não passou :warning:";
                            }
                            else if (scan.Key == "Google Safebrowsing" && scan.Value.Detected)
                            {
                                google = "não passou :warning:";
                            }
                            else if (scan.Key == "Avira" && scan.Value.Detected)
                            {
                                Avira = "não passou :warning:";
                            }
                            else if (scan.Key == "Baidu-International" && scan.Value.Detected)
                            {
                                Baidu = "não passou :warning:";
                            }
                        }
                        DiscordEmbedBuilder embed = new DiscordEmbedBuilder
                        {
                            Title = ":warning: **Verificação de virus concluida**",
                            Color = DiscordColor.CornflowerBlue,
                            Description =
                           $"**`{detectado}` sinalizaram este arquivo como malicioso**\n" +
                           $"**`{notdetectado}` não sinalizaram este arquivo como malicioso**\n\n" +
                           $"**PRINCIPAIS ANTIVIRUS**\n" +
                           $"BitDefender: {BitDefender}\n" +
                           $"Google: {google}\n" +
                           $"Avira: {Avira}\n" +
                           $"Baidu: {Baidu}\n"
                        };
                        await ctx.RespondAsync(embed);
                    }
                }
                else
                {
                    await ctx.RespondAsync("Por gentileza colocar o link desejado, link do arquivo ou anexe o arquivo na mensagem, " +
                        "ainda não tenho poder de adivinhar o que você quer verificar");
                }
            }
            catch
            {
                await ctx.RespondAsync("Ocorreu um erro ao verificar o arquivo, por gentileza tente novamente");
            }
            await Program.log("virus");
        }

        [Command("wiki"), Aliases("Wikipédia", "Wikipedia")]
        public async Task Wiki(CommandContext ctx, [RemainingText] string search)
        {
            try
            {
                string resultado = "";
                string[] palavras = search.Split(' ');
                foreach (string palavra in palavras)
                {
                    string textofinal = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(palavra.ToLower());
                    resultado += textofinal + "_";
                }
                WikiApi api = new WikiApi("https://pt.wikipedia.org/");
                IReadOnlyList<Section> sections = api.GetSections(resultado);
                IOpenSearch result = api.Search(resultado);
                DiscordEmbedBuilder embed = new DiscordEmbedBuilder
                {
                    Color = DiscordColor.CornflowerBlue,
                    Description = sections[0].Content
                }.WithAuthor(result.Titles[0], result.Urls[0], "https://media.discordapp.net/attachments/949836472985460766/966878248342544404/wikipedia.png"); ;
                await ctx.RespondAsync(embed);
            }
            catch
            {
                await ctx.RespondAsync(":pensive: resultado não encontrado, por gentileza verifique sua ortografia");
            }
            await Program.log("wiki");
        }

        [Command("tradutor"), Aliases("traduzir", "traduz")]
        public async Task Tradutor(CommandContext ctx, string idioma = "", [RemainingText] string texto = "")
        {
            if (idioma != "" && texto != "")
            {
                try
                {
                    TranslateService service = new TranslateService(new BaseClientService.Initializer { ApiKey = Program.config[4] });
                    TranslationClientImpl client = new TranslationClientImpl(service, TranslationModel.ServiceDefault);
                    TranslationResult result = client.TranslateText(texto, idioma);
                    await ctx.RespondAsync(result.TranslatedText);
                }
                catch
                {
                    await ctx.RespondAsync("Ocorreu algum ao tentar traduzir");
                }
            }
            else
            {
                await ctx.RespondAsync("Por gentileza insira as informações necessarias para fazer a tradução\n\n" +
                    "**-traduzir** [prefixo do idioma final] [texto em qualquer idioma que eu me viro pra descobrir qual é]");
            }
            await Program.log("tradutor");
        }

        [Command("weather"), Aliases("clima", "tempo")]
        public async Task weather(CommandContext ctx, [RemainingText] string city = "")
        {
            string url = $"https://api.hgbrasil.com/weather?array_limit=1&fields=only_results,temp,city_name,forecast,max,min,date&key={Program.config[6]}&city_name={city}";
            string json = new WebClient().DownloadString(url);
            dynamic data = JsonConvert.DeserializeObject(json);
            string temp = data.temp;
            string city_name = data.city_name;
            var forecast = data.forecast;

            string temp_max = forecast[0].max;
            string temp_min = forecast[0].min;

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder
            {
                Title = $"{city_name}",
                Color = DiscordColor.CornflowerBlue,
                Description = $"**Temperatura:** {temp}°C\n" +
                              $"**Temperatura máxima:** {temp_max}°C\n" +
                              $"**Temperatura mínima:** {temp_min}°C\n"
            };
            await ctx.RespondAsync(embed);
            await Program.log("clima");

        }

        [Command("search"), Aliases("busca", "pesquisa", "google", "pesquisar", "buscar")]
        public async Task Search(CommandContext ctx, [RemainingText] string search = "")
        {
            try
            {

                if (search != "")
                {
                    string cx = "012206473211536691174:p3wdsjftbeo";
                    string url = $" https://www.googleapis.com/customsearch/v1?key={Program.config[13]}&cx={cx}&q={search}";
                    var request = WebRequest.Create(url);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream data = response.GetResponseStream();
                    StreamReader reader = new StreamReader(data);
                    string rs = reader.ReadToEnd();
                    dynamic json = JsonConvert.DeserializeObject(rs);
                    string title = json.items[0].title;
                    string link = json.items[0].link;
                    string snippet = json.items[0].snippet;
                    string image = json.items[0].pagemap.cse_image[0].src;

                    string description = $"**Título:** {title}\n" +
                                        $"**Link:** {link}\n" +
                                        $"**Descrição:** {snippet}\n";

                    DiscordEmbedBuilder embed = new DiscordEmbedBuilder
                    {
                        Title = "Resultado da pesquisa",
                        Color = DiscordColor.CornflowerBlue,
                        Description = description,
                        ImageUrl = image

                    };
                    await ctx.RespondAsync(embed);
                }
                else
                {
                    await ctx.RespondAsync("Por gentileza insira o que você deseja pesquisar");
                }
            }
            catch
            {
                await ctx.RespondAsync("Ocorreu algum erro ao tentar pesquisar");
            }


        }

        [Command("pokemon"), Aliases("whatPokemon", "poke", "pokemoninfo")]
        public async Task Pokemon(CommandContext ctx, [RemainingText] string pokemon = "")
        {
            await ctx.TriggerTypingAsync();
            try
            {
                if (pokemon != "")
                {
                    string url = $"https://pokeapi.co/api/v2/pokemon/{pokemon}";
                    string json = new WebClient().DownloadString(url);
                    dynamic data = JsonConvert.DeserializeObject(json);
                    string name = data.name;
                    string id = data.id;
                    string height = data.height;
                    string weight = data.weight;
                    string base_experience = data.base_experience;
                    string order = data.order;
                    string category = data.types[0].type.name;
                    TranslateService service = new TranslateService(new BaseClientService.Initializer { ApiKey = Program.config[4] });
                    TranslationClientImpl client = new TranslationClientImpl(service, TranslationModel.ServiceDefault);
                    TranslationResult result = client.TranslateText(category, "pt");
                    category = result.TranslatedText;
                    string image = data.sprites.front_default;
                    DiscordEmbedBuilder embed = new DiscordEmbedBuilder
                    {
                        Title = $"{name}",
                        Color = DiscordColor.CornflowerBlue
                    }.AddField($"**ID:**:", id, true).AddField($"**Altura:**", height + "m", true)
                    .AddField($"**Peso:**", weight + "kg", true).AddField($"**XP base:**", base_experience, true)
                    .AddField($"**Tipo:**", category, true).AddField($"**Ordem:**", order, true).WithThumbnail(image);
                    await ctx.RespondAsync(embed);
                }
            }
            catch
            {
                await ctx.RespondAsync("Ocorreu algum erro ao tentar pesquisar esté Pokemon");
            }
        }

        [Command("anime"), Aliases("manga", "animes", "mangas")]
        public async Task Anime(CommandContext ctx, [RemainingText] string anime = "")
        {
            await ctx.TriggerTypingAsync();
            try
            {
                if (anime != "")
                {
                    string url = $"https://api.jikan.moe/v3/search/anime?q={anime}";
                    string json = new WebClient().DownloadString(url);
                    dynamic data = JsonConvert.DeserializeObject(json);
                    string title = data.results[0].title;
                    string image = data.results[0].image_url;
                    string synopsis = data.results[0].synopsis;
                    TranslateService service = new TranslateService(new BaseClientService.Initializer { ApiKey = Program.config[4] });
                    TranslationClientImpl client = new TranslationClientImpl(service, TranslationModel.ServiceDefault);
                    TranslationResult result = client.TranslateText(synopsis, "pt");
                    synopsis = result.TranslatedText;
                    string type = data.results[0].type;
                    string episodes = data.results[0].episodes;
                    string start_date = data.results[0].start_date;
                    start_date = start_date.Replace("21:00:00", "");
                    string end_date = data.results[0].end_date;
                    end_date = end_date.Replace("21:00:00", "");
                    DiscordEmbedBuilder embed = new DiscordEmbedBuilder
                    {
                        Title = $"{title}",
                        Color = DiscordColor.CornflowerBlue,
                        ImageUrl = image
                    }
                    .AddField($"**Tipo:**", type, true)
                    .AddField($"**Episódios:**", episodes, true)
                    .AddField($"**Data de início:**", start_date, true)
                    .AddField($"**Data de término:**", end_date, true)
                    .WithDescription(synopsis);
                    await ctx.RespondAsync(embed);
                }
                else
                {
                    await ctx.RespondAsync("Por gentileza insira o nome do anime que deseja pesquisar");
                }
            }
            catch (Exception ex)
            {
                await ctx.RespondAsync("Ocorreu um erro ao pesquisar o anime solicitado");
            }

        }

        [Command("minecraftskin"), Aliases("mc", "skin")]
        public async Task MinecraftSkin(CommandContext ctx, [RemainingText] string nick = "")
        {
            await ctx.TriggerTypingAsync();
            try
            {
                if (nick != "")
                {
                    string url = $"https://api.mojang.com/users/profiles/minecraft/{nick}";
                    string json = new WebClient().DownloadString(url);
                    dynamic data = JsonConvert.DeserializeObject(json);
                    string id = data.id;
                    string name = data.name;
                    DiscordMessageBuilder builder = new();
                    DiscordLinkButtonComponent download = new($"https://crafatar.com/renders/body/{id}?size=4&default=MHF_Steve&overlay", "Baixar");
                    DiscordLinkButtonComponent Skin = new($"https://crafatar.com/skins/{id}?default=MHF_Steve&overlay", "Skin");
                    DiscordEmbedBuilder embed = new DiscordEmbedBuilder
                    {
                        Title = $"Skin de {name}",
                        Color = DiscordColor.CornflowerBlue,
                        ImageUrl = $"https://crafatar.com/renders/body/{id}?size=4&default=MHF_Steve&overlay"
                    };
                    builder.AddEmbed(embed);
                    builder.AddComponents(download,Skin);
                    await ctx.RespondAsync(builder);
                }
                else
                {
                    await ctx.RespondAsync("Por gentileza insira o nome do cara que você deseja a skin");
                }
            }
            catch
            {
                await ctx.RespondAsync("Ocorreu algum erro ao tentar pesquisar o skin do Minecraft");
            }
        }

        [Command("minecrafthead"), Aliases("head")]
        public async Task minecraftHead(CommandContext ctx, [RemainingText] string nick = "")
        {
            await ctx.TriggerTypingAsync();
            try
            {
                if (nick != "")
                {
                    string url = $"https://api.mojang.com/users/profiles/minecraft/{nick}";
                    string json = new WebClient().DownloadString(url);
                    dynamic data = JsonConvert.DeserializeObject(json);
                    string id = data.id;
                    string name = data.name;
                    DiscordMessageBuilder builder = new();
                    DiscordLinkButtonComponent download = new($"https://crafatar.com/renders/head/{id}?size=4&default=MHF_Steve&overlay", "Baixar");
                    DiscordLinkButtonComponent Skin = new($"https://crafatar.com/skins/{id}?default=MHF_Steve&overlay", "Skin");
                    DiscordEmbedBuilder embed = new DiscordEmbedBuilder
                    {
                        Title = $"Skin de {name}",
                        Color = DiscordColor.CornflowerBlue,
                        ImageUrl = $"https://crafatar.com/renders/head/{id}?size=4&default=MHF_Steve&overlay"
                    };
                    builder.AddEmbed(embed);
                    builder.AddComponents(download, Skin);
                    await ctx.RespondAsync(builder);
                }
                else
                {
                    await ctx.RespondAsync("Por gentileza insira o nome do cara que você deseja a skin");
                }
            }
            catch 
            {
                await ctx.RespondAsync("Ocorreu algum erro ao tentar pesquisar o skin do Minecraft");
            }
        }

        [Command("minecraftcape"), Aliases("cape")]
        public async Task minecraftCape(CommandContext ctx, [RemainingText] string nick = "")
        {
            await ctx.TriggerTypingAsync();
            try
            {
                if (nick != "")
                {
                    string url = $"https://api.mojang.com/users/profiles/minecraft/{nick}";
                    string json = new WebClient().DownloadString(url);
                    dynamic data = JsonConvert.DeserializeObject(json);
                    string id = data.id;
                    string name = data.name;
                    DiscordMessageBuilder builder = new();
                    DiscordLinkButtonComponent download = new($"https://crafatar.com/renders/cape/{id}?size=4&default=MHF_Steve&overlay", "Baixar");
                    DiscordLinkButtonComponent Skin = new($"https://crafatar.com/skins/{id}?default=MHF_Steve&overlay", "Skin");
                    DiscordEmbedBuilder embed = new DiscordEmbedBuilder
                    {
                        Title = $"Skin de {name}",
                        Color = DiscordColor.CornflowerBlue,
                        ImageUrl = $"https://crafatar.com/renders/cape/{id}?size=4&default=MHF_Steve&overlay"
                    };
                    builder.AddEmbed(embed);
                    builder.AddComponents(download, Skin);
                    await ctx.RespondAsync(builder);
                }
                else
                {
                    await ctx.RespondAsync("Por gentileza insira o nome do cara que você deseja a skin");
                }
            }
            catch
            {
                await ctx.RespondAsync("Ocorreu algum erro ao tentar pesquisar o skin do Minecraft");
            }
        }
    }
}
