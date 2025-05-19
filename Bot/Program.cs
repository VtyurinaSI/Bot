using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Bot
{
    internal class Program
    {
        static async Task Main()
        {
            var cts = new CancellationTokenSource();
            var botClient = new TelegramBotClient(BotToken.token);
            var handler = new UpdateHandler();

            try
            {
                var me = await botClient.GetMe(cts.Token);

                Console.WriteLine($"{me.FirstName} запущен!");
                var receiverOptions = new ReceiverOptions
                {
                    AllowedUpdates = [UpdateType.Message],
                    DropPendingUpdates = true
                };

                handler.OnHandleUpdateStarted += Started;
                handler.OnHandleUpdateCompleted += Completed;
                botClient.StartReceiving(handler.HandleUpdateAsync, handler.HandleErrorAsync, receiverOptions);

                await Task.Delay(-1);
            }
            finally
            {
                handler.OnHandleUpdateStarted -= Started;
                handler.OnHandleUpdateCompleted -= Completed;
                await cts.CancelAsync();
            }
        }
        private static void Started(string msg) => Console.WriteLine($"Началась обработка сообщения \"{msg}\"");
        private static void Completed(string msg) => Console.WriteLine($"Закончилась обработка сообщения \"{msg}\"");
    }
}
