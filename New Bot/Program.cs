using System.Security.Cryptography.X509Certificates;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace NewBot
{
    class Program
    {
        static bool IsUserGEtName;
        static string userName;
        static void Main(string[] args)
        {
            string token = Environment.GetEnvironmentVariable("ONLINE_bot_tg_token");
            if (token == null) { Console.WriteLine("Токен подключения не найден"); return; }
            var client = new TelegramBotClient(token);

            client.StartReceiving(Update, Error);

            Console.Read();
        }

        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken cst)
        {
            var message = update.Message;
            if (message.Text != null)
            {
                await Console.Out.WriteLineAsync($"{message.Chat.FirstName}   |   {message.Text}");
                if (message.Text.ToLower().Contains("/start"))
                {
                    IsUserGEtName = false;
                    userName = null;
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Представтесть, пожалуйста");
                    return;
                }
                else if (!IsUserGEtName)
                {
                    IsUserGEtName = true;
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Здравствуйте, {message.Text} ");
                    userName = message.Text;
                    return;
                }
            }
        }

        private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }
    }
}