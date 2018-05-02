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
    [Cmdlet("Add", "GEVertex")]
    [Alias("Add-Vertex")]
    public class AddGENodeCmdlet : TrinityBaseCmdlet
    {
        [Parameter()]
        public ICell Vertex;

        protected override void ProcessRecord()
        {
            Global.LocalStorage.SaveGenericCell(Vertex);
            //base.ProcessRecord();
        }
    }
}
