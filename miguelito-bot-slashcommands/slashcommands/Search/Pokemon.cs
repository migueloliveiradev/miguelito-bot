using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Newtonsoft.Json;

namespace miguelito_bot_slashcommands.slashcommands.Search
{
    internal class Pokemon : ApplicationCommandModule
    {

        [SlashCommand("pokemon", "Search ┇ Name or number of the pokemon you want to search")]
        public async Task PokemonInfo(InteractionContext ctx,
               [Option("name", "Name or number of the pokemon you want to search")] string pokemon)
        {
            string url = $"https://pokeapi.co/api/v2/pokemon/{pokemon}";
            string json = await new HttpClient().GetStringAsync(url);
            dynamic? data = JsonConvert.DeserializeObject(json);
            string name = data.name;
            string id = data.id;
            string height = data.height;
            string weight = data.weight;
            string base_experience = data.base_experience;
            string order = data.order;
            string category = data.types[0].type.name;
            string types;
            switch (category.ToLower())
            {
                case "normal":
                    types = "normal";
                    break;
                case "fire":
                    types = "fogo";
                    break;
                case "water":
                    types = "água";
                    break;
                case "grass":
                    types = "grama";
                    break;
                case "electric":
                    types = "elétrico";
                    break;
                case "ice":
                    types = "gelo";
                    break;
                case "fighting":
                    types = "lutador";
                    break;
                case "Poison":
                    types = "veneno";
                    break;
                case "ground":
                    types = "terra";
                    break;
                case "flying":
                    types = "voador";
                    break;
                case "psychic":
                    types = "psíquico";
                    break;
                case "bug":
                    types = "inseto";
                    break;
                case "rock":
                    types = "pedra";
                    break;
                case "ghost":
                    types = "fantasma";
                    break;
                case "dark":
                    types = "dark";
                    break;
                case "dragon":
                    types = "dragão";
                    break;
                case "steel":
                    types = "aço";
                    break;
                case "fairy":
                    types = "fada";
                    break;
                default:
                    types = "Não sei";
                    break;
            }
            string image = data.sprites.front_default;
            DiscordEmbedBuilder embed = new()
            {
                Title = name,
                Color = DiscordColor.CornflowerBlue
            };
            embed.AddField("**ID:**:", id, true);
            embed.AddField("**Altura:**", (Convert.ToInt32(height) / 3.281) + "m", true);
            embed.AddField("**Peso:**", (Convert.ToInt32(weight) / 2.2046) + " kg", true);
            embed.AddField("**XP base:**", base_experience, true);
            embed.AddField("**Tipo:**", types, true);
            embed.AddField("**Ordem:**", order, true).WithThumbnail(image);
            await ctx.CreateResponseAsync(embed);
        }
    }
}
