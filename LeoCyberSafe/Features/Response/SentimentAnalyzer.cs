public class SentimentAnalyzer
{
    private readonly Dictionary<string, string> _sentimentKeywords = new()
    {
        ["worried"] = "worried|concerned|nervous|anxious|scared|afraid|fear|unsure|what if",
        ["frustrated"] = "frustrated|angry|mad|annoyed|pissed|irritated|fed up|sick of",
        ["curious"] = "curious|interested|wonder|how does|what is|why does|explain|tell me",
        ["confused"] = "confused|don't understand|not sure|what do you mean|help me",
        ["excited"] = "excited|happy|great|awesome|thank you|thanks|appreciate|love it"
    };

    public string DetectSentiment(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return "neutral";

        input = input.ToLower();

        foreach (var sentiment in _sentimentKeywords)
        {
            if (sentiment.Value.Split('|').Any(keyword => input.Contains(keyword)))
            {
                return sentiment.Key;
            }
        }

        return "neutral";
    }

    public string GetResponseAdjustment(string sentiment, string topic)
    {
        return sentiment switch
        {
            "worried" => $"I'm sorry that you're worried about {topic}. They are easier than they seem. {GetTopicAdvice(topic)}",
            "frustrated" => $"I understand that passwords can be frustrating. {GetTopicAdvice(topic)}",
            "curious" => $"That's a great question about {topic}! Here's what you should know: {GetTopicAdvice(topic)}",
            "confused" => $"I can clarify that for you regarding {topic}. {GetTopicAdvice(topic)}",
            "excited" => $"That's wonderful to hear! I'm excited to share more about {topic}. {GetTopicAdvice(topic)}",
            _ => ""
        };
    }

    private string GetTopicAdvice(string topic)
    {
        return topic switch
        {
            "vpn" => "VPNs are tools that encrypt your internet connection and help protect your privacy online.",
            "passwords" => "Using a password manager can help you create and store strong passwords securely.",
            _ => "Feel free to ask more about this topic!"
        };
    }
}
