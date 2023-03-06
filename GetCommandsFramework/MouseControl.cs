using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GetCommandsFramework
{
    public static class MouseControl
    {
        [DllImport("user32.dll")]
        public static extern long SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref POINT point);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dsFlags, int dx, int dy, int cButtons, int dsExtraInfo);

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;
        public const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        public const int MOUSEEVENTF_RIGHTUP = 0x10;

        public static void DoMouseLeftClick(int x, int y)
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, x, y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, x, y, 0, 0);

        }

        private static void DoMouseRightClick(int x, int y)
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN, x, y, 0, 0);
            mouse_event(MOUSEEVENTF_RIGHTUP, x, y, 0, 0);
        }

        private static void DoMouseDoubleLeftClick(int x, int y)
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, x, y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, x, y, 0, 0);

            mouse_event(MOUSEEVENTF_LEFTDOWN, x, y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, x, y, 0, 0);
        }

        public static void MoveMouse(int horizontal, int vertical)
        {
            POINT p = new POINT();


            p.x = Convert.ToInt16(Cursor.Position.X + horizontal);
            p.y = Convert.ToInt16(Cursor.Position.Y + vertical);
            IntPtr ptr = IntPtr.Zero;

            ClientToScreen(ptr, ref p);

            SetCursorPos(p.x, p.y);

        }

        public static void OpenFind()
        {
            POINT p = new POINT();

            POINT withprocents = new POINT();

            withprocents.x = Screen.PrimaryScreen.Bounds.Width / 100 * 10;
            p.x = Cursor.Position.X;
            p.y = Cursor.Position.Y;
            IntPtr ptr = IntPtr.Zero;
            ClientToScreen(ptr, ref p);
            SetCursorPos(withprocents.x, 850);

            DoMouseLeftClick(withprocents.x, 850);

            SetCursorPos(p.x, p.y);
        }
    }

 

}
