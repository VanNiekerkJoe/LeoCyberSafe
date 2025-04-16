using LeoCyberSafe.Core;
using LeoCyberSafe.Features.Password;
using LeoCyberSafe.Features.Phishing;
using LeoCyberSafe.Features.Questions;
using LeoCyberSafe.Features.SecureNotes;
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

            // Initialize Secure Notes with master password
            Console.Write("\nSet master password for secure notes: ");
            NoteService.Initialize(Console.ReadLine()!);

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

                    case 5: // Password Generator
                        Console.WriteLine("\nGenerated Password: " +
                            PasswordGeneratorService.GeneratePassword());
                        ConsoleHelper.PromptToContinue();
                        break;

                    case 6: // Secure Notes



                        Console.Write("\nEnter your master password: ");
                        var inputPassword = ConsoleHelper.ReadPassword(); // Masked input

                        if (!NoteService.VerifyPassword(inputPassword))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid password!");
                            Console.ResetColor();
                            ConsoleHelper.PromptToContinue();
                            break;
                        }
                        bool inNotesMode = true;
                        while (inNotesMode)
                        {
                            Console.Clear();
                            Console.WriteLine("\nSecure Notes System");
                            Console.WriteLine("1. Add Note\n2. View Notes\n3. Back");
                            var noteChoice = Console.ReadLine();

                            if (noteChoice == "1")
                            {
                                Console.Write("Title: ");
                                var title = Console.ReadLine();
                                Console.Write("Content: ");
                                var content = Console.ReadLine();
                                NoteService.AddNote(title!, content!);
                                Console.WriteLine("Note saved securely!");
                                ConsoleHelper.PromptToContinue();
                            }
                            else if (noteChoice == "2")
                            {
                                var notes = NoteService.GetNotes();
                                if (notes.Count == 0)
                                {
                                    Console.WriteLine("No notes found.");
                                }
                                else
                                {
                                    foreach (var note in notes)
                                    {
                                        Console.WriteLine($"\n[{note.Title}]\n{note.Content}");
                                    }
                                }
                                ConsoleHelper.PromptToContinue();
                            }
                            else if (noteChoice == "3")
                            {
                                inNotesMode = false;
                            }
                        }
                        break;

                    case 7: // Exit
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

                if (!exitRequested && choice != 5 && choice != 6)
                {
                    ConsoleHelper.PromptToContinue();
                }
            }
        }
    }
}