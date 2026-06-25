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
        private MessageHandler logMessage;

        // Part 3: New components
        private DatabaseHelper dbHelper;
        private QuizManager quizManager;
        private ActivityLogger activityLogger;
        private bool waitingForReminder;
        private string pendingTaskTitle;
        private bool quizActive;

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
            logMessage = LogToConsole;

            // Part 3: Initialise new components
            dbHelper = new DatabaseHelper();
            quizManager = new QuizManager();
            activityLogger = new ActivityLogger();
            waitingForReminder = false;
            quizActive = false;

            // Log initialisation
            activityLogger.Log("Chatbot started.");
        }

        public void Start()
        {
            audio.PlayGreeting();

            // Check database connection
            if (dbHelper.TestConnection())
            {
                AppendBotMessage("Hello! Welcome to CyberSafe.");
                activityLogger.Log("User greeted.");
            }
            else
            {
                AppendBotMessage("Hello! Welcome to CyberSafe.");
                AppendBotMessage("Note: Database is not connected. Task features will not work.");
                activityLogger.Log("Database connection failed.");
            }

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

            // === Handle name if not set ===
            if (string.IsNullOrEmpty(userName))
            {
                userName = input;
                userMemory["name"] = userName;
                AppendBotMessage($"Nice to meet you, {userName}!");
                AppendBotMessage("What topic interests you? (passwords, phishing, scams)");
                activityLogger.Log($"User name set: {userName}");
                return;
            }

            // === Handle favourite topic if not set ===
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
                AppendBotMessage("Ask me about passwords, phishing, scams, tasks, or quiz!");
                activityLogger.Log($"Favourite topic set: {userFavouriteTopic}");
                return;
            }

            // === Detect sentiment for worried/frustrated ===
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

            // === PART 3: Handle waiting for reminder ===
            if (waitingForReminder)
            {
                HandleReminderInput(input);
                return;
            }

            // === PART 3: Handle Quiz ===
            if (quizActive)
            {
                HandleQuizAnswer(input);
                return;
            }

            // === Check for "show activity log" ===
            if (lowerInput.Contains("show activity log") || lowerInput.Contains("what have you done"))
            {
                ShowActivityLog();
                return;
            }

            // === Check for "view tasks" ===
            if (lowerInput.Contains("view tasks") || lowerInput.Contains("show tasks"))
            {
                ViewTasks();
                return;
            }

            // === Check for "complete task" ===
            if (lowerInput.Contains("complete task"))
            {
                CompleteTask(input);
                return;
            }

            // === Check for "delete task" ===
            if (lowerInput.Contains("delete task"))
            {
                DeleteTask(input);
                return;
            }

            // === Check for "add task" or "remind me" ===
            if (lowerInput.Contains("add task") || lowerInput.Contains("new task"))
            {
                AddTask(input);
                return;
            }

            if (lowerInput.Contains("remind me") || lowerInput.Contains("set reminder"))
            {
                AddReminder(input);
                return;
            }

            // === Check for "start quiz" or "play quiz" ===
            if (lowerInput.Contains("start quiz") || lowerInput.Contains("play quiz") || lowerInput.Contains("quiz"))
            {
                StartQuiz();
                return;
            }

            // === Part 2 responses ===
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
                AppendBotMessage("You can ask me about:");
                AppendBotMessage("- passwords, phishing, scams (tips)");
                AppendBotMessage("- add task [title] (adds a task)");
                AppendBotMessage("- view tasks (shows all tasks)");
                AppendBotMessage("- complete task [id] (marks task done)");
                AppendBotMessage("- delete task [id] (deletes task)");
                AppendBotMessage("- start quiz (starts cybersecurity quiz)");
                AppendBotMessage("- show activity log (shows recent actions)");
                AppendBotMessage("- exit (closes the app)");
            }
            else if (lowerInput.Contains("exit"))
            {
                AppendBotMessage($"Goodbye, {userName}! Stay safe!");
                activityLogger.Log("User exited.");
                Application.Exit();
            }
            else
            {
                AppendBotMessage("Try asking about passwords, phishing, scams, or type 'help' for all options.");
            }

            userInput.Clear();
            userInput.Focus();
        }

        // === PART 3: Task Methods ===

        private void AddTask(string input)
        {
            string taskTitle = input.Replace("add task", "").Replace("new task", "").Trim();

            if (string.IsNullOrEmpty(taskTitle))
            {
                AppendBotMessage("What task would you like to add? For example: 'add task Review privacy settings'");
                return;
            }

            pendingTaskTitle = taskTitle;
            waitingForReminder = true;
            AppendBotMessage($"Task '{taskTitle}' added. Would you like to set a reminder? (yes/no)");
            activityLogger.Log($"Task pending reminder: {taskTitle}");
        }

        private void AddReminder(string input)
        {
            string reminderText = input.Replace("remind me", "").Replace("set reminder", "").Trim();

            if (string.IsNullOrEmpty(reminderText))
            {
                AppendBotMessage("What would you like to be reminded about?");
                return;
            }

            // Treat as a simple task with reminder
            pendingTaskTitle = reminderText;
            waitingForReminder = true;
            AppendBotMessage($"I'll help you set a reminder for '{reminderText}'. How many days from now?");
        }

        private void HandleReminderInput(string input)
        {
            string lowerInput = input.ToLower();

            if (lowerInput.Contains("no") || lowerInput.Contains("n"))
            {
                // Save task without reminder
                try
                {
                    dbHelper.AddTask(pendingTaskTitle, pendingTaskTitle, null);
                    AppendBotMessage($"Task '{pendingTaskTitle}' saved. No reminder set.");
                    activityLogger.Log($"Task added (no reminder): {pendingTaskTitle}");
                }
                catch (Exception ex)
                {
                    AppendBotMessage($"Error saving task: {ex.Message}");
                }
                waitingForReminder = false;
                pendingTaskTitle = null;
            }
            else if (lowerInput.Contains("yes") || lowerInput.Contains("y"))
            {
                AppendBotMessage("How many days from now should I remind you? (e.g., 3)");
                // Wait for number input
                waitingForReminder = true;
                // Flag that we're expecting a number
            }
            else if (int.TryParse(input, out int days))
            {
                // User entered a number of days
                try
                {
                    DateTime reminderDate = DateTime.Now.AddDays(days);
                    dbHelper.AddTask(pendingTaskTitle, pendingTaskTitle, reminderDate);
                    AppendBotMessage($"Task '{pendingTaskTitle}' saved. Reminder set for {reminderDate:yyyy-MM-dd}.");
                    activityLogger.Log($"Task added with {days} day reminder: {pendingTaskTitle}");
                }
                catch (Exception ex)
                {
                    AppendBotMessage($"Error saving task: {ex.Message}");
                }
                waitingForReminder = false;
                pendingTaskTitle = null;
            }
            else
            {
                AppendBotMessage("Please enter 'yes' to set a reminder, 'no' to skip, or a number of days.");
                return;
            }

            userInput.Clear();
            userInput.Focus();
        }

        private void ViewTasks()
        {
            try
            {
                var tasks = dbHelper.GetTasks(false);
                if (tasks.Count == 0)
                {
                    AppendBotMessage("You have no pending tasks. Great job staying on top of things!");
                    return;
                }

                AppendBotMessage("Here are your pending cybersecurity tasks:");
                foreach (var task in tasks)
                {
                    string reminder = task.ReminderDate.HasValue ? $" (Reminder: {task.ReminderDate:yyyy-MM-dd})" : "";
                    AppendBotMessage($"- ID: {task.Id} - {task.Title}{reminder}");
                }
                AppendBotMessage("To complete a task, type: complete task [ID]");
                AppendBotMessage("To delete a task, type: delete task [ID]");
                activityLogger.Log("Tasks viewed.");
            }
            catch (Exception ex)
            {
                AppendBotMessage($"Error retrieving tasks: {ex.Message}");
            }
        }

        private void CompleteTask(string input)
        {
            string[] parts = input.Split(' ');
            if (parts.Length < 3)
            {
                AppendBotMessage("Please specify the task ID. Example: complete task 1");
                return;
            }

            if (int.TryParse(parts[2], out int taskId))
            {
                try
                {
                    dbHelper.MarkTaskComplete(taskId);
                    AppendBotMessage($"Task {taskId} marked as complete! Well done!");
                    activityLogger.Log($"Task completed: ID {taskId}");
                }
                catch (Exception ex)
                {
                    AppendBotMessage($"Error completing task: {ex.Message}");
                }
            }
            else
            {
                AppendBotMessage("Invalid task ID. Please enter a number.");
            }
        }

        private void DeleteTask(string input)
        {
            string[] parts = input.Split(' ');
            if (parts.Length < 3)
            {
                AppendBotMessage("Please specify the task ID. Example: delete task 1");
                return;
            }

            if (int.TryParse(parts[2], out int taskId))
            {
                try
                {
                    dbHelper.DeleteTask(taskId);
                    AppendBotMessage($"Task {taskId} deleted.");
                    activityLogger.Log($"Task deleted: ID {taskId}");
                }
                catch (Exception ex)
                {
                    AppendBotMessage($"Error deleting task: {ex.Message}");
                }
            }
            else
            {
                AppendBotMessage("Invalid task ID. Please enter a number.");
            }
        }

        // === PART 3: Quiz Methods ===

        private void StartQuiz()
        {
            if (!dbHelper.TestConnection())
            {
                AppendBotMessage("Database not connected. Quiz will still work, but tasks won't be saved.");
            }

            quizManager.StartQuiz();
            quizActive = true;
            AppendBotMessage("Starting the Cybersecurity Quiz! You'll get 10+ questions.");
            AppendBotMessage("Type 'quit quiz' to stop at any time.");
            ShowNextQuestion();
            activityLogger.Log("Quiz started.");
        }

        private void ShowNextQuestion()
        {
            var question = quizManager.GetCurrentQuestion();
            if (question == null)
            {
                EndQuiz();
                return;
            }

            AppendBotMessage($"Question {quizManager.GetCurrentQuestionNumber()}/{quizManager.GetTotalQuestions()}:");
            AppendBotMessage(question.Question);

            for (int i = 0; i < question.Options.Count; i++)
            {
                AppendBotMessage($"  {i + 1}. {question.Options[i]}");
            }

            AppendBotMessage("Type the number of your answer (1, 2, 3, etc.)");
        }

        private void HandleQuizAnswer(string input)
        {
            string lowerInput = input.ToLower();  // ← FIX: Added this line

            if (lowerInput.Contains("quit quiz") || lowerInput.Contains("stop quiz"))
            {
                quizActive = false;
                AppendBotMessage("Quiz ended. You can restart anytime with 'start quiz'.");
                activityLogger.Log("Quiz quit by user.");
                userInput.Clear();
                userInput.Focus();
                return;
            }

            if (int.TryParse(input, out int answerIndex) && answerIndex >= 1 && answerIndex <= 4)
            {
                int selected = answerIndex - 1;
                bool correct = quizManager.SubmitAnswer(selected);

                if (correct)
                {
                    AppendBotMessage("✅ Correct! Well done!");
                }
                else
                {
                    AppendBotMessage($"❌ Incorrect. {GetCorrectAnswerText()}");
                }

                if (quizManager.HasMoreQuestions())
                {
                    ShowNextQuestion();
                }
                else
                {
                    EndQuiz();
                }
            }
            else
            {
                AppendBotMessage("Please type the number of your answer (1, 2, 3, or 4).");
            }

            userInput.Clear();
            userInput.Focus();
        }

        private string GetCorrectAnswerText()
        {
            // This returns a generic message
            // You can expand this to show the actual correct answer
            return "Please review the topic and try again.";
        }

        private void EndQuiz()
        {
            quizActive = false;
            int score = quizManager.GetScore();
            int total = quizManager.GetTotalQuestions();
            string feedback = quizManager.GetFinalFeedback();

            AppendBotMessage($"Quiz complete! You scored {score}/{total}.");
            AppendBotMessage(feedback);
            activityLogger.Log($"Quiz completed. Score: {score}/{total}");
        }

        // === PART 3: Activity Log Methods ===

        private void ShowActivityLog()
        {
            var logs = activityLogger.GetRecentLogs(10);
            AppendBotMessage("Here's a summary of recent actions:");

            int count = 1;
            foreach (var log in logs)
            {
                AppendBotMessage($"{count}. {log}");
                count++;
            }

            activityLogger.Log("Activity log viewed.");
        }

        // === Helper Methods ===

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
            logMessage?.Invoke(message);
            chatHistory.AppendText($"Chatbot: {message}{Environment.NewLine}");
            chatHistory.ScrollToCaret();
        }

        private void LogToConsole(string message)
        {
            System.Diagnostics.Debug.WriteLine($"LOG: {message}");
        }
    }
}