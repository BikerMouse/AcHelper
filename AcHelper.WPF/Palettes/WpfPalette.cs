using System;
using System.Windows.Controls;
using System.Windows.Forms.Integration;

namespace AcHelper.WPF.Palettes
{
    public delegate void WpfPaletteVisibleStateChangedEventHandler(object sender, WpfPaletteVisibleStateChangedEventArgs arg);
    public delegate void WpfPaletteClosingEventHandler(object sender, WpfPaletteClosingEventArgs arg);

    public class WpfPalette : ElementHost, IPalette, IDisposable
    {
        #region Fields ...
        private UserControl _view = null;
        private string _name = string.Empty;

        private VisibleState _visible_state = VisibleState.Unknown;
        private WpfPaletteSet _parent = null;
        private bool _closed = true;
        #endregion

        #region Ctor ...
        public WpfPalette(UserControl view, string name)
        {
            _view = view;
            _name = name;

            this.AutoSize = true;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Child = _view;

            _closed = false;
        }
        #endregion

        #region Properties ...
        /// <summary>
        /// Sets and gets the VisibleState.
        /// </summary>
        public VisibleState VisibleState
        {
            get { return _visible_state; }
            set 
            {
                if (_visible_state != value && value != null)
                {
                    OnVisibleStateChanged(value);
                }
                _visible_state = value; 
            }
        }
        /// <summary>
        /// Sets and gets the Palette name.
        /// </summary>
        public string PaletteName
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        ///  Sets and gets the parent paletteset.
        /// </summary>
        public WpfPaletteSet PaletteSet
        {
            get { return _parent; }
            set { _parent = value; }
        }
        #endregion

        #region Methods ...
        // Methods
        public void ClosePaletteSet()
        {
            if (_parent != null)
            {
                // TODO: Set visibility
                
            }
        }
        public void Close()
        {
            if (PaletteClosing != null)
            {
                OnPaletteClosing(PaletteName);
            }
            VisibleStateChanged = null;
            VisibleState = Palettes.VisibleState.Hide;
            _closed = true;
        }
        #endregion

        #region Events ...
        public event WpfPaletteClosingEventHandler PaletteClosing;
        public event WpfPaletteVisibleStateChangedEventHandler VisibleStateChanged;
        
        protected virtual void OnPaletteClosing(string paletteName)
        {
            if (PaletteClosing != null)
            {
                PaletteClosing(this, new WpfPaletteClosingEventArgs(paletteName));
            }
        }
        protected virtual void OnVisibleStateChanged(VisibleState newVisibleState)
        {
            if (VisibleStateChanged != null)
            {
                VisibleStateChanged(this, new WpfPaletteVisibleStateChangedEventArgs(VisibleState, newVisibleState));
            }
        }
        #endregion

        #region Dispose ...
        private bool disposed = false;
        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            if (disposing)
            {
                if (!_closed)
                {
                    Close();
                }
                if (_view != null)
                {
                    _view = null;
                }
                if (_parent != null)
                {
                    _parent = null;
                }
            }
        }
        #endregion
    }

    public class WpfPaletteVisibleStateChangedEventArgs : EventArgs
    {
        private VisibleState _new_visible_state;
        private VisibleState _old_visible_state;

        public WpfPaletteVisibleStateChangedEventArgs(VisibleState oldVisibleState, VisibleState newVisibleState)
        {
            _old_visible_state = oldVisibleState;
            _new_visible_state = newVisibleState;
        }

        public VisibleState NewVisibleState
        {
            get { return _new_visible_state; }
        }
        public VisibleState OldVisibleState
        {
            get { return _old_visible_state; }
        }
    }
    public class WpfPaletteClosingEventArgs
    {
        private string _name;

        public WpfPaletteClosingEventArgs(string paletteName)
        {
            _name = paletteName;
        }

        public string PaletteName
        {
            get { return _name; }
        }
    }
}
