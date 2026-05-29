# CyberSafe - Cybersecurity Chatbot GUI

**Author:** Sipho Swartbooi  
**Module:** PROG6221 - Programming 2A  
**Assessment:** POE Part 2  
**Date:** May 2026

---

## Project Description

CyberSafe is a Windows Forms GUI chatbot that educates South African citizens about cybersecurity threats. It covers topics including:

- Password safety
- Phishing detection
- Scam prevention

The chatbot features memory recall, sentiment detection, random responses, and a user-friendly graphical interface.

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
| Tagged Releases | v1.0 and v1.1 published on GitHub |

---

## Setup Instructions

### Prerequisites

- Windows OS
- Visual Studio (2019, 2022, or later)
- .NET Framework or .NET 6/8 (matches Part 1)

### Steps to Run

1. Clone this repository: https://github.com/sipho-ctrl/CyberSafe-Part2.git
2. Open the solution file `CyberSafeGUI.sln` in Visual Studio
3. Ensure `Greeting.wav` is in the project and set to "Copy if newer"
4. Press **F5** to build and run

### Using the Chatbot

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

---

## Project Structure

CyberSafe-Part2/
├── CyberSafeGUI/
│ ├── Form1.cs # Main window code-behind
│ ├── Form1.Designer.cs # GUI layout
│ ├── ChatbotGUI.cs # Chatbot logic (memory, keywords, responses)
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

### AudioService.cs
Plays the `Greeting.wav` file using `System.Media.SoundPlayer` with try-catch error handling.

### SentimentAnalyzer.cs
Detects keywords like "worried", "frustrated", "scared" to determine user sentiment.

---

## GitHub Information

### Commits
Repository contains 6+ meaningful commits with descriptive messages.

### Releases

| Release | Tag | Description |
|---------|-----|-------------|
| Initial GUI Release | v1.0 | Core chatbot functionality with GUI |
| Full Release | v1.1 | All Part 2 features including delegates and sentiment detection |

### Continuous Integration
GitHub Actions is configured to build the project on each push. A green check mark indicates a successful build.

---

## Video Presentation

[Watch the CyberSafe Part 2 Presentation]()



---

## Marking Criteria Coverage


---

## Author

**Sipho Swartbooi**  
PROG6221 - Programming 2A  
The IIE

---

*This chatbot was developed as part of the POE assessment for Programming 2A.*
