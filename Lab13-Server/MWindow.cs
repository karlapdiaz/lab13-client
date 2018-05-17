using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab13_Server
{
    public partial class MWindow : Form
    {
        Connection c;
        Logger l;

        public MWindow()
        {
            InitializeComponent();
            var th = new Thread(Connect);
            th.Start();
        }

        public void Connect()
        {
            try
            {
                c = new Connection();
                while (true)
                {
                    
                    c.Connect();
                    if (c.State())
                    {
                        c.Send("Enviando Procesos");
                        c.Listen();                       
                        ProcessClient process = new ProcessClient();
                        System.Diagnostics.Process[] p = process.ListAllApplications();

                        foreach (Process theprocess in p)
                        {
                            //c.Send(theprocess.ProcessName+" "+theprocess.Id);
                            c.Send(theprocess.ProcessName);
                            c.Listen();
                        }
                        c.Send("FIN");                        
                        string input = c.Listen();                        
                        process.killProcess(input);
                        //break;
                    }
                    else
                    {
                        Thread.Sleep(10000);
                    }
                    
                }

            }
            catch (Exception e)
            {
                //Debug.Print(e.ToString());
            }
        }        

        private void Email()
        {
            String ms = time.Text;
            int t = Int32.Parse(ms);
            int i = 0;
            while (true)
            {
                Thread.Sleep(t);
                String email = mailaddr.Text;
                l.Close();
                Mail mail = new Mail();
                mail.sendMail(i,email);
                i++;
                Debug.Print("CORREO ENVIADO");
            }
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            l = new Logger(richTextBox1,this);
            button1.Enabled = false;
            time.Enabled = false;
            mailaddr.Enabled = false;
            var th = new Thread(Email);
            th.Start();
        }


        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();

            
        }
    }
}
