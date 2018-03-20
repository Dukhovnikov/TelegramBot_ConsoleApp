using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Telegram.Bot.Types;
using File = System.IO.File;


namespace TelegramBot_ConsoleApp
{
    public class TelegramBotData
    {
        //private static string FilePath { get; set; } =
            //Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\TelegramBotDataUsers.txt";

        private static string FilePath { get; set; } = "D:\\db\\TelegramBotDataUsers.txt";
        
        private Dictionary<string, TelegramBotUser> TelegramBotDataUsers { get; set; }
        
        public string Logger { get; set; } = string.Empty;
        
        
        public TelegramBotData()
        {
            if (File.Exists(FilePath))
            {
                try
                {
                    var textFile = File.ReadAllText(FilePath);
                    TelegramBotDataUsers = JsonConvert.DeserializeObject<Dictionary<string, TelegramBotUser>>(textFile);
                }
                catch (Exception e)
                {
                    throw new Exception("Неверно задан JSON-файл. Не удалось считать!");
                }
            }
            else
            {
                TelegramBotDataUsers = new Dictionary<string, TelegramBotUser>();
                var telegramBotDataUsersJson = JsonConvert.SerializeObject(TelegramBotDataUsers, Formatting.Indented);

                File.WriteAllText(FilePath, telegramBotDataUsersJson);
            }
        }

        public bool ContainsUser(string username)
        {
            try
            {
                if (username == null)
                {
                    return false;
                }
                
                return TelegramBotDataUsers.ContainsKey(username);
            }
            catch (Exception e)
            {
                return false;
            }

        }
        
        public void AddUser(TelegramBotUser telegramBotUser)
        {
            TelegramBotDataUsers.Add(telegramBotUser.UserName, telegramBotUser);
            
            var telegramBotDataUsersJson = JsonConvert.SerializeObject(TelegramBotDataUsers, Formatting.Indented);
            File.WriteAllText(FilePath, telegramBotDataUsersJson);
        }

        public void ChangeAccessUser(string username, bool access)
        {
            TelegramBotDataUsers[username].Lock = access;
            
            var telegramBotDataUsersJson = JsonConvert.SerializeObject(TelegramBotDataUsers, Formatting.Indented);
            File.WriteAllText(FilePath, telegramBotDataUsersJson);
        }

        public void UpdateUserState(string username, TelegramBotStateEnum state)
        {
            TelegramBotDataUsers[username].CurrentState = state;
            
            var telegramBotDataUsersJson = JsonConvert.SerializeObject(TelegramBotDataUsers, Formatting.Indented);
            File.WriteAllText(FilePath, telegramBotDataUsersJson);
        }

        public TelegramBotUser GetTelegramBotUser(string userName)
        {
            try
            {
                return TelegramBotDataUsers[userName];
            }
            catch (Exception e)
            {
                Logger += "function: public TelegramBotUser GetTelegramBotUser(string userName). Incorrect removed";
                return null;
            }
        }
        
        public TelegramBotStateEnum GetTelegramBotUserState(string userName)
        {
            try
            {
                var user = TelegramBotDataUsers[userName];

                return user.CurrentState;
            }
            catch (Exception e)
            {
                Logger += "function: public TelegramBotUser GetTelegramBotUser(string userName). Incorrect removed";
                throw new Exception("Ошибка при возвращении состояния из словаря");
            }
        }

        public void Remove(string userName)
        {
            try
            {
                TelegramBotDataUsers.Remove(userName);
            }
            catch (Exception e)
            {
                Logger += "function: public void Remove(string userName). Incorrect removed";
            }
        }        
        
        public void Remove(TelegramBotUser telegramBotUser)
        {
            try
            {
                TelegramBotDataUsers.Remove(telegramBotUser.UserName);
            }
            catch (Exception e)
            {
                Logger += "function: public void Remove(TelegramBotUser telegramBotUser). Incorrect removed";
            }
        }
                                
    }
}