using LeoCyberSafe.Utilities;
using System.Collections.Generic;

namespace LeoCyberSafe.Features.Phishing
{
    public class PhishingSimulator
    {
        public void StartSimulation(string userName)
        {
            var scenarios = new List<PhishingScenario> {
                new(
                    Sender: "support@your-bank.com",
                    Subject: "Urgent: Account Verification Required",
                    Content: $"Dear {userName},\n\nWe detected unusual activity.\nVerify now: http://bit.ly/fake-bank",
                    IsMalicious: true
                ),
                new(
                    Sender: "hr@company.com",
                    Subject: "Your Quarterly Bonus",
                    Content: "Congratulations! Your bonus is approved.",
                    IsMalicious: false
                )
            };

            foreach (var scenario in scenarios)
            {
                PresentScenario(scenario);
                bool userResponse = GetUserJudgment();
                EvaluateResponse(userResponse, scenario.IsMalicious);
                Thread.Sleep(1500);
            }
        }

        private void PresentScenario(PhishingScenario scenario)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(@"
╔════════════════════════════════════╗
║        PHISHING SIMULATION         ║
╠════════════════════════════════════╣");
            Console.ResetColor();

            Console.WriteLine($"\n📧 From: {scenario.Sender}");
            Console.WriteLine($"📌 Subject: {scenario.Subject}\n");
            Console.WriteLine(scenario.Content);
        }

        private bool GetUserJudgment()
        {
            Console.Write("\nIs this phishing? (y/n): ");
            return Console.ReadLine()?.ToLower() == "y";
        }

        private void EvaluateResponse(bool userResponse, bool isMalicious)
        {
            Console.ForegroundColor = userResponse == isMalicious ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(userResponse == isMalicious ? "✅ Correct!" : "❌ Wrong!");
            Console.ResetColor();
        }
    }

    public record PhishingScenario(
        string Sender,
        string Subject,
        string Content,
        bool IsMalicious
    );
}