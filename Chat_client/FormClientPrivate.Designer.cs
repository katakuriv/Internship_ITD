namespace Chat_client
{
    partial class FormClientPrivate
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btSend = new System.Windows.Forms.Button();
            this.message = new System.Windows.Forms.TextBox();
            this.history = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btSend
            // 
            this.btSend.Location = new System.Drawing.Point(684, 371);
            this.btSend.Name = "btSend";
            this.btSend.Size = new System.Drawing.Size(104, 40);
            this.btSend.TabIndex = 0;
            this.btSend.Text = "Send";
            this.btSend.UseVisualStyleBackColor = true;
            this.btSend.Click += new System.EventHandler(this.btSend_Click);
            // 
            // message
            // 
            this.message.Location = new System.Drawing.Point(12, 371);
            this.message.Multiline = true;
            this.message.Name = "message";
            this.message.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.message.Size = new System.Drawing.Size(666, 40);
            this.message.TabIndex = 1;
            // 
            // history
            // 
            this.history.Location = new System.Drawing.Point(12, 25);
            this.history.Multiline = true;
            this.history.Name = "history";
            this.history.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.history.Size = new System.Drawing.Size(776, 322);
            this.history.TabIndex = 2;
            // 
            // FormClientPrivate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.history);
            this.Controls.Add(this.message);
            this.Controls.Add(this.btSend);
            this.Name = "FormClientPrivate";
            this.Text = "Private chat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClientPrivate_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btSend;
        private System.Windows.Forms.TextBox message;
        private System.Windows.Forms.TextBox history;
    }
}