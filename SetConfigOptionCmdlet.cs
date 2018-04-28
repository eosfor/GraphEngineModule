using System;
using System.IO;
using System.Management.Automation;
using Trinity;
using Trinity.Storage.Composite;
using Trinity.Configuration;
using System.Reflection;
using GraphEngineModule;

namespace GraphEngineMolule
{
    [Cmdlet("Set", "GEConfigOption")]
    public class SetGEConfigOptionCmdlet : PSCmdlet
    {
        [Parameter(Mandatory = false)]
        public string LogDirectory;

        [Parameter(Mandatory = false)]
        public Trinity.Diagnostics.LogLevel LogLevel;

        [Parameter(Mandatory = false)]
        public string StorageRoot;

        [Parameter(Mandatory = false)]
        public bool LogEchoOnConsole;
        
        protected override void BeginProcessing()
        {
            if (!GlobalState.Instance.IsInitialized)
            {
                Global.Initialize();
                TrinityConfig.LogEchoOnConsole = false;
                LoggingConfig.Instance.LogEchoOnConsole = false;
            }
            base.BeginProcessing();
        }

        protected override void ProcessRecord()
        {

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


            if (this.MyInvocation.BoundParameters.ContainsKey("LogEchoOnConsole"))
            {
                TrinityConfig.LogEchoOnConsole = false;
            }

            //base.ProcessRecord();
        }
    }
}
