using System;
using System.IO;
using System.Media;
using System.Threading;

namespace CybersecurityChatbot
{
    public class Chatbot
    {
        // Properties
        public string UserName { get; private set; } = string.Empty;
        private bool IsRunning { get; set; }

        // Constructor
        public Chatbot()
        {
            IsRunning = true;
        }

        // Public method to start the bot
        public void Start()
        {
            PlayVoiceGreeting(); //Added voice greeting funcionality
            DisplayAsciiArt();
            UserName = GetUserName();
            DisplayWelcomeMessage();
            ShowHelp();
            Run();
        }

        // Task 1:Plays voice greeting on startup
        private void PlayVoiceGreeting()
        {
            try
            {
                string audioPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "greeting.wav");

                if (File.Exists(audioPath))
                {
                    SoundPlayer player = new SoundPlayer(audioPath);
                    player.PlaySync();
                }
                else
                {
                    Console.WriteLine("[INFO] Audio file not found. Continuing without sound.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Note: Audio playback not available ({ex.Message})");
            }
        }

        // Task 2: ASCII Art Logo with Teal color
        private void DisplayAsciiArt()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            string art = @"  ░██████             ░██                            ░████████                 ░██    
 ░██   ░██            ░██                            ░██    ░██                ░██    
░██        ░██    ░██ ░████████   ░███████  ░██░████ ░██    ░██   ░███████  ░████████ 
░██        ░██    ░██ ░██    ░██ ░██    ░██ ░███     ░████████   ░██    ░██    ░██    
░██        ░██    ░██ ░██    ░██ ░█████████ ░██      ░██     ░██ ░██    ░██    ░██    
 ░██   ░██ ░██   ░███ ░███   ░██ ░██        ░██      ░██     ░██ ░██    ░██    ░██    
  ░██████   ░█████░██ ░██░█████   ░███████  ░██      ░█████████   ░███████      ░████ 
                  ░██                                                                 
            ░███████                                                                  
                                                                                      
                        CYBERSECURITY AWARENESS BOT                                    ";
            Console.WriteLine(art);
            Console.ResetColor();
        }

        // Task 3: Get user name with validation (Purple/Violet accents)
        private string GetUserName()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("\n┌─[ Please enter your name ]\n└─> ");
            Console.ResetColor();

