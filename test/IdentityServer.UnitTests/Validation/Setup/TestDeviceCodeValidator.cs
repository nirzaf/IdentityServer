// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System.Threading.Tasks;
using Duende.IdentityServer.Storage.Models;
using Duende.IdentityServer.Validation;
using Duende.IdentityServer.Validation.Models;

namespace IdentityServer.UnitTests.Validation.Setup;

public class TestDeviceCodeValidator : IDeviceCodeValidator
{
    private readonly bool shouldError;

    public DeviceCode DeviceCodeResult { get; set; } = new DeviceCode();

    public TestDeviceCodeValidator(bool shouldError = false)
    {
        this.shouldError = shouldError;
    }

    public Task ValidateAsync(DeviceCodeValidationContext context)
    {
        if (shouldError) context.Result = new TokenRequestValidationResult(context.Request, "error");
        else context.Result = new TokenRequestValidationResult(context.Request);

        context.Request.DeviceCode = DeviceCodeResult;

        return Task.CompletedTask;
    }
}