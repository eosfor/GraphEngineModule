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
using Trinity.Core.Lib;

namespace GraphEngineModule.Tests
{
    [Cmdlet("Get", "GEStringHash")]
    public class GetGEStringHashCmdlet : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public string String;

        protected override void ProcessRecord()
        {
            WriteObject(HashHelper.HashString2Int64(String));
            //base.ProcessRecord();
        }
    }
}
