using DSharpPlus.Entities;
using Google.Apis.Services;
using Google.Apis.Translate.v2;
using Google.Cloud.Translation.V2;

namespace miguelito_bot_slashcommands.Utils
{
    internal static class Methods
    {
        public static string Translator(string text, string lang = "pt")
        {
            TranslateService service = new(new BaseClientService.Initializer { ApiKey = Program.config[4] });
            TranslationClientImpl client = new(service, TranslationModel.ServiceDefault);
            TranslationResult result = client.TranslateText(text, lang);
            return result.TranslatedText;
        }
        public static string TranslateStatus(string status)
        {
            return status switch
            {
                "Idle" => "Ausente",
                "DoNotDisturb" => "Não pertube",
                _ => status,
            };
        }
        public static string Presence(DiscordPresence status)
        {
            if (string.IsNullOrEmpty(status.Activity?.Name))
            {
                if (status.Activity.Name == "Custom Status")
                {
                    return $"Status {status.Activity.CustomStatus.Name}";
                }
                else
                {
                    return $"Jogando {status.Activity.RichPresence.Application}";
                }
            }
            else
            {
                return "";
            }
        }
    }
}