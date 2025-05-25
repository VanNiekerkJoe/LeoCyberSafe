using System.Collections.Generic;
using System.Linq;

public class UserMemory
{
    public string Name { get; set; }
    public List<string> Interests { get; } = new();
    public string CurrentTopic { get; set; }
    private List<string> _conversationHistory { get; } = new();
    private Dictionary<string, string> _preferences { get; } = new();

    public void RememberInterest(string interest)
    {
        if (!Interests.Contains(interest))
            Interests.Add(interest);
    }

    public void RecallInterests()
    {
        if (Interests.Count == 0)
        {
            Console.WriteLine("You have no recorded interests.");
            return;
        }

        Console.WriteLine("Your recorded interests:");
        foreach (var interest in Interests)
        {
            Console.WriteLine($"- {interest}");
        }
    }

    public void AddToConversationHistory(string topic)
    {
        _conversationHistory.Add(topic);
    }

    public List<string> GetConversationHistory()
    {
        return new List<string>(_conversationHistory); // Return a copy to prevent external modification
    }

    public string GetMostFrequentTopic()
    {
        if (_conversationHistory.Count == 0)
            return "No conversation history available.";

        var mostFrequent = _conversationHistory
            .GroupBy(x => x)
            .OrderByDescending(g => g.Count())
            .FirstOrDefault();

        return mostFrequent?.Key ?? "No topics found.";
    }

    public string GetPreference(string key)
    {
        _preferences.TryGetValue(key, out var value);
        return value;
    }

    public void SetPreference(string key, string value)
    {
        if (value == null)
        {
            _preferences.Remove(key); // Remove the preference if value is null
        }
        else
        {
            _preferences[key] = value;
        }
    }

    public Dictionary<string, string> GetAllPreferences()
    {
        return new Dictionary<string, string>(_preferences); // Return a copy of preferences
    }
}
