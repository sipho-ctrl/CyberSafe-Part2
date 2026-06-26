# CyberSafe - Cybersecurity Chatbot GUI

**Author:** Sipho Swartbooi  
**Module:** PROG6221 - Programming 2A  
**Assessment:** POE Part 2 and Part 3  
**Date:** June 2026

---

## Project Description

CyberSafe is a Windows Forms GUI chatbot that educates South African citizens about cybersecurity threats. It covers topics including:

- Password safety
- Phishing detection
- Scam prevention
- Privacy protection
- Social engineering awareness

The chatbot features memory recall, sentiment detection, random responses, a user-friendly graphical interface, and advanced Part 3 features including a task assistant, cybersecurity quiz, and activity log.

---

## Features Implemented

### Part 2 Requirements

| Feature | Description |
|---------|-------------|
| GUI Design | Windows Forms interface with chat history display, text input, and send button |
| Voice Greeting | Plays WAV audio on startup (carried over from Part 1) |
| Keyword Recognition | Responds to "passwords", "phishing", and "scams" |
| Random Responses | Selects random tips from lists for varied conversations |
| Conversation Flow | Handles follow-up questions like "tell me more" |
| Memory and Recall | Remembers user's name and favourite topic |
| Sentiment Detection | Detects "worried" or "frustrated" and responds empathetically |
| Error Handling | Gracefully handles empty or unrecognised inputs |
| Delegates | Implements MessageHandler delegate for logging |

### Part 3 Features (New)

| Feature | Description |
|---------|-------------|
| Task Assistant | Add, view, complete, and delete cybersecurity tasks |
| Database Integration | SQL Server database (`cyber_tasks_db`) with `tasks` table |
| Reminder System | Set reminders in days for each task |
| Cybersecurity Quiz | 11 questions with instant feedback and score tracking |
| Activity Log | Tracks user actions with timestamps |
| NLP Simulation | Flexible keyword detection for varied phrasing |

---

## Setup Instructions

### Prerequisites

- Windows OS
- Visual Studio (2019, 2022, or later)
- .NET Framework or .NET 6/8
- SQL Server (Express or higher) for Part 3 database features

### Steps to Run

1. Clone this repository: https://github.com/sipho-ctrl/CyberSafe-Part2.git
2. Open the solution file `CyberSafeGUI.sln` in Visual Studio

3. Ensure `Greeting.wav` is in the project and set to "Copy if newer"

4. Set up the SQL Server database (for Part 3 features):
- Create a database called `cyber_tasks_db`
- Create a `tasks` table with columns: id, title, description, reminder_date, is_completed, created_at
- Create a SQL login `task_user` with password `TaskPass123!` and map it to the database

5. Update the connection string in `DatabaseHelper.cs` if needed

6. Press **F5** to build and run

### Using the Chatbot

#### Part 2 Commands

| You type | What happens |
|----------|--------------|
| `[your name]` | Bot remembers your name |
| `passwords` | Bot gives a random password tip |
| `phishing` | Bot gives a random phishing tip |
| `scams` | Bot gives a random scam tip |
| `what do you know about me` | Bot recalls your name and favourite topic |
| `I'm worried about scams` | Bot detects sentiment and responds empathetically |
| `help` | Shows available commands |
| `exit` | Closes the application |

#### Part 3 Commands

| You type | What happens |
|----------|--------------|
| `add task [title]` | Adds a new task and asks about a reminder |
| `yes` / `no` (after task) | Sets or skips a reminder |
| `[number]` (after yes) | Sets reminder in days |
| `view tasks` | Shows all pending tasks |
| `complete task [id]` | Marks a task as complete |
| `delete task [id]` | Deletes a task |
| `start quiz` | Starts the cybersecurity quiz |
| `quit quiz` | Stops the quiz early |
| `show activity log` | Shows recent actions |
| `what have you done` | Same as above |

---

