using LeoCyberSafe.Core;
using LeoCyberSafe.Features.Password;
using LeoCyberSafe.Features.Phishing;
using LeoCyberSafe.Features.Questions;
using LeoCyberSafe.Features.Tips;
using LeoCyberSafe.Utilities;
using System;

namespace LeoCyberSafe
{
    class Program
    {
        static void Main()
        {
            // Initialize services
            var passwordService = new PasswordAuditService();
            var phishingService = new PhishingSimulator();
            var threatScanner = new ThreatScanner();
            var tipsService = new CybersecurityTipsService();
            var questionService = new QuestionService();

            // Part 1 Requirements
            AudioHelper.PlayWelcomeSound();
            ConsoleHelper.DisplayAsciiArt();
            string userName = ConsoleHelper.GetValidName();

            bool exitRequested = false;
            while (!exitRequested)
            {
                ConsoleHelper.DisplayMainMenu(userName);
                tipsService.DisplayRandomTip();

                int choice = ConsoleHelper.GetMenuChoice();
                switch (choice)
                {
                    case 1: // Password Audit
                        Console.Write("\nEnter password to analyze: ");
                        string password = Console.ReadLine() ?? string.Empty;
                        var (score, feedback) = passwordService.Analyze(password);
                        Console.WriteLine($"\n{feedback}");
                        ConsoleHelper.DisplaySecurityLevel(score);
                        tipsService.DisplayTipsByCategory("passwords");
                        break;

                    case 2: // Phishing Test
                        phishingService.StartSimulation(userName);
                        tipsService.DisplayTipsByCategory("phishing");
                        break;

                    case 3: // Threat Scan
                        var report = threatScanner.GenerateReport();
                        ConsoleHelper.PrintReport(report);
                        break;

                    case 4: // Security Tips
                        Console.Write("\nEnter category (passwords/phishing/general): ");
                        string category = Console.ReadLine() ?? string.Empty;
                        tipsService.DisplayTipsByCategory(category);
                        break;

                    case 5: // Cybersecurity Q&A
                        bool inQAMode = true;
                        while (inQAMode)
                        {
                            Console.Clear();
                            ConsoleHelper.DisplayQAScreen();
                            questionService.DisplayAvailableTopics();

                            Console.Write("\nAsk a question (or type 'back' to return): ");
                            string question = Console.ReadLine()?.Trim() ?? string.Empty;

                            if (string.Equals(question, "back", StringComparison.OrdinalIgnoreCase))
                            {
                                inQAMode = false;
                                continue;
                            }

                            if (string.Equals(question, "topics", StringComparison.OrdinalIgnoreCase))
                            {
                                questionService.DisplayAvailableTopics();
                                ConsoleHelper.PromptToContinue();
                                continue;
                            }

                            string answer = questionService.GetAnswer(question);
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"\n{answer}");
                            Console.ResetColor();

                            ConsoleHelper.PromptToContinue();
                        }

                        break;

                    case 6: // Exit
                        exitRequested = true;
                        ConsoleHelper.PrintExitMessage(userName);
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid option. Please try again.");
                        Console.ResetColor();
                        Thread.Sleep(1000);
                        break;
                }

                if (!exitRequested && choice != 5)
                {
                    ConsoleHelper.PromptToContinue();
                }
            }
        }
    }
}