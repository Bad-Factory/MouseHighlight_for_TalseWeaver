using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using MouseHighlight.Properties;

namespace MouseHighlight
{
	// Token: 0x0200000F RID: 15
	public partial class AboutForm : Form
	{
		// Token: 0x06000025 RID: 37 RVA: 0x000026C0 File Offset: 0x000008C0
		public AboutForm()
		{
			this.InitializeComponent();
			this.label_ProductName.Text = "Mouse Highlight";
			this.label_Version.Text = "Version 1.0.0.2";
			this.label_Copyright.Text = "Copyright © 2020 Catnip5.com." + Environment.NewLine + "All rights reserved.";
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002718 File Offset: 0x00000918
		private void AboutForm_Shown(object sender, EventArgs e)
		{
			if (this.mIsInit)
			{
				return;
			}
			this.mIsInit = true;
			try
			{
				new SoundPlayer(Resources.lala_Catnip5).Play();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000020D6 File Offset: 0x000002D6
		private void button_OK_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000275C File Offset: 0x0000095C
		private void button_Visit_Click(object sender, EventArgs e)
		{
			string url = "http://www.catnip5.com";
			this.OpenUrl(url);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002778 File Offset: 0x00000978
		private void button_Donate_Click(object sender, EventArgs e)
		{
			string url = "https://catnip5.itch.io/mouse-highlight";
			this.OpenUrl(url);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002794 File Offset: 0x00000994
		private void OpenUrl(string url)
		{
			try
			{
				Process.Start(url);
			}
			catch (Win32Exception ex)
			{
				if (ex.ErrorCode == -2147467259)
				{
					MessageBox.Show(ex.Message);
				}
			}
			catch (Exception ex2)
			{
				MessageBox.Show(ex2.Message);
			}
		}

		// Token: 0x04000029 RID: 41
		private bool mIsInit;
	}
}
