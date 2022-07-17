using DSharpPlus.Entities;
using System.Reflection;
using System.Text;

namespace miguelito_bot_commands.Utils
{
    public class Variables
    {
        public static DiscordColor Cores()
        {
            DiscordColor[] colors = {
                DiscordColor.Aquamarine,
                DiscordColor.Azure,
                DiscordColor.Black,
                DiscordColor.Blue,
                DiscordColor.Blurple,
                DiscordColor.Brown,
                DiscordColor.Chartreuse,
                DiscordColor.CornflowerBlue,
                DiscordColor.Cyan,
                DiscordColor.DarkBlue,
                DiscordColor.DarkButNotBlack,
                DiscordColor.DarkGray,
                DiscordColor.DarkGreen,
                DiscordColor.DarkRed,
                DiscordColor.Gold,
                DiscordColor.Goldenrod,
                DiscordColor.Gray,
                DiscordColor.Grayple,
                DiscordColor.Green,
                DiscordColor.HotPink,
                DiscordColor.IndianRed,
                DiscordColor.LightGray,
                DiscordColor.Lilac,
                DiscordColor.Magenta,
                DiscordColor.MidnightBlue,
                DiscordColor.None,
                DiscordColor.NotQuiteBlack,
                DiscordColor.Orange,
                DiscordColor.PhthaloBlue,
                DiscordColor.PhthaloGreen,
                DiscordColor.Purple,
                DiscordColor.Red,
                DiscordColor.Rose,
                DiscordColor.SapGreen,
                DiscordColor.Sienna,
                DiscordColor.SpringGreen,
                DiscordColor.Teal,
                DiscordColor.Turquoise,
                DiscordColor.VeryDarkGray,
                DiscordColor.Violet,
                DiscordColor.Wheat,
                DiscordColor.White,
                DiscordColor.Yellow};
            Random rnd = new();
            int i = rnd.Next(0, colors.Length);
            return colors[i];
        }

        public static List<string> Bomdia = new();

        public static List<string> Conselhos = new();

        public static List<string> Piadas = new();

        public static List<string> Cantadas = new();

        public static List<string> Curiosidades = new();

        public static async Task AddVariables()
        {
            //Bom dia add
            DiscordChannel Channel_Bomdia = await Program.Cliente.GetChannelAsync(982120078420115496);
            foreach (DiscordMessage msg in Channel_Bomdia.GetMessagesAsync(1000).Result)
            {
                foreach (DiscordAttachment attachment in msg.Attachments)
                {
                    Bomdia.Add(attachment.Url);
                }
            }

            //Conselhos add
            Conselhos = ReadLines(() => Assembly.GetExecutingAssembly()
                        .GetManifestResourceStream("miguelito_bot_commands.text.conselhos.miguelito"), Encoding.UTF8).ToList();

            //Piadas add
            Piadas = ReadLines(() => Assembly.GetExecutingAssembly()
                     .GetManifestResourceStream("miguelito_bot_commands.text.piadas.miguelito"), Encoding.UTF8).ToList();

            //Cantadas add
            Cantadas = ReadLines(() => Assembly.GetExecutingAssembly()
                       .GetManifestResourceStream("miguelito_bot_commands.text.cantada.miguelito"), Encoding.UTF8).ToList();

            //Curiosidades add
            Curiosidades = ReadLines(() => Assembly.GetExecutingAssembly()
                           .GetManifestResourceStream("miguelito_bot_commands.text.curiosidades.miguelito"), Encoding.UTF8).ToList();
        }

        public static IEnumerable<string> ReadLines(Func<Stream> streamProvider, Encoding encoding)
        {
            Stream stream = streamProvider();
            StreamReader reader = new(stream, encoding);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
            }
        }
    }
}