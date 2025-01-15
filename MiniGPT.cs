namespace ProjectChatbot
{
    public class MiniGPT
    {
        private Random rand = new Random();
        private string name;
        private string terminal;

        Datasets datasets = new Datasets();

        public MiniGPT(string botName)
        {
            name = botName;
            terminal = $"Windows@{name} (host):~$ ";
        }

        // Display introduction
        public void DisplayIntro()
        {
            int randomIndex = rand.Next(0, datasets.introductions.Length);
            Console.WriteLine(terminal + datasets.introductions[randomIndex]);
        }

        public void DisplayEnd()
        {
            int randomIndex = rand.Next(0, datasets.conclusions.Length);
            Console.WriteLine(terminal + datasets.conclusions[randomIndex]);
            Environment.Exit(0);
        }

        // Process user input
        public void ProcessInput()
        {
            bool loop = true;

            while (loop)
            {
                Console.Write($"Windows@{name} (user):~$ "); // Display user prompt
                string? input = Console.ReadLine(); // Read input from user

                if (!string.IsNullOrWhiteSpace(input))
                {
                    if (input.ToLower() == "exit")
                    {
                        loop = false;
                        DisplayEnd();
                        break;
                    }
                    else if (int.TryParse(input, out int numericValue))
                    {
                        Console.WriteLine($"{terminal} I see you entered a number: {numericValue}.");
                    }
                    else
                    {
                        Console.WriteLine($"{terminal} You said, '{input}'.");
                    }
                }
                else
                {
                    Console.WriteLine($"{terminal} It seems like you didn't say anything. How can I assist?");
                }
            }
        }
    }
}