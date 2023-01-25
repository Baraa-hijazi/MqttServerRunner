using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using MQTTnet;

namespace MqttServerRunner
{
    public class Function1
    {
        [Function("Run")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            var mqttFactory = new MqttFactory();
            var mqttServerOptions = mqttFactory.CreateServerOptionsBuilder()
                .WithDefaultEndpoint()
                .WithDefaultEndpointPort(1883)
                //.WithEncryptedEndpoint()
                //.WithEncryptedEndpointPort(8883)
                .Build();
            
            await mqttFactory.CreateMqttServer(mqttServerOptions).StartAsync();

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteStringAsync("MQTT SERVER UP AND RUNNING");

            return response;
        }
    }
}