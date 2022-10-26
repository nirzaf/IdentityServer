// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System.Threading.Tasks;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Storage.Models;

namespace IdentityServer.UnitTests.Validation.Setup;

public class TestDeviceFlowThrottlingService : IDeviceFlowThrottlingService
{
    private readonly bool shouldSlownDown;

    public TestDeviceFlowThrottlingService(bool shouldSlownDown = false)
    {
        this.shouldSlownDown = shouldSlownDown;
    }

    public Task<bool> ShouldSlowDown(string deviceCode, DeviceCode details) => Task.FromResult(shouldSlownDown);
}