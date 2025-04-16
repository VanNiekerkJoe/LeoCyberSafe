using LeoCyberSafe.Core;
using LeoCyberSafe.Features.Password;
using LeoCyberSafe.Features.Phishing;
using LeoCyberSafe.Features.Response;
using LeoCyberSafe.Features.SecureNotes;
using LeoCyberSafe.Features.Tips;
using LeoCyberSafe.Utilities;
using System;
using System.Threading;

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

            // Part 1 Requirements
            AudioHelper.PlayWelcomeSound();
            ConsoleHelper.DisplayAsciiArt();
            string userName = ConsoleHelper.GetValidName();

            // Initialize Secure Notes
            Console.Write("\nSet master password for secure notes: ");
            NoteService.Initialize(ConsoleHelper.ReadPassword());

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
                        string password = ConsoleHelper.ReadPassword();
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
                        string category = Console.ReadLine()?.ToLower() ?? string.Empty;
                        tipsService.DisplayTipsByCategory(category);
                        break;

                    case 5: // Password Generator
                        Console.WriteLine("\nGenerated Passwords:");
                        Console.WriteLine($"1. {PasswordGeneratorService.GeneratePassword()}");
                        Console.WriteLine($"2. {PasswordGeneratorService.GeneratePassword(12, includeSpecial: false)}");
                        Console.WriteLine($"3. {PasswordGeneratorService.GeneratePassword(20)}");
                        ConsoleHelper.PromptToContinue();
                        break;

                    case 6: // Basic Response System
                        RunResponseSystem();
                        break;

                    case 7: // Secure Notes
                        RunSecureNotesSystem();
                        break;

                    case 8: // Exit
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

                if (!exitRequested && choice != 5 && choice != 6 && choice != 7)
                {
                    ConsoleHelper.PromptToContinue();
                }
            }
        }

        private static void RunResponseSystem()
        {
            Console.WriteLine("\nBasic Response System (type 'help' for options, 'exit' to quit)");
            bool inResponseMode = true;
            while (inResponseMode)
            {
                Console.Write("\nYou: ");
                var input = Console.ReadLine();

                if (input?.ToLower() == "exit")
                {
                    inResponseMode = false;
                }
                else if (input?.ToLower() == "help")
                {
                    ResponseService.DisplayHelp();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Bot: {ResponseService.GetResponse(input)}");
                    Console.ResetColor();
                }
            }
        }

        private static void RunSecureNotesSystem()
        {
            Console.Write("\nEnter master password: ");
            if (!NoteService.VerifyPassword(ConsoleHelper.ReadPassword()))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid password!");
                Console.ResetColor();
                ConsoleHelper.PromptToContinue();
                return;
            }

            bool inNotesMode = true;
            while (inNotesMode)
            {
                Console.Clear();
                Console.WriteLine("\nSecure Notes System");
                Console.WriteLine("1. Add Note\n2. View Notes\n3. Change Password\n4. Back");
                Console.Write("Select option: ");

                switch (Console.ReadLine())
                {
                    case "1": // Add Note
                        Console.Write("Title: ");
                        var title = Console.ReadLine();
                        Console.Write("Content: ");
                        var content = Console.ReadLine();

                        if (!string.IsNullOrWhiteSpace(title) && !string.IsNullOrWhiteSpace(content))
                        {
                            NoteService.AddNote(title, content);
                            Console.WriteLine("✓ Note saved securely");
                        }
                        else
                        {
                            Console.WriteLine("Title and content cannot be empty");
                        }
                        break;

                    case "2": // View Notes
                        var notes = NoteService.GetNotes();
                        if (notes.Count == 0)
                        {
                            Console.WriteLine("No notes found");
                        }
                        else
                        {
                            foreach (var note in notes)
                            {
                                Console.WriteLine($"\n[{note.Title}]\n{note.Content}");
                            }
                        }
                        break;

                    case "3": // Change Password
                        Console.Write("Current password: ");
                        var current = ConsoleHelper.ReadPassword();
                        Console.Write("New password: ");
                        var newPass = ConsoleHelper.ReadPassword();

                        try
                        {
                            NoteService.ChangePassword(current, newPass);
                            Console.WriteLine("✓ Password changed successfully");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;

                    case "4": // Back
                        inNotesMode = false;
                        continue;

                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
                ConsoleHelper.PromptToContinue();
            }
        }
    }
}