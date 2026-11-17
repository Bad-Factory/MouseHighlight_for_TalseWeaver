namespace MouseHighlight
{
	// Token: 0x0200000F RID: 15
	public partial class AboutForm : global::System.Windows.Forms.Form
	{
		// Token: 0x0600002B RID: 43 RVA: 0x000020DE File Offset: 0x000002DE
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000027F0 File Offset: 0x000009F0
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MouseHighlight.AboutForm));
			this.panel_Top = new global::System.Windows.Forms.Panel();
			this.pictureBox1 = new global::System.Windows.Forms.PictureBox();
			this.panel_main = new global::System.Windows.Forms.Panel();
			this.button_Visit = new global::System.Windows.Forms.Button();
			this.button_OK = new global::System.Windows.Forms.Button();
			this.label_Copyright = new global::System.Windows.Forms.Label();
			this.label_ProductName = new global::System.Windows.Forms.Label();
			this.label_Version = new global::System.Windows.Forms.Label();
			this.panel_Top.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
			this.panel_main.SuspendLayout();
			base.SuspendLayout();
			this.panel_Top.Controls.Add(this.pictureBox1);
			this.panel_Top.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.panel_Top.Font = new global::System.Drawing.Font("Segoe UI", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.panel_Top.Location = new global::System.Drawing.Point(0, 0);
			this.panel_Top.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panel_Top.Name = "panel_Top";
			this.panel_Top.Size = new global::System.Drawing.Size(254, 133);
			this.panel_Top.TabIndex = 0;
			this.pictureBox1.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.None;
			this.pictureBox1.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("pictureBox1.Image");
			this.pictureBox1.Location = new global::System.Drawing.Point(38, 17);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new global::System.Drawing.Size(173, 114);
			this.pictureBox1.SizeMode = global::System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.panel_main.Controls.Add(this.button_Visit);
			this.panel_main.Controls.Add(this.button_OK);
			this.panel_main.Controls.Add(this.label_Copyright);
			this.panel_main.Controls.Add(this.label_ProductName);
			this.panel_main.Controls.Add(this.label_Version);
			this.panel_main.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.panel_main.Font = new global::System.Drawing.Font("Segoe UI", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.panel_main.Location = new global::System.Drawing.Point(0, 133);
			this.panel_main.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panel_main.Name = "panel_main";
			this.panel_main.Size = new global::System.Drawing.Size(254, 250);
			this.panel_main.TabIndex = 1;
			this.button_Visit.Location = new global::System.Drawing.Point(12, 124);
			this.button_Visit.Name = "button_Visit";
			this.button_Visit.Size = new global::System.Drawing.Size(230, 28);
			this.button_Visit.TabIndex = 4;
			this.button_Visit.Text = "Visit www.Catnip5.com";
			this.button_Visit.UseCompatibleTextRendering = true;
			this.button_Visit.UseVisualStyleBackColor = true;
			this.button_Visit.Click += new global::System.EventHandler(this.button_Visit_Click);
			this.button_OK.Location = new global::System.Drawing.Point(165, 205);
			this.button_OK.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.button_OK.Name = "button_OK";
			this.button_OK.Size = new global::System.Drawing.Size(77, 31);
			this.button_OK.TabIndex = 3;
			this.button_OK.Text = "OK";
			this.button_OK.UseCompatibleTextRendering = true;
			this.button_OK.UseVisualStyleBackColor = true;
			this.button_OK.Click += new global::System.EventHandler(this.button_OK_Click);
			this.label_Copyright.AutoSize = true;
			this.label_Copyright.Font = new global::System.Drawing.Font("Segoe UI", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label_Copyright.Location = new global::System.Drawing.Point(12, 62);
			this.label_Copyright.Name = "label_Copyright";
			this.label_Copyright.Size = new global::System.Drawing.Size(49, 27);
			this.label_Copyright.TabIndex = 2;
			this.label_Copyright.Text = "label3";
			this.label_Copyright.UseCompatibleTextRendering = true;
			this.label_ProductName.AutoSize = true;
			this.label_ProductName.Font = new global::System.Drawing.Font("Segoe UI", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label_ProductName.Location = new global::System.Drawing.Point(12, 6);
			this.label_ProductName.Name = "label_ProductName";
			this.label_ProductName.Size = new global::System.Drawing.Size(49, 27);
			this.label_ProductName.TabIndex = 1;
			this.label_ProductName.Text = "label2";
			this.label_ProductName.UseCompatibleTextRendering = true;
			this.label_Version.AutoSize = true;
			this.label_Version.Font = new global::System.Drawing.Font("Segoe UI", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label_Version.Location = new global::System.Drawing.Point(12, 34);
			this.label_Version.Name = "label_Version";
			this.label_Version.Size = new global::System.Drawing.Size(49, 27);
			this.label_Version.TabIndex = 0;
			this.label_Version.Text = "label1";
			this.label_Version.UseCompatibleTextRendering = true;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(96f, 96f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoScroll = true;
			this.AutoSize = true;
			base.ClientSize = new global::System.Drawing.Size(254, 383);
			base.ControlBox = false;
			base.Controls.Add(this.panel_main);
			base.Controls.Add(this.panel_Top);
			this.Font = new global::System.Drawing.Font("Segoe UI", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "AboutForm";
			this.Text = "About";
			base.Shown += new global::System.EventHandler(this.AboutForm_Shown);
			this.panel_Top.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
			this.panel_main.ResumeLayout(false);
			this.panel_main.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x0400002A RID: 42
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400002B RID: 43
		private global::System.Windows.Forms.Panel panel_Top;

		// Token: 0x0400002C RID: 44
		private global::System.Windows.Forms.Panel panel_main;

		// Token: 0x0400002D RID: 45
		private global::System.Windows.Forms.Label label_Copyright;

		// Token: 0x0400002E RID: 46
		private global::System.Windows.Forms.Label label_ProductName;

		// Token: 0x0400002F RID: 47
		private global::System.Windows.Forms.Label label_Version;

		// Token: 0x04000030 RID: 48
		private global::System.Windows.Forms.Button button_OK;

		// Token: 0x04000031 RID: 49
		private global::System.Windows.Forms.Button button_Visit;

		// Token: 0x04000032 RID: 50
		private global::System.Windows.Forms.PictureBox pictureBox1;
	}
}
