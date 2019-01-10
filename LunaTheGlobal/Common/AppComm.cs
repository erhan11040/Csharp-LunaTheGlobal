using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LunaTheGlobal.Common
{
    public class AppComm
    {
        [DllImport("user32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);



        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(

            string lpClassName,

            string lpWindowName);



        [DllImport("User32.dll")]
        public static extern IntPtr FindWindowEx(

            IntPtr hwndParent,

            IntPtr hwndChildAfter,

            string lpszClass,

            string lpszWindos);



        [DllImport("User32.dll")]
        public static extern Int32 PostMessage(

             IntPtr hWnd,

             int Msg,
             int wParam,
             int lParam);

        public const int WM_KEYDOWN = 0x100;
        public const int WM_KEYUP = 0x101;
       /* private void button1_Click(object sender, EventArgs e)

        {

            // retrieve Notepad main window handle

            IntPtr hWnd = FindWindow("Notepad", "Untitled - Notepad");

            if (!hWnd.Equals(IntPtr.Zero))

            {

                // make the minimized window be normal

                ShowWindowAsync(hWnd, SW_SHOWNORMAL);

                Thread.Sleep(100);

                // retrieve Edit window handle of Notepad

                IntPtr edithWnd = FindWindowEx(hWnd, IntPtr.Zero, "Edit", null);

                if (!edithWnd.Equals(IntPtr.Zero))

                {

                    PostMessage(edithWnd, WM_KEYDOWN, (int)Keys.F5, 0);
                    PostMessage(edithWnd, WM_KEYUP, (int)Keys.F5, 1);

                }

            }
        }*/
    }
}
