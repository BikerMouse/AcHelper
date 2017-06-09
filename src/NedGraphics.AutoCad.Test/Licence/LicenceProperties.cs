namespace NedGraphics.AutoCad.Test.Licence
{
    /// <summary>
    /// License properties per command
    /// </summary>
    internal class LicenceProperties
    {
        private string _moduleName;
        internal string ModuleName
        {
            get { return _moduleName; }
            set { _moduleName = value; }
        }

        private string _version;
        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        internal LicenceProperties(string moduleName, string version)
        {
            _moduleName = moduleName;
            _version = version;
        }

    } //class
}
