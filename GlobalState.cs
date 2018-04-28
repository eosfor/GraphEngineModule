using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trinity.Storage.Composite;
using Trinity.Utilities;
using Trinity.Storage;

namespace GraphEngineModule
{
    internal class GlobalState
    {
        private static GlobalState _instance;
        private bool _isInitialized;
        //private List<StorageExtensionRecord> _extensionsLoaded;

        private GlobalState() { }

        public static GlobalState Instance {
            get
            {
                if (_instance == null)
                {
                    _instance = new GlobalState();
                }

                return _instance;

            }
        }

        public bool IsInitialized {  get; set; }

        //public List<StorageExtensionRecord> ExtensionsLoaded
        //{
        //    get
        //    {
        //        return _extensionsLoaded;
        //    }
        //}

        //public void AddExtensionRecord(StorageExtensionRecord record)
        //{
        //    _extensionsLoaded.Add(record);
        //}
    }

}
