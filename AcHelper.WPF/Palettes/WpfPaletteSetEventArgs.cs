using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AcHelper.WPF.Palettes
{
    public class WpfPaletteSetVisibleStateChangedEventArgs : EventArgs
    {
        private VisibleState _newVisibleState;
        private VisibleState _oldVisibleState;

        public WpfPaletteSetVisibleStateChangedEventArgs(VisibleState oldVisibleState, VisibleState newVisibleState)
        {
            _newVisibleState = newVisibleState;
            _oldVisibleState = oldVisibleState;
        }

        public VisibleState NewVisibleState
        {
            get { return _newVisibleState; }
        }
        public VisibleState OldVisibleState
        {
            get { return _oldVisibleState; }
        }
    }
}
