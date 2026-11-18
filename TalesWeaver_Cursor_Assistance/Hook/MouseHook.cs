using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Hook
{
	// Token: 0x0200000B RID: 11
	public class MouseHook
	{
		// Token: 0x06000014 RID: 20
		[DllImport("user32.dll")]
		private static extern IntPtr SetWindowsHookEx(int idHook, MouseHook.LowLevelProc lpfn, IntPtr hMod, uint dwThreadId);

		// Token: 0x06000015 RID: 21
		[DllImport("user32.dll")]
		private static extern bool UnhookWindowsHookEx(IntPtr hhk);

		// Token: 0x06000016 RID: 22
		[DllImport("user32.dll")]
		private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

		// Token: 0x06000017 RID: 23
		[DllImport("kernel32.dll")]
		private static extern IntPtr GetModuleHandle(string lpModuleName);

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000018 RID: 24 RVA: 0x0000248C File Offset: 0x0000068C
		// (remove) Token: 0x06000019 RID: 25 RVA: 0x000024C0 File Offset: 0x000006C0
		public static event MouseEventCallback MouseDown;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600001A RID: 26 RVA: 0x000024F4 File Offset: 0x000006F4
		// (remove) Token: 0x0600001B RID: 27 RVA: 0x00002528 File Offset: 0x00000728
		public static event MouseEventCallback MouseMove;

		// Token: 0x0600001C RID: 28 RVA: 0x0000255C File Offset: 0x0000075C
		private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
		{
			int num = (int)wParam;
			if ((nCode >= 0 && num == 512) || num == 513 || num == 516)
			{
				MouseHook.MSLLHOOKSTRUCT msllhookstruct = Marshal.PtrToStructure<MouseHook.MSLLHOOKSTRUCT>(lParam);
				int x = msllhookstruct.pt.x;
				int y = msllhookstruct.pt.y;
				bool flag = true;
				switch ((int)wParam)
				{
				case 512:
				{
					MouseEventCallback mouseMove = MouseHook.MouseMove;
					flag = (mouseMove == null || mouseMove(MouseEventType.NONE, x, y));
					break;
				}
				case 513:
				{
					MouseEventCallback mouseDown = MouseHook.MouseDown;
					flag = (mouseDown == null || mouseDown(MouseEventType.LEFT, x, y));
					break;
				}
				case 516:
				{
					MouseEventCallback mouseDown2 = MouseHook.MouseDown;
					flag = (mouseDown2 == null || mouseDown2(MouseEventType.RIGHT, x, y));
					break;
				}
				}
				if (!flag)
				{
					return (IntPtr)1;
				}
			}
			return MouseHook.CallNextHookEx(MouseHook._hookID, nCode, wParam, lParam);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002634 File Offset: 0x00000834
		public static bool HookStart()
		{
			bool result;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				using (currentProcess.MainModule)
				{
					IntPtr moduleHandle = MouseHook.GetModuleHandle("user32");
					MouseHook._proc = new MouseHook.LowLevelProc(MouseHook.HookCallback);
					MouseHook._hookID = MouseHook.SetWindowsHookEx(14, MouseHook._proc, moduleHandle, 0U);
					result = (MouseHook._hookID != IntPtr.Zero);
				}
			}
			return result;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000020BD File Offset: 0x000002BD
		public static void HookClose()
		{
			MouseHook.UnhookWindowsHookEx(MouseHook._hookID);
		}

		// Token: 0x0400001A RID: 26
		internal const int WH_MOUSE_LL = 14;

		// Token: 0x0400001B RID: 27
		internal const int WM_LBUTTONDOWN = 513;

		// Token: 0x0400001C RID: 28
		internal const int WM_MOUSEMOVE = 512;

		// Token: 0x0400001D RID: 29
		internal const int WM_RBUTTONDOWN = 516;

		// Token: 0x0400001E RID: 30
		private static IntPtr _hookID = IntPtr.Zero;

		// Token: 0x0400001F RID: 31
		private static MouseHook.LowLevelProc _proc;

		// Token: 0x0200000C RID: 12
		public struct POINT
		{
			// Token: 0x04000022 RID: 34
			public int x;

			// Token: 0x04000023 RID: 35
			public int y;
		}

		// Token: 0x0200000D RID: 13
		public struct MSLLHOOKSTRUCT
		{
			// Token: 0x04000024 RID: 36
			public MouseHook.POINT pt;

			// Token: 0x04000025 RID: 37
			public uint mouseData;

			// Token: 0x04000026 RID: 38
			public uint flags;

			// Token: 0x04000027 RID: 39
			public uint time;

			// Token: 0x04000028 RID: 40
			public IntPtr dwExtraInfo;
		}

		// Token: 0x0200000E RID: 14
		// (Invoke) Token: 0x06000022 RID: 34
		private delegate IntPtr LowLevelProc(int nCode, IntPtr wParam, IntPtr lParam);
	}
}
