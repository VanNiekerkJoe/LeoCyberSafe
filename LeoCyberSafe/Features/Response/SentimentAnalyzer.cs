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

    public string GetResponseAdjustment(string sentiment)
    {
        return sentiment switch
        {
            "worried" => "I understand this can be concerning. Let me reassure you that",
            "frustrated" => "I apologize for the frustration. Let me help resolve this by",
            "curious" => "That's a great question! Here's what you should know:",
            "confused" => "I'd be happy to clarify this for you. Essentially,",
            "excited" => "That's wonderful to hear! I'm excited to share that",
            _ => ""
        };
    }
}
