using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Runtime.CompilerServices;

namespace MouseHighlight.Properties
{
	// Token: 0x02000014 RID: 20
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00002050 File Offset: 0x00000250
		internal Resources()
		{
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002190 File Offset: 0x00000390
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					Resources.resourceMan = new ResourceManager("MouseHighlight.Properties.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000021BC File Offset: 0x000003BC
		// (set) Token: 0x06000054 RID: 84 RVA: 0x000021C3 File Offset: 0x000003C3
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000055 RID: 85 RVA: 0x000021CB File Offset: 0x000003CB
		internal static Bitmap catnip5_3color
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("catnip5_3color", Resources.resourceCulture);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000021E6 File Offset: 0x000003E6
		internal static Icon catnip5_3color_icon
		{
			get
			{
				return (Icon)Resources.ResourceManager.GetObject("catnip5_3color_icon", Resources.resourceCulture);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002201 File Offset: 0x00000401
		internal static UnmanagedMemoryStream lala_Catnip5
		{
			get
			{
				return Resources.ResourceManager.GetStream("lala_Catnip5", Resources.resourceCulture);
			}
		}

		// Token: 0x0400007E RID: 126
		private static ResourceManager resourceMan;

		// Token: 0x0400007F RID: 127
		private static CultureInfo resourceCulture;
	}
}
