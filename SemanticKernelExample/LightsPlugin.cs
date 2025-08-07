using System.ComponentModel;
using System.Text.Json.Serialization;
using Microsoft.SemanticKernel;

namespace SemanticKernelExample;

public class LightsPlugin
{
    // Mock data for the lights
    private readonly List<LightModel> lights = new()
    {
        new LightModel { Id = 1, Name = "Table Lamp", IsOn = false },
        new LightModel { Id = 2, Name = "Porch Light", IsOn = false },
        new LightModel { Id = 3, Name = "Chandelier", IsOn = false },
    };

    [KernelFunction("get_lights")]
    [Description("Get a list of lights and their current state")]
    public async Task<List<LightModel>> GetLightsAsync()
    {
        return lights;
    }

    [KernelFunction("change_state")]
    [Description("Change the state of the light")]
    public async Task<LightModel> ChangeStateAsync(int id, bool isOn)
    {
        var light = lights.FirstOrDefault(l => l.Id == id);

        if (light == null) return null;
        
        light.IsOn = isOn;
        
        return light;
    }
}

public class LightModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("is_on")]
    public bool IsOn { get; set; }
}