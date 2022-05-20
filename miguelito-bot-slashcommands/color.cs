using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miguelito_bot_slashcommands
{
    internal class color
    {
        public static DiscordColor cores()
        {
            DiscordColor[] cores = {
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
            Random rnd = new Random();
            return cores[rnd.Next(0, cores.Length)];
        }
    }
}
