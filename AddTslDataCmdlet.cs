using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Management.Automation;
using Trinity;
using Trinity.Storage.Composite;

namespace GraphEngineMolule
{
    [Cmdlet("Add", "TslData")]
    public class AddTslDataCmdlet : PSCmdlet
    {
        [Parameter()]
        public string Path;

        [Parameter()]
        public string Namespace;


        protected override void ProcessRecord()
        {
            Global.Initialize();
            CompositeStorage.AddStorageExtension(Path, Namespace);

            //base.ProcessRecord();
        }
    }
}
