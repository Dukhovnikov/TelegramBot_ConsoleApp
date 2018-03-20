namespace TelegramBot_ConsoleApp
{
    public class TelegramBotUser
    {
        public TelegramBotStateEnum CurrentState { get; set; }
        public long ChatId { get; set; }
        public string UserName { get; set; }     
        public bool Lock { get; set; }
    }
}