using Google.Apis.Services;
using Google.Apis.Translate.v2;
using Google.Cloud.Translation.V2;
using MySql.Data.MySqlClient;

namespace miguelito_bot_slashcommands.Utils
{
    internal static class Methods
    {
        public static string Translator(string text, string lang)
        {
            TranslateService service = new(new BaseClientService.Initializer { ApiKey = Program.config[4] });
            TranslationClientImpl client = new(service, TranslationModel.ServiceDefault);
            TranslationResult result = client.TranslateText(text, lang);
            return result.TranslatedText;
        }
    }
}