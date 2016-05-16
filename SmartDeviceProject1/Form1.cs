using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;


namespace SmartDeviceProject1
{
    public partial class Form1 : Form
    {
        public bool Stop = false;
        Socket sck;//declare the socket
        EndPoint epLocal, epRemote;
        public string localhostname;
        Readfromexml rfxml;
        
        public Form1()
        {
            InitializeComponent();
            button1.Text = "Start";
           
         

        }
        private void initsocket()//initialize the socket communication
        {
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private String getlocalip()//get the local ip of the computer...
        {
            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());
            localhostname = Dns.GetHostName();
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)//if it is AddressFamily ip version 4 (INternetwork family address type)
                {
                    
                    return ip.ToString();


                }

            }

            return "127.0.0.1";


        }
        private String getremoteip()//instatiate server ip
        {
            return rfxml.serverip;
            //return "192.168.1.21";
        }

        private void MessageCallback(IAsyncResult aResult) //variable iasync result 
        {
            try
            {
                if (Stop)
                { return; }

                int size = sck.EndReceiveFrom(aResult, ref epRemote);//stop reading from the socket
                if (size > 0)//checks if there is information on network or not
                {
                    byte[] receiveddata = new byte[1];
                    receiveddata = (byte[])aResult.AsyncState;//contains the info from the IASYNC connection
                    if (receiveddata[0] == 254)
                    {
                        MessageBox.Show("Received");
                    }



                }
                byte[] buffer = new byte[1];
                sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(MessageCallback), buffer);
                //start reading from the socket asynchronously


            }
            catch (Exception exp)
            {
                MessageBox.Show("ERROR IN CALLBACK FUNCTION :" + exp.ToString());

            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                rfxml = new Readfromexml("config.txt");//start reading from config.xml and pass data to parameters
                if (rfxml.error != null) MessageBox.Show(rfxml.error);//if there is error show it to a message box
                if (rfxml.formenabled == "00" || rfxml.formenabled == "0" || rfxml.formenabled == "NO") this.Visible = false;
                initsocket();
                epLocal = new IPEndPoint(IPAddress.Parse(getlocalip()), Convert.ToInt32(rfxml.port));
                label1.Text = label1.Text + "\n" + getlocalip();
                sck.Bind(epLocal);//bind socket to the local ip and port
                label1.Text = label1.Text + "\n" + "Bind it";
                epRemote = new IPEndPoint(IPAddress.Parse(getremoteip()), Convert.ToInt32(rfxml.port));
                label1.Text = label1.Text + "\n" + getremoteip();
                sck.Connect(epRemote);
                label1.Text = label1.Text + "\n" + "Connected";
                button1.Enabled = false;
                byte[] buffer = new byte[1];
                sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(MessageCallback), buffer);
            }
            catch (Exception exp)
            {
                MessageBox.Show("ERROR IN STARTING:" + exp.ToString());

            }
     

        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] msg = new byte[1];
            msg[0] = 254;
            sck.Send(msg);
           

        }

        
    }
}