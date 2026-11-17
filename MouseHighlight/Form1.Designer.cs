namespace MouseHighlight
{
	// Token: 0x02000010 RID: 16
	public partial class Form1 : global::System.Windows.Forms.Form
	{
		// =========================================================================
		// [메소드 분류] Dispose : 폼 종료 시 메모리 및 관리 리소스(트레이 아이콘 등) 해제
		// =========================================================================
		// Token: 0x0600003A RID: 58 RVA: 0x00002112 File Offset: 0x00000312
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.notifyIcon1 != null)
				{
					this.notifyIcon1.Dispose(); // 시스템 트레이 아이콘 리소스 해제
				}
				if (this.components != null)
				{
					this.components.Dispose(); // 컴포넌트 컨테이너 리소스 해제
				}
			}
			base.Dispose(disposing);
		}

        // =========================================================================
        // [메소드 분류] InitializeComponent : 폼 UI 컨트롤 생성, 속성 지정 및 이벤트 바인딩
        // =========================================================================
        // Token: 0x0600003B RID: 59 RVA: 0x0000351C File Offset: 0x0000171C
        private void InitializeComponent()
        {
            // 리소스 매니저 초기화 (아이콘 파일 등 로드에 사용)
            global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MouseHighlight.Form1));

            // [기능별 분류] 각 그룹 및 컨트롤 인스턴스 생성
            this.groupBox_MouseHighlight = new global::System.Windows.Forms.GroupBox();
            this.label1 = new global::System.Windows.Forms.Label();
            this.textBox_Mouse_Size = new global::System.Windows.Forms.TextBox();
            this.label2 = new global::System.Windows.Forms.Label();
            this.Mouse_Opacity = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.Mouse_Opacity)).BeginInit();

            this.toolTip1 = new global::System.Windows.Forms.ToolTip();

            this.groupBox_CustomCursor = new global::System.Windows.Forms.GroupBox();
            this.label_CursorDesc = new global::System.Windows.Forms.Label();
            global::System.Windows.Forms.Label label = new global::System.Windows.Forms.Label();
            this.comboBox_CursorPreset = new global::System.Windows.Forms.ComboBox();
            this.linkLabel_CursorUrl = new global::System.Windows.Forms.LinkLabel();
            this.textBox_CursorBase64 = new global::System.Windows.Forms.TextBox();

            this.groupBox_Skill = new global::System.Windows.Forms.GroupBox();
            this.checkBox_SkillEnable_UserRange_UserRange = new global::System.Windows.Forms.CheckBox();
            this.checkBox_Skill_RangeExtension = new global::System.Windows.Forms.CheckBox();
            this.checkBox_Skill_RangeExtensionItem = new global::System.Windows.Forms.CheckBox();
            this.label_Skill_Range = new global::System.Windows.Forms.Label();
            this.textBox_Skill_Range = new global::System.Windows.Forms.TextBox();

            this.button_ChangeCursor = new global::System.Windows.Forms.Button();
            this.button_Apply = new global::System.Windows.Forms.Button();
            this.button_MinimizeTray = new global::System.Windows.Forms.Button();
            this.notifyIcon1 = new global::System.Windows.Forms.NotifyIcon();
            this.contextMenuStrip1 = new global::System.Windows.Forms.ContextMenuStrip();
            this.toolStripMenuItemOpen = new global::System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemExit = new global::System.Windows.Forms.ToolStripMenuItem();

            // 레이아웃 일괄 적용 시작 (SuspendLayout)
            this.groupBox_MouseHighlight.SuspendLayout();
            this.groupBox_CustomCursor.SuspendLayout();
            this.groupBox_Skill.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            base.SuspendLayout();

            // ---------------------------------------------------------------------
            // [기능 분류: 1. 커서 설정 그룹 (groupBox_MouseHighlight)]
            // ---------------------------------------------------------------------
            this.groupBox_MouseHighlight.Controls.Add(this.label1);
            this.groupBox_MouseHighlight.Controls.Add(this.textBox_Mouse_Size);
            this.groupBox_MouseHighlight.Controls.Add(this.label2);
            this.groupBox_MouseHighlight.Controls.Add(this.Mouse_Opacity);
            this.groupBox_MouseHighlight.Font = new global::System.Drawing.Font("Segoe UI", 11f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
            this.groupBox_MouseHighlight.Location = new global::System.Drawing.Point(15, 12);
            this.groupBox_MouseHighlight.Name = "groupBox_MouseHighlight";
            this.groupBox_MouseHighlight.Size = new global::System.Drawing.Size(330, 120);
            this.groupBox_MouseHighlight.TabIndex = 0;
            this.groupBox_MouseHighlight.TabStop = false;
            this.groupBox_MouseHighlight.Text = "Cursor Settings";
            this.groupBox_MouseHighlight.UseCompatibleTextRendering = true;

            // 마우스 크기 입력 텍스트박스
            this.textBox_Mouse_Size.Font = new global::System.Drawing.Font("Segoe UI", 11f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
            this.textBox_Mouse_Size.ImeMode = global::System.Windows.Forms.ImeMode.Disable;
            this.textBox_Mouse_Size.Location = new global::System.Drawing.Point(15, 32);
            this.textBox_Mouse_Size.MaxLength = 3;
            this.textBox_Mouse_Size.Name = "textBox_Mouse_Size";
            this.textBox_Mouse_Size.Size = new global::System.Drawing.Size(130, 27);
            this.textBox_Mouse_Size.TabIndex = 0;
            this.textBox_Mouse_Size.KeyDown += new global::System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);

            // 마우스 크기 안내 라벨
            this.label1.AutoSize = true;
            this.label1.Font = new global::System.Drawing.Font("Segoe UI", 10f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
            this.label1.Location = new global::System.Drawing.Point(155, 34);
            this.label1.Name = "label1";
            this.label1.Size = new global::System.Drawing.Size(150, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Size (default 20)";
            this.label1.UseCompatibleTextRendering = true;

            // 투명도 입력 트랙바 (그룹박스 내부 좌표에 맞게 수정)
            this.Mouse_Opacity.Location = new global::System.Drawing.Point(15, 68);
            this.Mouse_Opacity.Name = "Mouse_Opacity";
            this.Mouse_Opacity.Size = new global::System.Drawing.Size(130, 45);
            this.Mouse_Opacity.Maximum = 255;
            this.Mouse_Opacity.TickFrequency = 50;
            this.Mouse_Opacity.Scroll += new System.EventHandler(this.Mouse_Opacity_Scroll);

            // 투명도 안내 라벨
            this.label2.AutoSize = true;
            this.label2.Font = new global::System.Drawing.Font("Segoe UI", 10f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
            this.label2.Location = new global::System.Drawing.Point(155, 74);
            this.label2.Name = "label2";
            this.label2.Size = new global::System.Drawing.Size(150, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Opacity (0~255)";
            this.label2.UseCompatibleTextRendering = true;

            // ---------------------------------------------------------------------
            // [기능 분류: 2. 커서 선택 및 Base64 입력 그룹 (groupBox_CustomCursor)]
            // ---------------------------------------------------------------------
            this.groupBox_CustomCursor.Controls.Add(this.label_CursorDesc);
            this.groupBox_CustomCursor.Controls.Add(label);
            this.groupBox_CustomCursor.Controls.Add(this.comboBox_CursorPreset);
            this.groupBox_CustomCursor.Controls.Add(this.linkLabel_CursorUrl);
            this.groupBox_CustomCursor.Controls.Add(this.textBox_CursorBase64);
            this.groupBox_CustomCursor.Font = new global::System.Drawing.Font("Segoe UI", 11f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
            this.groupBox_CustomCursor.Location = new global::System.Drawing.Point(15, 142);
            this.groupBox_CustomCursor.Name = "groupBox_CustomCursor";
            this.groupBox_CustomCursor.Size = new global::System.Drawing.Size(330, 185);
            this.groupBox_CustomCursor.TabIndex = 1;
            this.groupBox_CustomCursor.TabStop = false;
            this.groupBox_CustomCursor.Text = "Cursor Selection and Base64";
            this.groupBox_CustomCursor.UseCompatibleTextRendering = true;

            this.label_CursorDesc.AutoSize = true;
            this.label_CursorDesc.Font = new global::System.Drawing.Font("Segoe UI", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
            this.label_CursorDesc.Location = new global::System.Drawing.Point(15, 25);
            this.label_CursorDesc.Name = "label_CursorDesc";
            this.label_CursorDesc.Size = new global::System.Drawing.Size(260, 20);
            this.label_CursorDesc.TabIndex = 0;
            this.label_CursorDesc.Text = "커서 프리셋 선택 또는 Base64 직접 입력:";

            this.comboBox_CursorPreset.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_CursorPreset.Font = new global::System.Drawing.Font("Segoe UI", 9.5f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
            this.comboBox_CursorPreset.Items.AddRange(new object[]
            {
                "기본 커서 (Default)",
                "블루 커서 (Blue)",
                "레드 커서 (Red)",
                "직접 입력 (Custom)"
            });
            this.comboBox_CursorPreset.Location = new global::System.Drawing.Point(15, 50);
            this.comboBox_CursorPreset.Name = "comboBox_CursorPreset";
            this.comboBox_CursorPreset.Size = new global::System.Drawing.Size(300, 25);
            this.comboBox_CursorPreset.TabIndex = 1;
            this.comboBox_CursorPreset.SelectedIndexChanged += new System.EventHandler(this.comboBox_CursorPreset_SelectedIndexChanged);

            label.AutoSize = true;
            label.Font = new global::System.Drawing.Font("Segoe UI", 8f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
            label.Location = new global::System.Drawing.Point(15, 78);
            label.Name = "label_Base64CheckNote";
            label.Size = new global::System.Drawing.Size(200, 18);
            label.TabIndex = 2;
            label.Text = "※ Base64 문자열 ※";

            this.linkLabel_CursorUrl.AutoSize = true;
            this.linkLabel_CursorUrl.Font = new global::System.Drawing.Font("Segoe UI", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
            this.linkLabel_CursorUrl.Location = new global::System.Drawing.Point(15, 96);
            this.linkLabel_CursorUrl.Name = "linkLabel_CursorUrl";
            this.linkLabel_CursorUrl.Size = new global::System.Drawing.Size(220, 18);
            this.linkLabel_CursorUrl.TabIndex = 3;
            this.linkLabel_CursorUrl.TabStop = true;
            this.linkLabel_CursorUrl.Text = "https://www.base64.sh/ko/image-to-base64/";
            this.linkLabel_CursorUrl.LinkClicked += new global::System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_CursorUrl_LinkClicked);

            this.textBox_CursorBase64.Font = new global::System.Drawing.Font("Segoe UI", 8.5f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
            this.textBox_CursorBase64.Location = new global::System.Drawing.Point(15, 118);
            this.textBox_CursorBase64.Multiline = true;
            this.textBox_CursorBase64.ScrollBars = global::System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_CursorBase64.MaxLength = 0;
            this.textBox_CursorBase64.Name = "textBox_CursorBase64";
            this.textBox_CursorBase64.Size = new global::System.Drawing.Size(300, 55);
            this.textBox_CursorBase64.TabIndex = 4;

            // ---------------------------------------------------------------------
            // [기능 분류: 3. 센터 스킬 영역 오버레이 그룹 (groupBox_Skill)]
            // ---------------------------------------------------------------------
            this.groupBox_Skill.Controls.Add(this.checkBox_SkillEnable_UserRange_UserRange);
            this.groupBox_Skill.Controls.Add(this.checkBox_Skill_RangeExtension);
            this.groupBox_Skill.Controls.Add(this.checkBox_Skill_RangeExtensionItem);
            this.groupBox_Skill.Controls.Add(this.label_Skill_Range);
            this.groupBox_Skill.Controls.Add(this.textBox_Skill_Range);
            this.groupBox_Skill.Font = new global::System.Drawing.Font("Segoe UI", 11f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
            this.groupBox_Skill.Location = new global::System.Drawing.Point(15, 337);
            this.groupBox_Skill.Name = "groupBox_Skill";
            this.groupBox_Skill.Size = new global::System.Drawing.Size(330, 100);
            this.groupBox_Skill.TabIndex = 2;
            this.groupBox_Skill.TabStop = false;
            this.groupBox_Skill.Text = "Center Skill Area Overlay";
            this.groupBox_Skill.UseCompatibleTextRendering = true;

            // 체크박스 3개가 겹치지 않게 가로 좌표와 너비를 균등 분할 (330px 내에 안착)
            this.checkBox_SkillEnable_UserRange_UserRange.AutoSize = true;
            this.checkBox_SkillEnable_UserRange_UserRange.Font = new global::System.Drawing.Font("Segoe UI", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
            this.checkBox_SkillEnable_UserRange_UserRange.Location = new global::System.Drawing.Point(12, 28);
            this.checkBox_SkillEnable_UserRange_UserRange.Name = "checkBox_SkillEnable_UserRange_UserRange";
            this.checkBox_SkillEnable_UserRange_UserRange.Size = new global::System.Drawing.Size(102, 24);
            this.checkBox_SkillEnable_UserRange_UserRange.TabIndex = 0;
            this.checkBox_SkillEnable_UserRange_UserRange.Text = "자기 중심 범위";
            this.checkBox_SkillEnable_UserRange_UserRange.UseVisualStyleBackColor = true;
            this.checkBox_SkillEnable_UserRange_UserRange.CheckedChanged += new System.EventHandler(this.checkBox_SkillEnable_UserRange_UserRange_CheckedChanged);

            this.checkBox_Skill_RangeExtension.AutoSize = true;
            this.checkBox_Skill_RangeExtension.Font = new global::System.Drawing.Font("Segoe UI", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
            this.checkBox_Skill_RangeExtension.Location = new global::System.Drawing.Point(120, 28);
            this.checkBox_Skill_RangeExtension.Name = "checkBox_Skill_RangeExtension";
            this.checkBox_Skill_RangeExtension.Size = new global::System.Drawing.Size(82, 24);
            this.checkBox_Skill_RangeExtension.TabIndex = 1;
            this.checkBox_Skill_RangeExtension.Text = "범위 연마";
            this.checkBox_Skill_RangeExtension.UseVisualStyleBackColor = true;
            this.checkBox_Skill_RangeExtension.CheckedChanged += new System.EventHandler(this.checkBox_Skill_RangeExtension_CheckedChanged);

            this.checkBox_Skill_RangeExtensionItem.AutoSize = true;
            this.checkBox_Skill_RangeExtensionItem.Font = new global::System.Drawing.Font("Segoe UI", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
            this.checkBox_Skill_RangeExtensionItem.Location = new global::System.Drawing.Point(208, 28);
            this.checkBox_Skill_RangeExtensionItem.Name = "checkBox_Skill_RangeExtensionItem";
            this.checkBox_Skill_RangeExtensionItem.Size = new global::System.Drawing.Size(65, 24);
            this.checkBox_Skill_RangeExtensionItem.TabIndex = 2;
            this.checkBox_Skill_RangeExtensionItem.Text = "불시엔";
            this.checkBox_Skill_RangeExtensionItem.UseVisualStyleBackColor = true;
            this.checkBox_Skill_RangeExtensionItem.CheckedChanged += new System.EventHandler(this.checkBox_Skill_RangeExtensionItem_CheckedChanged);

            this.label_Skill_Range.AutoSize = true;
            this.label_Skill_Range.Font = new global::System.Drawing.Font("Segoe UI", 9.5f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
            this.label_Skill_Range.Location = new global::System.Drawing.Point(15, 62);
            this.label_Skill_Range.Name = "label_Skill_Range";
            this.label_Skill_Range.Size = new global::System.Drawing.Size(140, 20);
            this.label_Skill_Range.Text = "스킬 범위값 (예: 1~50)";

            this.textBox_Skill_Range.Font = new global::System.Drawing.Font("Segoe UI", 11f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
            this.textBox_Skill_Range.ImeMode = global::System.Windows.Forms.ImeMode.Disable;
            this.textBox_Skill_Range.Location = new global::System.Drawing.Point(165, 57);
            this.textBox_Skill_Range.MaxLength = 2;
            this.textBox_Skill_Range.Name = "textBox_Skill_Range";
            this.textBox_Skill_Range.Size = new global::System.Drawing.Size(150, 27);
            this.textBox_Skill_Range.TabIndex = 3;
            this.textBox_Skill_Range.KeyDown += new global::System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);

            // ---------------------------------------------------------------------
            // [기능 분류: 4. 하단 메인 제어 버튼들]
            // ---------------------------------------------------------------------
            this.button_ChangeCursor.Font = new global::System.Drawing.Font("Segoe UI", 9f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
            this.button_ChangeCursor.Location = new global::System.Drawing.Point(15, 450);
            this.button_ChangeCursor.Name = "button_ChangeCursor";
            this.button_ChangeCursor.Size = new global::System.Drawing.Size(103, 35);
            this.button_ChangeCursor.TabIndex = 3;
            this.button_ChangeCursor.Text = "커서 변경";
            this.button_ChangeCursor.UseCompatibleTextRendering = true;
            this.button_ChangeCursor.UseVisualStyleBackColor = true;
            this.button_ChangeCursor.Click += new System.EventHandler(this.button_ChangeCursor_Click);

            this.button_Apply.Font = new global::System.Drawing.Font("Segoe UI", 9f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
            this.button_Apply.Location = new global::System.Drawing.Point(123, 450);
            this.button_Apply.Name = "button_Apply";
            this.button_Apply.Size = new global::System.Drawing.Size(103, 35);
            this.button_Apply.TabIndex = 4;
            this.button_Apply.Text = "설정값 저장";
            this.button_Apply.UseCompatibleTextRendering = true;
            this.button_Apply.UseVisualStyleBackColor = true;
            this.button_Apply.Click += new global::System.EventHandler(this.button_Apply_Click);

            this.button_MinimizeTray.Font = new global::System.Drawing.Font("Segoe UI", 8.5f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
            this.button_MinimizeTray.Location = new global::System.Drawing.Point(231, 450);
            this.button_MinimizeTray.Name = "button_MinimizeTray";
            this.button_MinimizeTray.Size = new global::System.Drawing.Size(114, 35);
            this.button_MinimizeTray.TabIndex = 5;
            this.button_MinimizeTray.Text = "트레이로 최소화";
            this.button_MinimizeTray.UseCompatibleTextRendering = true;
            this.button_MinimizeTray.UseVisualStyleBackColor = true;
            this.button_MinimizeTray.Click += new global::System.EventHandler(this.button_MinimizeTray_Click);

            // ---------------------------------------------------------------------
            // [기능 분류: 5. 시스템 트레이 아이콘 및 우클릭 컨텍스트 메뉴]
            // ---------------------------------------------------------------------
            this.contextMenuStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
            {
                this.toolStripMenuItemOpen,
                this.toolStripMenuItemExit
            });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new global::System.Drawing.Size(100, 48);

            this.toolStripMenuItemOpen.Name = "toolStripMenuItemOpen";
            this.toolStripMenuItemOpen.Size = new global::System.Drawing.Size(99, 22);
            this.toolStripMenuItemOpen.Text = "열기";
            this.toolStripMenuItemOpen.Click += new global::System.EventHandler(this.TrayMenu_Open_Click);

            this.toolStripMenuItemExit.Name = "toolStripMenuItemExit";
            this.toolStripMenuItemExit.Size = new global::System.Drawing.Size(99, 22);
            this.toolStripMenuItemExit.Text = "종료";
            this.toolStripMenuItemExit.Click += new global::System.EventHandler(this.TrayMenu_Exit_Click);

            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
            this.notifyIcon1.Text = "Mouse Highlight";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new global::System.EventHandler(this.NotifyIcon_DoubleClick);

            // ---------------------------------------------------------------------
            // [기능 분류: 6. 메인 폼(Form1) 창 전체 속성 및 이벤트 설정]
            // ---------------------------------------------------------------------
            base.AutoScaleDimensions = new global::System.Drawing.SizeF(96f, 96f);
            base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoSize = true;
            base.ClientSize = new global::System.Drawing.Size(360, 500);

            // 컨트롤 컬렉션 추가 (중복 추가된 Mouse_Opacity는 base.Controls에서 제거하여 그룹박스 내부에 온전히 속하도록 정리)
            base.Controls.Add(this.button_MinimizeTray);
            base.Controls.Add(this.button_Apply);
            base.Controls.Add(this.button_ChangeCursor);
            base.Controls.Add(this.groupBox_Skill);
            base.Controls.Add(this.groupBox_CustomCursor);
            base.Controls.Add(this.groupBox_MouseHighlight);

            this.Font = new global::System.Drawing.Font("Segoe UI", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
            base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
            base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.Name = "Form1";
            this.Text = "Mouse Highlight for TalseWeaver";

            base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            base.Load += new global::System.EventHandler(this.Form1_Load);
            base.Shown += new global::System.EventHandler(this.Form1_Shown);

            // 레이아웃 일괄 적용 종료 (ResumeLayout)
            this.groupBox_MouseHighlight.ResumeLayout(false);
            this.groupBox_MouseHighlight.PerformLayout();
            this.groupBox_CustomCursor.ResumeLayout(false);
            this.groupBox_CustomCursor.PerformLayout();
            this.groupBox_Skill.ResumeLayout(false);
            this.groupBox_Skill.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);

            // ---------------------------------------------------------------------
            // [기능 분류: 7. 툴팁(ToolTip) 안내 메시지 설정]
            // ---------------------------------------------------------------------
            this.toolTip1.SetToolTip(this.Mouse_Opacity, "마우스 커서의 투명도를 슬라이더로 조절합니다 (0~255)");
            this.toolTip1.SetToolTip(this.comboBox_CursorPreset, "원하는 커서 디자인 프리셋을 선택하거나 직접 입력을 선택하세요");
            this.toolTip1.SetToolTip(this.textBox_CursorBase64, "커서 이미지의 Base64 문자열 코드를 입력합니다");
            this.toolTip1.SetToolTip(this.checkBox_SkillEnable_UserRange_UserRange, "화면에 자기 중심 범위 오버레이를 켭니다");
            this.toolTip1.SetToolTip(this.checkBox_Skill_RangeExtension, "스킬 범위 연마 효과를 적용하여 범위를 넓힙니다 (+20)");
            this.toolTip1.SetToolTip(this.checkBox_Skill_RangeExtensionItem, "불시엔 효과를 적용하여 범위를 넓힙니다 (+7)");
            this.toolTip1.SetToolTip(this.textBox_Skill_Range, "기본 44, 예프넨 48");
            this.toolTip1.SetToolTip(this.button_ChangeCursor, "선택/입력한 Base64 코드로 커서를 변경합니다");
            this.toolTip1.SetToolTip(this.button_Apply, "현재 변경된 모든 설정값을 설정 파일에 저장합니다");
            this.toolTip1.SetToolTip(this.button_MinimizeTray, "프로그램을 작업 표시줄에서 숨기고 시스템 트레이로 내립니다");
        }

        // =========================================================================
        // [필드 분류] 폼 내에서 사용되는 컨트롤 및 컴포넌트 변수 선언부
        // =========================================================================

        // 컴포넌트 관리 컨테이너
        private global::System.ComponentModel.IContainer components;

        // [기능별 분류: 툴팁 컴포넌트]
        private global::System.Windows.Forms.ToolTip toolTip1;

        // [기능별 분류: 커서 크기 및 투명도 설정 관련 필드]
        private global::System.Windows.Forms.GroupBox groupBox_MouseHighlight;
		private global::System.Windows.Forms.Label label2;
		private global::System.Windows.Forms.Label label1;
        private global::System.Windows.Forms.TrackBar Mouse_Opacity;
        private global::System.Windows.Forms.TextBox textBox_Mouse_Size;

		// [기능별 분류: 하단 제어 버튼 및 시스템 트레이/메뉴 관련 필드]
		private global::System.Windows.Forms.Button button_Apply;
		private global::System.Windows.Forms.Button button_MinimizeTray;
		private global::System.Windows.Forms.NotifyIcon notifyIcon1;
		private global::System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOpen;
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExit;

		// [기능별 분류: 커서 프리셋 및 Base64 입력 관련 필드]
		private global::System.Windows.Forms.GroupBox groupBox_CustomCursor;
		private global::System.Windows.Forms.TextBox textBox_CursorBase64;
		private global::System.Windows.Forms.Button button_ChangeCursor;
		private global::System.Windows.Forms.Label label_CursorDesc;
		private global::System.Windows.Forms.LinkLabel linkLabel_CursorUrl;
		private global::System.Windows.Forms.ComboBox comboBox_CursorPreset;

		// [기능별 분류: 센터 스킬 영역 오버레이 관련 필드]
		private global::System.Windows.Forms.GroupBox groupBox_Skill;
		private global::System.Windows.Forms.CheckBox checkBox_SkillEnable_UserRange_UserRange;
		private global::System.Windows.Forms.Label label_Skill_Range;
		private global::System.Windows.Forms.TextBox textBox_Skill_Range;
		private global::System.Windows.Forms.Button button_SkillApply;
		private global::System.Windows.Forms.CheckBox checkBox_Skill_RangeExtension;
        private global::System.Windows.Forms.CheckBox checkBox_Skill_RangeExtensionItem;
    }
}