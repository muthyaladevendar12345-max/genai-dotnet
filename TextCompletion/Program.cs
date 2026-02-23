using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using OpenAI;
using OpenAI.Chat;
using System.ClientModel;
using System.Text.Json.Serialization;

IConfigurationRoot config=new  ConfigurationBuilder().AddUserSecrets<Program>().Build();
var credential = new ApiKeyCredential(config["GitHub Models token:Token"] ?? throw new InvalidOperationException("Missing configuration: GitHubModels:Token."));
var options = new OpenAIClientOptions()
{
    Endpoint = new Uri("https://models.github.ai/inference")
};
IChatClient client =
    new OpenAIClient(credential, options).GetChatClient("openai/o4-mini").AsIChatClient(); 
ChatResponse response = await client.GetResponseAsync("what is  AI explain max in 20 words");

Console.WriteLine(response);