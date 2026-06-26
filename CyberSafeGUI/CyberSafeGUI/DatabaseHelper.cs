using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CyberSafeGUI
{
    public class DatabaseHelper
    {
        private string connectionString;

        public DatabaseHelper()
        {
            // YOUR EXACT CONNECTION STRING FROM SERVER EXPLORER
            connectionString = "Data Source=LabVM2047644\\SQLEXPRESS;Initial Catalog=cyber_tasks_db;User ID=task_user;Password=TaskPass123!;Encrypt=False;";
        }

        public bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DB Error: {ex.Message}");
                return false;
            }
        }

        public void AddTask(string title, string description, DateTime? reminderDate)
        {
            string query = "INSERT INTO tasks (title, description, reminder_date, is_completed) VALUES (@title, @desc, @reminder, 0)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
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
                query += " WHERE is_completed = 0";

            query += " ORDER BY created_at DESC";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TaskItem task = new TaskItem();
                        task.Id = Convert.ToInt32(reader["id"]);
                        task.Title = reader["title"].ToString();
                        task.Description = reader["description"]?.ToString();
                        task.ReminderDate = reader["reminder_date"] == DBNull.Value ? null : (DateTime?)reader["reminder_date"];
                        task.IsCompleted = Convert.ToBoolean(reader["is_completed"]);
                        tasks.Add(task);
                    }
                }
            }
            return tasks;
        }

        public void MarkTaskComplete(int taskId)
        {
            string query = "UPDATE tasks SET is_completed = 1 WHERE id = @id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", taskId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteTask(int taskId)
        {
            string query = "DELETE FROM tasks WHERE id = @id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
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