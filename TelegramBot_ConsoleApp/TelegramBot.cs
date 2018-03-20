using System;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot_ConsoleApp
{
/*    public enum TelegramBotStateEnum
    {
        MainMenu,
        DibisibilityCriteriaMenu,
        DibisibilityCriteriaPaskal,
        DibisibilityCriteriaRachinskii1
    }*/


    public enum TelegramBotStateEnum
    {
        MainMenu,
        
        DivisibilityCriteriaMenu,
        AmericanFindDeterminant,
        MultiplicationMatrixShtrassen,
        MultiplicationNubmersKaratsuba,
        
        DivisibilityCriteriaPaskal,
        DivisibilityCriteriaRachinskiiFirst,
        DivisibilityCriteriaRachinskiiSecond,
        DivisibilityCriteriaRachinskiiThird,
        
        MultiplicationMatrixShtrassenWatingMatrixB
    };
    
    public class TelegramBot
    {
        private readonly TelegramBotClient _bot;
        private const string Token = "Your token!";
        public TelegramBotState State { get; set; }
        public TelegramBotStateEnum StateEnum { get; set; }
        private TelegramBotData UserLibrary { get; set; }
        
        private Matrix AMatrix { get; set; }
        private Matrix BMatrix { get; set; }

        private readonly InlineKeyboardMarkup _inlineKeyboardMainMenu =  new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Признаки делимости", "DivisibilityCriteriaMenu")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Американский метод выч. определителя",
                        "AmericanFindDeterminant")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Алгоритм Карацубы",
                        "MultiplicationNubmersKaratsuba")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Умножения матриц (м. Штрассена)",
                        "MultiplicationMatrixShtrassen")
                }
            }
        );
            
        private readonly InlineKeyboardMarkup _inlineKeyboardDivisibilityMenu = new InlineKeyboardMarkup(new[]
            {
                new[] //First row
                {
                    //First column
                    InlineKeyboardButton.WithCallbackData("Признак Паскаля", "DivisibilityCriteriaPaskal"),    
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Признак Рачинсого №1", "DivisibilityCriteriaRachinskiiFirst")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Признак Рачинсого №2", "DivisibilityCriteriaRachinskiiSecond")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Признак Рачинсого №3", "DivisibilityCriteriaRachinskiiThird")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Главное меню", "GoToMainMenu")
                }
            }
        );
        
        private readonly InlineKeyboardMarkup _inlineKeyboardGoToMainMenu = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Главное меню", "GoToMainMenu")
                }
            }
        );
        
        public TelegramBot()
        {
            _bot = new TelegramBotClient(Token);
            UserLibrary = new TelegramBotData();
        }
        
        public void Run()
        {
            try
            {
                var me = _bot.GetMeAsync().Result;
                _bot.OnMessage += BotOnMessageReceivedSwitch;
                _bot.OnCallbackQuery += BotOnCallbackQueryReceivedSwitch;
                _bot.StartReceiving();
                
                Console.WriteLine($"Start listening for @{me.Username}");
                Console.ReadLine();

            }
            finally
            {
                _bot.StopReceiving();
            }
        }

        private async void BotOnMessageReceivedSwitch(object sender, MessageEventArgs messageEventArgs)
        {            
            var message = messageEventArgs.Message;
            var username = message.Chat.Username;
            var userState = TelegramBotStateEnum.MainMenu;

            if (username == null)
            {
                await _bot.SendTextMessageAsync(message.Chat.Id, $"Ошибка!\n У вас отсутвует имя пользователя!");
                return;
            }
            
            if (!UserLibrary.ContainsUser(username))
            {
                var user = new TelegramBotUser()
                {
                    CurrentState = TelegramBotStateEnum.MainMenu,
                    ChatId = message.Chat.Id,
                    UserName = username,
                    Lock = false               
                };
                Console.Write(username);
                UserLibrary.AddUser(user);
            }
            else
            {
                userState = UserLibrary.GetTelegramBotUserState(username);
            }

            //UserLibrary.Logger +=
                //$"Получено сообщение: от {username}. {message.Date}. State: {userState}. ({message.Text})";
            
            Console.WriteLine($"Получено сообщение: от {username}. {message.Date}. State: {userState}. ({message.Text})");
            
            if (!UserLibrary.GetTelegramBotUser(username).Lock)
            {
                var error = @"Error! 

You are haven't access to this bot.
Please, write to creator for access.";
                
                await _bot.SendTextMessageAsync(message.Chat.Id, error, replyMarkup: new ReplyKeyboardRemove());
                return;
            }

            if (message.Text.Split(" ").First().Equals("/access") && message.Chat.Username.Equals("Dukhovnikov"))
            {
                try
                {
                    var commandmessage = message.Text.Split(" ");
                    var userforaccess = commandmessage[1].Substring(1);

                    if (UserLibrary.ContainsUser(userforaccess))
                    {
                        await _bot.SendTextMessageAsync(message.Chat.Id,
                            $"Пользователю @{userforaccess} разрешён доступ.");
                        UserLibrary.ChangeAccessUser(userforaccess, true);
                    }
                    else
                    {
                        await _bot.SendTextMessageAsync(message.Chat.Id,
                            $"Ошибка!\n Данный пользователь отсутсвует в базе и не просил доступ.");
                    }
                }
                catch (Exception e)
                {
                    await _bot.SendTextMessageAsync(message.Chat.Id, $"Ошибка!\n Неверно задана конадна.");
                }
                
                return;
            }
            
            switch (userState)
            {
                case TelegramBotStateEnum.MainMenu:
                    await _bot.SendTextMessageAsync(message.Chat.Id, "Сделайте выбор!", replyMarkup: _inlineKeyboardMainMenu);                                        
                    break;
                    
                case TelegramBotStateEnum.DivisibilityCriteriaMenu:
                    await _bot.SendTextMessageAsync(message.Chat.Id, "Сделайте выбор!", replyMarkup: _inlineKeyboardDivisibilityMenu);
                    break;
                    
                case TelegramBotStateEnum.AmericanFindDeterminant:
                    var solveAmericanFindDeterminant = Executor.OnGetDeterminantOfAmericanMethod(message.Text);
                    await _bot.SendTextMessageAsync(message.Chat.Id, solveAmericanFindDeterminant, replyMarkup: _inlineKeyboardGoToMainMenu, parseMode: ParseMode.Html);
                    break;
                    
                case TelegramBotStateEnum.MultiplicationMatrixShtrassen:
                    var aMatrix = new Matrix(message.Text);

                    if (((INullable) aMatrix).IsNull)
                    {
                        await _bot.SendTextMessageAsync(message.Chat.Id, "Ошибка! Неправильно задана матрица А", replyMarkup: _inlineKeyboardGoToMainMenu, parseMode: ParseMode.Html);
                    }
                    else
                    {
                        AMatrix = aMatrix;
                        await _bot.SendTextMessageAsync(message.Chat.Id, "Введите матрицу В", replyMarkup: _inlineKeyboardGoToMainMenu, parseMode: ParseMode.Html);
                        UserLibrary.UpdateUserState(username, TelegramBotStateEnum.MultiplicationMatrixShtrassenWatingMatrixB);
                    }
                    break;
                    
                case TelegramBotStateEnum.MultiplicationNubmersKaratsuba:
                    var solveKaratsuba = Executor.OnGetMultiplicationNubmersKaratsubaSolve(message.Text);
                    await _bot.SendTextMessageAsync(message.Chat.Id,"<code>Алгоритм Карацубы</code>\n"+solveKaratsuba, replyMarkup: _inlineKeyboardGoToMainMenu, parseMode: ParseMode.Html);
                    break;
                    
                case TelegramBotStateEnum.DivisibilityCriteriaPaskal:
                    var solvePaskal = Executor.OnGetPaskaleSolve(message.Text);
                    await _bot.SendTextMessageAsync(message.Chat.Id, solvePaskal, replyMarkup: _inlineKeyboardGoToMainMenu, parseMode: ParseMode.Html);
                    break;
                    
                case TelegramBotStateEnum.DivisibilityCriteriaRachinskiiFirst:
                    var solveRachinskiiOne = Executor.OnGetRachinskiyOneSolve(message.Text);
                    await _bot.SendTextMessageAsync(message.Chat.Id, solveRachinskiiOne, replyMarkup: _inlineKeyboardGoToMainMenu, parseMode: ParseMode.Html);
                    break;
                    
                case TelegramBotStateEnum.DivisibilityCriteriaRachinskiiSecond:
                    var solveRachinskiiSecond = Executor.OnGetRachinskiySecondSolve(message.Text);
                    await _bot.SendTextMessageAsync(message.Chat.Id, solveRachinskiiSecond, replyMarkup: _inlineKeyboardGoToMainMenu, parseMode: ParseMode.Html);
                    break;
                    
                case TelegramBotStateEnum.DivisibilityCriteriaRachinskiiThird:
                    var solveRachinskiiThird = Executor.OnGetRachinskiyThridSolve(message.Text);
                    await _bot.SendTextMessageAsync(message.Chat.Id, solveRachinskiiThird, replyMarkup: _inlineKeyboardGoToMainMenu, parseMode: ParseMode.Html);
                    break;                                                          
                    
                case TelegramBotStateEnum.MultiplicationMatrixShtrassenWatingMatrixB:
                    var bMatrix = new Matrix(message.Text);

                    if (((INullable) bMatrix).IsNull)
                    {
                        await _bot.SendTextMessageAsync(message.Chat.Id, "Ошибка! Неправильно задана матрица B", replyMarkup: _inlineKeyboardGoToMainMenu, parseMode: ParseMode.Html);
                    }
                    else
                    {
                        BMatrix = bMatrix;
                        var solve = Executor.OnGetStrassenAlgorithmSolve(AMatrix, BMatrix);

                        var resultstandart = $"<b>Ответ (обычное умножение)</b>:\n\n {AMatrix * BMatrix}";
                        
                        await _bot.SendTextMessageAsync(message.Chat.Id, solve + "\n\n" + resultstandart, replyMarkup: _inlineKeyboardGoToMainMenu, parseMode: ParseMode.Html);
                    }
                    
                    break;
                    
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            
        }

        private async void BotOnCallbackQueryReceivedSwitch(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            var message = callbackQueryEventArgs.CallbackQuery.Message;
            var data = callbackQueryEventArgs.CallbackQuery.Data;
            var username = message.Chat.Username;

            if (!UserLibrary.GetTelegramBotUser(username).Lock)
            {
                var error = @"Error! 

You are haven't access to this bot.
Please, write to creator for access.";
                
                await _bot.SendTextMessageAsync(message.Chat.Id, error);
                return;
            }
            
            switch (data)
            {
                case "DivisibilityCriteriaMenu":                   
                    await _bot.SendTextMessageAsync(message.Chat.Id, "Сделайте выбор", replyMarkup: _inlineKeyboardDivisibilityMenu);
                    UserLibrary.UpdateUserState(username, TelegramBotStateEnum.DivisibilityCriteriaMenu);                  
                    break;
                    
                case "AmericanFindDeterminant":
                    
                    //var aboutAmericanFindDeterminant = "Введите исходную матрицу!\n";
/*                    aboutAmericanFindDeterminant += "Пример:\n{";
                    aboutAmericanFindDeterminant += "   1 2 3 4";
                    aboutAmericanFindDeterminant += "   5 <a href=\"http://www.example.com""/>6 7</a> 8\n";
                    aboutAmericanFindDeterminant += "Пример:\n";
                    aboutAmericanFindDeterminant += "Пример:\n";
                    aboutAmericanFindDeterminant += "Пример:\n";*/
                    var aboutAmericanFindDeterminant = @"Введите исходную матрицу!

Пример:
{
   1 2 3 4
   5 <a href=""http://www.example.com"">6 7</a> 8
   9 <a href=""http://www.example.com"">8 7</a> 6
   5 4 3 2
}

Выделенные элементы  не должны быть равны нулю.
Определитель выделенных элементом также не должен быть равен нулю.

Если вам не повезло, то измените местами сроку или столбец так, чтобы нулей не было.
";
                    await _bot.SendTextMessageAsync(message.Chat.Id, aboutAmericanFindDeterminant, ParseMode.Html,
                        replyMarkup: _inlineKeyboardGoToMainMenu);
                    UserLibrary.UpdateUserState(username, TelegramBotStateEnum.AmericanFindDeterminant);
                    break;
                    
                case "MultiplicationNubmersKaratsuba":
                    const string aboutMultiplicationNubmersKaratsuba = @"
Введите числа для умножения!

Пример: 4088*2065
";
                    UserLibrary.UpdateUserState(username, TelegramBotStateEnum.MultiplicationNubmersKaratsuba);
                    await _bot.SendTextMessageAsync(message.Chat.Id, aboutMultiplicationNubmersKaratsuba, replyMarkup: _inlineKeyboardGoToMainMenu);  
                    break;
                    
                case "MultiplicationMatrixShtrassen":
                    const string aboutMultiplicationMatrixShtrassen = @"
Введите матрицу А!

Пример:
{
   1 2 3 4
   5 6 7 8
   9 8 7 6
   5 4 3 2
}
";
                    UserLibrary.UpdateUserState(username, TelegramBotStateEnum.MultiplicationMatrixShtrassen);
                    await _bot.SendTextMessageAsync(message.Chat.Id, aboutMultiplicationMatrixShtrassen, replyMarkup: _inlineKeyboardGoToMainMenu); 
                    break;
                    
                case "DivisibilityCriteriaPaskal":                
                    const string about = "Введите делимое и делитель! \nПример: 4088/73";
                   
                    UserLibrary.UpdateUserState(username, TelegramBotStateEnum.DivisibilityCriteriaPaskal);
                    await _bot.SendTextMessageAsync(message.Chat.Id, about, replyMarkup: _inlineKeyboardGoToMainMenu);                     
                    break;
                case "DivisibilityCriteriaRachinskiiFirst":
                    
                    const string aboutRachinskiiOne = "Введите делимое и делитель! \nПример: 4088/73";
                    
                    UserLibrary.UpdateUserState(username, TelegramBotStateEnum.DivisibilityCriteriaRachinskiiFirst);
                    await _bot.SendTextMessageAsync(message.Chat.Id, aboutRachinskiiOne, replyMarkup: _inlineKeyboardGoToMainMenu);
                    
                    break;
                    
                case "DivisibilityCriteriaRachinskiiSecond":
                    const string aboutRachinskiiSecond = "Введите делимое и делитель! \nПример: 4088/73";
                    
                    UserLibrary.UpdateUserState(username, TelegramBotStateEnum.DivisibilityCriteriaRachinskiiSecond);
                    await _bot.SendTextMessageAsync(message.Chat.Id, aboutRachinskiiSecond, replyMarkup: _inlineKeyboardGoToMainMenu);
                    break;
                case "DivisibilityCriteriaRachinskiiThird":
                    const string aboutRachinskiiThird = "Введите делимое и делитель! \nПример: 4088/73";
                    
                    UserLibrary.UpdateUserState(username, TelegramBotStateEnum.DivisibilityCriteriaRachinskiiThird);
                    await _bot.SendTextMessageAsync(message.Chat.Id, aboutRachinskiiThird,
                        replyMarkup: _inlineKeyboardGoToMainMenu);
                    
                    break;
                case "GoToMainMenu":
                    await _bot.SendTextMessageAsync(message.Chat.Id, "Сделайте выбор!", replyMarkup: _inlineKeyboardMainMenu);
                    UserLibrary.UpdateUserState(username, TelegramBotStateEnum.MainMenu);
                    break;
                default:
                    break;;
            }
        }
        private async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            var username = message.Chat.Username;
            
            if (!UserLibrary.ContainsUser(username))
            {
                var user = new TelegramBotUser()
                {
                    ChatId = message.Chat.Id,
                    UserName = username,
                    Lock = true,
                    CurrentState = TelegramBotStateEnum.MainMenu
                };
                
                State = new MainMenuState(this);
                UserLibrary.AddUser(user);
            }
            else
            {
                var userState = UserLibrary.GetTelegramBotUserState(username);
                SetState(userState);
            }
            
            Console.WriteLine(message.Chat.Id);
            State.Response(_bot, messageEventArgs);
        }

        private void SetState(TelegramBotStateEnum telegramBotStateEnum)
        {
        }

    }
}