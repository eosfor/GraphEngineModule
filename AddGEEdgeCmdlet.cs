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
    public class AddGEEdgeCmdlet : TrinityBaseCmdlet
    {
        [Parameter(Mandatory = true)]
        public ICell From;

        [Parameter(Mandatory = true)]
        public ICell To;

        [Parameter(Mandatory = false)]
        public string Label;

        protected override void ProcessRecord()
        {
            From.AppendToField("OutEdge", To.CellId);
            Global.LocalStorage.SaveGenericCell(From);
            base.ProcessRecord();
        }
    }
}
