using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace miguelito_bot_commands.commands
{
    internal class commands_matematica : BaseCommandModule
    {
        [Command("soma")]
        [Aliases("somar")]
        public async Task soma(CommandContext ctx, long a = 0, long b = 0,
           long c = 0, long d = 0, long e = 0, long f = 0, long j = 0,
           long k = 0, long l = 0, long m = 0, long n = 0, long o = 0,
           long p = 0, long q = 0, long r = 0, long s = 0, long t = 0,
           long u = 0, long v = 0, long x = 0, [RemainingText] long ruim = 0)
        {
            await ctx.TriggerTypingAsync();
            if (ruim == 0)
            {
                await ctx.RespondAsync((a + b + c + d + e + f + j + k + l + m + n + o + p + q + r + s + t + u + v + x).ToString());
            }
            else
            {
                await ctx.RespondAsync("O maximo de numeros que eu consigo somar por vez é apenas 20 meu rei :pensive:");
            }
            await Program.log("soma");
        }

        [Command("subtrair")]
        [Aliases("diminuir")]
        public async Task subtrair(CommandContext ctx, long a = 0, long b = 0,
           long c = 0, long d = 0, long e = 0, long f = 0, long j = 0,
           long k = 0, long l = 0, long m = 0, long n = 0, long o = 0,
           long p = 0, long q = 0, long r = 0, long s = 0, long t = 0,
           long u = 0, long v = 0, long x = 0, [RemainingText] long ruim = 0)
        {
            await ctx.TriggerTypingAsync();
            if (ruim == 0)
            {
                await ctx.RespondAsync((a - b - c - d - e - f - j - k - l - m - n - o - p - q - r - s - t - u - v - x).ToString());
            }
            else
            {
                await ctx.RespondAsync("O maximo de numeros que eu consigo subtrair por vez é apenas 20 meu rei :pensive:");
            }
            await Program.log("soma");
        }

        [Command("Bhaskara")]
        public async Task Bhaskara(CommandContext ctx, double a = 0, double b = 0, double c = 0)
        {
            await ctx.TriggerTypingAsync();
            if (c < 0)
            {
                c = c * -1;
            }
            double delta_p1 = Math.Sqrt(b * b);
            double delta_p2 = Math.Sqrt(4 * (a * c));
            double delta = (delta_p1 - delta_p2);
            if (delta < 0)
            {
                double a1 = (-b + (delta)) / (2 * a);
                double a2 = (-b - (delta)) / (2 * a);
                await ctx.RespondAsync($"O resultado é:\n\n{a1}\nou\n{a2}");
            }
            else
            {
                await ctx.RespondAsync($"O resultado é Ø");
            }
            await Program.log("soma");
        }
        [Command("raiz")]
        public async Task raiz(CommandContext ctx, double a = 0)
        {
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync(Math.Sqrt(a).ToString());
            await Program.log("soma");
        }
        [Command("multiplicar")]
        public async Task multiplicar(CommandContext ctx, long a = 0, long b = 0,
           long c = 0, long d = 0, long e = 0, long f = 0, long j = 0,
           long k = 0, long l = 0, long m = 0, long n = 0, long o = 0,
           long p = 0, long q = 0, long r = 0, long s = 0, long t = 0,
           long u = 0, long v = 0, long x = 0)
        {
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync((a * b * c * d * e * f * j * k * l * m * n * o * p * q * r * s * t * u * v * x).ToString());
            await Program.log("soma");
        }
        [Command("dividir")]
        public async Task dividir(CommandContext ctx, long a = 0, long b = 0,
           long c = 0, long d = 0, long e = 0, long f = 0, long j = 0,
           long k = 0, long l = 0, long m = 0, long n = 0, long o = 0,
           long p = 0, long q = 0, long r = 0, long s = 0, long t = 0,
           long u = 0, long v = 0, long x = 0)
        {
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync((a / b / c / d / e / f / j / k / l / m / n / o / p / q / r / s / t / u / v / x).ToString());
            await Program.log("soma");
        }
        [Command("pi")]
        public async Task pi(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync(Math.PI.ToString());
            await Program.log("soma");
        }
        [Command("Potência")]
        [Aliases("Potencia", "potenciação", "potenciaçao", "potenciacao")]
        public async Task Potencia(CommandContext ctx, long a = 0, long b = 0)
        {
            await ctx.TriggerTypingAsync();
            if (a != 0 && b != 0)
            {
                await ctx.RespondAsync(Math.Pow(a, b).ToString());
                await Program.log("soma");
            }
            else
            {
                await ctx.RespondAsync($"Opa {ctx.Member.Mention}, por gentileza coloque os numeros pois ainda não tenho" +
                    $"poder de advinhar\n\n" +
                    $"**-Potência** [denominador] [numerador]\n\n" +
                    $"Aliases: Potencia, potenciação, potenciaçao, potenciacao");
            }
        }
    }
}
