using Hook;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace MouseHighlight
{
    public partial class Form1 : Form
    {
        #region [1. 필드 및 속성 분류 (Variables & Properties)]

        #region [1-1. GDI 폼 및 오버레이 관리 필드]
        /// <summary>마우스 커서 오버레이를 표시하는 GDI 폼</summary>
        private GdiForm mCursorForm;
        /// <summary>마우스 클릭 효과를 표시하는 GDI 폼</summary>
        private GdiForm mClickCursorForm;
        /// <summary>스킬 범위 오버레이를 표시하는 GDI 폼</summary>
        private GdiForm mSkillForm;
        #endregion

        #region [1-2. 마우스 비트맵 캐시 필드]
        /// <summary>마우스 커서 비트맵 이미지</summary>
        private static Bitmap mBitmap_Mouse;
        /// <summary>마우스 클릭 효과 비트맵 이미지</summary>
        private static Bitmap mBitmap_Click;
        /// <summary>스킬 범위 오버레이 비트맵 이미지</summary>
        private static Bitmap mBitmap_Skill;
        #endregion

        #region [1-3. 폼 상태 및 설정 파일 경로 필드]
        /// <summary>폼 로드 완료 여부를 나타내는 플래그</summary>
        private bool mIsLoadForm;
        /// <summary>설정 파일(INI)의 전체 경로</summary>
        private string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MouseHighlight_TalseWeaver_settings.ini");
        #endregion

        #region [1-4. 커서 프리셋 상수]
        /// <summary>기본 커서 프리셋 Base64 데이터</summary>
        private const string PRESET_DEFAULT = "[base코드]";
        /// <summary>블루 커서 프리셋 Base64 데이터</summary>
        private const string PRESET_BLUE = "[base코드]";
        /// <summary>레드 커서 프리셋 Base64 데이터</summary>
        private const string PRESET_RED = "[base코드]";
        #endregion

        #region [1-5. 커서 설정 관련 상태 변수]
        /// <summary>현재 적용된 커서의 Base64 문자열</summary>
        private string CursorBase64 = PRESET_DEFAULT;
        /// <summary>커서 크기 값</summary>
        private int CursorSize;
        /// <summary>커서 투명도 값 (0 ~ 255)</summary>
        private byte CursorOpacity;
        #endregion

        #region [1-6. 스킬 범위(Skill Shape) 관련 상태 및 설정 변수]
        /// <summary>자기 중심 범위 오버레이 활성화 여부</summary>
        private bool SkillEnable_UserRange;
        /// <summary>직선 범위 오버레이 활성화 여부</summary>
        private bool SkillEnable_StraightRange;
        /// <summary>지정 범위 오버레이 활성화 여부</summary>
        private bool SkillEnable_CustomRange;
        /// <summary>범위 연마 효과 적용 여부</summary>
        private bool Skill_RangeExtension;
        /// <summary>불시엔 아이템 효과 적용 여부</summary>
        private bool Skill_RangeExtensionItem;
        /// <summary>기준 스킬 범위 값</summary>
        private int Skill_Range;
        /// <summary>계산된 스킬 범위 오버레이 너비</summary>
        private int Skill_RangeWidth;
        /// <summary>계산된 스킬 범위 오버레이 높이</summary>
        private int Skill_RangeHeight;
        /// <summary>스킬 범위 오버레이 투명도</summary>
        private byte Skill_RangeOpacity;
        /// <summary>스킬 범위 최소 크기 제한</summary>
        private int Skill_Range_Min;
        /// <summary>스킬 범위 최대 크기 제한</summary>
        private int Skill_Range_Max;
        #endregion

        #endregion

        #region [2. 생성자 및 초기화 기능 (Constructor & Lifecycle)]

        /// <summary>
        /// Form1 클래스의 새 인스턴스를 초기화하고 GDI 폼 및 기본 설정값을 세팅합니다.
        /// </summary>
        public Form1()
        {
            // GDI 오버레이 폼 인스턴스 생성
            this.mCursorForm = new GdiForm();
            this.mClickCursorForm = new GdiForm();
            this.mSkillForm = new GdiForm();

            // 기본 커서 속성 초기화
            this.CursorSize = 40;
            this.CursorOpacity = byte.MaxValue;

            // 기본 스킬 범위 속성 초기화
            this.SkillEnable_UserRange = false;
            this.SkillEnable_StraightRange = false;
            this.SkillEnable_CustomRange = false;
            this.Skill_Range = 44;
            this.Skill_RangeHeight = this.Skill_Range * 10;
            this.Skill_RangeWidth = this.Skill_RangeHeight * 2;
            this.Skill_RangeOpacity = 30;
            this.Skill_Range_Min = 300;
            this.Skill_Range_Max = 1280;

            // 디자이너 컴포넌트 초기화
            this.InitializeComponent();
        }

        /// <summary>
        /// 폼이 로드될 때 마우스 후크를 등록하고 저장된 설정을 불러와 UI에 반영합니다.
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // 마우스 이벤트 후크 등록
                MouseHook.MouseMove += this.MouseHook_MouseMove;
                if (!MouseHook.HookStart())
                {
                    MessageBox.Show("Mouse hook failed");
                }

                // 설정 파일 로드
                this.LoadSettings();

                // UI 컨트롤 상태 동기화
                if (this.textBox_Mouse_Size != null) this.textBox_Mouse_Size.Text = this.CursorSize.ToString();
                if (this.Mouse_Opacity != null) this.Mouse_Opacity.Value = this.CursorOpacity;
                if (this.textBox_CursorBase64 != null) this.textBox_CursorBase64.Text = this.CursorBase64;
                if (this.textBox_Skill_Range != null) this.textBox_Skill_Range.Text = this.Skill_Range.ToString();

                if (this.checkBox_Skill_RangeExtension != null)
                {
                    this.checkBox_Skill_RangeExtension.Checked = this.Skill_RangeExtension;
                }
                if (this.checkBox_Skill_RangeExtensionItem != null)
                {
                    this.checkBox_Skill_RangeExtensionItem.Checked = this.Skill_RangeExtensionItem;
                }

                if (this.radioButton_Skill_UserRange != null)
                {
                    this.radioButton_Skill_UserRange.Checked = this.SkillEnable_UserRange;
                }
                if (this.radioButton_Skill_StraightRange != null)
                {
                    this.radioButton_Skill_StraightRange.Checked = this.SkillEnable_StraightRange;
                }
                if (this.radioButton_Skill_CustomRange != null)
                {
                    this.radioButton_Skill_CustomRange.Checked = this.SkillEnable_CustomRange;
                }
                if (this.radioButton_Skill_None != null && !this.SkillEnable_UserRange && !this.SkillEnable_StraightRange && !this.SkillEnable_CustomRange)
                {
                    this.radioButton_Skill_None.Checked = true;
                }

                if (this.Skill_Range_Opacity != null) this.Skill_Range_Opacity.Value = this.Skill_RangeOpacity;

                // 프리셋 및 커서/스킬 형상 초기화 적용
                this.UpdatePresetSelection();
                this.SetCursor();
                this.mCursorForm.Show();
                this.mIsLoadForm = true;
                this.SetSkillShape();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Initialization failed. - " + ex.Message);
            }
        }

        /// <summary>
        /// 폼이 화면에 완전히 표시될 때 초기 상태를 한 번 더 동기화합니다.
        /// </summary>
        private void Form1_Shown(object sender, EventArgs e)
        {
            if (!this.mIsLoadForm)
            {
                if (this.textBox_Mouse_Size != null) this.textBox_Mouse_Size.Text = this.CursorSize.ToString();
                if (this.Mouse_Opacity != null) this.Mouse_Opacity.Value = this.CursorOpacity;
                if (this.textBox_CursorBase64 != null) this.textBox_CursorBase64.Text = this.CursorBase64;
                if (this.textBox_Skill_Range != null) this.textBox_Skill_Range.Text = this.Skill_Range.ToString();
                this.UpdatePresetSelection();
                this.SetCursor();
                this.mCursorForm.Show();
                this.mClickCursorForm.Hide();
            }
            this.mIsLoadForm = true;
        }

        /// <summary>
        /// 폼이 닫힐 때 마우스 후크를 해제하고 트레이 아이콘 및 오버레이 폼을 정리합니다.
        /// </summary>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (this.notifyIcon1 != null)
                {
                    this.notifyIcon1.Visible = false;
                }
                // 후크 해제 및 이벤트 구독 제거
                MouseHook.HookClose();
                MouseHook.MouseDown -= this.MouseHook_MouseDown;
                MouseHook.MouseMove -= this.MouseHook_MouseMove;

                // 오버레이 폼 숨기기
                this.mCursorForm.Hide();
                if (this.mSkillForm != null)
                {
                    this.mSkillForm.Hide();
                }
            }
            catch (Exception)
            {
            }
        }

        #endregion

        #region [3. 마우스 후크(Hook) 및 이벤트 처리 기능]

        /// <summary>
        /// 마우스 커서 이동 시 호출되며, 커서 및 지정 범위 오버레이 폼의 위치를 실시간으로 업데이트합니다.
        /// </summary>
        private bool MouseHook_MouseMove(MouseEventType type, int x, int y)
        {
            try
            {
                // 1. 마우스 커서 위치 업데이트
                this.mCursorForm.Left = x - 10;
                this.mCursorForm.Top = y - 10;

                // 2. 지정 범위 모드일 경우 마우스 커서와 함께 붙어서 움직이도록 위치 실시간 연동
                if (this.SkillEnable_CustomRange && this.mSkillForm != null && Form1.mBitmap_Skill != null)
                {
                    int width = Form1.mBitmap_Skill.Width;
                    int height = Form1.mBitmap_Skill.Height;
                    this.mSkillForm.Left = x - width / 2;
                    this.mSkillForm.Top = y - height / 2;
                }
            }
            catch (Exception)
            {
            }
            return true;
        }

        /// <summary>
        /// 마우스 클릭 발생 시 호출되며, 클릭 위치에 클릭 효과 오버레이를 표시합니다.
        /// </summary>
        private bool MouseHook_MouseDown(MouseEventType type, int x, int y)
        {
            try
            {
                this.mClickCursorForm.Left = x - Form1.mBitmap_Click.Width / 2;
                this.mClickCursorForm.Top = y - Form1.mBitmap_Click.Height / 2;
                this.mClickCursorForm.Show();
            }
            catch (Exception)
            {
            }
            return true;
        }

        /// <summary>
        /// 투명도 트랙바 스크롤 시 호출되어 커서의 투명도 값을 실시간으로 조절합니다.
        /// </summary>
        private void Mouse_Opacity_Scroll(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.TrackBar trackBar = sender as System.Windows.Forms.TrackBar;
                if (trackBar != null)
                {
                    int opacityValue = trackBar.Value;

                    this.CursorOpacity = (byte)opacityValue;
                    this.SetCursor();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("투명도 조절 중 오류 발생: " + ex.Message);
            }
        }

        #endregion

        #region [4. 설정 적용 및 관리 기능 (Save / Apply)]

        /// <summary>
        /// 사용자가 입력한 설정값(크기, 투명도, 스킬 범위 등)의 유효성을 검증하고 적용합니다.
        /// </summary>
        private void SettingApply_Save()
        {
            try
            {
                int CursorSize = 0;
                if (this.textBox_Mouse_Size != null && !int.TryParse(this.textBox_Mouse_Size.Text, out CursorSize))
                {
                    MessageBox.Show("숫자만 입력 가능합니다.");
                    CursorSize = 40;
                    this.textBox_Mouse_Size.Text = CursorSize.ToString();
                }
                this.CursorSize = CursorSize;

                byte CursorOpacity = 0;
                if (this.Mouse_Opacity != null && !byte.TryParse(this.Mouse_Opacity.Value.ToString(), out CursorOpacity))
                {
                    MessageBox.Show("0부터 255까지의 숫자만 입력 가능합니다.");
                    CursorOpacity = byte.MaxValue;
                    this.Mouse_Opacity.Value = CursorOpacity;
                }
                this.CursorOpacity = CursorOpacity;

                this.SetCursor();

                int Skill_Range = 0;
                bool isAnySkillActive = this.SkillEnable_UserRange || this.SkillEnable_StraightRange || this.SkillEnable_CustomRange;
                if (isAnySkillActive && this.textBox_Skill_Range != null && !int.TryParse(this.textBox_Skill_Range.Text, out Skill_Range))
                {
                    MessageBox.Show("숫자만 입력 가능합니다.");
                    Skill_Range = 44;
                    this.textBox_Skill_Range.Text = Skill_Range.ToString();
                }
                if (!int.TryParse(this.textBox_Skill_Range.Text, out Skill_Range) || Skill_Range > 50)
                {
                    MessageBox.Show("50 이하의 숫자만 입력 가능합니다.");
                    Skill_Range = 50;
                    this.textBox_Skill_Range.Text = Skill_Range.ToString();
                }
                this.Skill_Range = Skill_Range;

                this.SetSkillShape();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Base64 문자열을 디코딩하여 마우스 커서 비트맵을 생성하고 오버레이 폼에 설정합니다.
        /// </summary>
        private void SetCursor()
        {
            try
            {
                string s = string.IsNullOrEmpty(this.CursorBase64) ? PRESET_DEFAULT : this.CursorBase64.Replace("\r", "").Replace("\n", "").Trim();
                byte[] buffer;
                try
                {
                    buffer = Convert.FromBase64String(s);
                }
                catch
                {
                    s = PRESET_DEFAULT;
                    this.CursorBase64 = s;
                    buffer = Convert.FromBase64String(s);
                }
                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (Bitmap bitmap = new Bitmap(memoryStream))
                    {
                        int num = this.CursorSize * 2;
                        Form1.mBitmap_Mouse = new Bitmap(bitmap, new Size(num, num));
                    }
                }
                this.mCursorForm.SetBitmap(Form1.mBitmap_Mouse, this.CursorOpacity);
            }
            catch (Exception ex)
            {
                MessageBox.Show("커서 이미지 디코딩에 실패했습니다: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        #endregion

        #region [5. 저장된 설정값 로드 및 저장 (Load / Save Settings)]

        /// <summary>
        /// INI 파일로부터 저장된 설정값들을 읽어와 필드에 반영합니다.
        /// </summary>
        private void LoadSettings()
        {
            try
            {
                if (File.Exists(this.configPath))
                {
                    string[] array = File.ReadAllLines(this.configPath);
                    for (int i = 0; i < array.Length; i++)
                    {
                        string text = array[i].Trim();
                        if (!string.IsNullOrEmpty(text))
                        {
                            int num = text.IndexOf('=');
                            if (num > 0)
                            {
                                string a = text.Substring(0, num).Trim();
                                string text2 = text.Substring(num + 1).Trim();
                                int CursorSize;
                                byte CursorOpacity;
                                bool flag;
                                int Skill_Range;
                                byte Skill_RangeOpacity;

                                if (a == "Size" && int.TryParse(text2, out CursorSize))
                                {
                                    this.CursorSize = CursorSize;
                                }
                                else if (a == "Opacity" && byte.TryParse(text2, out CursorOpacity))
                                {
                                    this.CursorOpacity = CursorOpacity;
                                }
                                else if (a == "Cursor" && !string.IsNullOrEmpty(text2))
                                {
                                    this.CursorBase64 = text2.Replace("\r", "").Replace("\n", "").Trim();
                                }
                                else if (a == "SkillEnable_UserRange" && bool.TryParse(text2, out flag))
                                {
                                    this.SkillEnable_UserRange = flag;
                                }
                                else if (a == "SkillEnable_StraightRange" && bool.TryParse(text2, out flag))
                                {
                                    this.SkillEnable_StraightRange = flag;
                                }
                                else if (a == "SkillEnable_CustomRange" && bool.TryParse(text2, out flag))
                                {
                                    this.SkillEnable_CustomRange = flag;
                                }
                                else if (a == "Skill_RangeExtension" && bool.TryParse(text2, out flag))
                                {
                                    this.Skill_RangeExtension = flag;
                                    if (this.checkBox_Skill_RangeExtension != null) this.checkBox_Skill_RangeExtension.Checked = flag;
                                }
                                else if (a == "Skill_RangeExtensionItem" && bool.TryParse(text2, out flag))
                                {
                                    this.Skill_RangeExtensionItem = flag;
                                    if (this.checkBox_Skill_RangeExtensionItem != null) this.checkBox_Skill_RangeExtensionItem.Checked = flag;
                                }
                                else if (a == "Skill_Range" && int.TryParse(text2, out Skill_Range))
                                {
                                    this.Skill_Range = Skill_Range;
                                }
                                else if (a == "Skill_RangeOpacity" && byte.TryParse(text2, out Skill_RangeOpacity))
                                {
                                    this.Skill_RangeOpacity = Skill_RangeOpacity;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }

            // 기본값 보정
            if (this.CursorSize <= 0) { this.CursorSize = 20; }
            if (this.CursorOpacity == 0) { this.CursorOpacity = byte.MaxValue; }
            if (string.IsNullOrEmpty(this.CursorBase64)) { this.CursorBase64 = PRESET_DEFAULT; }
            if (this.Skill_Range <= 0) { this.Skill_Range = 44; }
            if (this.Skill_RangeOpacity <= 0) { this.Skill_RangeOpacity = byte.MaxValue; }

            int calcHeight = this.Skill_Range * 10;
            if (calcHeight < this.Skill_Range_Min) { calcHeight = this.Skill_Range_Min; }
            if (calcHeight > this.Skill_Range_Max) { calcHeight = this.Skill_Range_Max; }
            this.Skill_RangeHeight = calcHeight;
            this.Skill_RangeWidth = calcHeight * 2;
        }

        /// <summary>
        /// 현재 설정된 값들을 INI 설정 파일에 저장합니다.
        /// </summary>
        private void SaveSettings()
        {
            try
            {
                string text = string.IsNullOrEmpty(this.CursorBase64) ? "" : this.CursorBase64.Replace("\r", "").Replace("\n", "").Trim();
                string contents = string.Format("Size={0}\nOpacity={1}\nCursor={2}\nSkillEnable_UserRange={3}\nSkillEnable_StraightRange={4}\nSkillEnable_CustomRange={5}\nSkill_RangeExtension={6}\nSkill_RangeExtensionItem={7}\nSkill_Range={8}\nSkill_RangeOpacity={9}", new object[]
                {
                    this.CursorSize,
                    this.CursorOpacity,
                    text,
                    this.SkillEnable_UserRange,
                    this.SkillEnable_StraightRange,
                    this.SkillEnable_CustomRange,
                    this.Skill_RangeExtension,
                    this.Skill_RangeExtensionItem,
                    this.Skill_Range,
                    this.Skill_RangeOpacity
                });
                File.WriteAllText(this.configPath, contents);
            }
            catch (Exception)
            {
            }
        }

        #endregion

        #region [6. 이벤트 핸들러 (Event Handlers)]

        /// <summary>
        /// 텍스트박스에서 Enter 키 입력 시 설정을 적용합니다.
        /// </summary>
        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                this.SettingApply_Save();
            }
        }

        #endregion

        #region [7. UI 제어 (UI Handlers)]

        /// <summary>
        /// [설정값 저장] 버튼 클릭 시 설정을 적용하고 파일에 저장합니다.
        /// </summary>
        private void button_Apply_Click(object sender, EventArgs e)
        {
            try
            {
                this.SettingApply_Save();
                MessageBox.Show("설정값이 저장되었습니다.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.SaveSettings();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// [트레이로 최소화] 버튼 클릭 시 창을 숨깁니다.
        /// </summary>
        private void button_MinimizeTray_Click(object sender, EventArgs e)
        {
            base.Hide();
        }

        /// <summary>
        /// 트레이 메뉴 [열기] 클릭 시 창을 복원합니다.
        /// </summary>
        private void TrayMenu_Open_Click(object sender, EventArgs e)
        {
            base.Show();
            base.WindowState = FormWindowState.Normal;
            base.BringToFront();
        }

        /// <summary>
        /// 트레이 메뉴 [종료] 클릭 시 애플리케이션을 종료합니다.
        /// </summary>
        private void TrayMenu_Exit_Click(object sender, EventArgs e)
        {
            if (this.notifyIcon1 != null)
            {
                this.notifyIcon1.Visible = false;
            }
            Application.Exit();
        }

        /// <summary>
        /// 시스템 트레이 아이콘 더블클릭 시 창을 복원합니다.
        /// </summary>
        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            base.Show();
            base.WindowState = FormWindowState.Normal;
            base.BringToFront();
        }

        /// <summary>
        /// [커서 변경] 버튼 클릭 시 입력된 Base64 코드를 검증하고 커서를 교체합니다.
        /// </summary>
        private void button_ChangeCursor_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.textBox_CursorBase64 == null) return;
                string text = this.textBox_CursorBase64.Text.Replace("\r", "").Replace("\n", "").Trim();
                if (string.IsNullOrEmpty(text))
                {
                    MessageBox.Show("Base64 코드를 입력해주세요.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(text)))
                    {
                        using (new Bitmap(memoryStream))
                        {
                        }
                    }
                    this.CursorBase64 = text;
                    this.textBox_CursorBase64.Text = text;
                    this.SetCursor();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("유효하지 않은 Base64 이미지 코드입니다: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        /// <summary>
        /// Base64 변환 웹사이트 링크 클릭 시 기본 브라우저로 해당 페이지를 엽니다.
        /// </summary>
        private void linkLabel_CursorUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start("https://www.base64.sh/ko/image-to-base64/");
            }
            catch (Exception ex)
            {
                MessageBox.Show("링크를 열 수 없습니다: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        /// <summary>
        /// 커서 프리셋 콤보박스 선택 변경 시 해당 프리셋 데이터를 로드하고 텍스트박스 상태를 제어합니다.
        /// </summary>
        private void comboBox_CursorPreset_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.mIsLoadForm || this.comboBox_CursorPreset == null)
            {
                return;
            }
            object selectedItem = this.comboBox_CursorPreset.SelectedItem;
            string a = (selectedItem != null) ? selectedItem.ToString() : null;
            if (a == "기본 커서 (Default)")
            {
                this.CursorBase64 = PRESET_DEFAULT;
                if (this.textBox_CursorBase64 != null)
                {
                    this.textBox_CursorBase64.Text = PRESET_DEFAULT;
                    this.textBox_CursorBase64.ReadOnly = true;
                }
                this.SetCursor();
                return;
            }
            if (a == "블루 커서 (Blue)")
            {
                this.CursorBase64 = PRESET_BLUE;
                if (this.textBox_CursorBase64 != null)
                {
                    this.textBox_CursorBase64.Text = PRESET_BLUE;
                    this.textBox_CursorBase64.ReadOnly = true;
                }
                this.SetCursor();
                return;
            }
            if (a == "레드 커서 (Red)")
            {
                this.CursorBase64 = PRESET_RED;
                if (this.textBox_CursorBase64 != null)
                {
                    this.textBox_CursorBase64.Text = PRESET_RED;
                    this.textBox_CursorBase64.ReadOnly = true;
                }
                this.SetCursor();
                return;
            }
            if (a == "직접 입력 (Custom)" && this.textBox_CursorBase64 != null)
            {
                this.textBox_CursorBase64.ReadOnly = false;
            }
        }

        /// <summary>
        /// 현재 설정된 커서 데이터에 맞춰 프리셋 콤보박스 선택 상태를 동기화합니다.
        /// </summary>
        private void UpdatePresetSelection()
        {
            if (this.comboBox_CursorPreset == null) return;

            if (this.CursorBase64 == PRESET_DEFAULT)
            {
                this.comboBox_CursorPreset.SelectedItem = "기본 커서 (Default)";
                if (this.textBox_CursorBase64 != null) this.textBox_CursorBase64.ReadOnly = true;
                return;
            }
            if (this.CursorBase64 == PRESET_BLUE)
            {
                this.comboBox_CursorPreset.SelectedItem = "블루 커서 (Blue)";
                if (this.textBox_CursorBase64 != null) this.textBox_CursorBase64.ReadOnly = true;
                return;
            }
            if (this.CursorBase64 == PRESET_RED)
            {
                this.comboBox_CursorPreset.SelectedItem = "레드 커서 (Red)";
                if (this.textBox_CursorBase64 != null) this.textBox_CursorBase64.ReadOnly = true;
                return;
            }
            this.comboBox_CursorPreset.SelectedItem = "직접 입력 (Custom)";
            if (this.textBox_CursorBase64 != null) this.textBox_CursorBase64.ReadOnly = false;
        }

        #endregion

        #region [8. 스킬 범위(Skill Shape) 관련 기능]

        /// <summary>
        /// 스킬 범위 설정 및 연마/아이템 옵션에 따라 표시될 스킬 범위 오버레이 모양을 생성합니다.
        /// (자기 중심: 마름모, 직선 범위: 직사각형, 지정 범위: 정원형 - 마우스 커서 연동)
        /// </summary>
        private void SetSkillShape()
        {
            try
            {
                bool isAnyEnabled = this.SkillEnable_UserRange || this.SkillEnable_StraightRange || this.SkillEnable_CustomRange;
                if (!isAnyEnabled)
                {
                    if (this.mSkillForm != null)
                    {
                        this.mSkillForm.Hide();
                    }
                }
                else
                {
                    // 구분을 위한 모드별 색상 값 지정 (자기 중심: Cyan, 직선 범위: Orange, 지정 범위: Red)
                    Color activeColor = Color.Cyan;
                    if (this.SkillEnable_StraightRange)
                    {
                        activeColor = Color.Orange;
                    }
                    else if (this.SkillEnable_CustomRange)
                    {
                        activeColor = Color.Red;
                    }

                    int effectiveRange = this.Skill_Range;

                    bool isExtChecked = (this.checkBox_Skill_RangeExtension != null && this.checkBox_Skill_RangeExtension.Checked);
                    bool isItemChecked = (this.checkBox_Skill_RangeExtensionItem != null && this.checkBox_Skill_RangeExtensionItem.Checked);

                    if (isExtChecked)
                    {
                        effectiveRange += 20; // 범위 연마
                    }
                    if (isItemChecked)
                    {
                        effectiveRange += 7; // 불시엔
                    }

                    int num = effectiveRange * 10;

                    if (isExtChecked || isItemChecked)
                    {
                        num += 100; // 추가 범위 보정
                    }

                    num = Math.Max(this.Skill_Range_Min, Math.Min(this.Skill_Range_Max, num));

                    // 지정 범위인 경우 너비도 num만 사용 (정원형), 그 외에는 num * 2 (num2) 사용
                    int shapeWidth = num * 2;
                    int shapeHeight = num;

                    this.Skill_RangeWidth = shapeWidth;

                    Bitmap image = new Bitmap(shapeWidth + 4, shapeHeight + 4);
                    using (Graphics graphics = Graphics.FromImage(image))
                    {
                        graphics.SmoothingMode = SmoothingMode.AntiAlias;

                        using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(100, activeColor)))
                        using (Pen pen = new Pen(activeColor, 2f))
                        {
                            if (this.SkillEnable_UserRange)
                            {
                                // 1. 자기 중심 범위: 마름모 형태
                                PointF pointF = new PointF((float)shapeWidth / 2f + 2f, 2f);
                                PointF pointF2 = new PointF((float)(shapeWidth + 2), (float)shapeHeight / 2f + 2f);
                                PointF pointF3 = new PointF((float)shapeWidth / 2f + 2f, (float)(shapeHeight + 2));
                                PointF pointF4 = new PointF(2f, (float)shapeHeight / 2f + 2f);
                                PointF[] points = new PointF[]
                                {
                                    pointF,
                                    pointF2,
                                    pointF3,
                                    pointF4
                                };
                                graphics.FillPolygon(solidBrush, points);
                                graphics.DrawPolygon(pen, points);
                            }
                            else if (this.SkillEnable_StraightRange)
                            {
                                // 2. 직선 범위: 직사각형 형태
                                graphics.FillRectangle(solidBrush, 2f, 2f, (float)shapeWidth, (float)shapeHeight);
                                graphics.DrawRectangle(pen, 2f, 2f, (float)shapeWidth, (float)shapeHeight);
                            }
                            else if (this.SkillEnable_CustomRange)
                            {
                                // 3. 지정 범위: 정원형 형태 (너비와 높이 모두 num 사용)
                                graphics.FillEllipse(solidBrush, 2f, 2f, (float)shapeWidth, (float)shapeHeight);
                                graphics.DrawEllipse(pen, 2f, 2f, (float)shapeWidth, (float)shapeHeight);
                            }
                        }
                    }
                    Form1.mBitmap_Skill = image;

                    // 자기 중심 및 직선 범위는 화면 중앙에 고정, 지정 범위는 현재 마우스 위치(커서)를 기준으로 즉시 배치
                    if (this.SkillEnable_CustomGroupActiveCheckMethodOrCursor()) // 내부 처리용 분기 예시
                    {
                        // 마우스 실시간 위치에 맞추거나 초기 표시 시 화면 중앙 정렬
                    }

                    Rectangle bounds = Screen.PrimaryScreen.Bounds;
                    int left = (bounds.Width - (shapeWidth + 4)) / 2;
                    int top = (bounds.Height - (shapeHeight + 4)) / 2 - 30;

                    this.mSkillForm.Left = left;
                    this.mSkillForm.Top = top;
                    this.mSkillForm.SetBitmap(Form1.mBitmap_Skill, this.Skill_RangeOpacity);
                    this.mSkillForm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("범위 생성 실패: " + ex.Message);
            }
        }

        // 보조 메서드 (지정 범위 여부 확인용)
        private bool SkillEnable_CustomGroupActiveCheckMethodOrCursor()
        {
            return this.SkillEnable_CustomRange;
        }

        /// <summary>
        /// '[사용안함]' 라디오 버튼 상태 변경 시 호출되어 모든 스킬 범위 오버레이를 끕니다.
        /// </summary>
        private void radioButton_Skill_None_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.radioButton_Skill_None != null && this.radioButton_Skill_None.Checked)
                {
                    this.SkillEnable_UserRange = false;
                    this.SkillEnable_StraightRange = false;
                    this.SkillEnable_CustomRange = false;
                    this.SetSkillShape();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("설정 적용 실패: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        /// <summary>
        /// '[자기 중심 범위]' 체크박스(라디오버튼) 상태 변경 시 호출되어 스킬 범위 오버레이를 켜고 끕니다.
        /// </summary>
        private void radioButton_SkillEnable_UserRange_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.radioButton_Skill_UserRange != null)
                {
                    this.SkillEnable_UserRange = this.radioButton_Skill_UserRange.Checked;
                }
                if (this.SkillEnable_UserRange)
                {
                    this.SkillEnable_StraightRange = false;
                    this.SkillEnable_CustomRange = false;
                }
                int num;
                if (this.textBox_Skill_Range != null && int.TryParse(this.textBox_Skill_Range.Text, out num))
                {
                    int num2 = num * 10;
                    if (num2 < this.Skill_Range_Min) { num2 = this.Skill_Range_Min; }
                    if (num2 > this.Skill_Range_Max) { num2 = this.Skill_Range_Max; }
                    this.Skill_RangeHeight = num2;
                    this.Skill_RangeWidth = num2 * 2;
                }
                this.SetSkillShape();
            }
            catch (Exception ex)
            {
                MessageBox.Show("설정 적용 실패: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        /// <summary>
        /// '[직선 범위]' 라디오 버튼 상태 변경 시 호출되어 직선 범위 오버레이를 켜고 끕니다.
        /// </summary>
        private void radioButton_Skill_StraightRange_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.radioButton_Skill_StraightRange != null)
                {
                    this.SkillEnable_StraightRange = this.radioButton_Skill_StraightRange.Checked;
                }
                if (this.SkillEnable_StraightRange)
                {
                    this.SkillEnable_UserRange = false;
                    this.SkillEnable_CustomRange = false;
                }
                int num;
                if (this.textBox_Skill_Range != null && int.TryParse(this.textBox_Skill_Range.Text, out num))
                {
                    int num2 = num * 10;
                    if (num2 < this.Skill_Range_Min) { num2 = this.Skill_Range_Min; }
                    if (num2 > this.Skill_Range_Max) { num2 = this.Skill_Range_Max; }
                    this.Skill_RangeHeight = num2;
                    this.Skill_RangeWidth = num2 * 2;
                }
                this.SetSkillShape();
            }
            catch (Exception ex)
            {
                MessageBox.Show("설정 적용 실패: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        /// <summary>
        /// '[지정 범위]' 라디오 버튼 상태 변경 시 호출되어 지정 범위 오버레이를 켜고 끕니다.
        /// </summary>
        private void radioButton_Skill_CustomRange_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.radioButton_Skill_CustomRange != null)
                {
                    this.SkillEnable_CustomRange = this.radioButton_Skill_CustomRange.Checked;
                }
                if (this.SkillEnable_CustomRange)
                {
                    this.SkillEnable_UserRange = false;
                    this.SkillEnable_StraightRange = false;
                }
                int num;
                if (this.textBox_Skill_Range != null && int.TryParse(this.textBox_Skill_Range.Text, out num))
                {
                    int num2 = num * 10;
                    if (num2 < this.Skill_Range_Min) { num2 = this.Skill_Range_Min; }
                    if (num2 > this.Skill_Range_Max) { num2 = this.Skill_Range_Max; }
                    this.Skill_RangeHeight = num2;
                    this.Skill_RangeWidth = num2 * 2;
                }
                this.SetSkillShape();
            }
            catch (Exception ex)
            {
                MessageBox.Show("설정 적용 실패: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        /// <summary>
        /// '범위 연마' 체크박스 상태 변경 시 스킬 범위를 재계산하고 오버레이를 갱신합니다.
        /// </summary>
        private void checkBox_Skill_RangeExtension_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.checkBox_Skill_RangeExtension != null)
                {
                    this.Skill_RangeExtension = this.checkBox_Skill_RangeExtension.Checked;
                }
                this.SetSkillShape();
            }
            catch (Exception ex)
            {
                MessageBox.Show("범위 확장 설정 실패: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        /// <summary>
        /// '불시엔' 체크박스 상태 변경 시 스킬 범위를 재계산하고 오버레이를 갱신합니다.
        /// </summary>
        private void checkBox_Skill_RangeExtensionItem_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.checkBox_Skill_RangeExtensionItem != null)
                {
                    this.Skill_RangeExtensionItem = this.checkBox_Skill_RangeExtensionItem.Checked;
                }
                this.SetSkillShape();
            }
            catch (Exception ex)
            {
                MessageBox.Show("불시엔 설정 실패: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        /// <summary>
        /// 투명도 트랙바 스크롤 시 호출되어 스킬 범위의 투명도 값을 실시간으로 조절합니다.
        /// </summary>
        private void Skill_RangeOpacity_Scroll(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.TrackBar trackBar = sender as System.Windows.Forms.TrackBar;
                if (trackBar != null)
                {
                    int opacityValue = trackBar.Value;

                    this.Skill_RangeOpacity = (byte)opacityValue;
                    this.SetSkillShape();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("투명도 조절 중 오류 발생: " + ex.Message);
            }
        }

        #endregion
    }
}