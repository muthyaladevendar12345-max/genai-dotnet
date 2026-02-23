

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
#region
//basic example 
//ChatResponse response = await client.GetResponseAsync("what is AI explain max in 20 words");
//System.Console.WriteLine(response);

//string prompt= "What is AI? Explain in max 20 words.";
//Console.WriteLine($"user prompt>>>{prompt}");

//ChatResponse response1=await client.GetResponseAsync(prompt);
//Console.WriteLine($"tokens used in input...{response1.Usage?.InputTokenCount} and {response1.Usage?.OutputTokenCount}");

#endregion
#region
//streaming example
//string prompt = "What is AI?.";
//Console.WriteLine($"user prompt>>>{prompt}");
//var response=client.GetStreamingResponseAsync(prompt);
//await foreach (var item in response)
//{
//    Console.Write(item.Text);
//}

#endregion

#region classification example
//var classificationPrompt = """
//Please classify the following sentences into categories: 
//- 'complaint' 
//- 'suggestion' 
//- 'praise' 
//- 'other'.

//1) "I love the new layout!"
//2) "You should add a night mode."
//3) "When I try to log in, it keeps failing."
//4) "This app is decent."
//""";
//ChatResponse response=await client.GetResponseAsync(classificationPrompt);

//Console.WriteLine($"prompt>>>{classificationPrompt}");
//Console.WriteLine("Ai response...................");
//Console.WriteLine(response);


#endregion
#region summarization example
//var summaryPrompt = """
//Summarize the following blog in 1 concise sentences:

//"Microservices architecture is increasingly popular for building complex applications, but it comes with additional overhead. It's crucial to ensure each service is as small and focused as possible, and that the team invests in robust CI/CD pipelines to manage deployments and updates. Proper monitoring is also essential to maintain reliability as the system grows."
//""";
//ChatResponse res=await client.GetResponseAsync(summaryPrompt);

//Console.WriteLine($"user propmpt");
//Console.WriteLine(".......Respnse...........");
//Console.WriteLine(res);
#endregion
#region sentiment analysis example
var analysisPrompt = """
        You will analyze the sentiment of the following product reviews. 
        Each line is its own review. Output the sentiment of each review in a bulleted list and then provide a generate sentiment of all reviews.

        I bought this product and it's amazing. I love it!
        This product is terrible. I hate it.
        I'm not sure about this product. It's okay.
        I found this product based on the other reviews. It worked for a bit, and then it didn't.
        """;

ChatResponse respnse = await client.GetResponseAsync(analysisPrompt);
Console.WriteLine($"user prompt {analysisPrompt}");
Console.WriteLine(respnse);
#endregion