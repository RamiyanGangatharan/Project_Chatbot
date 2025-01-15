using System;
using System.IO;
using System.Text.Json;
using System.Threading;

namespace ProjectChatbot
{
    public class MiniGPT
    {
        private Random rand = new Random();
        private string name;
        private string terminal;
        private Datasets datasets;

        public MiniGPT(string botName)
        {
            name = botName;
            terminal = $"Windows@{name} (host):~$";
            PrintWelcomeMessage();
            SimulateLoading();
            Console.Clear();  
        }

        private void PrintWelcomeMessage()
        {
            Console.WriteLine(" __       __  ______  __    __  ______         ______   _______   ________ ");
            Console.WriteLine("/  \\     /  |/      |/  \\  /  |/      |       /      \\ /       \\ /        |");
            Console.WriteLine("$$  \\   /$$ |$$$$$$/ $$  \\ $$ |$$$$$$/       /$$$$$$  |$$$$$$$  |$$$$$$$$/ ");
            Console.WriteLine("$$$  \\ /$$$ |  $$ |  $$$  \\$$ |  $$ | ______ $$ | _$$/ $$ |__$$ |   $$ |   ");
            Console.WriteLine("$$$$  /$$$$ |  $$ |  $$$$  $$ |  $$ |/      |$$ |/    |$$    $$/    $$ |   ");
            Console.WriteLine("$$ $$ $$/$$ |  $$ |  $$ $$ $$ |  $$ |$$$$$$/ $$ |$$$$ |$$$$$$$/     $$ |   ");
            Console.WriteLine("$$ |$$$/ $$ | _$$ |_ $$ |$$$$ | _$$ |_       $$ \\__$$ |$$ |         $$ |   ");
            Console.WriteLine("$$ | $/  $$ |/ $$   |$$ | $$$ |/ $$   |      $$    $$/ $$ |         $$ |   ");
            Console.WriteLine("$$/      $$/ $$$$$$/ $$/   $$/ $$$$$$/        $$$$$$/  $$/          $$/    ");
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine();
        }

        private void SimulateLoading()
        {
            Console.WriteLine("Starting up...");
            Thread.Sleep(1000);
            Console.WriteLine("Initializing components...");
            Thread.Sleep(1000);
            Console.Write("Loading");
            for (int i = 0; i < 3; i++)
            {
                PrintDots();
                RemoveDots();
            }
            Console.WriteLine();
            LoadDatasets();

            Console.WriteLine("\nSystem checks passed.");
            Thread.Sleep(500);
            Console.WriteLine("All systems operational.\n");
        }

        private void PrintDots()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.Write(".");
                Thread.Sleep(500); // Pause for 500 milliseconds
            }
        }

        private void RemoveDots()
        {
            Console.SetCursorPosition(Console.CursorLeft - 3, Console.CursorTop); // Move cursor back 3 positions
            Console.Write("   "); // Overwrite with spaces
            Console.SetCursorPosition(Console.CursorLeft - 3, Console.CursorTop); // Move cursor back 3 positions again
        }

        // Load datasets from JSON file
        private void LoadDatasets()
        {
            try
            {
                Console.WriteLine("Loading datasets from JSON file...");
                string json = File.ReadAllText("../../../datasets.json");
                datasets = JsonSerializer.Deserialize<Datasets>(json)
                    ?? throw new InvalidOperationException("Deserialization failed: datasets are null.");

                Thread.Sleep(1000); // Simulate time taken to load
                Console.WriteLine("Datasets loaded successfully!\n");

                if (datasets == null || datasets.introductions == null || datasets.conclusions == null)
                {
                    Console.WriteLine("Error: Datasets could not be loaded. Please check the JSON file.");
                    Environment.Exit(1); // Exit if datasets can't be loaded
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading datasets: {ex.Message}");
                Environment.Exit(1); // Exit if there's an error reading the file
            }
        }

        // Display introduction
        public void DisplayIntro()
        {
            int randomIndex = rand.Next(datasets.introductions.Length);
            Console.WriteLine(terminal + datasets.introductions[randomIndex]);
        }

        public void DisplayEnd()
        {
            int randomIndex = rand.Next(datasets.conclusions.Length);
            Console.WriteLine(terminal + datasets.conclusions[randomIndex]);
            Environment.Exit(0);
        }

        // Process user input
        public void ProcessInput()
        {
            while (true)
            {
                Console.Write($"Windows@{name} (user):~$ ");
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine($"{terminal} It seems like you didn't say anything. How can I assist?");
                    continue;
                }

                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    DisplayEnd();
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
        }
    }
}
