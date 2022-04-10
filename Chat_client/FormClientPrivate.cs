using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat_client
{
    public partial class FormClientPrivate : Form
    {
        private string oppUser;
        private FormClientPublic formPublic;
        public delegate void editHistory(string msg);

        public void addMessage(string msg)
        {
            if (history.InvokeRequired)
            {
                history.Invoke(new editHistory(addMessage), new object[] { msg });
            }
            else
            {
                history.AppendText(msg);
                history.AppendText(Environment.NewLine);
            }
        }

        public void sendMessage()
        {
            if (message.Text != "")
            {
                addMessage("You: " + message.Text);
                formPublic.sendPrivate(oppUser, message.Text);
                message.Clear();
            }
        }

        public FormClientPrivate()
        {
            InitializeComponent();
        }

        public FormClientPrivate(FormClientPublic form, string opp)
        {
            InitializeComponent();
            oppUser = opp;
            formPublic = form;
            this.Text += " with " + oppUser;
        }

        private void btSend_Click(object sender, EventArgs e)
        {
            sendMessage();
        }

        private void FormClientPrivate_FormClosing(object sender, FormClosingEventArgs e)
        {
            formPublic.delOppPrivate(oppUser);
        }
    }
}
