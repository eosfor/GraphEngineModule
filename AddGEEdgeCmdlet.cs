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
    [Cmdlet("Add", "GEEdge")]
    [Alias("Add-Edge")]
    class AddGEEdgeCmdlet : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            base.ProcessRecord();
        }
    }
}
