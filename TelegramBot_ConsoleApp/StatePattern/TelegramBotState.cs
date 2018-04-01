using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot_ConsoleApp
{
    public abstract class TelegramBotState
    {
        public TelegramApplication TelegramBot { get; set; }
        public IReplyMarkup ReplyMarkup { get; set; } = null;
        public ParseMode ParseMode { get; set; } = ParseMode.Default;

        protected TelegramBotState(TelegramApplication telegramBot)
        {
            TelegramBot = telegramBot;
        }

        public abstract string Response(string data);        
    }
}
