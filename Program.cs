namespace ProjectChatbot
{
    class Program
    {
        static void Main(string[] args)
        {
            MiniGPT bot = new MiniGPT("MiniGPT");
            bot.DisplayIntro();
            while (true)
            {
                bot.ProcessInput();
            }
        }
    }
}
