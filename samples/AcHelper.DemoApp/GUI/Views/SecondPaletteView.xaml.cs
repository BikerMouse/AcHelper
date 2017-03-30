using System.Windows.Controls;

namespace AcHelper.DemoApp.GUI.Views
{
    using WPF.Themes;
    /// <summary>
    /// Interaction logic for SecondPaletteView.xaml
    /// </summary>
    public partial class SecondPaletteView : UserControl
    {
        public SecondPaletteView()
        {
            InitializeComponent();
            this.ApplyTheme(ThemeManager.GetCurrentTheme());
        }
    }
}
