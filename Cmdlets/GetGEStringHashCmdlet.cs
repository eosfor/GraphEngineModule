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
    public class GetGEStringHashCmdlet : TrinityBaseCmdlet
    {
        [Parameter(Mandatory = true)]
        [AllowNull]
        public string[] String;

        protected override void BeginProcessing()
        {
            //base.BeginProcessing();
        }

        protected override void ProcessRecord()
        {
            List<long> ret = new List<long>();

            if (String == null) { WriteObject(ret); return; }

            foreach (var str in String)
            {
                if (! string.IsNullOrEmpty(str))
                    ret.Add(HashHelper.HashString2Int64(str));
            }

            if (ret.Count == 1)
            {
                WriteObject(ret[0]);
            }
            else
            {
                WriteObject(ret);
            }
            
            //base.ProcessRecord();
        }
    }
}
