using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace MouseHighlight
{
	// Token: 0x02000011 RID: 17
	internal static class Program
	{
        // Token: 0x0600004E RID: 78
        [DllImport("shcore.dll", SetLastError = true, EntryPoint = "SetProcessDpiAwareness")]
        [return: MarshalAs(UnmanagedType.U4)]
        public static extern int SetProcessDpiAwareness(Program.PROCESS_DPI_AWARENESS value);
        [DllImport("user32.dll", SetLastError = true)]
		public static extern Program.DPI_AWARENESS_CONTEXT SetThreadDpiAwarenessContext(Program.DPI_AWARENESS_CONTEXT value);

		// Token: 0x06000050 RID: 80 RVA: 0x00005078 File Offset: 0x00003278
		[STAThread]
		private static void Main()
		{
			if (Environment.OSVersion.Version.Major >= 6)
			{
				Program.SetProcessDpiAwareness(Program.PROCESS_DPI_AWARENESS.PROCESS_PER_MONITOR_DPI_AWARE);
			}
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			string name = "MouseHighlight";
			bool flag;
			new Mutex(true, name, out flag);
			if (!flag)
			{
				MessageBox.Show("It is already running.");
				return;
			}
			Application.Run(new Form1());
		}

		// Token: 0x02000012 RID: 18
		public enum PROCESS_DPI_AWARENESS
		{
			// Token: 0x04000075 RID: 117
			PROCESS_DPI_UNAWARE,
			// Token: 0x04000076 RID: 118
			PROCESS_SYSTEM_DPI_AWARE,
			// Token: 0x04000077 RID: 119
			PROCESS_PER_MONITOR_DPI_AWARE
		}

		// Token: 0x02000013 RID: 19
		public enum DPI_AWARENESS_CONTEXT
		{
			// Token: 0x04000079 RID: 121
			DPI_AWARENESS_CONTEXT_DEFAULT,
			// Token: 0x0400007A RID: 122
			DPI_AWARENESS_CONTEXT_UNAWARE = -1,
			// Token: 0x0400007B RID: 123
			DPI_AWARENESS_CONTEXT_SYSTEM_AWARE = -2,
			// Token: 0x0400007C RID: 124
			DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE = -3,
			// Token: 0x0400007D RID: 125
			DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2 = -4
		}
	}
}
