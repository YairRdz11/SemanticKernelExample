//1. Import packages
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace SemanticKernelExample;

public class FunctionCalling
{
    public static async Task Execute()
    {
        var modelDeploymentName = "gpt-4o";
        var azureOpenAIEndpoint = Environment.GetEnvironmentVariable("AZUREOPENAI_ENDPOINT");
        var azureOpenAIApiKey = Environment.GetEnvironmentVariable("AZUREOPENAI_APIKEY");
        
        //2. Add AI Services
        var builder = Kernel.CreateBuilder();
        builder.Services.AddAzureOpenAIChatCompletion(
            modelDeploymentName,
            azureOpenAIEndpoint,
            azureOpenAIApiKey,
            modelId: "gpt-4o"
        );
        
        //4. Build the kernel and retrieve services
        var kernel = builder.Build();
        
        //6. Add Plugin
        //Create Scientific plugin from prompt
        KernelFunction kernelFunctionRespondAsScientific = KernelFunctionFactory.CreateFromPrompt(
            "Respond to the user question as if you were a Scientific. Respond to it as you were him, showing your personality",
            functionName: "RespondAsScientific",
            description: "Responds to a question as a Scientific.");
        //Create Policeman plugin from prompt
        KernelFunction kernelFunctionRespondAsPoliceman =
            KernelFunctionFactory.CreateFromPrompt(
                "Respond to the user question as if you were a Policeman. Respond to it as you were him, showing your personality, humor and level of intelligence.",
                functionName: "RespondAsPoliceman",
                description: "Responds to a question as a Policeman.");
        //Create plugin mixing scientific and policeman plugin in a roletalk
        KernelPlugin roleOpinionsPlugin =
            KernelPluginFactory.CreateFromFunctions(
                "RoleTalk",
                "Responds to questions or statements assuming different roles.",
                new[] {
                    kernelFunctionRespondAsScientific,
                    kernelFunctionRespondAsPoliceman
                });
        //Add role-talk plugin
        kernel.Plugins.Add(roleOpinionsPlugin);
        
        string userPrompt = "I just woke up and found myself in the middle of nowhere, " +
                            "do you know what date is it? and what would a policeman and a scientist do in my place?" +
                            "Please provide me the date using the WhatDateIsIt plugin and the Date function, and then " +
                            "the responses from the policeman and the scientist, on this order. " +
                            "For this two responses, use the RoleTalk plugin and the RespondAsPoliceman and RespondAsScientific functions.";
        
        //6. Planning
        OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new()
        {
            ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
        };
        
        //10. Invoke
        var result = await kernel.InvokePromptAsync(
            userPrompt,
            new(openAIPromptExecutionSettings));
        
        Console.WriteLine($"Result: {result}");
        Console.ReadLine();
    }
}