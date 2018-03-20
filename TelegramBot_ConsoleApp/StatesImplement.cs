using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot_ConsoleApp
{
    public class MainMenuState : TelegramBotState
    {
        public MainMenuState(TelegramBot telegramBot) : base(telegramBot)
        {
        }


        public override void CallbackKeyboard(TelegramBotClient telegramBotClient, MessageEventArgs messageEventArgs)
        {
            ReplyKeyboardMarkup replyKeyboard = new[]
            {
                new[] {"Признаки делимости"},
                new[] {"Американский метод выч. определителя"},
                new[] {"Алгоритм Карацубы"},
                new[] {"Умножения матриц (м. Штрассе)"}
            };
            
            var message = messageEventArgs.Message;

            telegramBotClient.SendTextMessageAsync(message.Chat.Id, "<i>Главное меню</i>",
                replyMarkup: replyKeyboard, parseMode: ParseMode.Html);
        }

        public override void Response(TelegramBotClient telegramBotClient, MessageEventArgs messageEventArgs)
        {
            ReplyKeyboardMarkup replyKeyboard = new[]
            {
                new[] {"Признаки делимости"},
                new[] {"Американский метод выч. определителя"},
                new[] {"Алгоритм Карацубы"},
                new[] {"Умножения матриц (м. Штрассе)"}
            };
            
            var message = messageEventArgs.Message;

            telegramBotClient.SendTextMessageAsync(message.Chat.Id, "<i>Главное меню</i>",
                replyMarkup: replyKeyboard, parseMode: ParseMode.Html);
            
            if (message.Text.Equals("Признаки делимости"))
            {
                telegramBotClient.SendChatActionAsync(chatId: message.Chat.Id, chatAction: ChatAction.Typing);
                Task.Delay(millisecondsDelay: 500);
/*                const string usage = @"Введите делимое и делитель.

Пример:
4088/73

/keyboard - назад
";    */

                TelegramBot.State = new DivisibilityCriteriaMenu(TelegramBot);
                //await _bot.SendTextMessageAsync(chatId: message.Chat.Id, text: usage, replyMarkup: new ReplyKeyboardRemove());                
                //await _bot.SendTextMessageAsync(message.Chat.Id, "<i>Признаки делимости</i>", replyMarkup: _replyKeyboardDivisibility, parseMode: ParseMode.Html); 
            }
        }
    }

    public class DivisibilityCriteriaMenu : TelegramBotState
    {
        public DivisibilityCriteriaMenu(TelegramBot telegramBot) : base(telegramBot)
        {
        }

        public override void CallbackKeyboard(TelegramBotClient telegramBotClient, MessageEventArgs messageEventArgs)
        {
            throw new NotImplementedException();
        }

        public override void Response(TelegramBotClient telegramBotClient, MessageEventArgs messageEventArgs)
        {
            ReplyKeyboardMarkup replyKeyboardDivisibility = new[]
            {
                new[] {"Признак Паскаля"},
                new[] {"Признак Рачинсого №1"},
                new[] {"Признак Рачинсого №2"},
                new[] {"Признак Рачинсого №3"},
                new[] {"Главное меню"}
            };

            var message = messageEventArgs.Message;

            if (message.Text.Equals("Признак Паскаля"))
            {
                telegramBotClient.SendChatActionAsync(chatId: message.Chat.Id, chatAction: ChatAction.Typing);
                Task.Delay(millisecondsDelay: 500);
                const string usage = @"Введите делимое и делитель.

Пример: 4088/73
";    
                TelegramBot.State = new DivisibilityCriteriaPaskal(TelegramBot);
                telegramBotClient.SendTextMessageAsync(message.Chat.Id, usage, replyMarkup: new ReplyKeyboardRemove());
            }
            else
            {
                telegramBotClient.SendTextMessageAsync(message.Chat.Id, "<i>Меню алгоритмов деления</i>",
                    replyMarkup: replyKeyboardDivisibility, parseMode: ParseMode.Html);
            }
        }
    }

    public class DivisibilityCriteriaPaskal : TelegramBotState
    {
        public DivisibilityCriteriaPaskal(TelegramBot telegramBot) : base(telegramBot)
        {
        }

        public override void CallbackKeyboard(TelegramBotClient telegramBotClient, MessageEventArgs messageEventArgs)
        {
            throw new NotImplementedException();
        }

        public override void Response(TelegramBotClient telegramBotClient, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;

            var solve = Executor.OnGetPaskaleSolve(message.Text);
            
            telegramBotClient.SendTextMessageAsync(message.Chat.Id, solve, ParseMode.Html);
            
            
            
        }
    }
}