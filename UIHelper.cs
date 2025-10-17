using System;
using System.Windows.Forms;

namespace LiveCell_Gui
{
    public static class UIHelper
    {
        /// <summary>
        /// UI 스레드와 상관없이 AutoCloseForm을 안전하게 표시
        /// </summary>
        public static void ShowAutoCloseMsg(string title, string message, int timeout)
        {
            // 현재 실행 스레드가 UI 스레드인지 판단
            if (Application.OpenForms.Count > 0)
            {
                // 가장 먼저 열린 폼 (보통 MainForm)
                Form mainForm = Application.OpenForms[Application.OpenForms.Count - 1];

                if (mainForm != null && mainForm.InvokeRequired)
                {
                    mainForm.BeginInvoke(new Action(() =>
                    {
                        var f = new AutoCloseForm(title, message, timeout);
                        f.Show(mainForm);
                    }));
                }
                else
                {
                    var f = new AutoCloseForm(title, message, timeout);
                    f.Show(mainForm);
                }
            }
            else
            {
                // 폼이 아직 없을 때도 안전하게 실행
                var f = new AutoCloseForm(title, message, timeout);
                f.Show();
            }
        }
    }
}
