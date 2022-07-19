using DSharpPlus.Entities;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace miguelito_bot_site.Utils
{
    public class Auth2
    {
        static string access_toke = "";
        private static string access_token(string code)
        {
            string client_id = Program.Tokens[0];
            string client_sceret = Program.Tokens[1];
            string redirect_url = "https://localhost:7243/Home";
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://discordapp.com/api/oauth2/token");
            webRequest.Method = "POST";
            string parameters = $"client_id={client_id}&client_secret={client_sceret}&grant_type=authorization_code&code={code}&redirect_uri={redirect_url}";
            byte[] byteArray = Encoding.UTF8.GetBytes(parameters);
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = byteArray.Length;
            Stream postStream = webRequest.GetRequestStream();
            postStream.Write(byteArray, 0, byteArray.Length);
            postStream.Close();
            WebResponse response = webRequest.GetResponse();
            postStream = response.GetResponseStream();
            StreamReader reader = new(postStream);
            string responseFromServer = reader.ReadToEnd();
            dynamic? data = JsonConvert.DeserializeObject(responseFromServer);
            access_toke = data.access_token;
            return data.access_token;
        }

        public static string[] Infos(string code)
        {
            string token = access_token(code);
            HttpWebRequest webRequest1 = (HttpWebRequest)WebRequest.Create("https://discordapp.com/api/users/@me");
            webRequest1.Method = "Get";
            webRequest1.ContentLength = 0;
            webRequest1.Headers.Add("Authorization", "Bearer " + token);
            webRequest1.ContentType = "application/x-www-form-urlencoded";
            using HttpWebResponse response1 = webRequest1.GetResponse() as HttpWebResponse;
            StreamReader reader1 = new(response1.GetResponseStream());
            string apiResponse1 = reader1.ReadToEnd();
            dynamic data2 = JsonConvert.DeserializeObject(apiResponse1);
            string[] infos =
            {
                    data2.id,
                    data2.username,
                    $"https://cdn.discordapp.com/avatars/{data2.id}/{data2.avatar}.png",
                    data2.email
                };
            return infos;
        }

        public static async Task<List<DiscordGuild>> Guilds(string id)
        {
            
            List<DiscordGuild> guilds = new();
            foreach (var guild in Program.Discord.Guilds.Values)
            {
                try
                {
                    await guild.GetMemberAsync(Convert.ToUInt64(id));
                    guilds.Add(guild);
                }
                catch { }
            }
            return guilds;
        }
    }
}