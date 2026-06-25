using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace CyberSafeGUI
{
    public class DatabaseHelper
    {
        private string connectionString;

        public DatabaseHelper()
        {
            // Update this with your MySQL credentials
            connectionString = "Server=localhost;Database=cybersafe_db;Uid=root;Pwd=;";
        }

        public bool TestConnection()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public void AddTask(string title, string description, DateTime? reminderDate)
        {
            string query = "INSERT INTO tasks (title, description, reminder_date) VALUES (@title, @desc, @reminder)";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@desc", description);
                    cmd.Parameters.AddWithValue("@reminder", (object)reminderDate ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<TaskItem> GetTasks(bool showCompleted = false)
        {
            List<TaskItem> tasks = new List<TaskItem>();
            string query = "SELECT id, title, description, reminder_date, is_completed FROM tasks";

            if (!showCompleted)
                query += " WHERE is_completed = FALSE";

            query += " ORDER BY created_at DESC";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tasks.Add(new TaskItem
                        {
                            Id = reader.GetInt32("id"),
                            Title = reader.GetString("title"),
                            Description = reader.GetString("description"),
                            ReminderDate = reader.IsDBNull(reader.GetOrdinal("reminder_date")) ? null : reader.GetDateTime("reminder_date"),
                            IsCompleted = reader.GetBoolean("is_completed")
                        });
                    }
                }
            }
            return tasks;
        }

        public void MarkTaskComplete(int taskId)
        {
            string query = "UPDATE tasks SET is_completed = TRUE WHERE id = @id";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", taskId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteTask(int taskId)
        {
            string query = "DELETE FROM tasks WHERE id = @id";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", taskId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReminderDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}