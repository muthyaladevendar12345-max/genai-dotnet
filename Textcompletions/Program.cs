

using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using OpenAI;
using OpenAI.Chat;
using System.ClientModel;

IConfigurationRoot config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

var credential = new ApiKeyCredential(config["GitHubModels:Token"] ?? throw new InvalidOperationException("Missing configuration: GitHubModels:Token."));



var options = new OpenAIClientOptions()
{
    Endpoint = new Uri("https://models.github.ai/inference")
};

// ensure model is set to a supported value
var model = "openai/gpt-4o-mini";

IChatClient client =
    new OpenAIClient(credential, options)
        .GetChatClient(model)   // use variable instead of hard-coded unavailable model
        .AsIChatClient();

ChatResponse response = await client.GetResponseAsync("what is AI explain max in 20 words");
System.Console.WriteLine(response);