using System;
using System.Windows.Forms;

namespace CyberSafeGUI
{
    public partial class Form1 : Form
    {
        private ChatbotGUI chatbot;

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            this.btnSend.Click += btnSend_Click;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chatbot = new ChatbotGUI(txtChatHistory, txtUserInput);
            chatbot.Start();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string input = txtUserInput.Text.Trim();

            if (!string.IsNullOrEmpty(input))
            {
                chatbot.ProcessUserInput(input);
                txtUserInput.Clear();
                txtUserInput.Focus();
            }
        }
    }
}