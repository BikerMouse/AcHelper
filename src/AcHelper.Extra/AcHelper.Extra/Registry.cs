using Microsoft.Win32;
using System;

namespace AcHelper.Extra
{
    /// <summary>
    /// 
    /// </summary>
    public static class Registry
    {
        /// <summary>
        /// Reads a value from a subkey in the CurrentUser hive of the registry.
        /// </summary>
        /// <typeparam name="T">type you want to cast the return value to.</typeparam>
        /// <param name="subkey">The subkey from the Current User hive where the valuename is located.</param>
        /// <param name="valuename">The name of the value to be returned.</param>
        /// <returns>Value from the registry subkey cast to type T.</returns>
        public static T ReadFromCurrentUser<T>(string subkey, string valuename)
        {
            RegistryKey currentuser = Microsoft.Win32.Registry.CurrentUser;
            if (currentuser.OpenSubKey(subkey) is RegistryKey key)
            {
                using (key)
                {
                    if (key.GetValue(valuename) is T value)
                    {
                        return value;
                    }
                }
            }
            return default(T);
        }
        /// <summary>
        /// Reads a value from a subkey in the LocalMachine hive of the registry.
        /// </summary>
        /// <typeparam name="T">type you want to cast the return value to.</typeparam>
        /// <param name="subKey">The subkey from the Current User hive where the valuename is located.</param>
        /// <param name="valueName">The name of the value to be returned.</param>
        /// <returns>Value from the registry subkey cast to type T.</returns>
        public static T ReadFromLocalMachine<T>(string subKey, string valueName)
        {
            RegistryKey localmachine = Microsoft.Win32.Registry.LocalMachine;
            if (localmachine.OpenSubKey(subKey) is RegistryKey key)
            {
                using (key)
                {
                    if (key.GetValue(valueName) is T value)
                    {
                        return value;
                    }
                }
            }
            return default(T);
        }
        /// <summary>
        /// Retrieves the value associated with the specified name. If the name is not found it returns the default value of the type.
        /// </summary>
        /// <typeparam name="T">type you want to cast the return value to.</typeparam>
        /// <param name="hive">Hive from where to retrieve the subkey.</param>
        /// <param name="subkey">Subkey within the hive.</param>
        /// <param name="valuename">Name of the value you want to return.</param>
        /// <returns>Value from the registry subkey cast to type T.</returns>
        public static T TryReadFromRegistry<T>(RegistryHive hive, string subkey, string valuename)
        {
            if (string.IsNullOrEmpty(subkey))
            {
                throw new ArgumentNullException("subkey");
            }
            if (string.IsNullOrEmpty(valuename))
            {
                throw new ArgumentNullException("valuename");
            }

            T value = default(T);

            UseRegistryKey(hive, subkey, key =>
            {
                object obj = key.GetValue(valuename, value);
                value = (T)obj;
            });
            return value;
        }
        /// <summary>
        /// Sets the value of a name/value pair in the registry key, using the specified
        /// registry data type.
        /// </summary>
        /// <param name="hive"></param>
        /// <param name="subkey">The key within the hive where the value is stored.</param>
        /// <param name="valuename">The name of the value to be stored.</param>
        /// <param name="value">The data to be stored.</param>
        /// <param name="valueType">The registry data type to use when storing the data.</param>
        public static void SetValueToRegistry(RegistryHive hive, string subkey, string valuename, object value, RegistryValueKind valueType)
        {
            UseRegistryKey(hive, subkey, true, key =>
            {
                key.SetValue(valuename, value, valueType);
            });
        }
        /// <summary>
        /// Checks whether a registrykey exists.
        /// </summary>
        /// <param name="hive">Hive where the key is located.</param>
        /// <param name="subkey">Registrykey to check.</param>
        /// <returns>True if the key exists, otherwise false.</returns>
        public static bool CheckKeyExistance(RegistryHive hive, string subkey)
        {
            RegistryKey key = GetRegistryKey(hive, subkey);
            return key != null;
        }
        /// <summary>
        /// Retrieves a specified subkey, and specifies whether write access is to be applied
        /// to the key.
        /// </summary>
        /// <param name="hive">Hive where the subkey is located.</param>
        /// <param name="subkey">Name or path of the subkey to open.</param>
        /// <returns>The subkey requested, or null if the operation failed.</returns>
        public static RegistryKey GetRegistryKey(RegistryHive hive, string subkey)
        {
            return GetRegistryKey(hive, subkey, false);
        }
        /// <summary>
        /// Retrieves a specified subkey, and specifies whether write access is to be applied
        /// to the key.
        /// </summary>
        /// <param name="hive">Hive where the subkey is located.</param>
        /// <param name="subkey">Name or path of the subkey to open.</param>
        /// <param name="writable">Set to true if you need write access to the key.</param>
        /// <returns>The subkey requested, or null if the operation failed.</returns>
        public static RegistryKey GetRegistryKey(RegistryHive hive, string subkey, bool writable)
        {
            RegistryKey key;
            switch (hive)
            {
                case RegistryHive.LocalMachine:
                    key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(subkey, writable);
                    break;
                default:
                    key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(subkey, writable);
                    break;
            }

            return key;
        }

        #region private helpers ...
        /// <summary>
        /// Retrieves a subkey as read-only.
        /// </summary>
        /// <param name="hive">Hive from where to retrieve the subkey.</param>
        /// <param name="subkey">Subkey within the hive.</param>
        /// <param name="action">Delegate to execute with the retrieved RegistryKey.</param>
        private static void UseRegistryKey(RegistryHive hive, string subkey, Action<RegistryKey> action)
        {
            UseRegistryKey(hive, subkey, false, action);
        }
        /// <summary>
        /// Retrieves a specified subkey, and specifies whether write access is to be applied to the key.
        /// </summary>
        /// <param name="hive">Hive from where to retrieve the subkey.</param>
        /// <param name="subkey">Subkey within the hive.</param>
        /// <param name="writable">Set to true if you need write access to the key.</param>
        /// <param name="action">Delegate to execute with the retrieved RegistryKey.</param>
        private static void UseRegistryKey(RegistryHive hive, string subkey, bool writable, Action<RegistryKey> action)
        {
            RegistryKey key = GetRegistryKey(hive, subkey, writable);

            if (key != null)
            {
                using (key)
                {
                    action(key);
                }
            }

        }
        #endregion
    }
}
