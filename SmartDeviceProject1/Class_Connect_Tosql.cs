using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlTypes;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Threading;


namespace SmartDeviceProject1
{
    

    
    class Class_Connect_Tosql
    {   
        string server = "MGK_LAPTOP";//SERVER HOST NAME
        string dtb = "marin";//DATABASE
        //string usid;//SERVER USER ID
        string psw;//PASSWORD
        string tab = "WHMUSERS";
       // string macdress = "XX:XX:XX:XX:XX:XX";//MAC address of the device
        string sConnection;
        bool state= true;//state bool to prevent overflow of many threads
        string received;
        int receivedint;
        string s;
        bool b;

        public void connect()
        {
            while (true)
            {
                if (this.state)
                {
                    this.checkformessage();
                }
                Thread.Sleep(10000);

            }

        }

       /*

        void connectntake()//void task to connect to the sql and take the message+boolean field
        {
            //connec cnt = new connec();//create an instance of a class to have access to the objects-functions of connec class so that i can get params from config file
           //  cnt.xrdfmxml();  
            this.sConnection = "Data Source=" + this.server + ",1433;Initial Catalog=" + this.dtb + ";User ID=sa;Password=actae1234!@#$;";//  BUILD THE CONNECTION STRING
            string sSQL = "SELECT [pick] FROM " + this.tab ;//BUILD THE QUERRY
            SqlCommand comm = new SqlCommand(sSQL, new SqlConnection(this.sConnection));//START AN INSTANCE OF SQL COMMAND HAVING THE QUERRY AND THE CONNECTION STRING
            Form1 frm = new Form1();
          
         try
            {
                comm.Connection.Open();//TRY TO OPEN THE CONNECTION 
                SqlDataReader dr = comm.ExecuteReader();//PARSE DATA TO A DATAREADER
                dr.Read();//read data
                
                
              
              this.s=dr.GetString(0).ToString();//read the 1ST field of the SELECT querry which is the message for the user
              
            
                  frm.Shmessg(this.s);
                  Thread.Sleep(1000);
                  this.updrcms();//start a task to put parameter for received message
                  Thread.Sleep(100);
               
             
                frm = null;
                dr.Close();
                dr = null;
            }
            catch (Exception)//IN CASE IT CAN NOT OPEN THE CONNECTION
            {   
               
                frm.Shmessg("Error connecting to SQL in connecttake");//SHOW THE ERROR MESSAGE TO THE APP USER

                Thread.Sleep(500);

                frm = null;
            }
           
            comm.Connection.Close();
            if (comm.Connection.State != ConnectionState.Closed)
            { this.state = false; }
            comm.Dispose();
            comm = null;
            sSQL = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            
            Thread.Sleep(500);
        }


        */






        void updrcms()//open a new sql connection and update rcms bool value to 0-false so that will not show the message again
        {
            SqlCommand comma = new SqlCommand("UPDATE    "+this.tab+"   SET PICK=0   ", new SqlConnection(this.sConnection));

            comma.Connection.Open();

            comma.ExecuteNonQuery();

            comma.Connection.Close();

            comma.Dispose();

            comma = null;

            

        }

        void checkformessage()//task for checking for messages for the user 
        {
            this.sConnection = "Data Source=" + this.server 
                + ";Initial Catalog=" + this.dtb +
                ";User ID=sa;Password=actae1234!@#$;";//  BUILD THE CONNECTION STRING
            string sSQL = "SELECT [pick] FROM " + this.tab ;//BUILD THE QUERRY
            Form1 frm = new Form1();
            SqlCommand commn = new SqlCommand(sSQL, new SqlConnection(this.sConnection));//START AN INSTANCE OF SQL COMMAND HAVING THE QUERRY AND THE CONNECTION STRING
            try
            {


                commn.Connection.Open();
                this.s = commn.Connection.State.ToString();
               
                SqlDataReader dr = commn.ExecuteReader();//PARSE DATA TO A DATAREADER
               
                dr.Read();
                this.receivedint = dr.GetInt32(0);
               //check the pick parameter to show the new message or not to the user
                if (this.receivedint == 1)
                {
                    frm.Shmessg("You have a picking order");
                    updrcms();
                }

               


            }
            catch (Exception)//IN CASE IT CAN NOT OPEN THE CONNECTION
            {
               
                frm.Shmessg("Error connecting to SQL in check for msg");//SHOW THE ERROR MESSAGE TO THE APP USER

                Thread.Sleep(500);

                frm = null;
            }

            commn.Connection.Close();
          
            commn.Dispose();
            commn = null;
            sSQL = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            
            


        }








    }
}
