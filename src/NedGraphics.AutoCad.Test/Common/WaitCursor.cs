using System;
using System.Windows.Input;

namespace NedGraphics.AutoCad.Test.Common
{
    public class WaitCursor : IDisposable
    {
        private Cursor _previousCursor = null;

        /// <summary>
        /// Set the cursor to a wait cursor
        /// </summary>
        /// <param name="dbobject"></param>
        public WaitCursor()
        {
            _previousCursor = Mouse.OverrideCursor;

            Mouse.OverrideCursor = Cursors.Wait;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Mouse.OverrideCursor = _previousCursor;
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
