using System.Windows.Controls;

namespace AcHelper.DemoApp.GUI.Controls
{
    using WPF.Themes;
    public class PaletteControl : UserControl
    {
        //public override void OnApplyTemplate()
        //{
        //    base.OnApplyTemplate();
        //    this.ApplyTheme(ThemeManager.GetCurrentTheme());
        //}

        public override void EndInit()
        {
            base.EndInit();
            this.ApplyTheme(ThemeManager.GetCurrentTheme());
        }
    }
}
