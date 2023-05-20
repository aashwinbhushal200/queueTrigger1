using System;
using System.IO;
using System.Text;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace queueTrigger1
{
    public class queueTrigger
    {
        //#2 blob input binding :queue msg will be blob name

        /* [FunctionName("queueTrigger")]
         public void Run([QueueTrigger("myqueue-items", Connection = "AzureWebJobsStorage")] string myQueueItem,
            //#2.
            [Blob("dev/{queueTrigger}", FileAccess.Read, Connection = "AzureWebJobsStorage")] Stream s,
           //#3.
           ILogger log)
         {
             StreamReader reader = new StreamReader(s);
             log.LogInformation($"C# Queue trigger function processed: \n name:{myQueueItem}  \n size:{s.Length} \n content:{reader.ReadToEnd()}");
         }*/

        //#3 output binding: whenever the queue is triggered, use that msg and store in in blob.
        //video 22 of https://www.youtube.com/watch?v=XVWPabJUjt4&list=PLMWaZteqtEaLRsSynAsaS_aLzDPBUU4CV&index=22&t=324s 
        
        [FunctionName("queueTrigger")]
        public void Run([QueueTrigger("myqueue-items", Connection = "AzureWebJobsStorage")] string myQueueItem,
           //#3.
           [Blob("dev/latestText.txt", FileAccess.Write, Connection = "AzureWebJobsStorage")] Stream outBlob,
           ILogger log)
        {
            outBlob.Write(Encoding.ASCII.GetBytes(myQueueItem));
            log.LogInformation($"C# Queue trigger function processed: \n name:{myQueueItem}  \n size:{outBlob.Length} \n content:");
        }



    }
}
