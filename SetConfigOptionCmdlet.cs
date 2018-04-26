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
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_BindingRedirect;

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


        public static Assembly CurrentDomain_BindingRedirect(object sender, ResolveEventArgs args)

        {

            var name = new AssemblyName(args.Name);
            switch (name.Name)
            {
                case "Newtonsoft.Json":
                    return typeof(Newtonsoft.Json.JsonSerializer).Assembly;
                case "Trinity.Core":
                    return Assembly.LoadFrom(@"C:\Repo\Github\Public\00 - MSFT\GraphEngine\bin\netstandard2.0\Trinity.Core.dll");
                default:
                    return null;
            }

        }
    }
}
