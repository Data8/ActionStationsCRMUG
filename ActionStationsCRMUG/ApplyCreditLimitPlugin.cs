using System;
using Microsoft.Xrm.Sdk;

namespace ActionStationsCRMUG
{
    public class ApplyCreditLimitPlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            var req = new new_ApplyCreditLimitRequest {Parameters = context.InputParameters};

            // Validation - use InvalidPluginExecutionException to raise errors
            if ((req.RequestedCreditLimit.Value % 2) != 0)
                throw new InvalidPluginExecutionException("Only even credit limits are allowed");

            // Adjust input parameters during Pre-Operation step
            req.RequestedCreditLimit = new Money(req.RequestedCreditLimit.Value / 2);
        }
    }
}
