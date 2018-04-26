using System;
using System.IO;
using System.Management.Automation;
using Trinity;
using Trinity.Storage.Composite;
using Trinity.Configuration;
using System.Reflection;

namespace GraphEngineMolule
{
    [Cmdlet("Set", "ConfigOption")]
    public class SetConfigOptionCmdlet : PSCmdlet
    {
        [Parameter(Mandatory = false)]
        public string LogDirectory;

        [Parameter(Mandatory = false)]
        public Trinity.Diagnostics.LogLevel LogLevel;

        [Parameter(Mandatory = false)]
        public string StorageRoot;

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
        }

        protected override void ProcessRecord()
        {
            //Global.Initialize();

            if (this.MyInvocation.BoundParameters.ContainsKey("LogLevel"))
            {
                TrinityConfig.LoggingLevel = LogLevel;
            }

            if (this.MyInvocation.BoundParameters.ContainsKey("LogDirectory"))
            {
                if (Directory.Exists(LogDirectory))
                {
                    LoggingConfig.Instance.LogDirectory = LogDirectory;
                }
                else { throw new DirectoryNotFoundException(); }

            }

            if (this.MyInvocation.BoundParameters.ContainsKey("StorageRoot"))
            {
                if (Directory.Exists(StorageRoot))
                {
                    StorageConfig.Instance.StorageRoot = StorageRoot;
                }
                else { throw new DirectoryNotFoundException(); }

            }
            //base.ProcessRecord();
        }
    }
}
