using CyberAwareSA;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CyberSafeGUI
{
    // DELEGATE: Defines a method signature for logging messages
    public delegate void MessageHandler(string message);

    public class ChatbotGUI
    {
        private string userName;
        private string userFavouriteTopic;
        private Dictionary<string, string> userMemory;
        private AudioService audio;
        private SentimentAnalyzer sentimentAnalyzer;
        private Random random;
        private RichTextBox chatHistory;
        private TextBox userInput;
        private MessageHandler logMessage;  // DELEGATE field

        // Random password tips
        private List<string> passwordTips = new List<string>
        {
            "Use at least 12 characters with a mix of uppercase, lowercase, numbers, and symbols.",
            "Never reuse passwords across different accounts.",
            "Consider using a password manager to generate and store strong passwords.",
            "Avoid using personal information like birthdays or names in your passwords.",
            "Enable two-factor authentication whenever possible for extra security."
        };

        // Random phishing tips
        private List<string> phishingTips = new List<string>
        {
            "Be cautious of emails asking for personal information.",
            "Never click on links in suspicious emails.",
            "Check the sender's email address carefully.",
            "If an email creates urgency, it's likely a phishing attempt.",
            "Legitimate companies never ask for your password or OTP via email."
        };

        // Random scam tips
        private List<string> scamTips = new List<string>
        {
            "Scammers create fake urgency. Always verify before acting.",
            "Never share your OTP or PIN with anyone.",
            "If something sounds too good to be true, it probably is.",
            "Hang up and call back using an official number.",
            "Be wary of unexpected prizes or lotteries."
        };

        public ChatbotGUI(RichTextBox history, TextBox input)
        {
            chatHistory = history;
            userInput = input;
            audio = new AudioService();
            sentimentAnalyzer = new SentimentAnalyzer();
            random = new Random();
            userMemory = new Dictionary<string, string>();

            // DELEGATE: Assign method to delegate
            logMessage = LogToConsole;
        }

        public void Start()
        {
            audio.PlayGreeting();
            AppendBotMessage("Hello! Welcome to CyberSafe.");
            AppendBotMessage("What is your name?");
            userInput.Focus();
        }

        public void ProcessUserInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                AppendBotMessage("Please type something!");
                return;
            }

            AppendUserMessage(input);
            string lowerInput = input.ToLower();

            // Get name if not set
            if (string.IsNullOrEmpty(userName))
            {
                userName = input;
                userMemory["name"] = userName;
                AppendBotMessage($"Nice to meet you, {userName}!");
                AppendBotMessage("What topic interests you? (passwords, phishing, scams)");
                return;
            }

            // Get favourite topic if not set
            if (string.IsNullOrEmpty(userFavouriteTopic))
            {
                if (lowerInput.Contains("password"))
                    userFavouriteTopic = "passwords";
                else if (lowerInput.Contains("phish"))
                    userFavouriteTopic = "phishing";
                else if (lowerInput.Contains("scam"))
                    userFavouriteTopic = "scams";
                else
                    userFavouriteTopic = "general";

                userMemory["favourite_topic"] = userFavouriteTopic;
                AppendBotMessage($"Great! I'll remember you like {userFavouriteTopic}.");
                AppendBotMessage("Ask me about passwords, phishing, or scams!");
                return;
            }

            // Detect sentiment for worried/frustrated
            string sentiment = sentimentAnalyzer.DetectSentiment(lowerInput);
            if (sentiment == "worried" || sentiment == "frustrated")
            {
                if (lowerInput.Contains("scam"))
                    AppendBotMessage("I understand your concern. Scammers can be convincing. Let me help you stay safe.");
                else if (lowerInput.Contains("password"))
                    AppendBotMessage("I know passwords can be frustrating. Let me share some simple tips.");
                else if (lowerInput.Contains("phish"))
                    AppendBotMessage("It's smart to be worried about phishing. Here's how to protect yourself.");
            }

            // Responses
            if (lowerInput.Contains("password"))
            {
                string response = GetRandomResponse(passwordTips);
                AppendBotMessage(response);
            }
            else if (lowerInput.Contains("phish"))
            {
                string response = GetRandomResponse(phishingTips);
                AppendBotMessage(response);
            }
            else if (lowerInput.Contains("scam"))
            {
                string response = GetRandomResponse(scamTips);
                AppendBotMessage(response);
            }
            else if (lowerInput.Contains("what do you know about me") || lowerInput.Contains("remember me"))
            {
                AppendBotMessage($"I remember that your name is {userName} and you're interested in {userFavouriteTopic}.");
            }
            else if (lowerInput.Contains("help"))
            {
                AppendBotMessage("You can ask me about: passwords, phishing, or scams.");
            }
            else if (lowerInput.Contains("exit"))
            {
                AppendBotMessage($"Goodbye, {userName}! Stay safe!");
                Application.Exit();
            }
            else
            {
                AppendBotMessage("Try asking about passwords, phishing, or scams. Type 'help' for options.");
            }

            userInput.Clear();
            userInput.Focus();
        }

        // DELEGATE: Method that gets called through the delegate
        private void LogToConsole(string message)
        {
            System.Diagnostics.Debug.WriteLine($"LOG: {message}");
        }

        private string GetRandomResponse(List<string> responses)
        {
            int index = random.Next(responses.Count);
            return responses[index];
        }

        private void AppendUserMessage(string message)
        {
            chatHistory.AppendText($">>> {userName}: {message}{Environment.NewLine}");
            chatHistory.ScrollToCaret();
        }

        private void AppendBotMessage(string message)
        {
            // DELEGATE: Call the delegate to log the message
            logMessage?.Invoke(message);

            chatHistory.AppendText($"Chatbot: {message}{Environment.NewLine}");
            chatHistory.ScrollToCaret();
        }
    }
}