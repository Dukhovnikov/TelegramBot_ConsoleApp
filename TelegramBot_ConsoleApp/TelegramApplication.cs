using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot_ConsoleApp
{
    public class TelegramApplication
    {
        private readonly TelegramBotClient _bot;
        private const string Token = "493325045:AAF8Wu5VCb2iVMD-nzo9CtgQKcn9rYNPcMs";

        private readonly ReplyKeyboardMarkup _replyKeyboard = new[]
        {
            new[] {"Признаки делимости"},
            new[] {"Американский метод выч. определителя"},
            new[] {"Алгоритм Карацубы"},
            new[] {"Умножения матриц (м. Штрассе)"}
        };
           
        
        public TelegramApplication()
        {
            _bot = new TelegramBotClient(Token);
        }

        public void Run()
        {
            try
            {
                var me = _bot.GetMeAsync().Result;
                
                _bot.OnMessage += BotOnMessageReceived;
                _bot.StartReceiving();
                
                Console.WriteLine($"Start listening for @{me.Username}");
                Console.ReadLine();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _bot.StopReceiving();
            }
        }

        
        private async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            
            if (message == null || message.Type != MessageType.Text) return;    
            
            if (message.Text.Equals(value: "/keyboard") || message.Text.Equals(value: "/start"))
            {
                await _bot.SendChatActionAsync(chatId: message.Chat.Id, chatAction: ChatAction.Typing);
                await Task.Delay(millisecondsDelay: 500);               
                
                await _bot.SendTextMessageAsync(chatId: message.Chat.Id, text: "Главное меню",replyMarkup: _replyKeyboard);

            }  
            
            if (message.Text.Equals(value: "Американский метод выч. определителя"))
            {
                await _bot.SendChatActionAsync(chatId: message.Chat.Id, chatAction: ChatAction.Typing);
                await Task.Delay(millisecondsDelay: 500);

                const string usage = @"Введите исходную матрицу.

Пример:
1 1 1
1 1 1
1 1 1

/keyboard - назад
";             
                _bot.OnMessage += BotOnMessageReceivedAmericanMethod;
                _bot.OnMessage -= BotOnMessageReceived;
                await _bot.SendTextMessageAsync(chatId: message.Chat.Id, text: usage, replyMarkup: new ReplyKeyboardRemove());                
            }

            if (message.Text.Equals("Признаки делимости"))
            {
                await _bot.SendChatActionAsync(chatId: message.Chat.Id, chatAction: ChatAction.Typing);
                await Task.Delay(millisecondsDelay: 500);
                const string usage = @"Введите делимое и делитель.

Пример:
4088/73

/keyboard - назад
";               
                _bot.OnMessage += BotOnMessageReceivedPaskalMethod;
                _bot.OnMessage -= BotOnMessageReceived;
                await _bot.SendTextMessageAsync(chatId: message.Chat.Id, text: usage, replyMarkup: new ReplyKeyboardRemove()); 
            }

        }

        private async void BotOnMessageReceivedAmericanMethod(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            
            if (message == null || message.Type != MessageType.Text) return;            

            var rezult = Executor.OnGetDeterminantOfAmericanMethod(message.Text);
            
            if (message.Text.Equals("/keyboard") || message.Text.Equals("/start"))
            {
                _bot.OnMessage += BotOnMessageReceived;
                _bot.OnMessage -= BotOnMessageReceivedAmericanMethod;
                //await _bot.GetUpdatesAsync();
                await _bot.SendTextMessageAsync(chatId: message.Chat.Id, text: "Главное меню",replyMarkup: _replyKeyboard);
                return;
            }
            
            await _bot.SendTextMessageAsync(message.Chat.Id, rezult, ParseMode.Html);
            
        }

        private async void BotOnMessageReceivedPaskalMethod(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            
            if (message == null || message.Type != MessageType.Text) return;

            var rezult = Executor.OnGetPaskaleSolve(message.Text);
            
            if (message.Text.Equals("/keyboard") || message.Text.Equals("/start"))
            {
                _bot.OnMessage += BotOnMessageReceived;
                _bot.OnMessage -= BotOnMessageReceivedPaskalMethod;
                await _bot.SendTextMessageAsync(chatId: message.Chat.Id, text: "Главное меню",replyMarkup: _replyKeyboard);
                return;
            }
            
            await _bot.SendTextMessageAsync(message.Chat.Id, rezult, ParseMode.Html);
            
        }
        
        
    }

    #region OldestCode

/*    private async void BotOnMessageMenu(object sender, MessageEventArgs messageEventArgs)
    {
    var message = messageEventArgs.Message;
            
        if (message == null || message.Type != MessageType.Text) return;
            
    if (message.Text.Equals("/keyboard") || message.Text.Equals("/start"))
    {
        await _bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
        await Task.Delay(500);
                
        await _bot.SendTextMessageAsync(message.Chat.Id, "Take your pick!", replyMarkup: _replyKeyboard);

    }                        
    }*/


    #endregion
}