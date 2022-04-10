namespace Chat_server
{
    partial class FormServer
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
            this.ipHost = new System.Windows.Forms.TextBox();
            this.port = new System.Windows.Forms.TextBox();
            this.btStart = new System.Windows.Forms.Button();
            this.history = new System.Windows.Forms.TextBox();
            this.listConnectUsers = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ipHost
            // 
            this.ipHost.Location = new System.Drawing.Point(94, 33);
            this.ipHost.Name = "ipHost";
            this.ipHost.Size = new System.Drawing.Size(118, 22);
            this.ipHost.TabIndex = 0;
            // 
            // port
            // 
            this.port.Location = new System.Drawing.Point(94, 71);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(118, 22);
            this.port.TabIndex = 1;
            // 
            // btStart
            // 
            this.btStart.Location = new System.Drawing.Point(105, 119);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(75, 23);
            this.btStart.TabIndex = 2;
            this.btStart.Text = "Active";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // history
            // 
            this.history.Location = new System.Drawing.Point(250, 49);
            this.history.Multiline = true;
            this.history.Name = "history";
            this.history.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.history.Size = new System.Drawing.Size(318, 362);
            this.history.TabIndex = 4;
            // 
            // listConnectUsers
            // 
            this.listConnectUsers.FormattingEnabled = true;
            this.listConnectUsers.HorizontalScrollbar = true;
            this.listConnectUsers.ItemHeight = 16;
            this.listConnectUsers.Location = new System.Drawing.Point(30, 151);
            this.listConnectUsers.Margin = new System.Windows.Forms.Padding(4);
            this.listConnectUsers.Name = "listConnectUsers";
            this.listConnectUsers.ScrollAlwaysVisible = true;
            this.listConnectUsers.Size = new System.Drawing.Size(186, 260);
            this.listConnectUsers.Sorted = true;
            this.listConnectUsers.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 16);
            this.label2.TabIndex = 11;
            this.label2.Text = "Port";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "Ip adrres";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(373, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 16);
            this.label3.TabIndex = 12;
            this.label3.Text = "History";
            // 
            // FormServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 433);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listConnectUsers);
            this.Controls.Add(this.history);
            this.Controls.Add(this.btStart);
            this.Controls.Add(this.port);
            this.Controls.Add(this.ipHost);
            this.Name = "FormServer";
            this.Text = "Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormServer_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ipHost;
        private System.Windows.Forms.TextBox port;
        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.TextBox history;
        private System.Windows.Forms.ListBox listConnectUsers;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
    }
}

