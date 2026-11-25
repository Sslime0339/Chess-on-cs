using System;
using System.Runtime.InteropServices;

namespace ConsoleUI
{
    static class ConsoleFontHelper
    {
        private const int STD_OUTPUT_HANDLE = -11;
        private const int TMPF_TRUETYPE = 4;
        private const int LF_FACESIZE = 32;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct CONSOLE_FONT_INFOEX
        {
            public uint cbSize;
            public uint nFont;
            public COORD dwFontSize;
            public int FontFamily;
            public int FontWeight;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = LF_FACESIZE)]
            public string FaceName;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct COORD
        {
            public short X;
            public short Y;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool SetCurrentConsoleFontEx(
            IntPtr consoleOutput,
            bool maximumWindow,
            ref CONSOLE_FONT_INFOEX consoleCurrentFontEx);

        public static void SetConsoleFont(string fontName = "NSimSun", short fontSize = 24)
        {
            // Проверяем, что программа запущена на Windows (чтобы не упасть в Linux/Termux)
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return;

            IntPtr hnd = GetStdHandle(STD_OUTPUT_HANDLE);
            if (hnd == IntPtr.Zero || hnd == new IntPtr(-1))
                return;

            CONSOLE_FONT_INFOEX info = new CONSOLE_FONT_INFOEX();
            info.cbSize = (uint)Marshal.SizeOf(info);
            info.FontFamily = TMPF_TRUETYPE;
            info.FaceName = fontName;
            info.dwFontSize = new COORD { X = 0, Y = fontSize }; // X = 0 подбирает ширину автоматически
            info.FontWeight = 400; // Обычный (не жирный)

            SetCurrentConsoleFontEx(hnd, false, ref info);
        }
    }
}
