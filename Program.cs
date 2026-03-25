using System;

namespace CybersecurityChatbot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Cybersecurity Awareness Bot";

            Chatbot bot = new Chatbot();
            bot.Start();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
