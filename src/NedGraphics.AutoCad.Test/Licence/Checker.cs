using AcHelper;
using Autodesk.AutoCAD.Runtime;
using NedGraphics.AutoCad.Test.CAD;
using NedGraphics.AutoCad.Test.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NedGraphics.AutoCad.Test.Licence
{
    [Obsolete("Not in production yet.", true)]
    public class Checker : IDisposable
    {
        #region nglock_interface
        private const int LS_SUCCESS = 0;
        private const int LS_NO_SUCCESS = -939519985;

        [DllImport("NGLock_VC10_64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "nglock_get_dll_machine_code", CharSet = CharSet.Ansi)]
        extern static IntPtr nglock_get_dll_machine_code();

        [DllImport("NGLock_VC10_64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "nglock_init_license_system", CharSet = CharSet.Ansi)]
        extern static int nglock_init_license_system([MarshalAs(UnmanagedType.LPStr)]string name);

        [DllImport("NGLock_VC10_64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "nglock_check_license", CharSet = CharSet.Ansi)]
        extern static int nglock_check_license([MarshalAs(UnmanagedType.LPStr)]string feature, [MarshalAs(UnmanagedType.LPStr)]string version);

        [DllImport("NGLock_VC10_64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "nglock_claim_license", CharSet = CharSet.Ansi)]
        extern static int nglock_claim_license([MarshalAs(UnmanagedType.LPStr)]string feature, [MarshalAs(UnmanagedType.LPStr)]string version);

        [DllImport("NGLock_VC10_64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "nglock_free_license", CharSet = CharSet.Ansi)]
        extern static int nglock_free_license([MarshalAs(UnmanagedType.LPStr)]string feature, [MarshalAs(UnmanagedType.LPStr)]string version);

        [DllImport("NGLock_VC10_64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "nglock_free_all", CharSet = CharSet.Ansi)]
        extern static int nglock_free_all();
        #endregion

        private const string LICENCEKEY = "Software\\NedGraphics\\Common";
        private const string LIC_TONLCS2IMGEO = "ToNlcsImgeo";
        private const string LIC_NITEKPLUS = "NITEKPLUS";
        private const string VERSION_TO_LIC = "9";
        private const string VERSION_NI_LIC = "11";

        private LicenceProperties toLicence = new LicenceProperties(LIC_TONLCS2IMGEO, VERSION_TO_LIC);
        private LicenceProperties niLicence = new LicenceProperties(LIC_NITEKPLUS, VERSION_NI_LIC);
        private LicenceProperties currentLicence;

        /// <summary>
        /// Instance reference to this object.
        /// </summary>
        private static Checker _instance = new Checker();

        /// <summary>
        /// Is the nglock initialized?
        /// </summary>
        private bool _initialized = false;
        private RegistryKey _licenceRegKey;

        /// <summary>
        /// Table with command => module relations.
        /// </summary>
        private Dictionary<string, LicenceProperties> _commandModuleDictionary = new Dictionary<string, LicenceProperties>();

        #region Ctor
        private Checker()
        {
            string licenceValue = string.Empty;
            bool valid = true;

            #region get license setting
            _licenceRegKey = Registry.CurrentUser.OpenSubKey(LICENCEKEY, true);
            if (_licenceRegKey == null)
            {
                _licenceRegKey = Registry.LocalMachine.OpenSubKey(LICENCEKEY, true);
                if (_licenceRegKey == null)
                {
                    System.Exception exception = new System.Exception("Registry not complete: Missing key to Licence path.");
                    ExceptionHandler.ShowDialog(exception, cmdLine: true);
                    //MessageBox.Show("Registry not complete:\nMissing key to Licence path.", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    valid = false;
                }
            }
            if (valid)
            {
                licenceValue = _licenceRegKey.GetValue("LICENSE_NODE", string.Empty).ToString();

                if (string.IsNullOrEmpty(licenceValue))
                {
                    licenceValue = _licenceRegKey.GetValue("LICENSE_PATH", string.Empty).ToString();
                    if (!string.IsNullOrEmpty(licenceValue))
                    {
                        Active.WriteMessage("NLCS-IMGeo Converter maakt gebruik van lokaal bestand '{0}'.\n", licenceValue);
                    }
                }
                else
                {
                    Active.WriteMessage("NLCS-IMGeo Converter maakt gebruik van licentie server '{0}'.\n", licenceValue);
                }
            }
            if (string.IsNullOrEmpty(licenceValue))
            {
                licenceValue = "NO-NET";
            }
            #endregion

            #region Initialize licence system
            if (nglock_init_license_system(licenceValue) == LS_SUCCESS)
            {
                _initialized = true;

                if (_CheckLicence(niLicence))
                {
                    //PopulateTable(niLicence);
                    currentLicence = niLicence;
                }
                else if (_CheckLicence(toLicence))
                {
                    //PopulateTable(toLicence);
                    currentLicence = toLicence;

                }
                else
                {
                    System.Exception exception = new System.Exception("Geen Licenctie gevonden voor NLCS - IMGeo Converter.");
                    ExceptionHandler.WriteToCommandLine(exception);
                    //Active.WriteMessage("\nGeen Licenctie gevonden voor NLCS - IMGeo Converter.\n");
                }
            }
            #endregion
        }
        #endregion

        #region Methods
        public static bool CheckLicence(string module, string version)
        {
            using (new WaitCursor())
            {
                if (_instance != null)
                {
                    return _instance._CheckLicence(module, version);
                }
            }
            return false;
        }
        public static bool CheckCommandLicence(string command)
        {
            using (new WaitCursor())
            {
                if (_instance != null)
                {
                    return _instance._CheckLicence(command.ToUpper());
                } //if
            } //using
            return false;
        }
        public static bool ClaimCommandLicence(string command)
        {
            using (new WaitCursor())
            {
                if (_instance != null)
                {
                    return _instance._ClaimLicence(command.ToUpper());
                }
            }
            return false;
        }

        public static void FreeAllLicences()
        {
            using (new WaitCursor())
            {
                if (_instance != null)
                {
                    _instance.Dispose();
                    _instance = null;
                }
            }
        }
        #endregion

        #region Members
        //private void PopulateTable(LicenceProperties licenceProperties)
        //{
        //    _commandModuleDictionary[CommandHandler.CMD_OPENTOOLPALETTE] = licenceProperties;
        //}

        /// <summary>
        /// Check Product Licence. Display error message if no licence is available.
        /// </summary>
        /// <param name="module"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        private bool _CheckLicence(string module, string version)
        {
            int result = -1;
            if (_initialized)
            {
                if ((result = nglock_check_license(module, version)) == LS_SUCCESS)
                {
                    // success
                }
                else
                {
                    Active.WriteMessage("\nGeen licentie aanwezig voor module: '{0}'.\n", module);
                }
            }
            return result == LS_SUCCESS;
        }
        /// <summary>
        /// Check Product Licence. Display error message if no licence is available.
        /// </summary>
        /// <param name="licProp"></param>
        /// <returns></returns>
        private bool _CheckLicence(LicenceProperties licProp)
        {
            int result = -1;
            if (_initialized)
            {
                if ((result = nglock_check_license(licProp.ModuleName, licProp.Version)) == LS_SUCCESS)
                {
                    // Success
                }
            }
            return result == LS_SUCCESS;
        }
        /// <summary>
        /// Check Produce Licence. Display error if no licence is available.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private bool _CheckLicence(string command)
        {
            if (_initialized)
            {
                if (_commandModuleDictionary.ContainsKey(command))
                {
                    LicenceProperties prop = _commandModuleDictionary[command];
                    return _CheckLicence(prop);
                }
                else
                {
                    Active.WriteMessage("\nGeen licentie module naam gevonden voor commando: '{0}'. ", command);
                }
            }
            return false;
        }

        private bool _ClaimLicence(string module, string version)
        {
            int result = -1;
            if (_initialized)
            {
                if ((result = nglock_claim_license(module, version)) == LS_SUCCESS)
                {
                    // success
                }
                else
                {
                    Active.WriteMessage("\nGeen vrije licentie beschikbaar voor module: '{0}'.\n", module);
                }
            }
            return result == LS_SUCCESS;
        }
        private bool _ClaimLicence(LicenceProperties licence_properties)
        {
            return _ClaimLicence(licence_properties.ModuleName, licence_properties.Version);
        }
        private bool _ClaimLicence(string command)
        {
            int result = -1;
            if (_initialized)
            {
                if (_commandModuleDictionary.ContainsKey(command))
                {
                    LicenceProperties prop = _commandModuleDictionary[command];

                    if ((result = nglock_check_license(prop.ModuleName, prop.Version)) == LS_SUCCESS)
                    {
                        return _ClaimLicence(prop.ModuleName, prop.Version);
                    }
                    else
                    {
                        Active.WriteMessage("\nGeen licentie aanwezig voor module: '{0}'.\n", prop.ModuleName);
                    }
                }
                else
                {
                    Active.WriteMessage("\nGeen licentie module naam gevonden voor commando: '{0}'\n", command);
                }
            }
            return result == LS_SUCCESS;
        }
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Checker() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
