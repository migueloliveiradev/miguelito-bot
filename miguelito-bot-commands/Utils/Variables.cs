using DSharpPlus.Entities;

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

        public static List<string> bom_dia;

        public static async Task AddBomdia()
        {
            DiscordGuild miguelito = await Program.cliente.GetGuildAsync(822845253131829289);
            DiscordChannel channel = miguelito.GetChannel(824755190225174628);
            var msgs = channel.GetMessagesAsync();
            /*foreach (DiscordMessage msg in msgs.Result)
            {
                foreach (DiscordAttachment attachment in msg.Attachments)
                {
                    bom_dia.Add(attachment.Url);
                }
            }*/
        }
    }
}
