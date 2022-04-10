namespace Chat_client
{
    partial class FormClientPublic
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
            this.btJoin = new System.Windows.Forms.Button();
            this.ipHost = new System.Windows.Forms.TextBox();
            this.listConnectUsers = new System.Windows.Forms.ListBox();
            this.port = new System.Windows.Forms.TextBox();
            this.userName = new System.Windows.Forms.TextBox();
            this.message = new System.Windows.Forms.TextBox();
            this.history = new System.Windows.Forms.TextBox();
            this.btSend = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btJoin
            // 
            this.btJoin.Location = new System.Drawing.Point(74, 152);
            this.btJoin.Name = "btJoin";
            this.btJoin.Size = new System.Drawing.Size(105, 23);
            this.btJoin.TabIndex = 0;
            this.btJoin.Text = "Join";
            this.btJoin.UseVisualStyleBackColor = true;
            this.btJoin.Click += new System.EventHandler(this.btJoin_Click);
            // 
            // ipHost
            // 
            this.ipHost.Location = new System.Drawing.Point(78, 28);
            this.ipHost.Name = "ipHost";
            this.ipHost.Size = new System.Drawing.Size(129, 22);
            this.ipHost.TabIndex = 1;
            // 
            // listConnectUsers
            // 
            this.listConnectUsers.FormattingEnabled = true;
            this.listConnectUsers.HorizontalScrollbar = true;
            this.listConnectUsers.ItemHeight = 16;
            this.listConnectUsers.Location = new System.Drawing.Point(653, 12);
            this.listConnectUsers.Name = "listConnectUsers";
            this.listConnectUsers.ScrollAlwaysVisible = true;
            this.listConnectUsers.Size = new System.Drawing.Size(135, 420);
            this.listConnectUsers.TabIndex = 2;
            this.listConnectUsers.DoubleClick += new System.EventHandler(this.listConnectUsers_DoubleClick);
            // 
            // port
            // 
            this.port.Location = new System.Drawing.Point(78, 69);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(129, 22);
            this.port.TabIndex = 3;
            // 
            // userName
            // 
            this.userName.Location = new System.Drawing.Point(74, 111);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(133, 22);
            this.userName.TabIndex = 4;
            // 
            // message
            // 
            this.message.Location = new System.Drawing.Point(15, 190);
            this.message.Multiline = true;
            this.message.Name = "message";
            this.message.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.message.Size = new System.Drawing.Size(192, 213);
            this.message.TabIndex = 5;
            // 
            // history
            // 
            this.history.Location = new System.Drawing.Point(213, 12);
            this.history.Multiline = true;
            this.history.Name = "history";
            this.history.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.history.Size = new System.Drawing.Size(434, 420);
            this.history.TabIndex = 6;
            // 
            // btSend
            // 
            this.btSend.Location = new System.Drawing.Point(46, 409);
            this.btSend.Name = "btSend";
            this.btSend.Size = new System.Drawing.Size(133, 23);
            this.btSend.TabIndex = 7;
            this.btSend.Text = "Send";
            this.btSend.UseVisualStyleBackColor = true;
            this.btSend.Click += new System.EventHandler(this.btSend_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Ip adrres";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "Port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Nickname";
            // 
            // FormClientPublic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btSend);
            this.Controls.Add(this.history);
            this.Controls.Add(this.message);
            this.Controls.Add(this.userName);
            this.Controls.Add(this.port);
            this.Controls.Add(this.listConnectUsers);
            this.Controls.Add(this.ipHost);
            this.Controls.Add(this.btJoin);
            this.Name = "FormClientPublic";
            this.Text = "FormClientPublic";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClientPublic_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btJoin;
        private System.Windows.Forms.TextBox ipHost;
        private System.Windows.Forms.ListBox listConnectUsers;
        private System.Windows.Forms.TextBox port;
        private System.Windows.Forms.TextBox userName;
        private System.Windows.Forms.TextBox message;
        private System.Windows.Forms.TextBox history;
        private System.Windows.Forms.Button btSend;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

