using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Management.Automation;
using Trinity;
using Trinity.Storage.Composite;
using System.Reflection;
using Trinity.Utilities;
using Trinity.Storage;
using System.Linq;

namespace GraphEngineModule
{
    [Cmdlet("Reset", "GEStorage")]
    public class ResetGEStorageCmdlet : PSCmdlet
    {
        protected override void BeginProcessing()
        {
            if (!GlobalState.Instance.IsInitialized)
            {
                Global.Initialize();
            }
            base.BeginProcessing();
        }

        protected override void ProcessRecord()
        {
            Global.LocalStorage.ResetStorage();
            GlobalState.Instance.IsInitialized = false;
            //base.BeginProcessing();
        }
    }
}
