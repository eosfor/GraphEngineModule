﻿using System;
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
            CompositeStorage.AddStorageExtension(Path, Namespace);
            
            //base.ProcessRecord();
        }
    }
}
