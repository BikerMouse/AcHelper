using System;

namespace AcHelper.WPF.Palettes
{
    public class WpfPaletteVisibleStateChangedEventArgs : EventArgs
    {
        private VisibleState _new_visible_state;
        private VisibleState _old_visible_state;

        public WpfPaletteVisibleStateChangedEventArgs(VisibleState oldVisibleState, VisibleState newVisibleState)
        {
            _old_visible_state = oldVisibleState;
            _new_visible_state = newVisibleState;
        }

        public VisibleState NewVisibleState => _new_visible_state;
        public VisibleState OldVisibleState => _old_visible_state;
    }
    public class WpfPaletteClosingEventArgs : EventArgs
    {
        private string _name;

        public WpfPaletteClosingEventArgs(string paletteName)
        {
            _name = paletteName;
        }

        public string PaletteName => _name;
    }
    public class WpfPaletteClosedEventArgs : EventArgs
    {
        private string _name;

        public WpfPaletteClosedEventArgs(string paletteName)
        {
            _name = paletteName;
        }

        public string PaletteName => _name;
    }
}
