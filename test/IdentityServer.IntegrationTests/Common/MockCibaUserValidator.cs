// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System.Threading.Tasks;
using Duende.IdentityServer.Validation;
using Duende.IdentityServer.Validation.Models;

namespace IdentityServer.IntegrationTests.Common;

internal class MockCibaUserValidator : IBackchannelAuthenticationUserValidator
{
    public BackchannelAuthenticationUserValidationResult Result { get; set; } = new BackchannelAuthenticationUserValidationResult();
    public BackchannelAuthenticationUserValidatorContext UserValidatorContext { get; set; }

    public Task<BackchannelAuthenticationUserValidationResult> ValidateRequestAsync(BackchannelAuthenticationUserValidatorContext userValidatorContext)
    {
        UserValidatorContext = userValidatorContext;
        return Task.FromResult(Result);
    }
}