using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveCell_Gui
{
    public partial class AutoCloseForm : Form
    {
        private readonly int _timeout;

        public AutoCloseForm(string title, string message, int timeout)
        {
            InitializeComponent();
            _timeout = timeout;
            InitUI(title, message);
            this.Shown += async (s, e) => await AutoCloseAfterDelayAsync();
        }

        private void InitUI(string title, string message)
        {
            this.Width = 300;
            this.Height = 150;
            this.Text = title;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var label = new Label()
            {
                Text = message,
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Font = new System.Drawing.Font("맑은 고딕", 12),
                AutoSize = false,
                Padding = new Padding(10),
            };

            this.Controls.Add(label);
            CenterToScreen();
        }

        private async Task AutoCloseAfterDelayAsync()
        {
            await Task.Delay(_timeout);
            this.Close();
        }
    }
}


#if false
using System;
using System.Timers;
using System.Windows.Forms;

namespace LiveCell_Gui
{
    public partial class AutoCloseForm : Form
    {
        private System.Timers.Timer? _autoCloseTimer;

        public AutoCloseForm(string title, string message, int timeout)
        {
            InitializeComponent();
            InitUI(title, message);
            StartAutoCloseTimer(timeout);
        }

        private void InitUI(string title, string message)
        {
            this.Width = 300;
            this.Height = 150;
            this.Text = title;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var label = new Label()
            {
                Text = message,
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Font = new System.Drawing.Font("맑은 고딕", 12),
                AutoSize = false,
                Padding = new Padding(10),
            };

            this.Controls.Add(label);

            CenterToScreen();
        }

        private void StartAutoCloseTimer(int timeout)
        {
            _autoCloseTimer = new System.Timers.Timer(timeout);
            _autoCloseTimer.Elapsed += AutoCloseElapsed;
            _autoCloseTimer.AutoReset = false;
            _autoCloseTimer.Start();
        }

        private void AutoCloseElapsed(object sender, ElapsedEventArgs e)
        {
            _autoCloseTimer?.Stop();
            _autoCloseTimer?.Dispose();
            _autoCloseTimer = null;

            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => this.Close()));
            }
            else
            {
                this.Close();
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _autoCloseTimer?.Dispose();
            base.OnFormClosed(e);
        }
    }
}
#endif