using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Newtonsoft.Json;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AWSLambda1
{
    public class Function
    {
        /// <summary>
        /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
        /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
        /// region the Lambda function is executed in.
        /// </summary>
        public Function()
        {
        }


        /// <summary>
        /// This method is called for every Lambda invocation. This method takes in an SQS event object and can be used 
        /// to respond to SQS messages.
        /// </summary>
        /// <param name="evnt"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task FunctionHandler(SQSEvent evnt, ILambdaContext context)
        {
            foreach(var message in evnt.Records)
            {
                await ProcessMessageAsync(message, context);
            }
        }

        private async Task ProcessMessageAsync(SQSEvent.SQSMessage message, ILambdaContext context)
        {
            context.Logger.LogLine($"Processing message {message.Body}");

            try
            {
                using (var client = new HttpClient())
                {
                    var teamNumberMatch = message.Attributes["team"] == "42";
                    if (teamNumberMatch)
                    {
                        var selection = JsonConvert.DeserializeObject(message.Body);
                        client.BaseAddress = new Uri("http://adriana42.eu-west-1.elasticbeanstalk.com/api");
                        var response = client.PostAsJsonAsync("api/result", selection);
                        context.Logger.LogLine($"Processed message {message.Body}");
                    }
                    else
                    {
                        context.Logger.LogLine($"Ignored settlement {message.Body} fro team  : " + message.Attributes["team"]);
                    }
                }
            }
            catch
            {
                context.Logger.LogLine($"Error when proessing message {message.Body}");
            }

            await Task.CompletedTask;
        }
    }
}
