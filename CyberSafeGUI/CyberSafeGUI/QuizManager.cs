using System;
using System.Collections.Generic;

namespace CyberSafeGUI
{
    public class QuizManager
    {
        private List<QuizQuestion> questions;
        private int currentQuestionIndex;
        private int score;

        public bool IsQuizActive { get; private set; }
        public bool IsFinished { get; private set; }

        public QuizManager()
        {
            questions = new List<QuizQuestion>
            {
                new QuizQuestion
                {
                    Question = "What should you do if you receive an email asking for your password?",
                    Options = new List<string> { "Reply with your password", "Delete the email", "Report it as phishing", "Ignore it" },
                    CorrectAnswerIndex = 2,
                    Explanation = "Reporting phishing emails helps prevent scams and protects others."
                },
                new QuizQuestion
                {
                    Question = "True or False: Using the same password for multiple accounts is safe.",
                    Options = new List<string> { "True", "False" },
                    CorrectAnswerIndex = 1,
                    Explanation = "Reusing passwords is dangerous. If one account is breached, all accounts using that password are at risk."
                },
                new QuizQuestion
                {
                    Question = "What is a strong password?",
                    Options = new List<string> { "Your birthday", "A mix of letters, numbers, and symbols", "Your pet's name", "123456" },
                    CorrectAnswerIndex = 1,
                    Explanation = "Strong passwords include uppercase, lowercase, numbers, and special characters."
                },
                new QuizQuestion
                {
                    Question = "True or False: 'https://' means a website is secure.",
                    Options = new List<string> { "True", "False" },
                    CorrectAnswerIndex = 0,
                    Explanation = "HTTPS encrypts data between you and the website, making it more secure."
                },
                new QuizQuestion
                {
                    Question = "What is a common sign of a phishing email?",
                    Options = new List<string> { "Professional language", "Urgent requests for personal info", "Known sender", "Correct spelling" },
                    CorrectAnswerIndex = 1,
                    Explanation = "Phishing emails often create urgency to trick you into acting without thinking."
                },
                new QuizQuestion
                {
                    Question = "True or False: You should share your OTP with anyone who asks for it.",
                    Options = new List<string> { "True", "False" },
                    CorrectAnswerIndex = 1,
                    Explanation = "Never share your OTP. Scammers use it to access your accounts."
                },
                new QuizQuestion
                {
                    Question = "What is two-factor authentication (2FA)?",
                    Options = new List<string> { "A second password", "An extra layer of security", "A type of virus", "A phishing technique" },
                    CorrectAnswerIndex = 1,
                    Explanation = "2FA adds an extra step, like a code sent to your phone, to verify your identity."
                },
                new QuizQuestion
                {
                    Question = "True or False: Public Wi-Fi is always safe to use for banking.",
                    Options = new List<string> { "True", "False" },
                    CorrectAnswerIndex = 1,
                    Explanation = "Public Wi-Fi is often unsecured. Use a VPN or avoid sensitive transactions on public networks."
                },
                new QuizQuestion
                {
                    Question = "What should you do with suspicious links?",
                    Options = new List<string> { "Click it to check", "Hover over it to see the URL", "Share it with friends", "Ignore it completely" },
                    CorrectAnswerIndex = 1,
                    Explanation = "Hovering over links shows the actual URL. This helps you spot fake websites."
                },
                new QuizQuestion
                {
                    Question = "True or False: Software updates are important for security.",
                    Options = new List<string> { "True", "False" },
                    CorrectAnswerIndex = 0,
                    Explanation = "Updates fix security vulnerabilities. Always keep your software updated."
                },
                new QuizQuestion
                {
                    Question = "What is social engineering?",
                    Options = new List<string> { "A type of software", "Manipulating people to reveal information", "A secure network", "A password manager" },
                    CorrectAnswerIndex = 1,
                    Explanation = "Social engineering tricks people into sharing confidential information."
                }
            };
            Reset();
        }

        public void Reset()
        {
            currentQuestionIndex = 0;
            score = 0;
            IsQuizActive = false;
            IsFinished = false;
        }

        public void StartQuiz()
        {
            Reset();
            IsQuizActive = true;
        }

        public QuizQuestion GetCurrentQuestion()
        {
            if (currentQuestionIndex < questions.Count)
                return questions[currentQuestionIndex];
            return null;
        }

        public bool SubmitAnswer(int selectedIndex)
        {
            if (!IsQuizActive || currentQuestionIndex >= questions.Count)
                return false;

            bool correct = selectedIndex == questions[currentQuestionIndex].CorrectAnswerIndex;
            if (correct)
                score++;

            currentQuestionIndex++;

            if (currentQuestionIndex >= questions.Count)
            {
                IsQuizActive = false;
                IsFinished = true;
            }

            return correct;
        }

        public int GetScore() => score;
        public int GetTotalQuestions() => questions.Count;
        public int GetCurrentQuestionNumber() => currentQuestionIndex + 1;
        public bool HasMoreQuestions() => currentQuestionIndex < questions.Count;

        public string GetFinalFeedback()
        {
            double percentage = (double)score / questions.Count * 100;
            if (percentage >= 80)
                return $"Great job! You scored {score}/{questions.Count}. You're a cybersecurity pro!";
            else if (percentage >= 60)
                return $"Good effort! You scored {score}/{questions.Count}. Keep learning to stay safe online!";
            else
                return $"You scored {score}/{questions.Count}. Review the tips and try again to improve your cybersecurity knowledge!";
        }
    }

    public class QuizQuestion
    {
        public string Question { get; set; }
        public List<string> Options { get; set; }
        public int CorrectAnswerIndex { get; set; }
        public string Explanation { get; set; }
    }
}