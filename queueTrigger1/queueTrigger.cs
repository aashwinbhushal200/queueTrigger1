using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace queueTrigger1
{
    public class queueTrigger
    {
        [FunctionName("queueTrigger")]
        public void Run([QueueTrigger("myqueue-items", Connection = "AzureWebJobsStorage")]string myQueueItem,
            //blob input binding :queue msg will be blob name
            [Blob("dev/{queueTrigger}",FileAccess.Read,Connection="AzureWebJobsStorage")] Stream s ,
            ILogger log)
        {
            StreamReader reader = new StreamReader(s);
            log.LogInformation($"C# Queue trigger function processed: \n name:{myQueueItem}  \n size:{s.Length} \n content:{reader.ReadToEnd()}");
        }
    }
}
