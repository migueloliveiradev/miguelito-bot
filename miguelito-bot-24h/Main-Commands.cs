using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using MySql.Data.MySqlClient;

namespace miguelito_bot_24h
{
    internal class Main_Commands : BaseCommandModule
    {
        [Command("reminder"), Aliases("remind", "lembrar", "lembrete")]
        public async Task Reminder(CommandContext ctx, int value, string time, [RemainingText] string text)
        {
            if(value > 0  && text != "")
            {
                string cs = Program.config[2];
                using var con = new MySqlConnection(cs);
                await con.OpenAsync();
                using var cmd = new MySqlCommand();
                cmd.Connection = con;
                
                
                if (time == "days" || time == "dias" || time == "day" || time == "dia")
                {
                    if(value < 30)
                    {
                        cmd.CommandText = $"INSERT INTO REMINDER(ID_USER, DATA_HORA, MENSAGEM) VALUES('{ctx.User.Id}', 'NULL', '{text}')";
                        
                    }
                    else
                    {
                        await ctx.RespondAsync("O limite de dias é de 20 dias");
                    }
                    
                }
                else if (time == "hours" || time == "horas" || time == "hour" || time == "hora")
                {
                    if (value < 24)
                    {
                        cmd.CommandText = $"INSERT INTO REMINDER(ID_USER, DATA_HORA, MENSAGEM) VALUES('{ctx.User.Id}', 'NULL', '{text}')";
                        
                    }
                    else
                    {
                        await ctx.RespondAsync("O limite de horas é de 24 horas");
                    }
                }
                else if (time == "minutes" || time == "minutos" || time == "minute" || time == "minuto")
                {
                    if (value < 60)
                    {
                        cmd.CommandText = $"INSERT INTO REMINDER(ID_USER, DATA_HORA, MENSAGEM) VALUES('{ctx.User.Id}', 'NULL', '{text}')";
                        
                    }
                    else
                    {
                        await ctx.RespondAsync("O limite de minutos é de 60 minutos");
                    }
                }
                else
                {
                    await ctx.RespondAsync("O tempo deve ser dia, hora, minuto ou segundo");
                }
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                await ctx.RespondAsync($"{ctx.Member.Mention} Você não inseriu um valor válido ou nenhum texto.");
            }
        }
    }
}
