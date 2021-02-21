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


namespace WindowsFormsApp6
{
    public partial class Form1 : Form
    {
        static int port = 8005;
        static string adress = "127.0.0.1";
        public Form1()
        {
            InitializeComponent();
       
            
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(adress), port);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipPoint);
                string strData = dateTimePicker1.ToString() + "," + numericUpDown1.Value.ToString(); 
                byte[] data = Encoding.Unicode.GetBytes(strData);
                socket.Send(data);
                data = new byte[256];
                StringBuilder builder = new StringBuilder();
                int bytes = 0;
                do
                {
                    bytes = socket.Receive(data, data.Length, 0);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (socket.Available > 0);
                MessageBox.Show("ответ сервера: " + builder.ToString());
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

    }
}
