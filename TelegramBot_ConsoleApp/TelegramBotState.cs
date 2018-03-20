using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot_ConsoleApp
{
    public abstract class TelegramBotState
    {
        public TelegramBot TelegramBot;
        

        protected TelegramBotState(TelegramBot telegramBot)
        {
            TelegramBot = telegramBot;
        }

        public abstract void CallbackKeyboard(TelegramBotClient telegramBotClient, MessageEventArgs messageEventArgs);
        public abstract void Response(TelegramBotClient telegramBotClient, MessageEventArgs messageEventArgs);        
    }
}
