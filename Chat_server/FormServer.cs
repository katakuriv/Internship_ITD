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

namespace Chat_server
{
    public partial class FormServer : Form
    {
        Socket listener;
        Thread thread;
        private static String request = String.Empty;
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        public static ManualResetEvent sendDone = new ManualResetEvent(false);
        public Dictionary<string, string> listUsers = new Dictionary<string, string>();
        public Dictionary<string, Socket> listClients = new Dictionary<string, Socket>();
        public delegate void updateListConnectUsers(string user);
        public delegate void updateHistory(string msg);

        private void activeServer()
        {
            if (btStart.Text == "Active")
            {
                btStart.Text = "Shut down";
                ipHost.Enabled = false;
                port.Enabled = false;

                if (thread == null)
                {
                    thread = new Thread(startServer);
                }
                else
                {
                    if (!thread.IsAlive)
                    {
                        thread = new Thread(startServer);
                    }
                }

                thread.IsBackground = true;
                thread.Start();
            }
            else
            {
                btStart.Text = "Active";
                ipHost.Enabled = true;
                port.Enabled = true;
                StopServer();
            }
        }

        private void startServer()
        {
            IPAddress ip = IPAddress.Parse(ipHost.Text.Trim());
            int portH = int.Parse(port.Text.Trim());
            IPEndPoint localEndPoint = new IPEndPoint(ip, portH);

            listener = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(103);

                while (true)
                {
                    allDone.Reset();
                    listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
                    allDone.WaitOne();
                }
            }
            catch (Exception ex)
            {
                if (ex is SocketException || thread.IsAlive)
                {
                    thread.Abort();
                }

                btStart.Text = "Active";
                ipHost.Enabled = true;
                port.Enabled = true;
            }
        }

        private void StopServer()
        {
            foreach (KeyValuePair<string, Socket> client in listClients)
            {
                Send(client.Value, "Server stop");
                sendDone.WaitOne();

                client.Value.Close();
            }

            listener.Close();
            listUsers.Clear();
            listClients.Clear();
            history.Clear();
            listConnectUsers.Items.Clear();
            thread.Abort();
        }

        // https://docs.microsoft.com/vi-vn/dotnet/framework/network-programming/asynchronous-server-socket-example

        private void AcceptCallback(IAsyncResult ar)
        {
            allDone.Set();

            try
            {
                Socket listener = (Socket)ar.AsyncState;
                Socket handler = listener.EndAccept(ar);

                StateObject state = new StateObject();
                state.workSocket = handler;
                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        private void splitRequest(string request, Socket handler)
        {
            string content = request.Substring(0, request.IndexOf("<EOF>"));
            string reply = "";

            if (content.Contains("join in: "))
            {
                string user = content.Substring(9);
                string id = Guid.NewGuid().ToString();

                if (listUsers.ContainsKey(user))
                {
                    Send(handler, "Existed user");
                    handler.Close();

                    return;
                }

                addUser(user);
                reply = "join in: ";

                int count = 0;
                int total = listUsers.Count;

                foreach (KeyValuePair<string, string> u in listUsers)
                {
                    reply += u.Key;
                    count += 1;

                    if (count < total)
                    {
                        reply += ",";
                    }
                }

                listUsers.Add(user, id);

                foreach (KeyValuePair<string, Socket> c in listClients)
                {
                    Send(c.Value, "join in: " + user);
                }

                listClients.Add(id, handler);
                editHistory(user + " join in");
            }
            else if (content.Contains("left: "))
            {
                string user = content.Substring(6);
                string id = listUsers[user];

                listUsers.Remove(user);
                delUser(user);
                listClients[id].Close();
                listClients.Remove(id);

                foreach (KeyValuePair<string, Socket> c in listClients)
                {
                    Send(c.Value, "left: " + user);
                }

                editHistory(user + " left");
            }
            else if (content.Contains("Private: "))
            {
                string msg = content.Substring(9);

                Send(listClients[listUsers[msg.Substring(0, msg.IndexOf("-"))]], "Private: " + msg.Substring(msg.IndexOf("-") + 1));
            }
            else
            {
                foreach (KeyValuePair<string, Socket> c in listClients)
                {
                    Send(c.Value, content);
                }

                editHistory(content);
            }

            if (reply != "join in: ")
            {
                Send(handler, reply);
            }
        }

        private void ReadCallback(IAsyncResult ar)
        {
            try
            {
                StateObject state = (StateObject)ar.AsyncState;
                Socket handler = state.workSocket;

                int bytesRead = handler.EndReceive(ar);

                if (bytesRead > 0)
                {
                    state.sb.Append(Encoding.BigEndianUnicode.GetString(state.buffer, 0, bytesRead));
                    request = state.sb.ToString();

                    if (request.IndexOf("<EOF>") > -1)
                    {
                        splitRequest(request, handler);

                        // https://helpex.vn/question/nguyen-nhan-khien-mot-o-cam-duoc-ket-noi-chap-nhan-tin-nhan-moi-ngay-sau-.beginreceive-611d25e93e44cf71f60658ca
                        StateObject newstate = new StateObject();
                        newstate.workSocket = handler;
                        handler.BeginReceive(newstate.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), newstate);
                    }
                    else
                    {
                        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
                    }
                }
            }
            catch (Exception ex)
            {
                StateObject newstate = (StateObject)ar.AsyncState;
                Socket handler = newstate.workSocket;

                if (listClients.ContainsValue(handler))
                {
                    string id = listClients.ToList()[listClients.Values.ToList().IndexOf(handler)].Key;

                    listClients.Remove(id);

                    string name = listUsers.ToList()[listUsers.Values.ToList().IndexOf(id)].Key;

                    listUsers.Remove(name);

                    delUser(name);
                }

                MessageBox.Show(ex.Message);
            }
        }

        private void Send(Socket handler, string data)
        {
            try
            {
                byte[] byteData = Encoding.ASCII.GetBytes(data);

                handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), handler);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket)ar.AsyncState;

                handler.EndSend(ar);
                sendDone.Set();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public FormServer()
        {
            InitializeComponent();
            listUsers = new Dictionary<string, string>();
            listClients = new Dictionary<string, Socket>();
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            activeServer();
        }

        private void FormServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopServer();
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
