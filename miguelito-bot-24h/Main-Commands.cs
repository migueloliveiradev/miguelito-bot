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
            if (value > 0 && text != "")
            {
                string cs = Program.config[2];
                MySqlConnection con = new(cs);
                await con.OpenAsync();
                MySqlCommand cmd = new()
                {
                    Connection = con
                };
                DateTime time_reminder = DateTime.Now;
                if (time == "days" || time == "dias" || time == "day" || time == "dia")
                {
                    if (value < 30)
                    {
                        time_reminder = time_reminder.AddDays(value);
                        cmd.CommandText = $"INSERT INTO REMINDER(ID_USER, DATA_HORA, MENSAGEM, ID_GUILD) VALUES('{ctx.User.Id}','{time_reminder:dd/MM/yyyy HH:mm}', '{text}', '{ctx.Guild.Id}')";
                        await ctx.RespondAsync($"Opa {ctx.Member.Mention}, Lembrete criado com sucesso! a gente se vê em {value} {time} na sua DM ||abre a porra da dm||");
                    }
                    else
                    {
                        await ctx.RespondAsync("O limite de dias é de 30 dias");
                    }

                }
                else if (time == "hours" || time == "horas" || time == "hour" || time == "hora")
                {
                    if (value < 24)
                    {
                        time_reminder = time_reminder.AddHours(value);
                        cmd.CommandText = $"INSERT INTO REMINDER(ID_USER, DATA_HORA, MENSAGEM, ID_GUILD) VALUES('{ctx.User.Id}','{time_reminder:dd/MM/yyyy HH:mm}', '{text}', '{ctx.Guild.Id}')";
                        await ctx.RespondAsync($"Opa {ctx.Member.Mention}, Lembrete criado com sucesso! a gente se vê em {value} {time} na sua DM ||abre a porra da dm||");
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
                        time_reminder = time_reminder.AddMinutes(value);
                        cmd.CommandText = $"INSERT INTO REMINDER(ID_USER, DATA_HORA, MENSAGEM, ID_GUILD) VALUES('{ctx.User.Id}','{time_reminder:dd/MM/yyyy HH:mm}', '{text}', '{ctx.Guild.Id}')";
                        await ctx.RespondAsync($"Opa {ctx.Member.Mention}, Lembrete criado com sucesso! a gente se vê em {value} {time} na sua DM ||abre a porra da dm||");
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
