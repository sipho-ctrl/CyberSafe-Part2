using System;

namespace CyberAwareSA
{
    /// <summary>
    /// Analyzes user input to detect emotional sentiment and provides appropriate responses.
    /// </summary>
    public class SentimentAnalyzer
    {
        /// <summary>
        /// Detects the sentiment of user input.
        /// </summary>
        /// <param name="input">The user's input message</param>
        /// <returns>A string representing the detected sentiment</returns>
        public string DetectSentiment(string input)
        {
            string lowerInput = input.ToLower();

            // Detect worried/anxious sentiment
            if (lowerInput.Contains("worried") || lowerInput.Contains("scared") ||
                lowerInput.Contains("nervous") || lowerInput.Contains("afraid") ||
                lowerInput.Contains("anxious") || lowerInput.Contains("stress"))
            {
                return "worried";
            }

            // Detect frustrated sentiment
            if (lowerInput.Contains("frustrated") || lowerInput.Contains("annoyed") ||
                lowerInput.Contains("confused") || lowerInput.Contains("difficult") ||
                lowerInput.Contains("hard") || lowerInput.Contains("complicated"))
            {
                return "frustrated";
            }

            // Detect curious sentiment
            if (lowerInput.Contains("curious") || lowerInput.Contains("interested") ||
                lowerInput.Contains("tell me more") || lowerInput.Contains("learn") ||
                lowerInput.Contains("how does") || lowerInput.Contains("why do"))
            {
                return "curious";
            }

            // Detect grateful/positive sentiment
            if (lowerInput.Contains("thank") || lowerInput.Contains("helpful") ||
                lowerInput.Contains("appreciate") || lowerInput.Contains("good") ||
                lowerInput.Contains("great"))
            {
                return "grateful";
            }

            // Neutral sentiment
            return "neutral";
        }

        /// <summary>
        /// Generates an empathetic response based on detected sentiment.
        /// </summary>
        /// <param name="sentiment">The detected sentiment</param>
        /// <param name="userName">The user's name for personalisation</param>
        /// <returns>An empathetic response string</returns>
        public string GetEmpatheticResponse(string sentiment, string userName)
        {
            switch (sentiment)
            {
                case "worried":
                    return $"It's completely understandable to feel worried, {userName}. Cybersecurity threats can be intimidating. Let me share some simple steps to help you feel more secure.";

                case "frustrated":
                    return $"I hear your frustration, {userName}. Cybersecurity can feel overwhelming at first. Let me break this down into simpler terms for you.";

                case "curious":
                    return $"I love your curiosity, {userName}! That's exactly the right attitude for staying safe online. Let me share some interesting facts with you.";

                case "grateful":
                    return $"You're very welcome, {userName}! I'm glad I could help. Stay safe out there!";

                default:
                    return null;
            }
        }

        /// <summary>
        /// Gets a follow-up tip based on the current topic and sentiment.
        /// </summary>
        /// <param name="topic">The cybersecurity topic being discussed</param>
        /// <param name="sentiment">The detected sentiment</param>
        /// <returns>A supportive tip message</returns>
        public string GetSupportiveTip(string topic, string sentiment)
        {
            if (sentiment == "worried" || sentiment == "frustrated")
            {
                if (topic.Contains("password"))
                {
                    return "Tip: Start with one small change - use a password manager to generate and store strong passwords. You only need to remember one master password!";
                }
                else if (topic.Contains("phish"))
                {
                    return "Tip: When in doubt, don't click! Hover over links first to see where they really go, or contact the sender through a different channel.";
                }
                else if (topic.Contains("scam"))
                {
                    return "Tip: Remember - legitimate companies will never ask for your password or OTP over the phone or email. When in doubt, hang up and call back officially.";
                }
                else
                {
                    return "Tip: Start with one good habit - always check for 'https://' before entering personal information online. Small steps make a big difference!";
                }
            }

            return null;
        }
    }
}