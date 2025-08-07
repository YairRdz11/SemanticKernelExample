using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

var modelId = string.Empty;
var endpoint = string.Empty;
var apiKey = string.Empty;

var builder = Kernel.CreateBuilder();
builder.AddAzureOpenAIChatClient(modelId, endpoint, apiKey);

Console.WriteLine("Hello, World!");