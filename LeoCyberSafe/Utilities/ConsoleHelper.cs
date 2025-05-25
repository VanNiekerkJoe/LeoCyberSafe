using LeoCyberSafe.Core.Models;
using System;
using System.Text;
using System.Threading;

namespace LeoCyberSafe.Utilities
{
    public static class ConsoleHelper
    {
        // Part 1 Required Methods
        public static void DisplayAsciiArt()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
╔═════════════════════════════════════════╗
║                                         ║
║  ██████╗   ██████╗  ████████╗           ║
║  ██╔══██╗ ██╔═══██╗ ╚══██╔══╝           ║
║  ██████╔╝ ██║   ██║    ██║              ║
║  ██╔══██╗ ██║   ██║    ██║              ║
║  ██████╔╝ ╚██████╔╝    ██║              ║
║  ╚═════╝   ╚═════╝     ╚═╝              ║
║                                         ║
║   by Leo Van Niekerk                    ║
║                                         ║
╚═════════════════════════════════════════╝");
            Console.ResetColor();
            Thread.Sleep(1000);
        }

        public static string GetValidName()
        {
            string name;
            do
            {
                Console.Write("\nEnter your name: ");
                name = Console.ReadLine()?.Trim();
            } while (string.IsNullOrEmpty(name));
            return name;
        }



        public static string ReadPassword()
        {
            var password = new StringBuilder();
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                    break;

                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password.Remove(password.Length - 1, 1);
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    password.Append(key.KeyChar);
                    Console.Write("*");
                }
            }
            Console.WriteLine();
            return password.ToString();
        }

        // Enhanced Methods
        public static void DisplayMainMenu(string userName)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            // Update menu display
            Console.WriteLine(@"
╔════════════════════════════╗
║   LEO CYBERSAFE v2.1       ║
╠════════════════════════════╣
║ 1. 🔐 Password Audit       ║
║ 2. 🎣 Phishing Test        ║
║ 3. ⚠️  Threat Scan         ║
║ 4. 📚 Security Tips        ║
║ 5. 🛠️  Password Generator  ║
║ 6. 💬 Basic Response       ║
║ 7. 📝 Secure Notes         ║
║ 8. 🚪 Exit                 ║
║ 9. 🧠 Remember Interest    ║
║ 10.📖 Recall Interests     ║
╚════════════════════════════╝");

            Console.ResetColor();
            Console.WriteLine($"\nWelcome, {userName}!");
        }

        public static int GetMenuChoice()
        {
            Console.Write("\nSelect an option (1-4): ");
            if (int.TryParse(Console.ReadLine(), out int choice))
                return choice;
            return -1;
        }

        public static void DisplaySecurityLevel(int score)
        {
            ConsoleColor color = score switch
            {
                >= 80 => ConsoleColor.Green,
                >= 50 => ConsoleColor.Yellow,
                _ => ConsoleColor.Red
            };
            Console.ForegroundColor = color;
            Console.WriteLine($"Security Score: {score}/100");
            Console.ResetColor();
        }

        public static void DisplayScanHeader()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@"
╔════════════════════════════════════╗
║         THREAT SCAN REPORT         ║
╠════════════════════════════════════╣");
            Console.ResetColor();
        }

        public static void PrintReport(ThreatReport report)
        {
            DisplayScanHeader();
            Console.WriteLine($"\n🕒 Scan Time: {report.ScanTime:g}");
            Console.WriteLine($"📊 Threat Level: {report.Summary}");

            Console.WriteLine("\n🔍 Detected Threats:");
            foreach (var threat in report.Threats)
            {
                Console.ForegroundColor = threat.Severity switch
                {
                    SeverityLevel.High => ConsoleColor.Red,
                    SeverityLevel.Medium => ConsoleColor.Yellow,
                    _ => ConsoleColor.Gray
                };
                Console.WriteLine($"- {threat.Description}");
                Console.ResetColor();
            }
        }

        public static void PromptToContinue()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        public static void PrintExitMessage(string userName)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nGoodbye, {userName}! Stay secure online!\n");
            Console.ResetColor();
            Thread.Sleep(2000);
        }

            // Add to existing ConsoleHelper class
public static void DisplayTipsMenu()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(@"
╔════════════════════════════╗
║    CYBERSECURITY TIPS      ║
╠════════════════════════════╣
║ 1. Password Security       ║
║ 2. Phishing Prevention    ║
║ 3. General Best Practices ║
║ 4. Back to Main Menu       ║
╚════════════════════════════╝");
            Console.ResetColor();
        }

        public static void DisplayQAScreen()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(@"
╔════════════════════════════╗
║    CYBERSECURITY Q&A       ║
╠════════════════════════════╣
║ Ask me anything about:     ║
║ - Passwords                ║
║ - Phishing                 ║
║ - Malware                  ║
║ - VPNs                     ║
║ - And more!                ║
╚════════════════════════════╝");
            Console.ResetColor();
        }
    }
    }
