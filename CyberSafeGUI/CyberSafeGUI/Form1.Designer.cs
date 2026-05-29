namespace CyberSafeGUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            richTextBox1 = new RichTextBox();
            txtChatHistory = new RichTextBox();
            txtUserInput = new TextBox();
            btnSend = new Button();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(0, 0);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(100, 96);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            // 
            // txtChatHistory
            // 
            txtChatHistory.BackColor = Color.Black;
            txtChatHistory.Dock = DockStyle.Fill;
            txtChatHistory.ForeColor = Color.White;
            txtChatHistory.Location = new Point(0, 0);
            txtChatHistory.Name = "txtChatHistory";
            txtChatHistory.Size = new Size(800, 450);
            txtChatHistory.TabIndex = 1;
            txtChatHistory.Text = "";
            txtChatHistory.TextChanged += txtChatHistory_TextChanged;
            // 
            // txtUserInput
            // 
            txtUserInput.Location = new Point(12, 386);
            txtUserInput.Name = "txtUserInput";
            txtUserInput.Size = new Size(776, 23);
            txtUserInput.TabIndex = 2;
            // 
            // btnSend
            // 
            btnSend.BackColor = Color.Cyan;
            btnSend.Dock = DockStyle.Bottom;
            btnSend.Location = new Point(0, 415);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(800, 35);
            btnSend.TabIndex = 3;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = false;
            btnSend.Click += btnSend_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSend);
            Controls.Add(txtUserInput);
            Controls.Add(txtChatHistory);
            Controls.Add(richTextBox1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox richTextBox1;
        private RichTextBox txtChatHistory;
        private TextBox txtUserInput;
        private Button btnSend;
    }
}
