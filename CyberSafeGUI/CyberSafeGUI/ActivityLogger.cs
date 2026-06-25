using System;
using System.Collections.Generic;

namespace CyberSafeGUI
{
    public class ActivityLogger
    {
        private List<string> logEntries;
        private int maxEntries;

        public ActivityLogger()
        {
            logEntries = new List<string>();
            maxEntries = 50; // Keep last 50 entries
        }

        public void Log(string action)
        {
            string entry = $"{DateTime.Now:yyyy-MM-dd HH:mm} - {action}";
            logEntries.Add(entry);

            // Keep only the last maxEntries
            if (logEntries.Count > maxEntries)
                logEntries.RemoveAt(0);
        }

        public List<string> GetRecentLogs(int count = 10)
        {
            if (logEntries.Count == 0)
                return new List<string> { "No activities logged yet." };

            int start = Math.Max(0, logEntries.Count - count);
            return logEntries.GetRange(start, logEntries.Count - start);
        }

        public List<string> GetAllLogs()
        {
            if (logEntries.Count == 0)
                return new List<string> { "No activities logged yet." };
            return new List<string>(logEntries);
        }

        public void ClearLogs()
        {
            logEntries.Clear();
            Log("Activity log cleared.");
        }

        public int GetLogCount() => logEntries.Count;
    }
}