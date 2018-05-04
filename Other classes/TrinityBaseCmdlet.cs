using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;
using Trinity.Storage.Composite;
using GraphEngineModule.Exceptions;

namespace GraphEngineModule
{
    
    public class TrinityBaseCmdlet : PSCmdlet
    {
        protected override void BeginProcessing()
        {
            if (!GlobalState.Instance.IsInitialized)
            {
                ThrowTerminatingError(new ErrorRecord(new GEIsNotInitializedException(), "GE_NOT_INITIALIZED", ErrorCategory.ResourceUnavailable, this));
            }
        }
    }
}
