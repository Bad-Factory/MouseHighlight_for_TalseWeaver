using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MouseHighlight.Properties
{
	// Token: 0x02000015 RID: 21
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.6.0.0")]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002217 File Offset: 0x00000417
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000059 RID: 89 RVA: 0x0000221E File Offset: 0x0000041E
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00002230 File Offset: 0x00000430
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("30")]
		public int HighlightSize
		{
			get
			{
				return (int)this["HighlightSize"];
			}
			set
			{
				this["HighlightSize"] = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002243 File Offset: 0x00000443
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002255 File Offset: 0x00000455
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("127")]
		public byte HighlightOpacity
		{
			get
			{
				return (byte)this["HighlightOpacity"];
			}
			set
			{
				this["HighlightOpacity"] = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002268 File Offset: 0x00000468
		// (set) Token: 0x0600005E RID: 94 RVA: 0x0000227A File Offset: 0x0000047A
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("0")]
		public int HighlightColor
		{
			get
			{
				return (int)this["HighlightColor"];
			}
			set
			{
				this["HighlightColor"] = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600005F RID: 95 RVA: 0x0000228D File Offset: 0x0000048D
		// (set) Token: 0x06000060 RID: 96 RVA: 0x0000229F File Offset: 0x0000049F
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("True")]
		public bool ClickEnable
		{
			get
			{
				return (bool)this["ClickEnable"];
			}
			set
			{
				this["ClickEnable"] = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000022B2 File Offset: 0x000004B2
		// (set) Token: 0x06000062 RID: 98 RVA: 0x000022C4 File Offset: 0x000004C4
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("20")]
		public int ClickSize
		{
			get
			{
				return (int)this["ClickSize"];
			}
			set
			{
				this["ClickSize"] = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000022D7 File Offset: 0x000004D7
		// (set) Token: 0x06000064 RID: 100 RVA: 0x000022E9 File Offset: 0x000004E9
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("127")]
		public byte ClickOpacity
		{
			get
			{
				return (byte)this["ClickOpacity"];
			}
			set
			{
				this["ClickOpacity"] = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000022FC File Offset: 0x000004FC
		// (set) Token: 0x06000066 RID: 102 RVA: 0x0000230E File Offset: 0x0000050E
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("0")]
		public int ClickColor
		{
			get
			{
				return (int)this["ClickColor"];
			}
			set
			{
				this["ClickColor"] = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002321 File Offset: 0x00000521
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002333 File Offset: 0x00000533
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("800")]
		public int ClickDisplay
		{
			get
			{
				return (int)this["ClickDisplay"];
			}
			set
			{
				this["ClickDisplay"] = value;
			}
		}

		// Token: 0x04000080 RID: 128
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
