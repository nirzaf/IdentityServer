// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System.Threading.Tasks;
using Duende.IdentityServer.Storage.Models;
using Duende.IdentityServer.Validation;
using Duende.IdentityServer.Validation.Models;

namespace IdentityServer.UnitTests.Validation.EndSessionRequestValidation;

public class StubTokenValidator : ITokenValidator
{
    public TokenValidationResult AccessTokenValidationResult { get; set; } = new TokenValidationResult();
    public TokenValidationResult IdentityTokenValidationResult { get; set; } = new TokenValidationResult();

    public Task<TokenValidationResult> ValidateAccessTokenAsync(string token, string expectedScope = null)
    {
        return Task.FromResult(AccessTokenValidationResult);
    }

    public Task<TokenValidationResult> ValidateIdentityTokenAsync(string token, string clientId = null, bool validateLifetime = true)
    {
        return Task.FromResult(IdentityTokenValidationResult);
    }

    public Task<TokenValidationResult> ValidateRefreshTokenAsync(string token, Client client)
    {
        throw new System.NotImplementedException();
    }
}