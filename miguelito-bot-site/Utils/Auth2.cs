using DSharpPlus;
using DSharpPlus.Entities;
using Newtonsoft.Json;

namespace miguelito_bot_site.Utils
{
    public class Auth2
    {

        public async static Task<string> Token(string code)
        {
            string client_id = Program.Tokens[0];
            string client_sceret = Program.Tokens[1];
            string redirect_url = "https://localhost:7243/Home";
            HttpClient client = new();
            Dictionary<string, string> values = new()
            {
                { "client_id",  client_id},
                { "client_secret", client_sceret },
                { "grant_type", "authorization_code"},
                { "code", code},
                { "redirect_uri", redirect_url}
            };
            FormUrlEncodedContent content = new(values);
            HttpResponseMessage response = await client.PostAsync("https://discordapp.com/api/oauth2/token", content);
            string responseString = await response.Content.ReadAsStringAsync();
            dynamic? data = JsonConvert.DeserializeObject(responseString);
            return data.access_token;
        }

        public static async Task<DiscordUser> User(string token)
        {
            DiscordConfiguration cfg = new()
            {
                Token = token,
                TokenType = TokenType.Bearer,
            };
            DiscordRestClient Client = new(cfg);
            await Client.InitializeAsync();
            DiscordUser CurrentUser = Client.CurrentUser;
            Client.Dispose();
            return CurrentUser;
        }

        public static async Task<IEnumerable<DiscordGuild>> Guilds(string token)
        {
            DiscordConfiguration cfg = new()
            {
                Token = token,
                TokenType = TokenType.Bearer,
            };
            DiscordRestClient Client = new(cfg);
            await Client.InitializeAsync();

            var GuildsUser = await Client.GetCurrentUserGuildsAsync();
            var Guilds = GuildsUser.Where(p => p.Members[Client.CurrentUser.Id].Permissions == Permissions.ManageGuild && p.Members.ContainsKey(949488330620432386));
            Client.Dispose();
            return Guilds;
        }
    }
}