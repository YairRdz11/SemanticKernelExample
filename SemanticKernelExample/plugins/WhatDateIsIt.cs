using System.ComponentModel;
using Microsoft.SemanticKernel;

namespace SemanticKernelExample.plugins;

public class WhatDateIsIt
{
    [KernelFunction, Description("Get the current date")]
    public string Date(IFormatProvider? formatProvider = null) =>
        DateTimeOffset.UtcNow.ToString("D", formatProvider);
}