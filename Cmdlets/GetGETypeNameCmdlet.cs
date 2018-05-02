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
    [Cmdlet("Get", "GETypeName")]
    public class GetGETypeNameCmdlet : TrinityBaseCmdlet
    {

        protected override void ProcessRecord()
        {
            WriteObject(Global.StorageSchema.CellDescriptors, true);
        }
    }
}
