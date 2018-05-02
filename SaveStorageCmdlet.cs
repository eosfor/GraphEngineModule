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
    [Cmdlet("Save","GEStorage")]
    class SaveStorageCmdlet : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            Global.LocalStorage.SaveStorage();
            base.ProcessRecord();
        }
    }
}
