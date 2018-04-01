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
        private const string Token = "your token";
        private readonly TelegramBotClient _bot = new TelegramBotClient(Token);
        private TelegramBotState State { get; set; }

        private readonly ReplyKeyboardMarkup _replyKeyboard = new[]
        {
            new[] {"Признаки делимости"},
            new[] {"Американский метод выч. определителя"},
            new[] {"Алгоритм Карацубы"},
            new[] {"Умножения матриц (м. Штрассе)"}
        };
        
        private readonly ReplyKeyboardMarkup _replyKeyboardDivisibility = new[]
        {
            new[] {"Признак Паскаля"},
            new[] {"Признак Рачинсого №1"},
            new[] {"Признак Рачинсого №2"},
            new[] {"Признак Рачинсого №3"},
            new[] {"Главное меню"}
        };

        private readonly InlineKeyboardMarkup _inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                new[] //First row
                {
                    //First column
                    InlineKeyboardButton.WithCallbackData("Назад", "GoToMainMenu")
                }
            }
        );        

        public void Run()
        {
            try
            {
                var me = _bot.GetMeAsync().Result;
                _bot.OnMessage += BotOnMessageReceived;
                _bot.OnCallbackQuery += BotOnCallbackQueryReceived;
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

        private async void BotOnMessageReceivedNew(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;

            
        }
        
        
        
        private async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            
            if (message == null || message.Type != MessageType.Text) return;    
            
            if (message.Text.Equals(value: "/keyboard") || message.Text.Equals(value: "/start"))
            {
                await _bot.SendChatActionAsync(chatId: message.Chat.Id, chatAction: ChatAction.Typing);
                await Task.Delay(millisecondsDelay: 500); // simulate longer running task    
                
                Console.WriteLine($"Connected user: @{message.Chat.Username}");        
                Console.WriteLine(message.Chat.Id);
                
                await _bot.SendTextMessageAsync(chatId: message.Chat.Id, text: "<i>Главное меню</i>",
                    replyMarkup: _replyKeyboard, parseMode: ParseMode.Html);

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
/*                const string usage = @"Введите делимое и делитель.

Пример:
4088/73

/keyboard - назад
";    */           
                //_bot.OnMessage += BotOnMessageReceivedPaskalMethod;
                _bot.OnMessage += BotOnMessageReceivedDivisibiltyMenu;
                _bot.OnMessage -= BotOnMessageReceived;
                //await _bot.SendTextMessageAsync(chatId: message.Chat.Id, text: usage, replyMarkup: new ReplyKeyboardRemove());                
                await _bot.SendTextMessageAsync(message.Chat.Id, "<i>Признаки делимости</i>", replyMarkup: _replyKeyboardDivisibility, parseMode: ParseMode.Html); 
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
                await _bot.SendTextMessageAsync(chatId: message.Chat.Id, text: "Главное меню",replyMarkup: _replyKeyboard);
                return;
            }
            
            //await _bot.SendTextMessageAsync(message.Chat.Id, rezult, ParseMode.Html);
            await _bot.SendTextMessageAsync(message.Chat.Id, rezult, ParseMode.Html, replyMarkup: _inlineKeyboard);

        }

        private async void BotOnMessageReceivedDivisibiltyMenu(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            
            if (message == null || message.Type != MessageType.Text) return;    
            
            if (message.Text.Equals("/keyboard") || message.Text.Equals("/start") || message.Text.Equals("Главное меню"))
            {
                await _bot.SendChatActionAsync(chatId: message.Chat.Id, chatAction: ChatAction.Typing);
                await Task.Delay(millisecondsDelay: 500); // simulate longer running task
                
                _bot.OnMessage += BotOnMessageReceived;
                _bot.OnMessage -= BotOnMessageReceivedDivisibiltyMenu;
                
                await _bot.SendTextMessageAsync(chatId: message.Chat.Id, text: "Главное меню",replyMarkup: _replyKeyboard);

            }                         
            
            if (message.Text.Equals("Признак Паскаля"))
            {
                await _bot.SendChatActionAsync(chatId: message.Chat.Id, chatAction: ChatAction.Typing);
                await Task.Delay(millisecondsDelay: 500); // simulate longer running task
                
                _bot.OnMessage += BotOnMessageReceivedPaskalMethod;
                _bot.OnMessage -= BotOnMessageReceivedDivisibiltyMenu;
                
                const string usage = @"Введите делимое и делитель.

Пример:
4088/73

/keyboard - назад
";               
                await _bot.SendTextMessageAsync(chatId: message.Chat.Id, text: usage, replyMarkup: new ReplyKeyboardRemove());     
            }
            
            if (message.Text.Equals("Признак Рачинсого №1"))
            {
                await _bot.SendChatActionAsync(chatId: message.Chat.Id, chatAction: ChatAction.Typing);
                await Task.Delay(millisecondsDelay: 500); // simulate longer running task
                
                _bot.OnMessage += BotOnMessageReceivedRachinskiyOne;
                _bot.OnMessage -= BotOnMessageReceivedDivisibiltyMenu;
                
                const string usage = @"Введите делимое и делитель.

Пример:
4088/73

/keyboard - назад
";               
                await _bot.SendTextMessageAsync(chatId: message.Chat.Id, text: usage, replyMarkup: new ReplyKeyboardRemove());    
            }
            
            if (message.Text.Equals("Признак Рачинсого №2"))
            {

            }
            
            if (message.Text.Equals("Признак Рачинсого №3"))
            {

            }
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
            
            await _bot.SendTextMessageAsync(message.Chat.Id, rezult, ParseMode.Html, replyMarkup: _inlineKeyboard);
            //await _bot.SendTextMessageAsync(message.Chat.Id, rezultRachinskiy, ParseMode.Html);
            
        }
        
        private async void BotOnMessageReceivedRachinskiyOne(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            
            if (message == null || message.Type != MessageType.Text) return;

            var rezultRachinskiy = Executor.OnGetRachinskiyOneSolve(message.Text);
            
            if (message.Text.Equals("/keyboard") || message.Text.Equals("/start"))
            {
                _bot.OnMessage += BotOnMessageReceived;
                _bot.OnMessage -= BotOnMessageReceivedRachinskiyOne;
                await _bot.SendTextMessageAsync(chatId: message.Chat.Id, text: "Главное меню",replyMarkup: _replyKeyboard);
                return;
            }
            
            //await _bot.SendTextMessageAsync(message.Chat.Id, rezultRachinskiy, ParseMode.Html);
            await _bot.SendTextMessageAsync(message.Chat.Id, rezultRachinskiy, ParseMode.Html, replyMarkup: _inlineKeyboard);
            
        }

        private async void BotOnCallbackQueryReceived(object sender,
            CallbackQueryEventArgs callbackQueryEventArgs)
        {
            var message = callbackQueryEventArgs.CallbackQuery.Message;
            var data = callbackQueryEventArgs.CallbackQuery.Data;
            
            if (data.Equals("GoToMainMenu"))
            {
                await _bot.AnswerCallbackQueryAsync(callbackQueryEventArgs.CallbackQuery.Id, "Переход в главное меню");
                _bot.OnMessage -= BotOnMessageReceived;
                _bot.OnMessage += BotOnMessageReceived;
                _bot.OnMessage -= BotOnMessageReceivedAmericanMethod;
                _bot.OnMessage -= BotOnMessageReceivedPaskalMethod;
                _bot.OnMessage -= BotOnMessageReceivedRachinskiyOne;
                await _bot.SendTextMessageAsync(chatId: message.Chat.Id, text: "Главное меню",replyMarkup: _replyKeyboard);
            }
        }
/*        private async void BotOnMessageReceivedPaskalMethod(object sender, MessageEventArgs messageEventArgs)
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
            
        }*/
        
        
    }

    #region OldestCode

    
/*        private async void BotOnMessageReceivedAmericanMethod(object sender, MessageEventArgs messageEventArgs)
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
            
            //await _bot.SendTextMessageAsync(message.Chat.Id, rezult, ParseMode.Html);
            await _bot.SendTextMessageAsync(message.Chat.Id, rezult, ParseMode.Html, replyMarkup: _inlineKeyboard);

        }*/
    
    
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