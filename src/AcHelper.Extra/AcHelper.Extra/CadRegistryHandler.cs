using Autodesk.AutoCAD.DatabaseServices;
using Microsoft.Win32;
using System;

namespace AcHelper.Extra
{
    public class CadRegistryHandler : IDisposable
    {
        private const string ERROR_NOROOTKEY = "No Application Registry key provided. To read the application plugin registry settings, please provide the plugin root key.";
        private const string ERROR_NOROOTKEYINREGISTRY = "Registrykey '{0}' does not exist.";

        private readonly string _applicationName;
        private readonly RegistryHive _hive = RegistryHive.CurrentUser;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="applicationName">The name of the subkey for the application.</param>
        public CadRegistryHandler(string applicationName)
        {
            if (string.IsNullOrEmpty(applicationName))
            {
                throw new RegistryHandlerException(ERROR_NOROOTKEY);
            }
            if (!Registry.CheckKeyExistance(RegistryHive.CurrentUser, CreateRootKey(applicationName)))
            {
                throw new RegistryHandlerException(ERROR_NOROOTKEYINREGISTRY);
            }
            
            _applicationName = applicationName;
        }

        #region [           PROPERTIES          ]
        /// <summary>
        /// Full path to the subkey of the provided application name.
        /// </summary>
        public string RootKey => CreateRootKey(_applicationName);
        /// <summary>
        /// LocalUser Hive.
        /// </summary>
        public RegistryHive Hive => _hive;
        #endregion

        #region [           Methods         ]
        /// <summary>
        /// Retrieves the value associated with the specified name. If the name is not found it returns the default value of the type.
        /// </summary>
        /// <typeparam name="T">Type of the value to retrieve.</typeparam>
        /// <param name="valuename">The name of the value to retrieve. This string is not case-sensitive.</param>
        /// <returns></returns>
        public T GetValue<T>(string valuename)
        {
            return Registry.TryReadFromRegistry<T>(Hive, RootKey, valuename);
        }
        /// <summary>
        /// Sets the value of a name/value pair in the application's registry key, using the specified
        /// registry data type.
        /// </summary>
        /// <param name="valuename">The name of the value to be stored.</param>
        /// <param name="value">The data to be stored.</param>
        /// <param name="valueType">The registry data type to use when storing the data.</param>
        public void SetValue(string valuename, object value, RegistryValueKind valueType)
        {
            Registry.SetValueToRegistry(Hive, RootKey, valuename, value, valueType);
        }
        #endregion

        #region [           Helpers         ]
        public static string CreateRootKey(string applicationName)
        {
            return string.Concat(HostApplicationServices.Current.UserRegistryProductRootKey, @"\", applicationName);
        }
        private bool CheckApplicationRootKeyExistance()
        {
            return Registry.CheckKeyExistance(RegistryHive.CurrentUser, RootKey);
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

                // TODO: set large fields to null.

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
