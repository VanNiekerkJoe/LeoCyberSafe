using LeoCyberSafe.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeoCyberSafe.Core.Services
{
    public class MemoryService
    {
        private readonly UserMemory _memory;

        public MemoryService(UserMemory memory)
        {
            _memory = memory;
        }

        public string RecallContext(string currentInput)
        {
            // Check if we're continuing the same topic
            if (!string.IsNullOrEmpty(_memory.CurrentTopic) &&
                currentInput.ToLower().Contains(_memory.CurrentTopic.ToLower()))
            {
                return $"Continuing our discussion about {_memory.CurrentTopic}...";
            }

            // Check if this relates to a previous topic
            var relatedTopics = _memory.Interests
                .Where(t => currentInput.ToLower().Contains(t.ToLower()))
                .ToList();

            if (relatedTopics.Any())
            {
                _memory.CurrentTopic = relatedTopics.First(); // Set the first related topic as current
                return $"Ah, this relates to our previous discussions about {string.Join(", ", relatedTopics)}.";
            }

            return "I don't recall discussing that before. Can you tell me more?";
        }

        public string GetPersonalizedGreeting()
        {
            var mostFrequentTopic = _memory.GetMostFrequentTopic();

            if (_memory.Interests.Count == 0)
                return $"Hello {_memory.Name}! How can I help with cybersecurity today?";

            return $"Welcome back {_memory.Name}! " +
                   $"Last time we discussed {_memory.CurrentTopic ?? "security"}. " +
                   $"Your most frequent interest is {mostFrequentTopic}. " +
                   $"What would you like to explore today?";
        }

        public string RecallPreference(string key)
        {
            return _memory.GetPreference(key) ?? "No preference found for that key.";
        }

        public void RememberPreference(string key, string value)
        {
            _memory.SetPreference(key, value);
        }

        public void ClearPreference(string key)
        {
            _memory.SetPreference(key, null);
        }

        public void ListPreferences()
        {
            var preferences = _memory.GetAllPreferences();
            Console.WriteLine("Your preferences:");
            foreach (var kvp in preferences)
            {
                Console.WriteLine($"- {kvp.Key}: {kvp.Value}");
            }
        }

        public void AddToConversationHistory(string topic)
        {
            _memory.AddToConversationHistory(topic);
        }

        public List<string> GetConversationHistory()
        {
            return _memory.GetConversationHistory();
        }

        public void SuggestRelatedInterests(string currentInput)
        {
            var suggestions = _memory.Interests
                .Where(t => currentInput.ToLower().Contains(t.ToLower()))
                .ToList();

            if (suggestions.Any())
            {
                Console.WriteLine("You might also be interested in:");
                foreach (var suggestion in suggestions)
                {
                    Console.WriteLine($"- {suggestion}");
                }
            }
            else
            {
                Console.WriteLine("No related interests found.");
            }
        }
    }
}