            string? name = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(name))
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("╔═[ Name cannot be empty ]\n╚═> ");
                Console.ResetColor();
                name = Console.ReadLine();
            }

            return name;
        }

        // Task 3: Welcome message with Sky Blue
        private void DisplayWelcomeMessage()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\n╔══════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║  HELLO, {UserName.ToUpper()}!                                                               ║");
            Console.WriteLine($"║                                                                              ║");
            Console.WriteLine($"║  Welcome to CYBERBOT - Cybersecurity Awareness Assistant                    ║");
            Console.WriteLine($"║  Let's learn how to stay safe online together.                              ║");
            Console.WriteLine($"║                                                                              ║");
            Console.WriteLine($"╚══════════════════════════════════════════════════════════════════════════════╝");
            Console.ResetColor();
        }

        // Main conversation loop
        private void Run()
        {
            while (IsRunning)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"\n┌─[{UserName}]\n└─> ");
                Console.ResetColor();

                string? userInput = Console.ReadLine();

                // Exit condition
                if (userInput?.ToLower() == "exit" || userInput?.ToLower() == "quit")
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"\n╔══════════════════════════════════════════════════════════════════════════════╗");
                    Console.WriteLine($"║  GOODBYE, {UserName.ToUpper()}!                                                           ║");
                    Console.WriteLine($"║                                                                              ║");
                    Console.WriteLine($"║  Remember: Stay safe, stay secure.                                          ║");
                    Console.WriteLine($"║                                                                              ║");
                    Console.WriteLine($"╚══════════════════════════════════════════════════════════════════════════════╝");
                    Console.ResetColor();
                    IsRunning = false;
                    break;
                }

                string response = GetResponse(userInput ?? string.Empty);
                TypeWriterEffect(response);
            }
        }

        // Task 4 & 5: Response system with validation
        private string GetResponse(string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput))
            {
                return "I didn't catch that. Could you please say something?";
            }

            string lowerInput = userInput.ToLower().Trim();

            // Greeting responses
            if (lowerInput.Contains("how are you") || lowerInput.Contains("how are u"))
            {
                return "I'm functioning well, thanks for asking. How can I help you with cybersecurity today?";
            }

            // Purpose responses
            if (lowerInput.Contains("purpose") || lowerInput.Contains("what do you do") || lowerInput.Contains("what is your purpose"))
            {
                return "My purpose is to educate and assist you with cybersecurity awareness. I can help with topics like password safety, phishing, and safe browsing.";
            }

            // What can I ask about
            if (lowerInput.Contains("what can i ask") || lowerInput.Contains("what topics") || lowerInput.Contains("help"))
            {
                return "Here's what I can help you with:\n\n  'password' - Get password safety tips\n  'phishing' - Learn to spot scams\n  'safe browsing' - Browse the web safely\n  'tip' - Random cybersecurity advice\n  'joke' - A cybersecurity joke\n  'fun fact' - Interesting cybersecurity facts\n\nType a topic to get started.";
            }

            // Joke response
            if (lowerInput.Contains("joke"))
            {
                string[] jokes = {
                    "Why did the hacker go to the bank? To get his account... hacked.",
                    "What's a computer's favorite beat? An encryption.",
                    "Why was the computer cold? It left its Windows open.",
                    "What do you call a hacker who flies planes? A cyber-pilot."
                };
                Random random = new Random();
                return jokes[random.Next(jokes.Length)];
            }

            // Fun fact response
            if (lowerInput.Contains("fun fact") || lowerInput.Contains("fact"))
            {
                string[] facts = {
                    "Did you know? The first computer virus was created in 1983 and was called 'Elk Cloner'.",
                    "Fact: '123456' is still the most commonly used password worldwide.",
                    "Did you know? 95% of cybersecurity breaches are caused by human error.",
                    "Fact: The world's first hacker was a woman named Susan Headley in the 1970s."
                };
                Random random = new Random();
                return facts[random.Next(facts.Length)];
            }

            // Password safety
            if (lowerInput.Contains("password"))
            {
                return "PASSWORD SAFETY TIPS:\n\n  - Use at least 12 characters\n  - Mix uppercase, lowercase, numbers, and symbols\n  - Never reuse passwords across accounts\n  - Consider using a password manager\n  - Enable two-factor authentication (2FA) whenever possible\n  - Avoid using personal information like birthdays or names";
            }

            // Phishing
            if (lowerInput.Contains("phishing"))
            {
                return "PHISHING AWARENESS:\n\n  - Never click suspicious links in emails or text messages\n  - Do not share personal information via email\n  - Check sender's email address carefully\n  - Legitimate companies never ask for passwords via email\n  - When in doubt, contact the company directly through their official website\n  - Look for spelling errors, which are common in phishing attempts";
            }

            // Safe browsing
            if (lowerInput.Contains("safe browsing") || lowerInput.Contains("browsing") || lowerInput.Contains("browser") || lowerInput.Contains("web"))
            {
                return "SAFE BROWSING TIPS:\n\n  - Look for 'https://' and the padlock icon in the URL\n  - Avoid using public Wi-Fi for banking or sensitive transactions\n  - Keep your browser updated to the latest version\n  - Clear cookies and cache regularly\n  - Do not save passwords in your browser";
            }

            // General tip
            if (lowerInput.Contains("tip") || lowerInput.Contains("advice") || lowerInput.Contains("tell me something"))
            {
                string[] tips = {
                    "Enable two-factor authentication (2FA) on all accounts that offer it.",
                    "Keep your software and operating systems updated - updates fix security vulnerabilities.",
                    "Backup your important files to an external drive or cloud storage regularly.",
                    "Be cautious of unsolicited attachments in emails, even from people you know.",
                    "Use a VPN when connecting to public Wi-Fi networks.",
                    "Create unique passwords for every account and avoid reusing them.",
                    "Review your social media privacy settings regularly.",
                    "Change your passwords regularly and never share them with anyone.",
                    "If an offer sounds too good to be true online, it is likely a scam."
                };
                Random random = new Random();
                return $"CYBERSECURITY TIP:\n\n  {tips[random.Next(tips.Length)]}";
            }

            // Default response
            return "I didn't quite understand that. Could you rephrase?\n\n  Try asking about:\n  - 'password' for safety tips\n  - 'phishing' to spot scams\n  - 'safe browsing' for web safety\n  - 'tip' for cybersecurity advice\n  - 'joke' for a laugh\n  - 'fun fact' to learn something new\n  - 'help' to see all options";
        }

        // Task 6: Typing effect with Purple accent
        private void TypeWriterEffect(string message)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("\n╔══[ CYBERBOT ]══╗\n║ ");

            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(12);
            }

            Console.WriteLine("\n╚═══════════════╝");
            Console.ResetColor();
        }

        // Task 6: Help menu with all three colors
        private void ShowHelp()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n╔══════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║  CYBERBOT HELP MENU                                                           ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════════════════════════════╣");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("║                                                                              ║");
            Console.WriteLine("║  'password'      - Get password safety tips                                 ║");
            Console.WriteLine("║  'phishing'      - Learn to spot phishing scams                             ║");
            Console.WriteLine("║  'safe browsing' - Browse the web safely                                    ║");
            Console.WriteLine("║  'tip'           - Get random cybersecurity advice                          ║");
            Console.WriteLine("║  'joke'          - Hear a cybersecurity joke                                ║");
            Console.WriteLine("║  'fun fact'      - Learn interesting cybersecurity facts                    ║");
            Console.WriteLine("║  'help'          - Show this menu again                                     ║");
            Console.WriteLine("║  'exit'          - End the conversation                                     ║");
            Console.WriteLine("║                                                                              ║");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════════════╝");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nTIP: Type any of the commands above to interact with CyberBot.");
            Console.ResetColor();
        }
    }
}