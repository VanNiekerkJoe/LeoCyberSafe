using LeoCyberSafe.Core.Models;
using LeoCyberSafe.Utilities;
using System;
using System.Threading;

namespace LeoCyberSafe.Core
{
    public class ThreatScanner
    {
        public ThreatReport GenerateReport()
        {
            var report = new ThreatReport();
            ConsoleHelper.DisplayScanHeader();

            AddSimulatedThreat(report, "Outdated Windows Defender definitions", SeverityLevel.High);
            AddSimulatedThreat(report, "Unsecured WiFi network detected", SeverityLevel.Medium);
            AddSimulatedThreat(report, "2 passwords reused across accounts", SeverityLevel.Medium);
            AddSimulatedThreat(report, "No multi-factor authentication enabled", SeverityLevel.High);

            return report;
        }

        private void AddSimulatedThreat(ThreatReport report, string desc, SeverityLevel severity)
        {
            Thread.Sleep(800);
            report.AddThreat(desc, severity);
            Console.WriteLine($"• {desc} ({severity})");
        }
    }
}