## Project Structure
CyberSafe-Part2/
├── CyberSafeGUI/
│ ├── Form1.cs # Main window code-behind
│ ├── Form1.Designer.cs # GUI layout
│ ├── ChatbotGUI.cs # Chatbot logic (memory, keywords, responses, Part 3 commands)
│ ├── DatabaseHelper.cs # SQL Server database operations (Part 3)
│ ├── QuizManager.cs # Quiz questions, scoring, and feedback (Part 3)
│ ├── ActivityLogger.cs # Activity log with timestamps (Part 3)
│ ├── AudioService.cs # Voice greeting playback
│ ├── SentimentAnalyzer.cs # Sentiment detection
│ ├── Greeting.wav # Voice greeting audio file
│ └── CyberSafeGUI.csproj # Project file
├── .gitignore
├── README.md
└── CyberSafeGUI.sln


---

## Code Explanation

### Form1.cs
Handles the window events. On load, it creates the ChatbotGUI object and calls Start(). When the user clicks Send or presses Enter, it passes the input to the chatbot.

### ChatbotGUI.cs
Contains the main chatbot logic:
- Stores user name and favourite topic in a Dictionary
- Uses Lists to store multiple tips for random selection
- Contains keyword detection using `Contains()`
- Implements a delegate (`MessageHandler`) for logging
- Handles Part 3 commands (tasks, quiz, activity log)

### DatabaseHelper.cs (Part 3)
Handles all SQL Server database operations:
- `AddTask()` - Inserts a new task
- `GetTasks()` - Retrieves pending tasks
- `MarkTaskComplete()` - Updates task status
- `DeleteTask()` - Removes a task

### QuizManager.cs (Part 3)
Manages the cybersecurity quiz:
- 11 questions covering various cybersecurity topics
- Score tracking and feedback
- Instant answer validation with explanations

### ActivityLogger.cs (Part 3)
Tracks user actions:
- Logs tasks, quiz activity, and log views
- Stores timestamps for each action
- Displays last 10 actions on request

### AudioService.cs
Plays the `Greeting.wav` file using `System.Media.SoundPlayer` with try-catch error handling.

### SentimentAnalyzer.cs
Detects keywords like "worried", "frustrated", "scared" to determine user sentiment.

---

## GitHub Information

### Commits
Repository contains 6+ meaningful commits with descriptive messages for each part.

### Releases

| Release | Tag | Description |
|---------|-----|-------------|
| Initial GUI Release | v1.0 | Core chatbot functionality with GUI |
| Full Release | v1.1 | All Part 2 features including delegates and sentiment detection |
| Part 3 - Task Assistant | v3.0 | Task Assistant with database integration |
| Part 3 - Quiz Added | v3.1 | Cybersecurity Quiz with 11 questions |
| Part 3 - Final Release | v3.2 | Activity Log and complete Part 3 features |

### Continuous Integration
GitHub Actions is configured to build the project on each push. A green check mark indicates a successful build.

---

## Video Presentations

| Part | Link |
|------|------|
| Part 3 | [Watch Part 2 Presentation]([https://youtu.be/Cn9aY3kiT30?si=tzwHvIge0eJ1bRVV](https://youtu.be/9NoSapFif9o?si=avHYd-7zfw_WZNI_)) |


---

## Marking Criteria Coverage

### Part 2

| Criteria | Status |
|----------|--------|
| Correct Submission | ✅ |
| GitHub and Releases | ✅ |
| Keyword Recognition | ✅ |
| Random Responses | ✅ |
| Conversation Flow | ✅ |
| Memory and Recall | ✅ |
| Sentiment Detection | ✅ |
| Code Structure and Optimisation | ✅ |
| Chatbot GUI Design | ✅ |
| Video Presentation | ✅ |

### Part 3

| Criteria | Status |
|----------|--------|
| Correct Submission | ✅ |
| GitHub and Releases with Tags | ✅ |
| Task Assistant with Reminders | ✅ |
| Database Integration | ✅ |
| Cybersecurity Mini-Game | ✅ |
| NLP Simulation | ✅ |
| Activity Log Feature | ✅ |
| Combining Parts 1, 2, and 3 | ✅ |
| Video Presentation | ⏳ Pending |

---

## Technologies Used

- C# .NET
- Windows Forms (GUI)
- SQL Server (Database)
- System.Media (Voice playback)
- GitHub Actions (CI/CD)

---

## Author

**Sipho Swartbooi**  
PROG6221 - Programming 2A  
The IIE

---

*This chatbot was developed as part of the POE assessment for Programming 2A.*
