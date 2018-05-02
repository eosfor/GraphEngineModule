using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;
using Trinity.Storage.Composite;

namespace GraphEngineModule
{
    
    public class TrinityBaseCmdlet : PSCmdlet
    {
        protected override void BeginProcessing()
        {
            if (!GlobalState.Instance.IsInitialized)
            {
                ThrowTerminatingError(new ErrorRecord(new NotInitializedException(), "100", ErrorCategory.ResourceUnavailable, this));
            }
            //base.BeginProcessing();
        }
    }
}
