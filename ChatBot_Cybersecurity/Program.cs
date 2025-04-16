using System;
using System.Media;
using System.Threading;
using System.IO;
using System.Reflection;


namespace ChatBot_Cybersecurity
{
    class ChatBot
    {
        private static string userName = " "; //name variable is null in the beginning

        
        private static ConsoleColor defaultColor = ConsoleColor.White;
        private static ConsoleColor botColor = ConsoleColor.Cyan;
        private static ConsoleColor userColor = ConsoleColor.Green;
        private static ConsoleColor warningColor = ConsoleColor.Yellow;
        private static ConsoleColor errorColor = ConsoleColor.White;

        //sets exit boolean to false
        private static bool exitRequested = false;
        static void Main(string[] args)
        {
            Console.Title = "Cybersecurity Awareness Bot";

            //play welcome audio
            PlayWelcomeAudio();

            //Display ASCII art
            DisplayAsciiArt();

            //Get user name
            GetUserName();

            //Chatbot loop
            while (!exitRequested)
            {
                Console.WriteLine();
                TypeWrite("Which cybersecurity topic(s) can I help you with today?", botColor);
                Console.Write("> ");
                Console.ForegroundColor = userColor;
                string input = Console.ReadLine();
                Console.ForegroundColor = defaultColor;

                ProcessInput(input);
            }

            // Farewell
            TypeWrite($"Goodbye, {userName}! Stay safe online!", botColor);
            Thread.Sleep(2000);//puts a bit of pause/delay for smooth convo with user
    
    
        }
        private static void PlayWelcomeAudio()
        {
            try
            {//retrieve audio wav file
                string path = "WAVChatbotWelcomeAudio.wav";//Path.Combine(Directory.GetCurrentDirectory(), "Resources", "WAVChatbotWelcomeAudio.wav");
                if (File.Exists(path))  //play sound when audio is found
                {
                    using (SoundPlayer player = new SoundPlayer(path))
                    {
                        player.PlaySync();
                        // Wait for the greeting to finish playing
                        //Thread.Sleep(3000);
                    }
                }
                else //continue program even when audio can't play
                {
                    Console.ForegroundColor = warningColor;
                    Console.WriteLine("Welcome sound file not found. Continuing without audio...");
                    Console.ForegroundColor = defaultColor;
                }
            }
            catch (Exception ex) //continue program even when audio can't play
            {
                Console.ForegroundColor = errorColor;
                Console.WriteLine($"Error playing welcome sound: {ex.Message}");
                Console.ForegroundColor = defaultColor;
            }
        }
        private static void DisplayAsciiArt() //display ascii art logo
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(@"
  ____      _                                        _ _            
 / ___|   _| |__   ___ _ __ ___  ___  ___ _   _ _ __(_) |_ _   _    
| |  | | | | '_ \ / _ \ '__/ __|/ _ \/ __| | | | '__| | __| | | |   
| |__| |_| | |_) |  __/ |  \__ \  __/ (__| |_| | |  | | |_| |_| |   
 \____\__, |_.__/ \___|_|  |___/\___|\___|\__,_|_|  |_|\__|\__, |   
   / \|___/   ____ _ _ __ ___ _ __   ___  ___ ___  | __ )  |___/ |_ 
  / _ \ \ /\ / / _` | '__/ _ \ '_ \ / _ \/ __/ __| |  _ \ / _ \| __|
 / ___ \ V  V / (_| | | |  __/ | | |  __/\__ \__ \ | |_) | (_) | |_ 
/_/   \_\_/\_/ \__,_|_|  \___|_| |_|\___||___/___/ |____/ \___/ \__|
    
 ");
            Console.ForegroundColor = defaultColor;
            Console.WriteLine("\n" + new string('=', 80) + "\n");
        }
        private static void GetUserName() //prompt user input for name
        {
            while (string.IsNullOrWhiteSpace(userName))
            {
                TypeWrite("Hello! Before we begin, what's your name?", botColor); //ask user for name
                Console.Write("> ");
                Console.ForegroundColor = userColor;
                userName = Console.ReadLine(); //use the name to personalise response
                Console.ForegroundColor = defaultColor;

                if (string.IsNullOrWhiteSpace(userName)) //blank username input
                {
                    Console.ForegroundColor = warningColor;
                    TypeWrite("I didn't catch your name. Could you please tell me your name?", botColor);
                    Console.ForegroundColor = defaultColor;
                }
                else
                {
                    Console.WriteLine();  
                    TypeWrite($"Nice to meet you, {userName}! I'm your Cybersecurity Awareness ChatBot.", botColor);
                    TypeWrite("I can help you with topics like PASSWORD SAFETY, PHISHING, and SAFE BROWSING.", botColor);
                    TypeWrite("Reply with 'password' / 'scam' / 'browsing'", botColor);
                    Console.WriteLine(new string('-', 60));
                }
            }
        }
          
                private static void ProcessInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) //invalid user input
            {
                TypeWrite("I didn't quite understand that. Could you rephrase?", botColor);
                return;
            }

            string lowerInput = input.ToLower(); //make user input case insensitive

            //user greets, reply
            if (lowerInput.Contains("hello") || lowerInput.Contains("hi") || lowerInput.Contains("hey"))
            {
                TypeWrite($"Hello again, {userName}! How can I help you today?", botColor);
            }
            
            else if (lowerInput.Contains("how are you"))
            {
                TypeWrite("I'm just a bot, but I'm working perfectly! Ready to help you with cybersecurity.", botColor);
            }
            //user asks for purpose
            else if (lowerInput.Contains("purpose") || lowerInput.Contains("what do you do"))
            {
                TypeWrite("My purpose is to help you stay safe online by providing cybersecurity awareness.", botColor);
                TypeWrite("I can explain common threats and best practices for staying secure.", botColor);
            }
           //user asks about password
            else if (lowerInput.Contains("password"))
            {
                DisplayPasswordSafetyAnswers();
            }
            //user asks about phishing
            else if (lowerInput.Contains("phishing") || lowerInput.Contains("scam")||lowerInput.Contains("scams"))
            {
                DisplayPhishingAnswers();
            }
            //user asks about internet safety
            else if (lowerInput.Contains("browsing") || lowerInput.Contains("safe"))
            {
                DisplaySafeBrowsingInfo();
            }
            //user exits program
            else if (lowerInput.Contains("exit") || lowerInput.Contains("quit") || lowerInput.Contains("bye"))
            {
                exitRequested = true; //sets exit boolean to true
            }
            
            else //user input invalid
            {
                TypeWrite("I didn't quite understand that. I can help with:", botColor);
                TypeWrite("- PASSWORD SAFETY", botColor);
                TypeWrite("- PHISHING SCAMS", botColor);
                TypeWrite("- SAFE BROWSING PRACTICES", botColor);
                TypeWrite("Try asking about one of these topics.", botColor);
            }
        }

        private static void DisplayPasswordSafetyAnswers() //dispaly answers for password safety
        {
            Console.ForegroundColor = botColor;
            Console.WriteLine("\n" + new string('=', 40) + " PASSWORD SAFETY " + new string('=', 40) + "\n");

            TypeWrite("Creating strong passwords is crucial for online security. Here are some tips:", botColor);
            Console.WriteLine();

            TypeWrite("1. Use long passwords (at least 10 characters)", botColor);
            TypeWrite("2. Include uppercase, lowercase, numbers, and special characters", botColor);
            TypeWrite("3. Don't use personal information like birthdays or names", botColor);
            TypeWrite("4. Use a unique password for each account", botColor);
            TypeWrite("5. Consider using a password manager to keep track", botColor);
            TypeWrite("6. Enable two-factor authentication where available", botColor);

            Console.WriteLine("\n" + new string('-', 60));
            TypeWrite("Example of a strong password: 'Blue42$ky!Turtle@9'", botColor);
            TypeWrite("Example of a weak password: 'password0000'", warningColor);

            Console.WriteLine("\n" + new string('=', 97) + "\n");
            Console.ForegroundColor = defaultColor;
        }

        private static void DisplayPhishingAnswers() //dispaly answers for phishing
        {
            Console.ForegroundColor = botColor;
            Console.WriteLine("\n" + new string('=', 38) + " PHISHING PROTECTION " + new string('=', 38) + "\n");

            TypeWrite("Phishing is when attackers try to trick you into giving sensitive information.", botColor);
            Console.WriteLine();

            TypeWrite("How to spot phishing attempts:", botColor);
            TypeWrite("1. Urgent or threatening language demanding immediate action", botColor);
            TypeWrite("2. Requests for personal information via email or message", botColor);
            TypeWrite("3. Suspicious sender addresses (hover to check before clicking)", botColor);
            TypeWrite("4. Poor spelling and grammar", botColor);
            TypeWrite("5. Links that don't match the supposed sender (hover to check)", botColor);
            TypeWrite("6. Unexpected attachments", botColor);

            Console.WriteLine("\n" + new string('-', 60));
            TypeWrite("Always verify requests through official channels before responding.", botColor);
            TypeWrite("When in doubt, don't click! Contact the organisation directly.", warningColor);

            Console.WriteLine("\n" + new string('=', 97) + "\n");
            Console.ForegroundColor = defaultColor;
        }

        private static void DisplaySafeBrowsingInfo() //dispaly answers for internet browsing
        {
            Console.ForegroundColor = botColor;
            Console.WriteLine("\n" + new string('=', 37) + " SAFE BROWSING PRACTICES " + new string('=', 37) + "\n");

            TypeWrite("Staying safe while browsing the internet:", botColor);
            Console.WriteLine();

            TypeWrite("1. Look for 'https://' and the padlock icon in your browser", botColor);
            TypeWrite("2. Keep your browser and operating system updated", botColor);
            TypeWrite("3. Use reputable antivirus and anti-malware software", botColor);
            TypeWrite("4. Be cautious with downloads - only from trusted sources", botColor);
            TypeWrite("5. Use a VPN on public Wi-Fi networks", botColor);
            TypeWrite("6. Regularly clear cookies and cache", botColor);
            TypeWrite("7. Be careful of 'too good to be true' offers", botColor);

            Console.WriteLine("\n" + new string('-', 60));
            TypeWrite("Remember: If something feels suspicious, it probably is!", warningColor);

            Console.WriteLine("\n" + new string('=', 97) + "\n");
            Console.ForegroundColor = defaultColor;
        }

        private static void TypeWrite(string message, ConsoleColor color)  //typing effect: display message letter by letter
        {
            Console.ForegroundColor = color;
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(20); //pauses after message is displayed
            }
            Console.WriteLine();
            Console.ForegroundColor = defaultColor;
        }
    }
}
       