using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Bot
{
    internal class Program
    {
        private static void UpdateReceived(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            if (update.Message?.From is null)
            {
                Console.WriteLine("Message is null");
                return;
            }
            var info = $"{update.Message.From.Username} : {update.Message.Text}";
            Console.WriteLine(info);
            botClient.SendMessage(update.Message.Chat.Id, info, cancellationToken: token);
        }

        static async Task Main()
        {
            var cts = new CancellationTokenSource();
            string? key = Environment.GetEnvironmentVariable("BOT_TOKEN_HW");

            var botClient = new TelegramBotClient(key);

            var me = await botClient.GetMe(cts.Token);
            Console.WriteLine($"Hello, {me.FirstName} {me.LastName} with username {me.Username} and id {me.Id}!");
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = [UpdateType.Message],
                DropPendingUpdates = true
            };
            var handler = new UpdateHandler();
            botClient.StartReceiving(handler, receiverOptions);

            await Task.Delay(-1);
            await cts.CancelAsync();
        }
    }
}
