using LeoCyberSafe.Utilities;
using System;
using System.Linq;

namespace LeoCyberSafe.Features.Password
{
    public class PasswordAuditService
    {
        public (int score, string feedback) Analyze(string password)
        {
            int score = 0;
            string feedback = "";

            // Length check
            if (password.Length >= 12)
            {
                score += 30;
                feedback += "✓ Good length\n";
            }
            else
            {
                feedback += "✗ Should be at least 12 characters\n";
            }

            // Complexity checks
            if (password.Any(char.IsUpper))
            {
                score += 15;
                feedback += "✓ Contains uppercase\n";
            }
            if (password.Any(char.IsLower))
            {
                score += 15;
                feedback += "✓ Contains lowercase\n";
            }
            if (password.Any(char.IsDigit))
            {
                score += 15;
                feedback += "✓ Contains numbers\n";
            }
            if (password.Any(c => !char.IsLetterOrDigit(c)))
            {
                score += 25;
                feedback += "✓ Contains special characters\n";
            }

            // Common password check
            if (IsCommonPassword(password))
            {
                score = Math.Max(0, score - 30);
                feedback += "✗ This is a commonly used password\n";
            }

            return (Math.Min(score, 100), feedback);
        }

        private bool IsCommonPassword(string password)
        {
            string[] commonPasswords = { "password", "123456", "qwerty", "letmein" };
            return commonPasswords.Contains(password.ToLower());
        }
    }
}