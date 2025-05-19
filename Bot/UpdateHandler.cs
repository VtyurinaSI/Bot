using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace Bot
{
    class UpdateHandler : IUpdateHandler
    {
        public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            var info = "Сообщение получено!";
            Console.WriteLine(info);
            botClient.SendMessage(update.Message?.Chat.Id, info, cancellationToken: token);

           // Console.WriteLine("Сообщение получено!");
            return Task.CompletedTask;
        }
    }
}
