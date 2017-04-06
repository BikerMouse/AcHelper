using System.Windows.Controls;

namespace AcHelper.WPF.Controls
{
    using Themes;
    public class PaletteControl : UserControl
    {
        public override void EndInit()
        {
            base.EndInit();
            this.ApplyTheme(ThemeManager.GetCurrentTheme());
        }
    }
}
