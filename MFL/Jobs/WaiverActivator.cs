using MFL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFL.Jobs
{
    public class WaiverActivator : Hangfire.JobActivator
    {
        private readonly IServiceProvider _serviceProvider;
        public WaiverActivator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override object ActivateJob(Type type)
        {
            return _serviceProvider.GetService(type);
        }
    }
}
