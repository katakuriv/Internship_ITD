using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Chat_client
{
    public partial class FormClientPublic : Form
    {

        Socket client;
        private Dictionary<string, FormClientPrivate> listPrivate;
        private static ManualResetEvent connectDone = new ManualResetEvent(false);
        private static ManualResetEvent sendDone = new ManualResetEvent(false);
        public delegate void delListUsers();
        public delegate void updateListConnectUsers(string user);
        public delegate void updateHistory(string text);
        private static String response = String.Empty;

        private void connectServer()
        {
            if (btJoin.Text == "Join")
            {
                try
                {
                    btJoin.Text = "Left";

                    IPAddress ip = IPAddress.Parse(ipHost.Text.Trim());
                    IPEndPoint remoteEP = new IPEndPoint(ip, int.Parse(port.Text.Trim()));

                    client = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), client);
                    connectDone.WaitOne();

                    if (userName.Text == "")
                    {
                        MessageBox.Show("Missing username");
                        disconnectServer();
                        return;
                    }

                    editHistory("Conservation of chat room " + ipHost.Text);
                    Send(client, "join in: " + userName.Text + "<EOF>");
                    sendDone.WaitOne();

                    Receive(client);
                }
                catch (Exception ex)
                {
                    btJoin.Text = "Join";
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                Send(client, "left: " + userName.Text + "<EOF>");
                sendDone.WaitOne();
                disconnectServer();
            }
        }

        private void clearListUsers()
        {
            if (listConnectUsers.InvokeRequired)
            {
                listConnectUsers.Invoke(new delListUsers(clearListUsers), null);
            }
            else
            {
                listConnectUsers.Items.Clear();
            }
        }

        private void disconnectServer()
        {
            editHistory("Left out of room chat " + ipHost.Text);
            connectDone.WaitOne();

            if (client != null && client.Connected)
            {
                client.Close();
            }

            clearListUsers();
            btJoin.Text = "Join";
            message.Clear();

            foreach (KeyValuePair<string, FormClientPrivate> p in listPrivate)
            {
                p.Value.Close();
            }
            
            listPrivate.Clear();
        }

        // https://docs.microsoft.com/vi-vn/dotnet/framework/network-programming/asynchronous-client-socket-example

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;

                client.EndConnect(ar);
                connectDone.Set();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                disconnectServer();
            }
        }

        private void Send(Socket client, String data)
        {
            try
            {
                byte[] byteData = Encoding.BigEndianUnicode.GetBytes(data);

                client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), client);
            }
            catch (Exception ex)
            {
                disconnectServer();
                MessageBox.Show(ex.Message);
            }

        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;

                int bytesSent = client.EndSend(ar);
                sendDone.Set();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                disconnectServer();
            }
        }

        private void addUser(string user)
        {
            if (listConnectUsers.InvokeRequired)
            {
                listConnectUsers.Invoke(new updateListConnectUsers(addUser), new object[] { user });
            }
            else
            {
                listConnectUsers.Items.Add(user);
            }
        }

        private void delUser(string user)
        {
            if (listConnectUsers.InvokeRequired)
            {
                listConnectUsers.Invoke(new updateListConnectUsers(delUser), new object[] { user });
            }
            else
            {
                listConnectUsers.Items.Remove(user);
            }
        }

        private void editHistory(string message)
        {
            if (history.InvokeRequired)
            {
                history.Invoke(new updateHistory(editHistory), new object[] { message });
            }
            else
            {
                history.AppendText(message);
                history.AppendText(Environment.NewLine);
            }
        }

        private void splitResponse()
        {
            if (response == "Existed user")
            {
                MessageBox.Show("The user you entered in already exists");
                disconnectServer();

                return;
            }
            else if (response == "Server stop")
            {
                disconnectServer();
                return;
            }
            else if (response.Contains("join in: ") && response.IndexOf("join in: ") == 0)
            {
                string temp = response.Substring(9);
                int index = temp.IndexOf(",");

                while (index > -1)
                {
                    string user = temp.Substring(0, index);

                    if (user != userName.Text)
                    {
                        addUser(user);
                    }

                    temp = temp.Substring(index + 1);
                    index = temp.IndexOf(",");
                }

                if (temp != userName.Text)
                {
                    addUser(temp);
                }
            }
            else if (response.Contains("left: ") && response.IndexOf("left: ") == 0)
            {
                string user = response.Substring(6);
                delUser(user);
            }
            else if (response.Contains("Private: "))
            {
                string temp = response.Substring(9);
                string opp = temp.Substring(0, temp.IndexOf(": "));
                
                if (!listPrivate.ContainsKey(opp))
                {
                    FormClientPrivate p = new FormClientPrivate(this, opp);

                    listPrivate.Add(opp, p);
                    p.addMessage(temp);
                    p.ShowDialog();
                }
                else
                {
                    listPrivate[opp].addMessage(temp);
                }
            }
            else
            {
                editHistory(response);
            }
        }

        private void Receive(Socket client)
        {
            try
            {
                StateObject state = new StateObject();

                state.workSocket = client;
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                disconnectServer();
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            { 
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;
                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                    response = state.sb.ToString();
                    splitResponse();
                    state.sb.Clear();
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                }
                else
                {
                    if (state.sb.Length > 1)
                    {
                        response = state.sb.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                disconnectServer();
                MessageBox.Show(ex.Message);
            }
        }

        public void delOppPrivate(string opp)
        {
            listPrivate.Remove(opp);
        }

        public void sendPrivate(string opp, string msg)
        {
            Send(client, "Private: " + opp + "-" + userName.Text + ": " + msg + "<EOF>");
        }

        private void startPrivate()
        {
            if (listConnectUsers.SelectedItem != null)
            {
                string opp = listConnectUsers.SelectedItem.ToString();

                if (!listPrivate.ContainsKey(opp))
                {
                    FormClientPrivate p = new FormClientPrivate(this, opp);

                    listPrivate.Add(opp, p);
                    p.ShowDialog();
                }
            }
        }

        public FormClientPublic()
        {
            InitializeComponent();
            listPrivate = new Dictionary<string, FormClientPrivate>();
        }

        private void btJoin_Click(object sender, EventArgs e)
        {
            connectServer();
        }

        private void btSend_Click(object sender, EventArgs e)
        {
            if (client != null)
            {
                Send(client, userName.Text + ":" + message.Text.Trim() + "<EOF>");
                sendDone.WaitOne();
                message.Clear();
            }
        }

        private void listConnectUsers_DoubleClick(object sender, EventArgs e)
        {
            startPrivate();
            sendDone.WaitOne();
        }

        private void FormClientPublic_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client != null)
            {
                Send(client, "left: " + userName.Text + "<EOF>");
                sendDone.WaitOne();
            }
        }
    }

    public class StateObject
    {
        public const int BufferSize = 1024;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder sb = new StringBuilder();
        public Socket workSocket = null;
    }
}
