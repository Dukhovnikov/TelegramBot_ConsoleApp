using System;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;
using static System.String;

namespace TelegramBot_ConsoleApp
{
    internal static class Program
    {
        private static TelegramBotClient _client;

        private static void Main(string[] args)
        {

            Main1();



        }

        private static void Main1()
        {
            var bot = new TelegramApplication();
            bot.Run();
        }        
        private static void Main2()
        {
            _client = new TelegramBotClient("493325045:AAF8Wu5VCb2iVMD-nzo9CtgQKcn9rYNPcMs");
            _client.OnMessage += BotOnMessageReceived;
            _client.OnMessageEdited += BotOnMessageReceived;
            _client.OnCallbackQuery += BotOnCallbackQueryReceived;
            _client.OnInlineResultChosen += BotOnChosenInlineResultReceived;
            _client.StartReceiving();
            
            Console.ReadLine();
            
            _client.StopReceiving(); 
        }        
        private static void Main3()
        {

        }
        
/*        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            var s = Empty;

            if (message.Text.Equals("/start"))
            {
                s = "Hello!\n";
                s += "Please, input some matrix.\n";
                s += "For example:\n";
                s += "1 1 1\n";
                s += "1 1 1\n";
                s += "1 1 1\n";
            }
            else
            {
                var matrix = new Matrix(message.Text);
                var solve = new DeterminantOfAmericanMethod(matrix);
                solve.GetDeterminant();
                s = solve.Logger;
            }                     
            
            if (message?.Type == Telegram.Bot.Types.Enums.MessageType.Text)
            {
                await _client.SendTextMessageAsync(message.Chat.Id, s);
            }
        }  */    
        
/*        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {  
            var message = messageEventArgs.Message;

            if (message == null || message.Type != MessageType.Text) return;

            ReplyKeyboardMarkup replyKeyboard = new[]
            {
                new[] {"One", "Two"},
                new[] {"Three", "Four"}
            };

            await _client.SendTextMessageAsync(message.Chat.Id, "Choose", replyMarkup: replyKeyboard);
        }  */      
        
        
        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {  
            var message = messageEventArgs.Message;

            if (message == null || message.Type != MessageType.Text) return;

            var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                new [] // first row
                {
                    InlineKeyboardButton.WithCallbackData("Button 1", "callback1"),
                    InlineKeyboardButton.WithCallbackData("Button 2", "callback2"),
                }
            });


            await _client.SendTextMessageAsync(message.Chat.Id, "Choose", replyMarkup: inlineKeyboard);
        }  
        
        private static void BotOnChosenInlineResultReceived(object sender, ChosenInlineResultEventArgs chosenInlineResultEventArgs)
        {
            Console.WriteLine($"Received inline result: {chosenInlineResultEventArgs.ChosenInlineResult.ResultId}");
        }

        private static async void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            var message = callbackQueryEventArgs.CallbackQuery.Message;

            if (callbackQueryEventArgs.CallbackQuery.Data.Equals("callback1"))
            {
                await _client.AnswerCallbackQueryAsync(callbackQueryEventArgs.CallbackQuery.Id,
                    "You has choosen Button 1");
            } 
            
            if (callbackQueryEventArgs.CallbackQuery.Data.Equals("callback2"))
            {
                await _client.SendTextMessageAsync(message.Chat.Id, "тест", replyToMessageId: message.MessageId);
                await _client.AnswerCallbackQueryAsync(callbackQueryEventArgs.CallbackQuery.Id,
                    "You has choosen Button 2");
            }
            
            
        }
    }
}