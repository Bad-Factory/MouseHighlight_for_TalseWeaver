using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

// Token: 0x02000008 RID: 8
internal partial class GdiForm : Form
{
	// Token: 0x0600000D RID: 13 RVA: 0x00002078 File Offset: 0x00000278
	public GdiForm()
	{
		base.FormBorderStyle = FormBorderStyle.None;
		base.TopMost = true;
		base.ShowInTaskbar = false;
		base.ControlBox = false;
		this.AllowDrop = false;
	}

	// Token: 0x0600000E RID: 14 RVA: 0x00002364 File Offset: 0x00000564
	public void SetBitmap(Bitmap bitmap, byte opacity)
	{
		if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
		{
			throw new ApplicationException("The bitmap must be 32ppp with alpha-channel.");
		}
		IntPtr dc = Win32.GetDC(IntPtr.Zero);
		IntPtr intPtr = Win32.CreateCompatibleDC(dc);
		IntPtr intPtr2 = IntPtr.Zero;
		IntPtr hObject = IntPtr.Zero;
		try
		{
			intPtr2 = bitmap.GetHbitmap(Color.FromArgb(0));
			hObject = Win32.SelectObject(intPtr, intPtr2);
			Win32.Size size = new Win32.Size(bitmap.Width, bitmap.Height);
			Win32.Point point = new Win32.Point(0, 0);
			Win32.Point point2 = new Win32.Point(base.Left, base.Top);
			Win32.BLENDFUNCTION blendfunction = default(Win32.BLENDFUNCTION);
			blendfunction.BlendOp = 0;
			blendfunction.BlendFlags = 0;
			blendfunction.SourceConstantAlpha = opacity;
			blendfunction.AlphaFormat = 1;
			Win32.UpdateLayeredWindow(base.Handle, dc, ref point2, ref size, intPtr, ref point, 0, ref blendfunction, 2);
			Win32.SetWindowLong(base.Handle, -20, (int)Win32.GetWindowLong(base.Handle, -20) | 32);
		}
		finally
		{
			Win32.ReleaseDC(IntPtr.Zero, dc);
			if (intPtr2 != IntPtr.Zero)
			{
				Win32.SelectObject(intPtr, hObject);
				Win32.DeleteObject(intPtr2);
			}
			Win32.DeleteDC(intPtr);
		}
	}

	// Token: 0x17000001 RID: 1
	// (get) Token: 0x0600000F RID: 15 RVA: 0x000020A3 File Offset: 0x000002A3
	protected override CreateParams CreateParams
	{
		get
		{
			CreateParams createParams = base.CreateParams;
			createParams.ExStyle |= 524288;
			return createParams;
		}
	}
}
