using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace miguelito_bot_commands.commands
{
    internal class commands_matematica : BaseCommandModule
    {
        [Command("soma")]
        [Aliases("somar")]
        public async Task soma(CommandContext ctx, [RemainingText] string value = "")
        {
            if (value == "")
            {
                await ctx.RespondAsync("Você deve informar pelo menos dois valores para realizar a soma.");
            }
            else
            {
                string[] valores = value.Split(' ');
                double soma = 0;
                foreach (string valor in valores)
                {
                    soma += Convert.ToDouble(valor);
                }
                await ctx.RespondAsync(soma.ToString());
            }
            await Program.Log("soma");
        }

        [Command("subtrair")]
        [Aliases("diminuir")]
        public async Task subtrair(CommandContext ctx, [RemainingText] string value = "")
        {
            if (value == "")
            {
                await ctx.RespondAsync("Você deve informar pelo menos dois valores para realizar a subtração.");
            }
            else
            {
                string[] valores = value.Split(' ');
                double subtrair = int.Parse(valores[0]);
                for (int i = 1; i < valores.Length; i++)
                {
                    subtrair -= int.Parse(valores[i]);
                }
                await ctx.RespondAsync(subtrair.ToString());
            }
            await Program.Log("subtrair");
        }

        [Command("Bhaskara")]
        public async Task Bhaskara(CommandContext ctx, double a, double b, double c)
        {
            if (a == 0)
            {
                await ctx.RespondAsync("A equação não é do tipo ax²+bx+c=0");
            }
            else
            {
                double delta = Math.Pow(b, 2) - 4 * a * c;
                if (delta < 0)
                {
                    await ctx.RespondAsync("A equação não possui raízes reais");
                }
                else if (delta == 0)
                {
                    double x = -b / (2 * a);
                    await ctx.RespondAsync("A equação possui apenas uma raiz real: " + x);
                }
                else
                {
                    double x1 = (-b + Math.Sqrt(delta)) / (2 * a);
                    double x2 = (-b - Math.Sqrt(delta)) / (2 * a);
                    await ctx.RespondAsync("A equação possui duas raízes reais: " + x1 + " e " + x2);
                }
            }
            await Program.Log("Bhaskara");
        }
        [Command("raiz")]
        public async Task raiz(CommandContext ctx, double a)
        {
            await ctx.TriggerTypingAsync();
            if (a < 0)
            {
                await ctx.RespondAsync("A raiz de um número negativo não existe meu nobre.");
            }
            else
            {
                double raiz = Math.Sqrt(a);
                await ctx.RespondAsync("A raiz de " + a + " é " + raiz);
            }
            await Program.Log("raiz");
        }
        [Command("multiplicar")]
        public async Task multiplicar(CommandContext ctx, [RemainingText] string value = "")
        {
            if (value == "")
            {
                await ctx.RespondAsync("Você deve informar pelo menos dois valores para realizar a multiplicação meu nobre.");
            }
            else
            {
                string[] valores = value.Split(' ');
                double multiplicar = Convert.ToDouble(valores[0]);
                for (int i = 1; i < valores.Length; i++)
                {
                    multiplicar *= Convert.ToDouble(valores[i]);
                }
                await ctx.RespondAsync(multiplicar.ToString());
            }
            await Program.Log("multiplicar");
        }
        [Command("dividir")]
        public async Task dividir(CommandContext ctx, [RemainingText] string value = "")
        {
            if (value == "")
            {
                await ctx.RespondAsync("Você deve informar pelo menos dois valores para realizar a divisão meu nobre.");
            }
            else
            {
                string[] valores = value.Split(' ');
                double dividir = Convert.ToDouble(valores[0]);
                for (int i = 1; i < valores.Length; i++)
                {
                    dividir /= Convert.ToDouble(valores[i]);
                }
                await ctx.RespondAsync(dividir.ToString());
            }
            await Program.Log("dividir");
        }
        [Command("pi")]
        public async Task pi(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync($"O valor de pi é: {Math.PI}");
            await Program.Log("pi");
        }
        [Command("Potência")]
        [Aliases("Potencia", "potenciação", "potenciaçao", "potenciacao")]
        public async Task Potencia(CommandContext ctx, double base_, double expoente)
        {
            await ctx.TriggerTypingAsync();
            if (expoente == 0)
            {
                await ctx.RespondAsync("A potência de um número elevado a zero é 1");
            }
            else
            {
                double potencia = 1;
                for (int i = 0; i < expoente; i++)
                {
                    potencia *= base_;
                }
                await ctx.RespondAsync("A potência de " + base_ + " elevado a " + expoente + " é " + potencia);
            }
            await Program.Log("Potencia");
        }
    }
}