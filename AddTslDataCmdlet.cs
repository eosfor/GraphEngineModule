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
using GraphEngineModule;

namespace GraphEngineMolule
{
    [Cmdlet("Add", "GETslData")]
    public class AddTslDataCmdlet : PSCmdlet
    {
        [Parameter()]
        public string Path;

        [Parameter()]
        public string Namespace;

        protected override void BeginProcessing()
        {
            if (! GlobalState.Instance.IsInitialized)
            {
                Global.Initialize();
            }
            base.BeginProcessing();
        }


        protected override void ProcessRecord()
        {
            // var asm = Assembly.LoadFrom(@"C:\Users\Andrey_Vernigora\AppData\Local\Temp\tmp5276.tmp\obj\Release\netstandard2.0\da1afc060ec746f49c23a14661222256.dll");
            CompositeStorage.AddStorageExtension(Path, Namespace);
            
            //base.ProcessRecord();
        }
    }
}
